﻿using AmidogsManager.Database;
using AmidogsManager.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.Repository.Repositories
{
    public class DogRepository
    {
        private readonly AmidogsManagerContext amidogsManagerContext;
        public DogRepository(AmidogsManagerContext amidogsManagerContext)
        {
            this.amidogsManagerContext = amidogsManagerContext;
        }

        public Dog GetById(int dogId)
        {
            return amidogsManagerContext.Dogs.Where(d => d.Id == dogId).FirstOrDefault();
        }

        public List<Dog> GetByUser(int userId) 
        {
            return amidogsManagerContext.Dogs.Where(d => d.UserId == userId).ToList<Dog>();
        }
    }
}
