using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WiseThing.Data.Respository
{
    public class UserDeviceRepository : BaseRepository,IUserDeviceRepository
    {
        public UserDeviceRepository(WisethingPortalContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task AddUserDevice(UserDeviceDTO userDeviceDto)
        {
            var userDevice = _mapper.Map<Userdevice>(userDeviceDto);
            userDevice.InputDate = DateTime.Now;
            _context.Userdevices.Add(userDevice);
            await _context.SaveChangesAsync();
        }
       
    }
}
