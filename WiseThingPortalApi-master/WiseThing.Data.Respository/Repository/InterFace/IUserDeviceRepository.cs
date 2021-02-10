using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WiseThing.Data.Respository
{
    public interface IUserDeviceRepository
    {
        Task AddUserDevice(UserDeviceDTO userDeviceDto);
        
    }
}
