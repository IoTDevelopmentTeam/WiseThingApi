using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiseThing.Data.Respository;
using WiseThing.Portal.Business;



namespace WiseThingPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserHandler _userHandler;
        public UserController(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }
        
        
        [HttpGet]
        [Route("GetLoginUserDetails/{userName}/{passWord}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<UserDTO>> GetLoginUserDetails(string userName, string passWord)
        {
            try
            { 
                if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(userName))
                {
                    var response = await _userHandler.GetUserByLogin(userName, passWord);
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


        [HttpGet]
        [Route("GetUserById/{userid}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<UserDTO>> GetUserById(int userid)
        {
            try
            {
                if (userid != 0)
                {
                    var response = await _userHandler.GetUserById(userid); ;
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
        [Route("UserNameExist/{userName}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<bool>> CheckUserNameAvialable(string userName)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    var response = await _userHandler.IsUserNameAlreadyExsist(userName); ;
                    return Ok(response);
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
        [Route("AddEditUser")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> AddEditUser([FromBody] UserDTO user)
        {
            try
            {

                if (user != null)
                {
                    await _userHandler.AddEditUser(user);
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

        
    }
}