using Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.TrainerDTO
{
    public class TrainerAddRequest
    {
        [Required(ErrorMessage = "ID is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Specialties are required")]
        public string Specialties { get; set; }

        [Required(ErrorMessage = "Certifications are required")]
        public string Certifications { get; set; }

        public Trainer ToTrainer()
        {
            return new Trainer
            {
                UserId = Id,
                Specialties = Specialties,
                Certifications = Certifications
            };
        }
    }
}
