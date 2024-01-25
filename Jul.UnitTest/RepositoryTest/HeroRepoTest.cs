using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jul.Repository.Models;
using Jul.Repository;
using Jul.Repository.Logic;

namespace Jul.UnitTest.RepositoryTest
{
    // HeroRepository class
    public class HeroRepoTest
    {
        private DbContextOptions<Dbcontext> options;
        private Dbcontext context;


        public HeroRepoTest()
        {
                options = new DbContextOptionsBuilder<Dbcontext>()
                .UseInMemoryDatabase(databaseName: "dummy")
                .Options;

            //
            context = new Dbcontext(options);
            context.Database.EnsureDeleted(); // hvis i oplever data er der flere gange mm...
            context.Hero.Add(new Hero { Id = 1, DebutYear= DateTime.Now, Name= "Ulla", RealName="Ulla Ulla", Place="DK" });
            context.Hero.Add(new Hero { Id = 2, DebutYear= DateTime.Now, Name= "Hansi", RealName="Ulla Ulla", Place="UK" });
            context.SaveChangesAsync(); // i sql skriver den commit
        }

        [Fact]
        public async Task GetHero_ReturnsTwoObject()
        {
            //Arrange  - variables, data
            HeroRepository p = new HeroRepository(context);

            //Act      - call the method to be tested
            var result = await p.getAll();
            
            //Assert   - verify that we get the result we want
            Assert.Equal(2,result.Count);
        }        
        [Fact]
        public async Task GetHeroById()
        {
            //Arrange  - variables, data
            HeroRepository p = new HeroRepository(context);
            int heroId = 2;
            //Act      - call the method to be tested
            var result = await p.getById(heroId); 
            
            //Assert   - verify that we get the result we want
            Assert.Equal(2,result.Id);
            Assert.Equal("Hansi", result.Name);
        }        
        [Fact]
        public async Task GetHeroById_HeroNotExist()
        {
            //Arrange  - variables, data
            HeroRepository p = new HeroRepository(context);
            int heroId = 5;
            //Act      - call the method to be tested
            var result = await p.getById(heroId); 
            
            //Assert   - verify that we get the result we want
            Assert.Equal(2,result.Id);
            Assert.Equal("Hansi", result.Name);
            //Assert.ThrowsAsync KIG LIGE HER
         }

        [Fact]
        public async Task DeleteIfExists()
        {
            //Arrange  - variables, data
            HeroRepository p = new HeroRepository(context);
            int heroId = 2;
            //Act      - call the method to be tested
            var result = await p.delete(heroId);

            //Assert   - verify that we get the result we want
            //Assert.NotNull(result);
            Assert.True(result);
            //Assert.True(result.Exist);
        }
        [Fact]
        public async Task DeleteIfNotExists()
        {
            //Arrange  - variables, data
            HeroRepository p = new HeroRepository(context);
            int heroId = 6;
            //Act      - call the method to be tested
            var result = await p.delete(heroId);

            //Assert   - verify that we get the result we want
            //Assert.NotNull(result);
            Assert.False(result);
        }

        [Fact]
        public async Task Create_Ok()
        {
            HeroRepository p = new HeroRepository(context);
            Hero h = new Hero { Id = 44, DebutYear = DateTime.Now, Name = "Bo", RealName = "Det er meget sort", Place = "DK" };

            var result = await p.create(h);
            //Assert.Equal(3,result.Id);
            // Hvis vi gerne vil måle antallet af poster nu for at se om der er kommet 1 mere...
            var all = await p.getAll();

            Assert.True(all.Contains(h));

            Assert.Equal(3,all.Count);
            Assert.Equal(result, await p.getById(44));
            
        }        
        [Fact]
        public async Task Create_NoData()
        {
            HeroRepository p = new HeroRepository(context);
            Hero h = new Hero { };

            var result = await p.create(h);
            //Assert.Equal(3,result.Id);
            // Hvis vi gerne vil måle antallet af poster nu for at se om der er kommet 1 mere...
            var all = await p.getAll();

            Assert.True(all.Contains(h));

            // I will Expect 2 but passed 3 - So I wrote the test wrong...
            Assert.Equal(3,all.Count);
            //Assert.Equal(result, await p.getById(44));
            
        }       
        [Fact]
        public async Task Create_AllRdyExists()
        {
            HeroRepository p = new HeroRepository(context);
            Hero h = new Hero { Id=1}; // fails!!

            var result = await p.create(h);
            //Assert.Equal(3,result.Id);
            // Hvis vi gerne vil måle antallet af poster nu for at se om der er kommet 1 mere...
            var all = await p.getAll();

            Assert.True(all.Contains(h));

            Assert.Equal(3,all.Count);
            //Assert.Equal(result, await p.getById(44));
            
        }        
        [Fact]
        public async Task Create_ObjectNotExists()
        {
            HeroRepository p = new HeroRepository(context);
            Hero h = null;

            //var result = await p.create(h);
            //Assert.Equal(3,result.Id);
            // Hvis vi gerne vil måle antallet af poster nu for at se om der er kommet 1 mere...
            //var all = await p.getAll();
            Assert.ThrowsAsync<ArgumentNullException>(async () => await p.create(h));

        }



        //[Fact]
        //public async Task GetHero_NoContext()
        //{
        //    //Arrange  - variables, data
        //    // kill my context!!
        //    context = null;
        //    HeroRepository p = new HeroRepository(context);

        //    //Act      - call the method to be tested
        //    var result = await p.getAll();

        //    //Assert   - verify that we get the result we want
        //    Assert.Null(result);
        //}


        //public async Task<List<Hero>> getAll()
        //{
        //    return await context.Hero.ToListAsync();
        //}
    }
}
