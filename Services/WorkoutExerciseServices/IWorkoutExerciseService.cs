using Entities.DTO.WorkoutExerciseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkoutExerciseServices
{
    public interface IWorkoutExerciseService
    {
        Task<WorkoutExerciseResponse> CreateWorkoutExerciseAsync(WorkoutExerciseAddRequest request);
    }
}
