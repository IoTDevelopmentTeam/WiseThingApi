using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WiseThing.Data.Respository
{
    public class SecurityQuestionRepository : BaseRepository, ISecurityQuestionRepository
    {
        //private readonly WisethingPortalContext _context;
        //private readonly IMapper _mapper;
        public SecurityQuestionRepository(WisethingPortalContext context, IMapper mapper ):base(context, mapper)
        {
            //_context = context;
            //_mapper = mapper;
        }
        

        public async Task<UserSecurityQuestionDTO> GetUserSecurityQuestion(string email)
        {
            

            var secQues = await (from sq in _context.SecurityQuestion
                                 join u in _context.Users
                                 on sq.QuestionId equals u.SecurityQuesId
                                 where u.Email == email
                                 select new { u.SecurityQuesId, sq.Question, u.SecurityQuesAns }).ToListAsync();
            return _mapper.Map<UserSecurityQuestionDTO>(secQues);

            

        }


        public async Task<IEnumerable<SecurityQuestionMasterDTO>> GetSecurityQuestion()
        {
            List<SecurityQuestionMasterDTO> quesList = new List<SecurityQuestionMasterDTO>();

            
            var questions = await _context.SecurityQuestion.OrderBy(e => e.QuestionId).ToListAsync();
            questions.ForEach(x =>
            {
                var dto = _mapper.Map<SecurityQuestionMasterDTO>(x);
                quesList.Add(dto);
            });
            return quesList;

        }

    }
}
