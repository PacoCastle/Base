using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Services
{
    public interface IMachineService
    {
        Task<IEnumerable<MachineModel>> GetMachines();
        Task<MachineModel> GetMachineById(int id);
        Task<MachineModel> CreateMachine(MachineModel machineModel);
        Task  UpdateMachine(MachineModel machineModel , MachineModel machineUpdateModel);
        Task<MachineModel> GetMachineByName(string name);
    }
}
