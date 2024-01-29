using Jul.API.Controllers;
using Jul.Repository.Interfaces;
using Jul.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Jul.UnitTest.ControllerTest
{
    public class HeroControllerTest
    {
        // interface
        // Data hvor kommer de fra? 
        // Simulere en class
        private IHeroRepository repo {  get; set; } // the old way...
        private DataStore store { get; set; }
        HeroesController controller;
        public HeroControllerTest() { // passing??
            
            store = new DataStore(); // + setData() har mulighed for mere logik

            controller = new HeroesController(store);
        }

        #region Get
        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            //Arrange
            List<Hero> heroes = new List<Hero>()
            {
                new Hero { Id = 1, DebutYear= DateTime.Now, Name= "Ulla", RealName="Ulla Ulla", Place="DK" },
                new Hero { Id = 2, DebutYear = DateTime.Now, Name = "Hansi", RealName = "Ulla Ulla", Place = "UK" }
            };
            store.setData(heroes);
            //Act
            var result = await controller.GetHero();
            //Assert
            var statusResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusResult.StatusCode);
        }        
        [Fact]
        public async Task GetAll_ShouldReturn204_NoData()
        {
            //Arrange
            List<Hero> heroes = new List<Hero>()
            {
            };
            store.setData(heroes);
            //Act
            var result = await controller.GetHero();
            //Assert
            var statusResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusResult.StatusCode);
        }        
        [Fact]
        public async Task GetAll_ShouldReturn500_NoHeroesObject()
        {
            //Arrange
            List<Hero> heroes = null; // Data is invalid
            store.setData(heroes);
            //Act
            var result = await controller.GetHero();
            //Assert
            var statusResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusResult.StatusCode);
        }        
        [Fact]
        public async Task GetAll_ShouldReturn500_AnnihilationException()
        {
            //Arrange
            //List<Hero> heroes = null;
            //store.setData(heroes);
            store = null; //Data and our Repository is Invalid

            //Act
            var result = await controller.GetHero();
            //Assert
            var statusResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusResult.StatusCode);
        }

        #endregion get
        #region Delete
        [Fact]
        public async Task Delete_FoundData()
        {
            List<Hero> heroes = new List<Hero>()
            {
                new Hero { Id = 1, DebutYear= DateTime.Now, Name= "Ulla", RealName="Ulla Ulla", Place="DK" },
                new Hero { Id = 2, DebutYear = DateTime.Now, Name = "Hansi", RealName = "Ulla Ulla", Place = "UK" }
            };
            store.setData(heroes);

            var result = await controller.DeleteHero(2);
            Assert.Equal(200, ((IStatusCodeActionResult)result).StatusCode);
        }        
        [Fact]
        public async Task DeleteNoDataFound()
        {
            List<Hero> heroes = new List<Hero>()
            {
                new Hero { Id = 1, DebutYear= DateTime.Now, Name= "Ulla", RealName="Ulla Ulla", Place="DK" },
                new Hero { Id = 2, DebutYear = DateTime.Now, Name = "Hansi", RealName = "Ulla Ulla", Place = "UK" }
            };
            store.setData(heroes);

            var result = await controller.DeleteHero(234);
            Assert.Equal(404, ((IStatusCodeActionResult)result).StatusCode);
        }

        // TODO - Throw...
        #endregion Delete
        #region    Update
        [Fact]
        public async Task UpdateHero_OK()
        {
            List<Hero> heroes = new List<Hero>()
            {
                new Hero { Id = 1, DebutYear= DateTime.Now, Name= "Ulla", RealName="Ulla Ulla", Place="DK" },
                new Hero { Id = 2, DebutYear = DateTime.Now, Name = "Hansi", RealName = "Ulla Ulla", Place = "UK" }
            };
            store.setData(heroes);
            Hero hero = new Hero { Id = 1, DebutYear = DateTime.Now, Name = "Anna", RealName = "Anna the great", Place = "UK" };

            var result = await controller.PutHero(1,hero);
            //Assert
            var statusResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusResult.StatusCode);
            //Assert.Equal(2, store.Count()); // så skal vi kode Count()
        }
        [Fact]
        public async Task UpdateHero_ShouldReturn404()
        {
            //List<Hero> heroes = new List<Hero>()
            //{
            //    new Hero { Id = 1, DebutYear= DateTime.Now, Name= "Ulla", RealName="Ulla Ulla", Place="DK" },
            //    new Hero { Id = 2, DebutYear = DateTime.Now, Name = "Hansi", RealName = "Ulla Ulla", Place = "UK" }
            //};
            //store.setData(heroes);
            Hero hero = new Hero { Id = 1, DebutYear = DateTime.Now, Name = "Anna", RealName = "Anna the great", Place = "UK" };

            var result = await controller.PutHero(2,hero);
            //Assert
            var statusResult = (IStatusCodeActionResult)result;
            Assert.Equal(400, statusResult.StatusCode);
        }


        #endregion Update
        #region    Create
        [Fact]
        public async Task CreateHero_ObjectExists()
        {
            var hero = new Hero { Id = 5, Name = "Hansi", RealName = "Gorm den smarte", DebutYear = DateTime.Now, Place = "The sky" };

            var result = await controller.PostHero(hero);

            var statusResult = (IStatusCodeActionResult)result;
            Assert.Equal(201, statusResult.StatusCode);
        }        
        
        [Fact]
        public async Task CreateHero_IfNoDataExists()
        {
            Hero hero = null;

            var result = await controller.PostHero(hero);

            var statusResult = (IStatusCodeActionResult)result;
            Assert.Equal(400, statusResult.StatusCode);
        }

        [Fact]
        public async Task CreateHero_ExceptionIsThrown()
        {
            // kill db!!
            Hero hero = null;

            var result = await controller.PostHero(hero);

            var statusResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusResult.StatusCode);
        }

        #endregion Create
    }

    /// <summary>
    /// 1) Data
    /// </summary>
    public class DataStore : IHeroRepository // DataStore is almost HeroRepository....
    {
        private List<Hero> heroes {  get; set; }
        public DataStore() { } // need data directly or not?
        public DataStore(List<Hero> heroes)
        {
            this.heroes = heroes;
        }


        public void setData(List<Hero> heroes) { this.heroes = heroes; }




        public async Task<Hero> create(Hero entity)
        {
            return await Task.FromResult<Hero>(entity);
        }
        public async Task<Hero> update(Hero entity)
        {
            // EF is tracking an object that it can modify
            var objToUpdate = heroes.FirstOrDefault(h=>h.Id == entity.Id);
            objToUpdate.Name = entity.Name;
            objToUpdate.RealName = entity.RealName;
            objToUpdate.DebutYear = entity.DebutYear;
            objToUpdate.Place = entity.Place;
            // AutoMapper <obj1, obj2>
            return await Task.FromResult<Hero>(entity);

        }

        public async Task<bool> delete(int id) //2
        {
            var found = heroes.Where(h=>h.Id == id).FirstOrDefault();
            //????? or what??
            //return await Task.FromResult(if (found != null) { return true; } else return false);
            return await Task.FromResult(found != null ?true:false);
        }

        // right now its repeting code. Iam so sry
        public async Task<List<Hero>> getAll()
        {
            // WaitAll -- prøv lige den her af...
            return await Task.FromResult(heroes);
        }
        //public async Task<List<Hero>> getAll()
        //{
        //    return await context.Hero.ToListAsync();
        //}

        public Task<Hero> getById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Hero> getByName(string name)
        {
            throw new NotImplementedException();
        }


    }
}
// Assert.Equal(HttpStatusCode.OK , (HttpStatusCode)result.StatusCode);
// tænk lidt på matematik  2+3*4 = 14
//                        (2+3)*4
//                         2+(3*4)

//HttpStatusCode.NoContent == (HttpStatusCode)test.StatusCode


//var statusResult = (IStatusCodeActionResult)result;
//Assert.Equal(200, statusResult.StatusCode);