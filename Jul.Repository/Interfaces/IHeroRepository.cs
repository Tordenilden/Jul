using Jul.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jul.Repository.Interfaces
{
    /// <summary>
    /// What? contract, specifications for methods, etc. Blueprint
    /// boundaries for a class
    /// polymorf - abstract => create can variate....
    /// </summary>
    public interface IHeroRepository
    {
        //CRUD
        public Task<Hero> create(Hero entity);
        public Task<Hero> update(Hero entity);
        public Task<bool> delete(int id);
        public Task<Hero> getById(int id);
        public Task<Hero> getByName(string name);
        public Task<List<Hero>> getAll();
    }
    
}
