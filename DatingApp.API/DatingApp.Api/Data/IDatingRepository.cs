using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Api.Dtos;
using DatingApp.Api.Helpers;
using DatingApp.Core.Models;

namespace DatingApp.Api.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id, bool isCurrentUser);
        // Task<Photo> GetPhoto(int id);
        // Task<Photo> GetMainPhotoForUser(int userId);
        // Task<Like> GetLike(int userId, int recipientId);
        // Task<Message> GetMessage(int id);
        //Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        //Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
        Task<PagedList<Plc>> GetPlcs(PlcParams plcParams);
        Task<IEnumerable<Plc>> GetPlcForDevice(int idDevice);
        Task<Product> GetProduct(int id);
        Task<PagedList<Product>> GetProducts(ProductParams productParams);
        Task<int> AddByStored(List<ConfigurationForRegisterDto> configurationForRegisterDto);
        Task<List<DeviceConfiguration>> GetDevices();
        Task<DeviceConfiguration> GetDevice(int id);
        Task<PartModel> GetPart(int id);
        Task<List<PartModel>> GetParts();
    }
}