using CRUD_Thunders.Application.DTOs;
using CRUD_Thunders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Thunders.Application.IServices
{
    public interface IUserService
    {
        List<UserDTO> GetUsers();
        string PostUser(User user);
        string DeleteUser(Guid Id);
        string UpdateUser(User user);
    }
}
