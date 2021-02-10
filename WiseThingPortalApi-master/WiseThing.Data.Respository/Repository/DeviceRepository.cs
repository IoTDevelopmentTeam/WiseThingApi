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
        public async Task<DeviceDTO> AddDevice(DeviceDTO deviceceDto)
        {
            var device = _mapper.Map<Device>(deviceceDto);
            device.InputDate = DateTime.Now;
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return deviceceDto;
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
    }
}
