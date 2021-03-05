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
        private const string deviceStatusAlreadyExist = "Entered device tag is already in use ";
        private const string sucessMessageDeviceStatus = "Device is in use. Ready to update First Used date ";
        private const string sucessMessageDeviceAddStatus = "Device data updated successfully ";
        private const string notUsedDeviceStatus = "Device not in used ";
        public DeviceHandler(IUserDeviceRepository userDeviceRepo, IDeviceRepository deviceRepo)
        {
             _devicerepo = deviceRepo;
        }
       public async Task<string> AddNewDevice(DeviceDTO[] devices)
       {
            string deviceTagName = string.Empty;
            for (int i = 0; i < devices.Length; i++)
            {
                devices[i].DeviceTagName = UniqueIdGenerator.GetUniqueId();
                deviceTagName = deviceTagName + devices[i].DeviceTagName + ",";
            }
            deviceTagName = deviceTagName.TrimEnd(',');
            await _devicerepo.AddDevice(devices);
            return deviceTagName;
       }
        public async Task UpdateDevice(DeviceDTO device)
        {
           await _devicerepo.UpdateDevice(device);
            
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

        public async Task<IEnumerable<DeviceDTO>> GetAdminDevices()
        {
            return await _devicerepo.GetAdminDevices();
        }

        private DeviceAssociationResult DeriveDeviceAssociationResult(int deviceId, int userId, UserDeviceAssociation userDevice, bool isSuccess)
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
            else if (!isSameUser)
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
        private DeviceStatusResult GetDeviveStatusResult(int deviceId,DateTime? firstUsed,bool? isUsed,bool isSuccess,string methodType)
        {
            if (isSuccess)
                return new DeviceStatusResult()
                {
                    IsDeviceStatusSucceded = true,
                    Message = (methodType=="GET")?sucessMessageDeviceStatus:sucessMessageDeviceAddStatus
                };
            
            if (deviceId == 0)
                return new DeviceStatusResult()
                {
                    IsDeviceStatusSucceded = false,
                    Message = invalidDeviceTag
                };
            if(firstUsed!=null)
                return new DeviceStatusResult()
                {
                    IsDeviceStatusSucceded = false,
                    Message = deviceStatusAlreadyExist
                };
            if(isUsed != true)
                return new DeviceStatusResult()
                {
                    IsDeviceStatusSucceded = false,
                    Message = notUsedDeviceStatus
                };

            return new DeviceStatusResult()
            {
                IsDeviceStatusSucceded = false,
                Message = "Unknown"
            };
        }

        public async Task<DeviceStatusResult> GetDeviceStatus(string tagName) 
        {
            
            var deviceId = await _devicerepo.IsDeviceTagExist(tagName);
            if (deviceId > 0)
            {
               var deviceStatus= await _devicerepo.GetDeviceStatus(tagName);
                bool isSuccess = false;
                if (deviceStatus.IsUsed == true && deviceStatus.FirstUse == null)
                    isSuccess = true;
                return GetDeviveStatusResult(deviceId, deviceStatus.FirstUse, deviceStatus.IsUsed, isSuccess,"GET");
            }
            else
            {
                return GetDeviveStatusResult(deviceId, null, false, false, "GET");
            }

            
        }
        public async Task<DeviceStatusResult> AddDeviceStatus(DeviceAddStatusDTO device) {
            
            
            var deviceId = await _devicerepo.IsDeviceTagExist(device.DeviceTagName);
            if (deviceId > 0)
            {
                var deviceStatus = await _devicerepo.GetDeviceStatus(device.DeviceTagName);
                bool isSuccess = false;
                if (deviceStatus.IsUsed == true && deviceStatus.FirstUse == null)
                {
                    isSuccess = true;
                    await _devicerepo.AddDeviceStatus(device);
                }

                return GetDeviveStatusResult(deviceId, deviceStatus.FirstUse, deviceStatus.IsUsed, isSuccess, "POST");

            }
            return GetDeviveStatusResult(deviceId,null,false,false, "POST");
        }
    }
}
