using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<dynamic> GetMenuById(int id);
        Task<IEnumerable<Menu>> GetMenus();
    }
}