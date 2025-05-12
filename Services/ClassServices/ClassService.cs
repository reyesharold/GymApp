using Entities.Domain;
using Entities.DTO.ClassDTO;
using Entities.Extensions;
using Microsoft.EntityFrameworkCore;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ClassServices
{
    public class ClassService : IClassService
    {
        private readonly ICommonRepo<Class> _commonRepo;

        public ClassService(ICommonRepo<Class> commonRepo)
        {
            _commonRepo = commonRepo;
        }

        public async Task<ClassResponse> AddClassAsync(ClassAddRequest request)
        {
            var createdClass = await _commonRepo.AddAync(request.ToClass());

            var response = await _commonRepo.GetAsync(i => i.ClassId == createdClass.ClassId, query => query
            .Include(t => t.Trainer)
                .ThenInclude(u => u.User)
            .Include(b => b.Bookings)
                .ThenInclude(m => m.Member)
                    .ThenInclude(u => u.User)
            );

            return new ClassResponse
            {
                Id = response.ClassId,
                ClassName = response.ClassName,
                Capacity = response.Capacity,
                ScheduleDateTime = response.ScheduleDateTime,
                TrainerId = response.TrainerId,
                TrainerName = response.Trainer.User.DisplayName,
            };
        }

        public async Task<ClassResponse> GetClassViaIdAsync(int ClassId)
        {
            var @class = await _commonRepo.GetAsync(i => i.ClassId == ClassId, query => query
            .Include(t => t.Trainer)
                .ThenInclude(u => u.User)
            .Include(b => b.Bookings)
                .ThenInclude(m => m.Member)
                    .ThenInclude(u => u.User));

            if(@class == null){ throw new ArgumentException("Invalid Class ID", nameof(ClassId));}

            return @class.ToClassReponse();
        }

        public async Task<ICollection<ClassResponse>> GetAllClassesAsync()
        {
            var classes = await _commonRepo.GetAllAsync(null, query => query
            .Include(t => t.Trainer)
                .ThenInclude(u => u.User)
            .Include(b => b.Bookings)
                .ThenInclude(m => m.Member)
                    .ThenInclude(u => u.User)
                );

            return classes.Select(c => c.ToClassReponse()).ToList();
        }

        public async Task<ICollection<ClassResponse>> GetClassesOfTrainerAsync(Guid TrainerId)
        {
            var classes = await _commonRepo.GetAllAsync(t => t.TrainerId == TrainerId, query => query
            .Include(t => t.Trainer)
                .ThenInclude(u => u.User)
            .Include(b => b.Bookings)
                .ThenInclude(m => m.Member)
                    .ThenInclude(u => u.User)
                );

            return classes.Select(temp =>temp.ToClassReponse()).ToList();
        }
    }
}
