using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiseThing.Data.Respository;

namespace WiseThing.Portal.Business
{
    public class SecurityQuestionHandler : ISecurityQuestionHandler
    {
        private readonly ISecurityQuestionRepository _secuRepo;
        public SecurityQuestionHandler(ISecurityQuestionRepository secuRepo)
        {
            _secuRepo = secuRepo;
        }
        public async Task<UserSecurityQuestionDTO> GetUserSecurityQuestion(string email)
        {
            var usersecurity = await _secuRepo.GetUserSecurityQuestion(email);
            
            return usersecurity;
        }

        public async Task<IEnumerable<SecurityQuestionMasterDTO>> GetSecurityQuestions()
        {
            var secQuestions = await _secuRepo.GetSecurityQuestion();

            return secQuestions;
        }

       

    }
}
