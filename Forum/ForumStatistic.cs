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
        private int _totalQuestions;
        private int _totalAnswer;
        private int _totalUnansweredQuestions;
        private int _totalQuestionsWithAnswers;

        public ForumStatistic(Forum forum)
        {
            Forum = forum;
            _totalQuestions = 0;
            _totalAnswer = 0;
            _totalUnansweredQuestions = 0;
            _totalQuestionsWithAnswers = 0;

            Forum.QuestionAdded += Forum_QuestionAdded;
            Forum.AnswerAdded += Forum_AnswerAdded;
        }   

        public int TotalQuestions => _totalQuestions;
        public int TotalAnswer => _totalAnswer;
        public int TotalUnansweredQuestions => _totalUnansweredQuestions;
        public int TotalQuestionsWithAnswer => _totalQuestionsWithAnswers;
        public double AverageAnswersPerQuestion => _totalQuestions > 0 ? (double)TotalAnswer / TotalQuestions : 0; 

        private void Forum_AnswerAdded(object sender, AnswerEventArgs e)
        {
            _totalAnswer++;
            _totalUnansweredQuestions--;

            if (_totalUnansweredQuestions < 0)
                _totalUnansweredQuestions = 0;

            if (!HaveAnswers(e.Answer.QuestionId))
                _totalQuestionsWithAnswers++;
        }

        private void Forum_QuestionAdded(object sender, QuestionEventArgs e)
        {
            _totalQuestions++;
            _totalUnansweredQuestions++;
        }

        private bool HaveAnswers(string questionId) => Forum.GetAnswerByQuestion(questionId).Count > 0;

        public void DisplayStatistic()
        {
            Console.WriteLine();
            Console.WriteLine("Forum statistic:");
            Console.WriteLine($"Number of questions: {TotalQuestions}");
            Console.WriteLine($"Number of answers: {TotalAnswer}");
            Console.WriteLine($"Average answers per question: {AverageAnswersPerQuestion}");
            Console.WriteLine($"Number of question without answers: {TotalUnansweredQuestions}");
            Console.WriteLine($"Questions with minimum one answer: {TotalQuestionsWithAnswer}");
            Console.WriteLine();
        }
    }
}
