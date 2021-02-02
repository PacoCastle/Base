using DatingApp.Core;
using DatingApp.Core.Models;
using DatingApp.Core.Repositories;
using DatingApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Services
{
    public class MachinesService : IMachineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MachinesService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        
        public async Task<MachineModel> GetMachineById(int id)
        {
            return await _unitOfWork.MachineRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MachineModel>> GetMachines()
        {
            return await _unitOfWork.MachineRepository.GetAllAsync();
        }

        public async Task<MachineModel> CreateMachine(MachineModel machineToBeCreatedModel)
        {
             await _unitOfWork.MachineRepository.AddAsync(machineToBeCreatedModel);
             await _unitOfWork.CommitAsync();
            return machineToBeCreatedModel;
        }

        public async Task UpdateMachine(MachineModel machineToBeUpdateModel, MachineModel machineUpdateModel)
        {
            machineToBeUpdateModel.Name = machineUpdateModel.Name;
            machineToBeUpdateModel.Description = machineUpdateModel.Description;
            machineToBeUpdateModel.Status = machineUpdateModel.Status;
            await _unitOfWork.CommitAsync();
        }
        public async Task<MachineModel> GetMachineByName(string name)
        {
            return await _unitOfWork.MachineRepository.GetMachineByName(name);
        }
    }
}
