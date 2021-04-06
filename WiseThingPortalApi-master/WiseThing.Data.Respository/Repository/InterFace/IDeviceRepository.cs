using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WiseThing.Data.Respository
{
    public interface IDeviceRepository
    {
        Task<DeviceDTO[]> AddDevice(DeviceDTO[] deviceceDto);
        Task UpdateDevice(DeviceDTO deviceDto);
        Task EditDevice(UserDeviceDTO dto, string deviceName);
        Task<int> IsDeviceTagExist(string deviceTagName);
        Task<int> GetUserIdbyDeviceId(int deviceId);
        Task<IEnumerable<DeviceDTO>> GetDevicesByuserId(int userId);

        Task<IEnumerable<DeviceDTO>> GetAdminDevices();

        Task AddDeviceStatus(DeviceAddStatusDTO deviceStatusDto);
        Task<DeviceStatusDTO> GetDeviceStatus(string tagName);

        Task<string> GetDeviceTagName(int id);

        Task EditDeviceLabelName(DeviceDTO deviceDto);
    }
}
