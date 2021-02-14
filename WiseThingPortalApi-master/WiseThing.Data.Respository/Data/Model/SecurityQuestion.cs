using System;
using System.Collections.Generic;

#nullable disable

namespace WiseThing.Data.Respository
{
    internal partial class SecurityQuestionMaster
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }

    internal partial class UserSecurityQuestion
    {
        public int SecurityQuesId { get; set; }
        public string Question { get; set; }
        public string SecurityQuesAns { get; set; }
    }
}
