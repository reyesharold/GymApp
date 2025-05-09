using Entities.DTO.UserDTO;
using Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.TrainerDTO
{
    public class CreateTrainerDTO
    {
        public RegisterDTO RegisterDTO { get; set; }
        public TrainerAddRequest TrainerAddRequest { get; set; }
    }
}
