using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WiseThing.Data.Respository
{
    public interface ISecurityQuestionRepository
    {
        Task<IEnumerable<SecurityQuestionMasterDTO>> GetSecurityQuestion();

        Task<UserSecurityQuestionDTO> GetUserSecurityQuestion(string email);


    }
}
