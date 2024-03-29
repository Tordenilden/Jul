﻿using Jul.Repository.Interfaces;
using Jul.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jul.Repository.Logic
{
    public class HeroRepository : IHeroRepository
    {
        Dbcontext context;
        public HeroRepository(Dbcontext c) { context = c; }// Dependency Injection - DI
        #region impl
        public async Task<Hero> create(Hero entity)
        {
            // connect to db "directly" and use the tables
            // SingleOrDefault(), FirstOrDefault() , Find(), Where(), Add()
            context.Hero.Add(entity);
            //context.Hero.Add(new Hero { })
            await context.SaveChangesAsync(); // SaveChangesAsync()
            return entity;
        }

        public async Task<bool> delete(int Id)
        {
            var hero = await getById(Id);
            if (hero != null) {
                context.Hero.Remove(hero);
                await context.SaveChangesAsync();
                return true;
            }
            return false;          
        }

        public async Task<List<Hero>> getAll()
        {
            return await context.Hero.ToListAsync();
        }

        public async Task<Hero> getById(int id)
        {          
            return await context.Hero.FirstOrDefaultAsync(h => h.Id == id);
        }
        #endregion impl

#region not impl
        public async Task<Hero> getByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Hero> update(Hero entity)
        {
            // findes obj?
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

#endregion not impl
    }



    //List<Hero> list = new List<Hero>
    //{new Hero{Id=2,Name="Bo",RealName="bandit",Place="dk",DebutYear= DateTime.Now } };

    //foreach (var hero in list)
    //{
    //    if (hero.Id == 2) { We have vacation for a week};
    //}

    // DO we have to check if its null? - ternary eller normalt

    //context.Hero.Where((hero) => hero.Id == id).ToList();
    //context.Hero.Where((hero) => hero.Id != id).ToList();
    //context.Hero.Where((hero) => hero.Id < 2).ToList();
    //context.Hero.Where((hero) => hero.Name != "Ulla").ToList();
}
