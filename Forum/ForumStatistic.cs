using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum
{
    /// <summary>
    /// Represents a forum statistic collector that gathers and provides statistics about a forum.
    /// </summary>
    public class ForumStatistic
    {
        private Forum Forum;
        private int _totalQuestions;
        private int _totalAnswer;
        private int _totalUnansweredQuestions;
        private int _totalQuestionsWithAnswers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForumStatistic"/> class with the specified forum.
        /// </summary>
        /// <param name="forum">The forum to collect statistics from.</param>
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

        /// <summary>
        /// Gets the total number of questions in the forum.
        /// </summary>
        public int TotalQuestions => _totalQuestions;

        /// <summary>
        /// Gets the total number of answers in the forum.
        /// </summary>
        public int TotalAnswer => _totalAnswer;

        /// <summary>
        /// Gets the total number of unanswered questions in the forum.
        /// </summary>
        public int TotalUnansweredQuestions => _totalUnansweredQuestions;

        /// <summary>
        /// Gets the total number of questions with at least one answer in the forum.
        /// </summary>
        public int TotalQuestionsWithAnswer => _totalQuestionsWithAnswers;

        /// <summary>
        /// Gets the average number of answers per question in the forum.
        /// </summary>
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

        /// <summary>
        /// Displays the collected forum statistics on the console.
        /// </summary>
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
