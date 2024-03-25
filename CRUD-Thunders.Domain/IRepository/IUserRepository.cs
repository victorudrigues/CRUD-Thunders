using CRUD_Thunders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Thunders.Domain.IRepository
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(Guid id);
        void PostUser(User user);
        void DeleteUser(User user);

    }
}
