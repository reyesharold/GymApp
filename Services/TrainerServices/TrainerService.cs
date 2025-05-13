using Azure.Core;
using Entities.Domain;
using Entities.DTO.TrainerDTO;
using Entities.Enums;
using Entities.Extensions;
using Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TrainerServices
{
    public class TrainerService : ITrainerService
    {
        private readonly ICommonRepo<Trainer> _commonRepo;
        private readonly UserManager<UserApplication> _userManager;

        public TrainerService(ICommonRepo<Trainer> commonRepo,UserManager<UserApplication> userManager)
        {
            _commonRepo = commonRepo;
            _userManager = userManager;
        }

        /// <summary>
        /// Create a user and assign it as a trainer
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Trainer Response</returns>
        /// <exception cref="Exception"></exception>
        public async Task<TrainerResponse> CreateTrainerAsync(CreateTrainerDTO request)
        {
            var user = new UserApplication
            {
                DisplayName = request.RegisterDTO.Name,
                Email = request.RegisterDTO.Email,
                UserName = request.RegisterDTO.Email,
                Address = request.RegisterDTO.Address,
                PhoneNumber = request.RegisterDTO.PhoneNumber,
                AccountStatus = AccountStatus.Active,
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.RegisterDTO.Password);
            if(!result.Succeeded) { throw new Exception("Error in creating Trainer"); }

            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, Roles.Trainer.ToString());
            if (!roleResult.Succeeded) { throw new Exception("Error assigning Trainer Role"); }

            var trainer = new TrainerAddRequest
            {
                Id = user.Id,
                Certifications = request.TrainerAddRequest.Certifications,
                Specialties = request.TrainerAddRequest.Specialties,
            };

            var response = await _commonRepo.AddAync(trainer.ToTrainer());

            return new TrainerResponse
            {
                User = user.ToUserResponse(),
                Specialties = response.Specialties,
                Certifications = response.Certifications,
            };
        }

        public async Task<ICollection<TrainerResponse>> GetAllAsync()
        {
            var trainers = await _commonRepo.GetAllAsync(null, query => query
            .Include(u => u.User)
                .ThenInclude(ur => ur.UserRoles)
                    .ThenInclude(r => r.Role)
            .Include(w => w.WorkoutPlans)
                .ThenInclude(we => we.WorkoutExercises)
            .Include(c => c.Classes)
                .ThenInclude(b => b.Bookings)
                    .ThenInclude(m => m.Member)
                        .ThenInclude(u => u.User)
            );

            return trainers.Select(t => t.ToTrainerResponse()).ToList();
        }
    }
}
