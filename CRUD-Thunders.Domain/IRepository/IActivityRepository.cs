using CRUD_Thunders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Thunders.Domain.IRepository
{
    public interface IActivityRepository
    {
        List<Activity> GetActivities();
        Activity GetActivityById(Guid id);
        void PostActivity(Activity activity);
        void DeleteActivity(Activity activity);
    }
}
