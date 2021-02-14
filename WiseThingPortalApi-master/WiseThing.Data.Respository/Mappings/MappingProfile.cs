using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace WiseThing.Data.Respository
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<Device, DeviceDTO>();
            CreateMap<DeviceDTO, Device>();
            CreateMap<Userdevice, UserDeviceDTO>();
            CreateMap<UserDeviceDTO, Userdevice>();
            CreateMap<PaneDetail, PaneDetailsDTO>();
            CreateMap<PaneDetailsDTO, PaneDetail>();
            CreateMap<ConfigDetail, ConfigDetailsDTO>();
            CreateMap<ConfigDetailsDTO, ConfigDetail>();
            CreateMap<SecurityQuestionMaster, SecurityQuestionMasterDTO>();
            CreateMap<SecurityQuestionMasterDTO, SecurityQuestionMaster>();
            CreateMap<UserSecurityQuestion, UserSecurityQuestionDTO>();
            CreateMap<UserSecurityQuestionDTO, UserSecurityQuestion>();
        }
    }
}
