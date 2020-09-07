using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.Core.Services
{
    public interface IPartService
    {
        Task<BaseResponse<IEnumerable<PartModel>>> GetParts();
        Task<BaseResponse<PartModel>> GetPartById(int id);
        Task<BaseResponse<PartModel>>  CreatePart(PartModel partModel);
        Task<BaseResponse<PartModel>>  UpdatePart(PartModel partModel , PartModel partUpdateModel);
        Task<BaseResponse<PartModel>>  GetPartByName(string name);
    }
}
