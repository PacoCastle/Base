using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Repositories
{
    public interface IMachineRepository : IRepository<MachineModel>
    {
        //Task<int> AddByStored(string MachineModel, string PartModel, string InternalSequence);
    }
}