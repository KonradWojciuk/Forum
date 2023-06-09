using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum
{
    public class Question
    {
        public string Id { get; }
        public string Author { get; }
        public string Content { get; }

        public Question(string id, string author, string content)
        {
            Id = id;
            Author = author;
            Content = content;
        }
    }

    public class Answer
    {
        public string Id { get; }
        public string QuestionId { get; }
        public string Author { get; }
        public string Content { get; }

        public Answer(string id, string questionId, string author, string content)
        {
            Id = id;
            QuestionId = questionId;
            Author = author;
            Content = content;
        }
    }

    public class QuestionEventArgs
    {
        public Question Question { get; }

        public QuestionEventArgs(Question question)
        {
            Question = question;
        }
    }

    public class AnswerEventArgs
    {
        public Answer Answer { get; }

        public AnswerEventArgs(Answer answer)
        {
            Answer = answer;
        }
    }

    public delegate void QuestionAddedEventHandler(object sender, QuestionEventArgs e);

    public delegate void AnswerAddedEventHandler(object sender, AnswerEventArgs e);

    public class Forum
    {
        private List<Question> Questions;

        private List<Answer> Answers;

        public event QuestionAddedEventHandler QuestionAdded;

        public event AnswerAddedEventHandler AnswerAdded;

        public Forum()
        {
            Questions = new List<Question>();
            Answers = new List<Answer>();
        }

        public void AddQuestion(string author, string content)
        {
            string questioId = GenerateId();
            Question question = new Question(questioId, author, content);
            Questions.Add(question);
            OnQuestionAdded(question);
        }

        public void AddAnswer(string questionId, string author, string content)
        {
            string answerId = GenerateId();
            Answer answer = new Answer(answerId, questionId, author, content);
            Answers.Add(answer);
            OnAnswerAdded(answer);
        }

        public List<Question> GetQuestions() => Questions;

        public List<Question> GetQuestionsByUser(string user) => Questions.FindAll(question => question.Author == user);

        public List<Answer> GetAnswerByQuestion(string questionId)
        {
            return Answers.FindAll(answer => answer.QuestionId == questionId);
        }

        protected virtual void OnQuestionAdded(Question question) => QuestionAdded?.Invoke(this, new QuestionEventArgs(question));

        protected virtual void OnAnswerAdded(Answer answer) => AnswerAdded?.Invoke(this, new AnswerEventArgs(answer));

        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
