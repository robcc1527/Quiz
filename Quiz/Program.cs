// See https://aka.ms/new-console-template for more information
using System.Collections;
namespace Quiz;
public class Program
{
    public static void Main(string[] args)
    {


        Quiz newquiz = new Quiz();

        Question question = new Question();
        question.Text = "Where is Carmen Sandiego?";
        question.Type = QuestionType.MultipleChoice;
        question.PossibleAnswers = new List<Answer>(){
            new Answer {Text = "Denver", IsCorrect = false },
        };
        newquiz.AddQuestion();
        newquiz.DisplayQuiz();


        

       

        
    }
}