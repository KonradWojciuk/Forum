using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum
{
    /// <summary>
    /// Represents forum questions
    /// </summary>
    public class Question
    {
        public string Id { get; }
        public string Author { get; }
        public string Content { get; }

        /// <summary>
        /// Initialize new instance of <see cref="Question"/> class.
        /// </summary>
        /// <param name="id">Id question</param>
        /// <param name="author">Author of the question</param>
        /// <param name="content">Content of the question</param>
        public Question(string id, string author, string content)
        {
            Id = id;
            Author = author;
            Content = content;
        }
    }

    /// <summary>
    /// Represents forum answers
    /// </summary>
    public class Answer
    {
        public string Id { get; }
        public string QuestionId { get; }
        public string Author { get; }
        public string Content { get; }

        /// <summary>
        /// Initialize new instance of <see cref="Answer"/>.
        /// </summary>
        /// <param name="id">Id answer</param>
        /// <param name="questionId">Id question</param>
        /// <param name="author">Author of the answer</param>
        /// <param name="content">Content of the answer</param>
        public Answer(string id, string questionId, string author, string content)
        {
            Id = id;
            QuestionId = questionId;
            Author = author;
            Content = content;
        }
    }

    /// <summary>
    /// Represents event arguments to add question.
    /// </summary>
    public class QuestionEventArgs
    {
        /// <summary>
        /// Add question
        /// </summary>
        public Question Question { get; }

        /// <summary>
        /// Initializes a new instance of the QuestionEventArgs class.
        /// </summary>
        /// <param name="question">Added question</param>
        public QuestionEventArgs(Question question)
        {
            Question = question;
        }
    }

    /// <summary>
    /// Represents the arguments of the reply add event.
    /// </summary>
    public class AnswerEventArgs
    {
        /// <summary>
        /// Add answer
        /// </summary>
        public Answer Answer { get; }

        /// <summary>
        /// Initializes a new instance of the AnswerEventArgs class.
        /// </summary>
        /// <param name="answer">Added answer</param>
        public AnswerEventArgs(Answer answer)
        {
            Answer = answer;
        }
    }

    /// <summary>
    /// Delegate handling the question addition event.
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Arguments of the event to add the question.</param>
    public delegate void QuestionAddedEventHandler(object sender, QuestionEventArgs e);

    /// <summary>
    /// Delegate handling the answer addition event.
    /// </summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Arguments of the event to add the answers.</param>
    public delegate void AnswerAddedEventHandler(object sender, AnswerEventArgs e);

    /// <summary>
    /// Represents a forum.
    /// </summary>
    public class Forum
    {
        private List<Question> Questions;

        private List<Answer> Answers;

        private int _idQuestionCounter;

        private int _idAnswerCounter;

        public event QuestionAddedEventHandler QuestionAdded;

        public event AnswerAddedEventHandler AnswerAdded;

        /// <summary>
        /// Initializes a new instance of the Forum class.
        /// </summary>
        public Forum()
        {
            Questions = new List<Question>();
            Answers = new List<Answer>();
            _idQuestionCounter = 0;
            _idAnswerCounter = 0;
        }

        /// <summary>
        /// Adds a question to the forum.
        /// </summary>
        /// <param name="author">The author of the question.</param>
        /// <param name="content">The content of the question.</param>
        public void AddQuestion(string author, string content)
        {
            string questioId = GenerateIdForQuestion();
            Question question = new Question(questioId, author, content);
            Questions.Add(question);
            OnQuestionAdded(question);
        }

        /// <summary>
        /// Adds an answer to a question in the forum.
        /// </summary>
        /// <param name="questionId">The ID of the question.</param>
        /// <param name="author">The author of the answer.</param>
        /// <param name="content">The content of the answer.</param>
        public void AddAnswer(string questionId, string author, string content)
        {
            string answerId = GenerateIdForAnswer();
            Answer answer = new Answer(answerId, questionId, author, content);
            Answers.Add(answer);
            OnAnswerAdded(answer);
        }

        /// <summary>
        /// Displays a question with its answers in the forum.
        /// </summary>
        /// <param name="questionId">The ID of the question.</param>
        public void DisplayQuestionWithAnswer(string questionId)
        {
            Question question = GetQuestionById(questionId);

            if (question is null)
            {
                Console.WriteLine($"Question {question.Id}: {question.Content}");
                List<Answer> answers = GetAnswerByQuestion(question.Id);
                if (answers.Count > 0)
                {
                    Console.WriteLine("Answers:");
                    foreach (Answer answer in answers)
                        Console.WriteLine($" - {answer.Content} (reply {answer.Id} from user {answer.Author})");
                }
                else
                    Console.WriteLine("The question with the given ID does not exist");
            }
        }

        /// <summary>
        /// Displays all questions with their answers in the forum.
        /// </summary>
        public void DisplayQuestionsWithAnswer()
        {
            List<Question> questions = GetQuestions();

            foreach (Question question in questions)
            {
                Console.WriteLine($"Question {question.Id}: {question.Content}");
                List<Answer> answers = GetAnswerByQuestion(question.Id);

                if (answers.Count > 0)
                {
                    Console.WriteLine("Answer");
                    foreach (Answer answer in answers)
                        Console.WriteLine($" - {answer.Content} (reply {answer.Id} from user {answer.Author})");
                }
                else
                    Console.WriteLine("No answers");

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays questions with their answers posted by a specific user in the forum.
        /// </summary>
        /// <param name="user">The user whose questions and answers should be displayed.</param>
        public void DisplayQuestionsWithAnswersByUser(string user)
        {
            List<Question> questions = GetQuestionsByUser(user);
            foreach (Question question in questions)
            {
                Console.WriteLine($"Question {question.Id}: {question.Content}");
                List<Answer> answers = GetAnswerByQuestion(question.Id);

                if (answers.Count > 0)
                {
                    Console.WriteLine("Answer");
                    foreach (Answer answer in answers)
                        Console.WriteLine($" - {answer.Content} (reply {answer.Id} from user {answer.Author})");
                }
                else
                    Console.WriteLine("No answers");

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Gets all questions in the forum.
        /// </summary>
        /// <returns>A list of questions.</returns>
        public List<Question> GetQuestions() => Questions;

        /// <summary>
        /// Gets questions posted by a specific user in the forum.
        /// </summary>
        /// <param name="user">The user whose questions should be retrieved.</param>
        /// <returns>A list of questions posted by the user.</returns>
        public List<Question> GetQuestionsByUser(string user) => Questions.FindAll(question => question.Author == user);

        /// <summary>
        /// Gets all answers for a specific question in the forum.
        /// </summary>
        /// <param name="questionId">The ID of the question.</param>
        /// <returns>A list of answers for the question.</returns>
        public List<Answer> GetAnswerByQuestion(string questionId)
        {
            return Answers.FindAll(answer => answer.QuestionId == questionId);
        }

        /// <summary>
        /// Gets a question by its ID.
        /// </summary>
        /// <param name="questionId">The ID of the question.</param>
        /// <returns>The question with the specified ID, or null if not found.</returns>
        public Question GetQuestionById(string questionId) => Questions.FirstOrDefault(question => question.Id == questionId);

        /// <summary>
        /// Raises the event when a question is added to the forum.
        /// </summary>
        /// <param name="question">The question that was added.</param>
        protected virtual void OnQuestionAdded(Question question) => QuestionAdded?.Invoke(this, new QuestionEventArgs(question));

        /// <summary>
        /// Raises the event when an answer is added to a question in the forum.
        /// </summary>
        /// <param name="answer">The answer that was added.</param>
        protected virtual void OnAnswerAdded(Answer answer) => AnswerAdded?.Invoke(this, new AnswerEventArgs(answer));

        /// <summary>
        /// Generates a new unique ID for questions and answers.
        /// </summary>
        /// <returns>A new unique ID.</returns
        private string GenerateIdForQuestion()
        {
            _idQuestionCounter++;
            return "Q" + _idQuestionCounter.ToString();
        }

        private string GenerateIdForAnswer()
        {
            _idAnswerCounter++;
            return "A" + _idAnswerCounter.ToString();
        }
    }
}
