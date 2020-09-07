using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Repositories
{
    public interface IMachineRepository : IRepository<MachineModel>
    {
        Task<MachineModel> GetMachineByName(string MachineModel);
    }
}