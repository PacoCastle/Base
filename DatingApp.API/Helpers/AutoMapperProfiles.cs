using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.Core.Models;

/*
The main AutoMapperProfiles class
Contains all mappings necesary for use in Controllers
*/
/// <summary>
/// The AutoMapperProfiles class.
/// Contains all validations rules for Update  MachinePartAttepmt
/// </summary>    
/// 
namespace DatingApp.API.Helpers
{
    
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.MapFrom(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.SenderPhotoUrl, opt => opt
                    .MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(m => m.RecipientPhotoUrl, opt => opt
                    .MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
            CreateMap<PlcForCreationDto, Plc>();
            CreateMap<Plc, PlcForReturnDto>();
            CreateMap<ProductForRegisterDto, Product>();
            CreateMap<Product, ProductForReturnDto>();
            CreateMap<ProductForUpdateDto, Product>();
            CreateMap<PartForRegisterDto,PartModel>();
            CreateMap<PartModel, PartForReturnDto>();
            CreateMap<PartForUpdateDto, PartModel>();
            CreateMap<MachPartAttemCreateDto,MachinePartAttempt>();
            CreateMap<MachinePartAttempt, MachPartAttemReturnDto>();
            CreateMap<MachPartAttemUpdateDto, MachinePartAttempt>();       
            CreateMap<AttemptDetailRegisterDto,AttemptDetail>();
            CreateMap<AttemptDetail, AttemptDetailReturnDto>(); 
            CreateMap<MachineRegisterDto,MachineModel>();
            CreateMap<MachineModel, MachineReturnDto>();
            CreateMap<MachineUpdateDto, MachineModel>();
            CreateMap<DeviceConfigurationForUpdateDto, DeviceConfiguration>();
            
        }
    }
}