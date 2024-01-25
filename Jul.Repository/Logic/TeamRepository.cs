using Jul.Repository.Interfaces;
using Jul.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jul.Repository.Logic
{
    public class TeamRepository : ITeamRepository
    {
        Dbcontext context;
        public TeamRepository(Dbcontext c) { context = c; }

        public async Task<List<Team>> getAll()
        {
            return await context.Team.ToListAsync();
        }

    }
}
