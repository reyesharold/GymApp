using Entities.DTO.ClassDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ClassServices
{
    public interface IClassService
    {
        Task<ClassResponse> AddClassAsync(ClassAddRequest request);
        Task<ClassResponse> GetClassViaIdAsync(int ClassId);
        Task<ICollection<ClassResponse>> GetAllClassesAsync();
        Task<ICollection<ClassResponse>> GetClassesOfTrainerAsync(Guid TrainerId);
    }
}
