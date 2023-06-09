namespace Forum
{
    public class Program
    {
        static void Main(string[] args)
        {
            Forum forum = new Forum();
            ForumStatistic forumStatistic = new ForumStatistic(forum);

            string user1 = "user1";
            string user2 = "user2";
            string user3 = "user3";

            forum.AddQuestion(user1, "Question1");
            forum.AddQuestion(user2, "Question2");
            forum.AddQuestion(user1, "Question3");
            
            forum.AddAnswer("Q1", user3, "Answer1 to Question1");
            forum.AddAnswer("Q3", user1, "Answer1 to Question3");
            forum.AddAnswer("Q3", user2, "Answer2 to Question3");
            forum.AddAnswer("Q3", user2, "Answer3 to Question3");

            forumStatistic.DisplayStatistic();

            forum.DisplayQuestionWithAnswer("Q3");
            forum.GetQuestionsByUser(user1);
            forum.GetQuestionsByUser(user2);
            forum.GetQuestions();
            forum.DisplayQuestionsWithAnswersByUser(user3);
            forum.DisplayQuestionsWithAnswer();
        }
    }
}