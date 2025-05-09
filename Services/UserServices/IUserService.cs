﻿using Entities.DTO.MemberDTO;
using Entities.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public interface IUserService
    {
        Task<RegisterMemberResponse> RegisterMember(RegisterMemberAddRequest request);
        Task<UserResponse> UpdateUserDetails(MemberUpdateRequest request);
        Task<bool> DeleteUser(Guid userId);
    }
}
