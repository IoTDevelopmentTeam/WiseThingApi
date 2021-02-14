using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiseThing.Data.Respository;

namespace WiseThing.Portal.Business
{
    public interface ISecurityQuestionHandler
    {
        
        Task<UserSecurityQuestionDTO> GetUserSecurityQuestion(string email);
        
        Task<IEnumerable<SecurityQuestionMasterDTO>> GetSecurityQuestions();

    }
}
