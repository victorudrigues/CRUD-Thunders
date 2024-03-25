﻿using AutoMapper;
using CRUD_Thunders.Application.DTOs;
using CRUD_Thunders.Application.IServices;
using CRUD_Thunders.Domain.Entities;
using CRUD_Thunders.Domain.IRepository;
using CRUD_Thunders.Infra.Infrastructure.CRUDContext;
using Microsoft.EntityFrameworkCore;



namespace CRUD_Thunders.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly Context _context;
        public UserService(IUserRepository userRepository, IMapper mapper, Context context) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
        public string PostUser(User user)
        {
            try
            {
                _userRepository.PostUser(user);
                return "Usuário salvo com sucesso";

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public string UpdateUser(User user)
        {
            try
            {
                // Selecionar usuário antigo
                var existingUser = _userRepository.GetUserById(user.Id);


                if (existingUser != null)
                {
                    // Desanexa a entidade do contexto
                    _context.Entry(existingUser).State = EntityState.Detached;
                }

                //Passando usuário atualizado
                existingUser = user;

                // Informa ao contexto que a entidade foi modificada
                _context.Entry(existingUser).State = EntityState.Modified;

                // Salva as alterações no banco de dados
                _context.SaveChanges();

                return "Usuário Atualizado com sucesso";

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public string DeleteUser(Guid Id)
        {
            try
            {   //Selecionar usuário
                var user = _userRepository.GetUserById(Id);
                //Deleta usuário
                _userRepository.DeleteUser(user);
                return "Usuário deletado com sucesso";
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}