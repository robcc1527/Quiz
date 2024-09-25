using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Quiz
{
    internal class Quiz
    {
        private double Grade = 0;
        private double QuestionValue = 0;
        public List<Question> myQuiz = new List<Question>()
        {
            new Question()
            {
                Text = "True or False: The war of 1812 was in 1812? ",
                Type = QuestionType.TrueFalse,
                PossibleAnswers = new List<Answer>
                {
                    new Answer{Text = "true", IsCorrect=true},
                    new Answer{Text = "false", IsCorrect=false}
                }
            },
            new Question()
            {
                Text = "What is the capital of France?",
                Type = QuestionType.MultipleChoice,
                PossibleAnswers = new List<Answer>
                {
                    new Answer { Text = "London", IsCorrect = false },
                    new Answer { Text = "Paris", IsCorrect = true },
                    new Answer { Text = "Berlin", IsCorrect = false },
                    new Answer { Text = "Rome", IsCorrect = false }
                }
            },
            new Question()
            {
                Text = "A client is admitted to the same-day surery unit for liver biopsy. Which of the following laboratory tests assesses coaulation? Select all that apply.",
                Type = QuestionType.Checkbox,
                PossibleAnswers = new List<Answer>
                {
                    new Answer { Text = "Partial thromboplastine time", IsCorrect= false },
                    new Answer { Text = "Prothrombin time", IsCorrect= true },
                    new Answer { Text = "Platelet count", IsCorrect= true },
                    new Answer { Text = "Hemoglobin", IsCorrect= false },
                    new Answer { Text = "Complete Blood Count", IsCorrect= false },
                    new Answer { Text = "White Blood Cell Count", IsCorrect= false },
                }
            },
            new Question()
            {
                Text = "What is the keyword used to define a class in C#?",
                Type = QuestionType.FreeResponse,
                Words = new List<KeyWords>
                {
                    new KeyWords {Text = "class"}
                }
            }

        };

        public void DisplayQuiz()
        {
            int correctAnswer = 0;
            QuestionValue = 100 / myQuiz.Count;
            foreach (Question question in myQuiz)
            {
                Console.WriteLine(question.Text);
                int questionNumb = 1;
                string multipleAnswers = "";

                if (question.PossibleAnswers != null)
                {
                    foreach (Answer answer in question.PossibleAnswers)
                    {
                        Console.WriteLine(questionNumb + " " + answer.Text);
                        if (answer.IsCorrect == true && (question.Type == QuestionType.TrueFalse || question.Type == QuestionType.MultipleChoice))
                        {
                            correctAnswer = questionNumb;
                        }
                        else if (answer.IsCorrect == true && question.Type == QuestionType.Checkbox)
                        {
                            multipleAnswers = multipleAnswers + questionNumb;
                        }
                        questionNumb++;
                    }
                    if (question.Type == QuestionType.MultipleChoice || question.Type == QuestionType.TrueFalse)
                    {
                        QuestionMultChoiceOrTrueFalse(correctAnswer);
                        Space();

                    }
                    else if (question.Type == QuestionType.Checkbox)
                    {
                        QuestionCheckbox(multipleAnswers);
                        Space();
                    }
                }
                if (question.Type == QuestionType.FreeResponse)
                {
                    QuestionFreeResponse(question);
                }
            }
            DisplayGrade();
        }

        public void DisplayGrade()
        {
            Console.WriteLine("You made an " + Grade);
        }

        public void CheckAnswer(int userAnswer, int ans)
        {
            Console.WriteLine(userAnswer + " " + ans);
            if (userAnswer == ans)
            {
                Grade = Grade + QuestionValue;
                Console.WriteLine("That is correct");
                Space();
            }
            else
            {
                Console.WriteLine("Sorry, that is not correct");
                Space();
            }
        }


        public void Space()
        {
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public void QuestionMultChoiceOrTrueFalse(int correctAnswer)
        {
            Console.WriteLine("");
            Console.WriteLine("What do you think the answer is? ");
            string quizAnswer = Console.ReadLine();
            int number = 0;
            try
            {
                number = int.Parse(quizAnswer);
                Console.WriteLine($"You entered: {number}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
            CheckAnswer(number, correctAnswer);
        }

        public void QuestionCheckbox(string checkboxAnswers)
        {
            Space();
            Console.WriteLine("Enter your answers with a comma inbetween each number: ");
            string tempUserAnswer = Console.ReadLine();
            int indexOfAnswers = 0;
            int answerLength = checkboxAnswers.Length;
            int numbOfAnswers = 0;
            foreach (char n in tempUserAnswer)
            {
                if (n != ',' && n != ' ' && checkboxAnswers.Contains(n))
                {
                    indexOfAnswers = checkboxAnswers.IndexOf(n);
                    checkboxAnswers = checkboxAnswers.Remove(indexOfAnswers, 1);
                    numbOfAnswers++;
                }
                else if (n != ' ' && n != ',')
                {
                    numbOfAnswers++;
                }

            }
            if (checkboxAnswers.Length == 0 && numbOfAnswers == answerLength)
            {
                Console.WriteLine("Great Job! You got it correct");
                Grade = Grade + QuestionValue;
            }
            else
            {
                Console.WriteLine("sorry, you got it wrong");
            }
        }

        public void QuestionFreeResponse(Question question)
        {
            Space();
            int keyWords = question.Words.Count;
            int userKeyWords = 0;
            Console.WriteLine("Please type your answer below");
            string userAnswerFreeResponse = Console.ReadLine();
            string[] userAnserWords = userAnswerFreeResponse.Split(new char[] { ' ', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            HashSet<string> uniqueWords = new HashSet<string>(userAnserWords);
            userAnserWords = uniqueWords.ToArray();
            foreach (KeyWords answerWords in question.Words)
            {
                foreach (string userWords in userAnserWords)
                {
                    if (userWords.Equals(answerWords.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        userKeyWords++;
                    }
                }
            }
            if ((double)userKeyWords / keyWords > .65)
            {
                Grade = Grade + QuestionValue;
                Console.WriteLine("Congrats, you got it right!");
                Space();
            }
            else
            {
                Console.WriteLine("Sorry, you got it wrong");
                Space();
            }
        }

        public void AddQuestion(Question question)
        {
            if (question != null)
            {
                myQuiz.Add(question);
            }
        }

        public void AddQuestion()
        {
            QuestionType type;
            Question questionToEnter = new Question();
            string userType = "";
            string stringNumberOfAnswers = "";
            List<Answer> PossibleAnswers = new List<Answer>();
            List<KeyWords> keyWords = new List<KeyWords>();
            int intNumbOfAnswers = 0;
            int userNumber = 0;
            int userPick = 1;
            bool token;
            Console.WriteLine("Enter your questions: ");
            string question = Console.ReadLine();
            Console.WriteLine("What is your question type? ");
            Space();
            foreach (QuestionType name in Enum.GetValues(typeof(QuestionType)))
            {
                Console.WriteLine(userPick + " " + name.ToString());
                userPick++;
            }
            userType = Console.ReadLine();
            try
            {
                userNumber = int.Parse(userType);
                Console.WriteLine($"You entered: {userNumber}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter a valid integer.");
            }
            if (userNumber == 1)
            {
                type = QuestionType.MultipleChoice;
            }
            else if (userNumber == 2)
            {
                type = QuestionType.TrueFalse;
            }
            else if (userNumber == 3)
            {
                type = QuestionType.Checkbox;
            }
            else
            {
                type = QuestionType.FreeResponse;
            }
            if (type == QuestionType.TrueFalse || type == QuestionType.MultipleChoice || type == QuestionType.Checkbox)
            {
                Console.WriteLine("How many answers do you want to add?");
                stringNumberOfAnswers = Console.ReadLine();
                try
                {
                    intNumbOfAnswers = int.Parse(stringNumberOfAnswers);
                    Console.WriteLine($"You entered: {intNumbOfAnswers}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input! Please enter a valid integer.");
                }
                for(int i =1;i<intNumbOfAnswers+1;i++) 
                {
                    Console.WriteLine("What is answer number " + i);
                    string temp = Console.ReadLine();
                    Console.WriteLine("Type true for correct answer or false for not correct");
                    string truefalse = Console.ReadLine();
                    if (truefalse.Equals("true", StringComparison.OrdinalIgnoreCase)) 
                    {
                        token = true;
                    }
                    else
                    {
                        token = false;
                    }
                    PossibleAnswers.Add(new Answer { Text = temp, IsCorrect = token });
                }
                questionToEnter.Text = question;
                questionToEnter.Type = type;
                questionToEnter.PossibleAnswers = PossibleAnswers;
                myQuiz.Add(questionToEnter);

            }
            else
            {
                Console.WriteLine("How many answers do you want to add?");
                stringNumberOfAnswers = Console.ReadLine();
                Console.WriteLine("How many key words do you want to add?");
                stringNumberOfAnswers = Console.ReadLine();
                try
                {
                    intNumbOfAnswers = int.Parse(stringNumberOfAnswers);
                    Console.WriteLine($"You entered: {intNumbOfAnswers}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input! Please enter a valid integer.");
                }
                for (int i = 1; i < intNumbOfAnswers + 1; i++)
                {
                    Console.WriteLine("What is answer number " + i);
                    string temp = Console.ReadLine();
                    keyWords.Add(new KeyWords { Text = temp });
                }
                questionToEnter.Text = question;
                questionToEnter.Type = type;
                questionToEnter.Words = keyWords;
                myQuiz.Add(questionToEnter);
            }
            
        }
    }
}
