using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    internal class Question
    {
        public string Text { get; set; }

        public QuestionType Type { get; set; }

        public List<Answer> PossibleAnswers { get; set; }

        public List<KeyWords> Words { get; set; }
    }

    public enum QuestionType : int
    {
        MultipleChoice,
        TrueFalse,
        Checkbox,
        FreeResponse,
    }

    public class Answer
    {
        public string Text { get; set; }

        public bool IsCorrect { get; set; }
    }

    public class KeyWords
    {
        public string Text { get; set; }
    }
}
