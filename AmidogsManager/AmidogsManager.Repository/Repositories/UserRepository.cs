using AmidogsManager.Database;
using AmidogsManager.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.Repository.Repositories
{
    public class UserRepository
    {
        private readonly AmidogsManagerContext amidogsManagerContext;
        public UserRepository(AmidogsManagerContext amidogsManagerContext)
        {
            this.amidogsManagerContext = amidogsManagerContext;
        }
    }
}
