using Entities.Identities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Domain
{
    public class Trainer
    {
        [Key]
        public Guid UserId { get; set; } //foreignKey & Acts as TrainerId
        public UserApplication User {  get; set; } // one to one -> UserApplication
        public string Specialties { get; set; }
        public  string Certifications { get; set; }
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>(); // one to many -> WorkoutPlan
        public ICollection<Class> Classes { get; set; } = new List<Class>();// one to many -> Class
    }
}
