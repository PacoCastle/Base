using DatingApp.Core;
using DatingApp.Core.Models;
using DatingApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Services
{
    public class PartService : IPartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PartService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        
        public async Task<PartModel> GetPartById(int id)
        {
            return await _unitOfWork.PartRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<PartModel>> GetParts()
        {
            return await _unitOfWork.PartRepository.GetAllAsync();
        }

        public async Task<PartModel> CreatePart(PartModel PartToBeCreatedModel)
        {
             await _unitOfWork.PartRepository.AddAsync(PartToBeCreatedModel);
             await _unitOfWork.CommitAsync();
            return PartToBeCreatedModel;
        }

        public async Task UpdatePart(PartModel PartToBeUpdateModel, PartModel PartUpdateModel)
        {
            PartToBeUpdateModel.Name = PartUpdateModel.Name;
            PartToBeUpdateModel.Description = PartUpdateModel.Description;
            PartToBeUpdateModel.Status = PartUpdateModel.Status;
            await _unitOfWork.CommitAsync();
        }

        public async Task<PartModel> GetPartByName(string name)
        {
            return await _unitOfWork.PartRepository.GetPartByName(name);
        }
    }
}
