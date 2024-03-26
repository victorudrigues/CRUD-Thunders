using CRUD_Thunders.Domain.Entities;
using CRUD_Thunders.Domain.IRepository;
using CRUD_Thunders.Infra.Infrastructure.CRUDContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Thunders.Infra.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly IActivityRepository _activityRepository;

        public UserRepository(Context context, IActivityRepository activityRepository) 
        { 
            _context = context;
            _activityRepository = activityRepository;
        }
        public List<User> GetUsers()
        {
            return _context.User.ToList();
        }
        public User GetUserById(Guid id)
        {
            return _context.User.Include(x => x.Activities).FirstOrDefault(x => x.Id == id);
            
        }

        public void PostUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.User.Remove(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            // Buscando usuários existentes
            var existingUser = GetUserById(user.Id);

            if (existingUser == null)
            {
                throw new Exception("Nenhum usuário encontrado");
            }

            // evitando conflitos 
            _context.Entry(existingUser).State = EntityState.Detached;

            // listando atividades existentes e atividades da requisição
            var existingActivities = existingUser.Activities.ToList();
            var newActivities = user.Activities.ToList();

            //Criando lista para atividades que precisam de updates e lista para atividades que precisam serem adicionadas
            var activitiesToUpdate = new List<Activity>();
            var activitiesToAdd = new List<Activity>();


            foreach (var newActivity in newActivities)
            {
                //Caso atividade não esteja no banco
                if (newActivity.Id == Guid.Empty)
                {
                    activitiesToAdd.Add(newActivity);
                }
                else
                {
                    //Atividades que axistem mas precisam de update
                    activitiesToUpdate.Add(newActivity);
                }
            }

            //confirmação e verificação de atividades existentes e iguais
            foreach (var activityToUpdate in activitiesToUpdate)
            {
                var existingActivity = existingActivities.FirstOrDefault(a => a.Id == activityToUpdate.Id);
                if (existingActivity != null && !AreActivitiesEqual(existingActivity, activityToUpdate))
                {
                    _context.Activity.Update(activityToUpdate);
                }
            }

            //identificando atividades para deletar, caso passe menos atividades do que já existem no banco
            var activitiesToDelete = existingActivities.Where(a => !newActivities.Any(na => na.Id == a.Id)).ToList();

            //Deletando atividades
            foreach (var activityToDelete in activitiesToDelete)
            {
                _activityRepository.DeleteActivity(activityToDelete);
            }

            // Adicionando novas
            foreach (var activity in activitiesToAdd)
            {
                _activityRepository.PostActivity(activity);
            }

            // Alterando nome
            existingUser.Name = user.Name;

            // Informando o contexto que foi modificado
            _context.Entry(existingUser).State = EntityState.Modified;

            // persistência de salve
            _context.SaveChanges();
        }

        public bool AreActivitiesEqual(Activity dtActivity, Activity newActivity)
        {
         
            return dtActivity.Name == newActivity.Name && dtActivity.Description == newActivity.Description;
        }
    }
}
