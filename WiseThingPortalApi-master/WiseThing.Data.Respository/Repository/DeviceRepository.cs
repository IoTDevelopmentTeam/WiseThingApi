using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WiseThing.Data.Respository
{
    public class DeviceRepository : BaseRepository, IDeviceRepository
    {
        public DeviceRepository(WisethingPortalContext context, IMapper mapper) : base(context, mapper)
        {

        }
        public async Task<DeviceDTO[]> AddDevice(DeviceDTO[] deviceceDto)
        {
            for (int i = 0; i < deviceceDto.Length; i++)
            {
                var device = _mapper.Map<Device>(deviceceDto[i]);
                device.InputDate = DateTime.Now;
                _context.Devices.Add(device);
            }
            await _context.SaveChangesAsync();
            return deviceceDto;
        }
        public async Task UpdateDevice(DeviceDTO deviceDto)
        {
            var device = await _context.Devices.SingleAsync(x => x.DeviceTagName == deviceDto.DeviceTagName);
            device.IsUsed = deviceDto.IsUsed;
            await _context.SaveChangesAsync();
    
        }
        public async Task EditDevice(UserDeviceDTO dto, string deviceName)
        {
            var dev = await _context.Devices.SingleOrDefaultAsync(x => x.DeviceId == dto.DeviceId);
            dev.DeviceName = deviceName;
            var userDevice = _mapper.Map<Userdevice>(dto);
            userDevice.InputDate = DateTime.Now;
            _context.Userdevices.Add(userDevice);
            await _context.SaveChangesAsync();


        }

        public async Task<IEnumerable<DeviceDTO>> GetDevicesByuserId(int userId)
        {
            List<DeviceDTO> deviceList = new List<DeviceDTO>();
            var devices = await _context.Userdevices.Where(x => x.UserId == userId).Include(y => y.Device).ToListAsync();
            devices.ForEach(x =>
            {
                var dto= _mapper.Map<DeviceDTO>(x.Device);
                deviceList.Add(dto);
            });
            return deviceList;
        }

        public async Task<IEnumerable<DeviceDTO>> GetAdminDevices()
        {
            List<DeviceDTO> deviceList = new List<DeviceDTO>();
            var devices = await _context.Devices.OrderByDescending(e => e.InputDate).ToListAsync();
            devices.ForEach(x =>
            {
                var dto = _mapper.Map<DeviceDTO>(x);
                deviceList.Add(dto);
            });
            return deviceList;
        }
        public async Task<int> IsDeviceTagExist(string deviceTagName)
        {
            var result= await _context.Devices.SingleOrDefaultAsync(x => x.DeviceTagName == deviceTagName);
            return result!=null?result.DeviceId:0;
        }

       
        public async Task<int> GetUserIdbyDeviceId(int deviceId)
        {
            var userIdlist = await (from d in _context.Devices
                                    join ud in _context.Userdevices
                                    on d.DeviceId equals ud.DeviceId
                                    where d.DeviceId == deviceId
                                   select ud.UserId).ToListAsync();
            return userIdlist!=null && userIdlist.Count>0 ? userIdlist.First():0;
        }
        public async Task AddDeviceStatus(DeviceAddStatusDTO deviceStatusDto)
        {
            var deviceStatus = await _context.Devices.SingleAsync(x => x.DeviceTagName == deviceStatusDto.DeviceTagName);
            DateTime FirstUsed = deviceStatusDto.FirstUse;
            deviceStatus.FirstUse = FirstUsed;
            deviceStatus.ExpDate = FirstUsed.AddMonths(6);
            await _context.SaveChangesAsync();
           
        }

        public async Task<DeviceStatusDTO> GetDeviceStatus(string tagName)
        {
            string retString = string.Empty;
            var deviceStausDTO = await _context.Devices.SingleOrDefaultAsync(x => x.DeviceTagName == tagName);
            var deviceStaus = _mapper.Map<DeviceStatusDTO>(deviceStausDTO);
            return deviceStaus;

        }


        public async Task<string> GetDeviceTagName(int id)
        {
            string retString = string.Empty;
            var deviceDTO = await _context.Devices.SingleAsync(x => x.DeviceId == id);
            var device = _mapper.Map<DeviceStatusDTO>(deviceDTO);
            retString = device.DeviceTagName;
            return retString;

        }
        public async Task EditDeviceLabelName(DeviceDTO deviceDto)
        {
            var device = await _context.Devices.SingleAsync(x => x.DeviceTagName == deviceDto.DeviceTagName);
            device.DeviceName = deviceDto.DeviceName;
            await _context.SaveChangesAsync();

        }

    }
}
