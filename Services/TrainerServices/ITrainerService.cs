using Entities.DTO.TrainerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TrainerServices
{
    public interface ITrainerService
    {
        Task<TrainerResponse> CreateTrainerAsync(CreateTrainerDTO request);
        Task<ICollection<TrainerResponse>> GetAllAsync();
    }
}
