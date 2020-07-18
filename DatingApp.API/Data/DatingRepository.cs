using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DatingApp.Core;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync(u =>
                u.LikerId == userId && u.LikeeId == recipientId);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<User> GetUser(int id, bool isCurrentUser)
        {
            var query = _context.Users.Include(p => p.Photos).AsQueryable();

            if (isCurrentUser)
                query = query.IgnoreQueryFilters();

            var user = await query.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.OrderByDescending(u => u.LastActive).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);

            users = users.Where(u => u.Gender == userParams.Gender);

            if (userParams.Likers)
            {
                var userLikers = await GetUserLikes(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikers.Contains(u.Id));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUserLikes(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikees.Contains(u.Id));
            }

            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserLikes(int id, bool likers)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (likers)
            {
                return user.Likers.Where(u => u.LikeeId == id).Select(i => i.LikerId);
            }
            else
            {
                return user.Likees.Where(u => u.LikerId == id).Select(i => i.LikeeId);
            }
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Messages.AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId
                        && u.RecipientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == messageParams.UserId
                        && u.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId
                        && u.RecipientDeleted == false && u.IsRead == false);
                    break;
            }

            messages = messages.OrderByDescending(d => d.MessageSent);

            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _context.Messages
                .Where(m => m.RecipientId == userId && m.RecipientDeleted == false
                    && m.SenderId == recipientId
                    || m.RecipientId == recipientId && m.SenderId == userId
                    && m.SenderDeleted == false)
                .OrderByDescending(m => m.MessageSent)
                .ToListAsync();

            return messages;
        }
        public async Task<PagedList<Plc>> GetPlcs(PlcParams plcParams)
        {
            var plcs = _context.Plc.AsQueryable().OrderByDescending(p => p.Id);

            return await PagedList<Plc>.CreateAsync(plcs, plcParams.PageNumber, plcParams.PageSize);
        }

        public async Task<IEnumerable<Plc>> GetPlcForDevice(int deviceId)
        {
            var plcs = await _context.Plc
                .Where(m => m.DeviceId == deviceId)
                .ToListAsync();

            return plcs;
        }
        public async Task<Product> GetProduct(int id)
        {
            return await _context.Product.FirstOrDefaultAsync(p =>
                p.Id == id);
        }

        public async Task<PagedList<Product>> GetProducts(ProductParams productParams)
        {
            var products = _context.Product;

            return await PagedList<Product>.CreateAsync(products, productParams.PageNumber, productParams.PageSize);
        }

        public async Task<int> AddByStored(List<ConfigurationForRegisterDto> configurationForRegisterDto)
        {
            int res = 0; 
            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(configurationForRegisterDto);
            foreach ( ConfigurationForRegisterDto current in configurationForRegisterDto)            {           

                
                res = res =+ await _context.Database.ExecuteSqlCommandAsync(
                    "spXML_DeviceConfiguration @ServerName,@Chanel, @Device, @Tags",
                    new SqlParameter("@ServerName", current.ServerName),
                    new SqlParameter("@Chanel", current.Chanel),
                    new SqlParameter("@Device", current.Device),
                    new SqlParameter("@Tags", Newtonsoft.Json.JsonConvert.SerializeObject(current.Tags)));

            }          

            return res;               
                
        }
        public async Task<List<DeviceConfiguration>> GetDevices()
        {
            return await _context.DeviceConfiguration.ToListAsync();            
        } 
        public async Task<DeviceConfiguration> GetDevice(int id)
        {
            return await _context.DeviceConfiguration.FirstOrDefaultAsync(p =>
                p.Id == id);
        }   

        public async Task<MachineModel> GetMachine(int id)
        {
            return await _context.MachineModel.FirstOrDefaultAsync(p =>
                p.Id == id);
        }        
        public async Task<List<MachineModel>> GetMachines()
        {
            return await  _context.MachineModel.ToListAsync();            
        } 
        public async Task<PartModel> GetPart(int id)
        {
            return await _context.PartModel.FirstOrDefaultAsync(p =>
                p.Id == id);
        }        
        public async Task<List<PartModel>> GetParts()
        {
            return await  _context.PartModel.ToListAsync();            
        }  

        public async Task<MachinePartAttempt> RegisterMachinePartAttempt(MachPartAttemRegisterDto MachPartAttemRegisterDto)
        {
            MachinePartAttempt machinePartAttempt = new MachinePartAttempt();            

            var machineModel = await _context.MachineModel.FirstOrDefaultAsync(m =>
                m.Name == MachPartAttemRegisterDto.MachineModel);

            var partModel = await _context.PartModel.FirstOrDefaultAsync(m =>
                m.Name == MachPartAttemRegisterDto.PartModel);

            machinePartAttempt.InternalSequence = MachPartAttemRegisterDto.InternalSequence;
            machinePartAttempt.AvailableAttempts = partModel.Attempts;
            machinePartAttempt.DefaultAttempts = partModel.Attempts;
            machinePartAttempt.MachineModelId = machineModel.Id;
            machinePartAttempt.PartModelId = partModel.Id;
            

            await _context.MachinePartAttempt.AddAsync(machinePartAttempt);
            await _context.SaveChangesAsync();

            return machinePartAttempt;
        }
        public async Task<MachinePartAttempt> GetMachinePartAttempt(int id)
        {
            return await _context.MachinePartAttempt.FirstOrDefaultAsync(p =>
                p.Id == id);
        }
        public async Task<List<MachinePartAttempt>> GetMachinePartsAttempts()
        {
            return await  _context.MachinePartAttempt.ToListAsync();            
        }  

        public async Task<AttemptDetail> GetAttemptDetail(int id)
        {
            return await _context.AttemptDetail.FirstOrDefaultAsync(p =>
                p.Id == id);
        }
        public async Task<List<AttemptDetail>> GetAttemptDetails()
        {
            return await  _context.AttemptDetail.ToListAsync();            
        } 
               
    }
}