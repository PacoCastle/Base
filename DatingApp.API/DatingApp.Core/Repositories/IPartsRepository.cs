using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Repositories
{
    public interface IPartRepository : IRepository<PartModel>
    {
        Task<PartModel> GetPartByName(string PartModel);
    }
}