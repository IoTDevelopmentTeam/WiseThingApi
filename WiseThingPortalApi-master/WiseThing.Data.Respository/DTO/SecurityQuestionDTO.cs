using System;
using System.Collections.Generic;
using System.Text;

namespace WiseThing.Data.Respository
{
    public class SecurityQuestionMasterDTO
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }

    public class UserSecurityQuestionDTO
    {
        public int SecurityQuesId { get; set; }
        public string Question { get; set; }
        public string SecurityQuesAns { get; set; }
    }
}
