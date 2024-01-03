using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Jul.Repository.Models;

namespace Jul.Repository
{
    /// <summary>
    /// What about my connection to Db?
    /// 0) create a Model, add a DbSet<ModelName>
    /// 1) add-migration name
    /// 2) update-database
    /// 
    /// Migration error - remove or
    /// Error Handling) Erase the Migration folder, Annihilate the DB
    /// </summary>
    public class Dbcontext : DbContext
    {
        public Dbcontext(DbContextOptions<Dbcontext> option) : base(option)
        {
            
        }
        //tables in my DB
        public DbSet<Hero> Hero {  get; set; }
        //public DbSet<ModelName> Hero { get; set; }
    }
}
