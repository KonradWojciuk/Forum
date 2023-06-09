using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum
{
    public class ForumStatistic
    {
        private Forum Forum;
        private int TotalQuestions;
        private int TotalAnswer;
        private int TotalUnansweredQuestions;
        private int TotalQuestionsWithAnswers;

        public ForumStatistic(Forum forum)
        {
            Forum = forum;
            TotalQuestions = 0;
            TotalAnswer = 0;
            TotalUnansweredQuestions = 0;
            TotalQuestionsWithAnswers = 0;

            
        }

       
    }
}
