﻿using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.BusinessLogic.Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User GetUserById(int userId)
        {
            try
            {
                User? user = userRepository.GetUserById(userId);
                return user ?? throw new InvalidOperationException($"No Match found with {userId}"); ;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public User? GetUserByDogId(int dogId)
        {
            try
            {
                return userRepository.GetUserByDogId(dogId);
            }
            catch (Exception ex)
            {
                // Registrar el error
                Console.WriteLine($"Error in GetUserByDogId: {ex.Message}");
                throw;
            }
        }

        public string UpdateComplaintNumber(int userId)
        {
            try
            {
                userRepository.UpdateComplaintNumber (userId);
                return "Denuncia hecha";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
