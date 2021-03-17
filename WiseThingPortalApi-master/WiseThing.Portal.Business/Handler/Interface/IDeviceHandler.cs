using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiseThing.Data.Respository;

namespace WiseThing.Portal.Business
{
    public interface IDeviceHandler
    {
        Task<DeviceAssociationResult> EditDeviceWithUserAssociation(UserDeviceAssociation userDevice);
        Task<string> AddNewDevice(DeviceDTO[] devices);
        Task UpdateDevice(DeviceDTO device);
        Task<IEnumerable<DeviceDTO>> GetDevicesforUser(int userId);
        Task<IEnumerable<DeviceDTO>> GetAdminDevices();

        Task<DeviceStatusResult> GetDeviceStatus(string tagName);
        Task<DeviceStatusResult> AddDeviceStatus(DeviceAddStatusDTO device);

        Task<string> GetDeviceTagname(int id);

    }
}
