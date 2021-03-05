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
        [Route("GetLoginUserDetails/{email}/{passWord}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<UserDTO>> GetLoginUserDetails(string email, string passWord)
        {
            try
            { 
                if (!string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(email))
                {
                    var response = await _userHandler.GetUserByLogin(email, passWord);
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
        [Route("EmailExist/{email}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<bool>> CheckEmailAvialable(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var response = await _userHandler.IsEmailAlreadyExsist(email); ;
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
        [HttpPost]
        [Route("ResetPassword")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPassword)
        {
            try
            {

                if (resetPassword != null)
                {
                    await _userHandler.ResetPassword(resetPassword);
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