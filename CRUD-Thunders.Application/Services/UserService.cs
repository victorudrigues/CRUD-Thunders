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
                _userRepository.UpdateUser(user);
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

       
    }
}
