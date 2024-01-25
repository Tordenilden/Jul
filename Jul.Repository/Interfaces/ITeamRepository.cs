using Jul.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jul.Repository.Interfaces
{
    public interface ITeamRepository
    {
        public Task<List<Team>> getAll();
    }
}
