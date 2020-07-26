using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Services
{
    public interface IPartService
    {
        Task<IEnumerable<PartModel>> GetParts();
        Task<PartModel> GetPartById(int id);
        Task<PartModel> CreatePart(PartModel partModel);
        Task  UpdatePart(PartModel partModel , PartModel partUpdateModel);
        Task<PartModel> GetPartByName(string name);
    }
}
