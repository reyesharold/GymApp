using Entities.DTO.WorkoutPlanDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkoutPlanServices
{
    public interface IWorkoutPlanService
    {
        Task<WorkoutPlanResponse> AddWorkoutPlanAsync(WorkoutPlanAddRequest request);
        Task<WorkoutPlanResponse> GetWorkoutPlanViaIdAsync(int id);
        Task<ICollection<WorkoutPlanResponse>> GetAllWorkoutPlanAsync();
    }
}
