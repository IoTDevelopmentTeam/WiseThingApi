using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WiseThing.Data.Respository;
using WiseThing.Portal.Business;



namespace WiseThingPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaneController : ControllerBase
    {
        private readonly IPaneHandler _paneHandler;
        private readonly IDeviceHandler _deviceHandler;
        public PaneController(IPaneHandler paneHandler,IDeviceHandler deviceHandler)
        {
            _paneHandler = paneHandler;
            _deviceHandler = deviceHandler;
        }
       


        [HttpGet]
        [Route("GetPaneDetails/{id}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<PaneDetailsDTO[]>> GetPaneDetails(int id)
        {
            try
            { 
                if (id>0)
                {
                    var response = await _paneHandler.GetPaneDetailsById(id);
                    if (response != null)
                        return Ok(response);
                   else
                        return NoContent();
                }
                else
                {
                    return BadRequest();
                }
                   
            }
            catch(Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        


        [HttpPost]
        [Route("AddPaneDetails")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> AddPaneDetails([FromBody] PaneDetailsDTO paneDetails)
        {
            try
            {

                if (paneDetails != null)
                {
                    var paneId=await _paneHandler.AddPaneDetails(paneDetails);
                    return Ok(paneId);
                }
                else
                {

                    return BadRequest();

                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }


        [HttpGet]
        [Route("GetConfigDetails/{id}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<ConfigDetailsDTO[]>> GetConfigDetails(int id)
        {
            try
            {
                if (id > 0)
                {
                    var response = await _paneHandler.GetConfigDetailsById(id);
                    if (response != null)
                        return Ok(response);
                    else
                        return NoContent();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpPost]
        [Route("AddConfigDetails")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> AddConfigDetails([FromBody] ConfigDetailsDTO[] configDetails)
        {
            try
            {

                if (configDetails != null)
                {
                    await _paneHandler.AddConfigDetails(configDetails);
                    return Ok();
                }
                else
                {

                    return BadRequest();

                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet]
        [Route("GetPaneDetailsByUser/{id}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<ConfigDetailsDTO[]>> PaneDetailsByUser(int id)
        {
            try
            {
                if (id > 0)
                {
                    var response = await _paneHandler.PaneDetailsByUser(id);
                    
                    if (response != null)
                        return Ok(response);
                    else
                        return NoContent();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("AttributeName/{deviceName}")]
        [EnableCors("MyPolicy")]
        public async Task<List<string>> GetAsync(string deviceName)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.wisethingz.com/Dev/thingsapi?thingname="+ deviceName + "&count=20");
            DashboardModel1[] responseBody = JsonConvert.DeserializeObject<DashboardModel1[]>(await response.Content.ReadAsStringAsync());
            List<string> AttrName = new List<string>();
            if (responseBody.Length > 0)
            {
                AttrName = new List<string>(responseBody[0].content.Count);
                for (int i = 0; i < responseBody[0].content.Count; i++)
                {
                    AttrName.Add((responseBody[0].content.Keys).ElementAt(i));
                }
            }

            return AttrName;
        }

    }
}
