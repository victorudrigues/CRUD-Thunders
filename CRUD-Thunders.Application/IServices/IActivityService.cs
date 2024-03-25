using CRUD_Thunders.Application.DTOs;
using CRUD_Thunders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Thunders.Application.IServices
{
    public interface IActivityService
    {
        List<ActivityDTO> GetActivities();
        void PostActivity(Activity activity);
        void DeleteActivity(Guid Id);
        void UpdateActivity(Activity activity);
    }
}
