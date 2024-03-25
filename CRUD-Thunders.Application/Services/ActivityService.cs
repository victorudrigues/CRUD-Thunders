using AutoMapper;
using CRUD_Thunders.Application.DTOs;
using CRUD_Thunders.Application.IServices;
using CRUD_Thunders.Domain.Entities;
using CRUD_Thunders.Domain.IRepository;
using CRUD_Thunders.Infra.Infrastructure.CRUDContext;
using CRUD_Thunders.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Thunders.Application.Services
{
    public class ActivityService: IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly Context _context;
        public ActivityService(IActivityRepository activityRepository, IMapper mapper, Context context)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _context = context;
        }

        public List<ActivityDTO> GetActivities()
        {
            try
            {
                var activities = _activityRepository.GetActivities();
                return _mapper.Map<List<ActivityDTO>>(activities);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void PostActivity(Activity activity)
        {
            try
            {
                _activityRepository.PostActivity(activity);
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void UpdateActivity(Activity activity)
        {
            try
            {
                // Selecionar atividade antiga
                var existingActivity = _activityRepository.GetActivityById(activity.Id);


                if (existingActivity != null)
                {
                    // Desanexa a entidade do contexto
                    _context.Entry(existingActivity).State = EntityState.Detached;
                }

                //Passando atividade atualizada
                existingActivity = activity;

                // Informa ao contexto que a entidade foi modificada
                _context.Entry(existingActivity).State = EntityState.Modified;

                // Salva as alterações no banco de dados
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }

        }

        public void DeleteActivity(Guid Id)
        {
            try
            {   //Selecionar usuário
                var activity = _activityRepository.GetActivityById(Id);
                //Deleta usuário
                _activityRepository.DeleteActivity(activity);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
