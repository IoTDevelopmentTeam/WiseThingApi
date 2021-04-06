using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiseThing.Data.Respository;
using WiseThing.Portal.Business;
using System.Net.Http;
using Newtonsoft.Json;

namespace WiseThingPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceHandler _deviceHandler;
        public DeviceController(IDeviceHandler deviceHandler)
        {
            _deviceHandler = deviceHandler;
        }
        

        
        [HttpGet]
        [Route("DeviceList/{userId}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<IEnumerable<DeviceDTO>>> GetListOfDevicesForUser(int userId)
        {
            try
            {
                var response= await _deviceHandler.GetDevicesforUser(userId);
                if (response != null && response.Count() > 0)
                    return Ok(response);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("DeviceListAdmin")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<IEnumerable<DeviceDTO>>> GetAdminListOfDevices()
        {
            try
            {
                var response = await _deviceHandler.GetAdminDevices();
                if (response != null && response.Count() > 0)
                    return Ok(response);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddDevice")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<string>> AddNewDevice(DeviceDTO[] devices)
        {
            try
            {
                var deviceTag= await _deviceHandler.AddNewDevice(devices);
                return Ok(deviceTag);
                
            }
            catch(Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateDevice")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult> UpdateDevice(DeviceDTO device)
        {
            try
            {
                await _deviceHandler.UpdateDevice(device);
                return Ok();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UserDeviceAssociation")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<DeviceAssociationResult>> AddDeviceToUser([FromBody] UserDeviceAssociation userDevice)
        {
            try
            {
                if (userDevice != null && userDevice.UserId!=0 && !string.IsNullOrEmpty(userDevice.TagName) && !string.IsNullOrEmpty(userDevice.DeviceName))
                    return Ok(await _deviceHandler.EditDeviceWithUserAssociation(userDevice));
                else
                    return BadRequest();
            }
            catch(Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("DeviceData/{id}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<DashboardModel1[]>> GetAsync(int id)
        {
            try { 
            string tagName = string.Empty;
            tagName = await _deviceHandler.GetDeviceTagname(id);
            HttpClient client = new HttpClient();
                      
            HttpResponseMessage response = await client.GetAsync("https://api.wisethingz.com/Dev/thingsapi?thingname="+tagName+"&count=20");
            DashboardModel1[] responseBody = JsonConvert.DeserializeObject<DashboardModel1[]>(await response.Content.ReadAsStringAsync());
           
            return responseBody;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
}

        [HttpGet]
        [Route("GetDeviceStatus/{tagname}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<DeviceStatusDTO>> GetDeviceStatus(string tagname)
        {
            try
            {
                var deviceStatus = await _deviceHandler.GetDeviceStatus(tagname);
                if (deviceStatus != null)
                    return Ok(deviceStatus);
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddDeviceStatus")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult> AddDeviceStatus(DeviceAddStatusDTO deviceStatusDto)
        {
            try
            {
                await _deviceHandler.AddDeviceStatus(deviceStatusDto);
                return Ok();

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Route("EditDeviceLabelName")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult> EditDeviceLabelName(DeviceDTO device)
        {
            try
            {
                await _deviceHandler.EditDeviceLabelName(device);
                return Ok();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
