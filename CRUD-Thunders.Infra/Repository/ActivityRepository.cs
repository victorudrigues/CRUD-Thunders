using CRUD_Thunders.Domain.Entities;
using CRUD_Thunders.Domain.IRepository;
using CRUD_Thunders.Infra.Infrastructure.CRUDContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Thunders.Infra.Repository
{
    public class ActivityRepository: IActivityRepository
    {
        private readonly Context _context;
        public ActivityRepository(Context context)
        {
            _context = context;
        }

        public List<Activity> GetActivities()
        {
            return _context.Activity.ToList();
        }
        public Activity GetActivityById(Guid id)
        {
            return _context.Activity.FirstOrDefault(x => x.Id == id);

        }
        public void PostActivity(Activity activity)
        {
            _context.Activity.Add(activity);
            _context.SaveChanges();
        }

        public void DeleteActivity(Activity activity)
        {
            _context.Activity.Remove(activity);
            _context.SaveChanges();
        }

       

       

       
    }
}
