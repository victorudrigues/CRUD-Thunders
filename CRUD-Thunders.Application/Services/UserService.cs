using AutoMapper;
using CRUD_Thunders.Application.DTOs;
using CRUD_Thunders.Application.IServices;
using CRUD_Thunders.Domain.Entities;
using CRUD_Thunders.Domain.IRepository;
using CRUD_Thunders.Infra.Infrastructure.CRUDContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Activity = CRUD_Thunders.Domain.Entities.Activity;



namespace CRUD_Thunders.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly Context _context;
        public UserService(IUserRepository userRepository, IMapper mapper, IActivityRepository activityRepository, Context context)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _activityRepository = activityRepository;
            _context = context;
        }
        public List<UserDTO> GetUsers()
        {
            try
            {
                var users = _userRepository.GetUsers();
                return _mapper.Map<List<UserDTO>>(users);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void PostUser(User user)
        {
            try
            {
                _userRepository.PostUser(user);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                // Buscando usuários existentes
                var existingUser = _userRepository.GetUserById(user.Id);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUser(Guid Id)
        {
            try
            {   //Selecionar usuário
                var user = _userRepository.GetUserById(Id);
                //Deleta usuário
                _userRepository.DeleteUser(user);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetUserById(Guid id)
        {
            try
            {
                return _userRepository.GetUserById(id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Metodos
        //Comparação
        private bool AreActivitiesEqual(Activity dtActivity, Activity newActivity)
        {
            return dtActivity.Name == newActivity.Name && dtActivity.Description == newActivity.Description;
        }
    }
}
