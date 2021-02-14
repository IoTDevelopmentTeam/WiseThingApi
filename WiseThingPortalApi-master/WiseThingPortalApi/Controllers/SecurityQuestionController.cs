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
    public class SecurityQuestionController : ControllerBase
    {
        
        private readonly ISecurityQuestionHandler _secquesHandler;
        public SecurityQuestionController(ISecurityQuestionHandler secquesHandler)
        {
            
            _secquesHandler = secquesHandler;
        }
       

       
        [HttpGet]
        [Route("SecurityQuestions")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<SecurityQuestionMasterDTO>> GetSecurityQuestions()
        {
            try
            {
                var response = await _secquesHandler.GetSecurityQuestions(); ;
                return Ok(response);
              
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet]
        [Route("UserSecurityQuestion/{email}")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<SecurityQuestionMasterDTO>> GetUserSecurityQuestions(string email)
        {
            try
            {
                var response = await _secquesHandler.GetUserSecurityQuestion(email); ;
                return Ok(response);

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }


    }
}