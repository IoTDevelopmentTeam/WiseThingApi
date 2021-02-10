using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiseThing.Data.Respository;

namespace WiseThing.Portal.Business
{ 
   public class DeviceHandler : IDeviceHandler
   {
        
        private readonly IDeviceRepository _devicerepo;
        private const string invalidDeviceTag = "Invalid device tag. Please enter correct tag name.";
        private const string deviceTagAlreadyAssociated = "Device tag already associated with the user";
        private const string deviceTagAssociatedOtherUser = "Entered device tag is associated with other user ";
        private const string sucessMessage = "Device added successfully";
        public DeviceHandler(IUserDeviceRepository userDeviceRepo, IDeviceRepository deviceRepo)
        {
             _devicerepo = deviceRepo;
        }
       public async Task<string> AddNewDevice(DeviceDTO device)
       {
            device.DeviceTagName = UniqueIdGenerator.GetUniqueId();
            await _devicerepo.AddDevice(device);
            return device.DeviceTagName;
       }
        public async Task<DeviceAssociationResult> EditDeviceWithUserAssociation(UserDeviceAssociation userDevice)
        {
            int userId = 0;
            bool isDeviceAssociationSuccess = false;
            var deviceId = await _devicerepo.IsDeviceTagExist(userDevice.TagName);
            if (deviceId>0)
            {
                userId = await _devicerepo.GetUserIdbyDeviceId(deviceId);
                if (userId==0)
                {
                    var userdeviceDto = new UserDeviceDTO()
                    {
                        UserId = userDevice.UserId,
                        DeviceId = deviceId

                    };
                   await _devicerepo.EditDevice(userdeviceDto, userDevice.DeviceName);
                   isDeviceAssociationSuccess = true;
                }
            }
            return DeriveDeviceAssociationResult(deviceId, userId, userDevice, isDeviceAssociationSuccess);
        }
        


       public async Task<IEnumerable<DeviceDTO>> GetDevicesforUser(int userId)
       {
            return await _devicerepo.GetDevicesByuserId(userId);
       }
        private DeviceAssociationResult DeriveDeviceAssociationResult(int deviceId, int userId, UserDeviceAssociation userDevice,bool isSuccess)
        {
            if (isSuccess)
                return new DeviceAssociationResult()
                {
                    IsDeviceUserAssociationSucceded = true,
                    Message = sucessMessage
                };
            bool isSameUser = (userId > 0 && userId == userDevice.UserId) ? true : false;
            if (deviceId == 0)
                return new DeviceAssociationResult()
                {
                    IsDeviceUserAssociationSucceded = false,
                    Message = invalidDeviceTag
                };
            else if(!isSameUser)
                return new DeviceAssociationResult()
                {
                    IsDeviceUserAssociationSucceded = false,
                    Message = deviceTagAssociatedOtherUser
                };
            else
                return new DeviceAssociationResult()
                {
                    IsDeviceUserAssociationSucceded = false,
                    Message = deviceTagAlreadyAssociated
                };

        }
    }
}
