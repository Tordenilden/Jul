using Jul.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jul.Repository.Logic
{
    public class HeroRepository_Tests
    {
        private DbContextOptions<DbContext> options;
        private DbContext context;

        public HeroRepository_Tests()
        {
            //options = new DbContextOptionsBuilder<DbContext>()
            //  .UseInMemoryDatabase(databaseName: "dummyDatabase")
            //  .Options;

            //context = new DbContext(options);
            //context.Database.EnsureDeleted();

            //context.Hero.Add(new Hero { Id = 1, Name = "action" , RealName = "Hansi" , Place = "DK", DebutYear = 2004});
            //context.Hero.Add(new Hero { Id = 2, Name = "comedy" , RealName = "Hansi" , Place = "EU", DebutYear = 2009});
            //context.Hero.Add(new Hero { Id = 3, Name = "funny"  , RealName = "Hansi" , Place = "DK", DebutYear = 2014});
            //context.SaveChanges();
        }

    }
}
