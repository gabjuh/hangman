

using System.Text;

namespace Hangman {

    internal class Program {

        static void Main(string[] args) {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool gameFinished = false;
            char letter = ' ';
            string hiddenText = string.Empty;
            string welcomeText = "Welcome to my HANGMAN Game";
            string gitLink = "https://github.com/gabjuh/hangman";
            string infoText = "";
            bool isErr = false;
            int headerWidth = welcomeText.Length + 40;
            char heart = '\u2665';
            string usedLetters = "";
            List<char> usedLettersArr = new List<char>();
            int life = 8;
            string[] itBegriffe = {
                "Bug Fixing",
                "Rubber Ducking",
                "Code Monkey",
                "Null Pointer",
                "Spaghetti Code",
                "Feature Creep",
                "Refactoring",
                "Version Control",
                "Legacy Code",
                "WYSIWYG",
                "Yak Shaving",
                "Bikeshedding",
                "Fizz Buzz",
                "Hello World",
                "Rubber Duck",
                "Deep Learning",
                "REST API",
                "Scrum Master",
                "Waterfall",
                "Unit Testing",
                "Continuous Integration",
                "Cryptocurrency",
                "Blockchain",
                "Cybersecurity",
                "Agile Sprint",
                "Hackathon",
                "API Gateway",
                "Cloud Computing",
                "Big Data",
                "Machine Learning",
                "DevOps Culture",
                "Containerization",
                "Microservices",
                "Serverless",
                "Git Branch",
                "Pull Request",
                "Merge Conflict",
                "Regex Pattern",
                "JSON Parsing",
                "Lambda Function",
                "Semantic Versioning",
                "Garbage Collection",
                "Infinite Loop",
                "Binary Search",
                "Recursive Function",
                "Thread Deadlock",
                "Memory Leak",
                "Stack Overflow",
                "Tech Debt",
                "Continuous Deployment"
            };
            string text = itBegriffe[getRandomNumberBetween(0, itBegriffe.Length)];

            void init() {

                /*
                 * [x] Show program header
                 * [X] Convert hidden text to blank fields (only letters)
                 * [X] Convert hidden text to blank fields (only letters)
                 * [X] Show blank fields
                 * [X] Get a letter as user input
                 * [X] Compare them
                 * [x] Show life
                 * [X] Refactor
                 * [X] Set headline design
                 * [X] Show already used characters
                 * [X] Make game case insensitive
                 * [X] Change text color 
                 * [ ] Allow only one character and no other chars
                 * [X] Add an array of possible texts and set them random
                 * [ ] Ask at the end if the game should be repeated
                 * [ ] Whole sentence as solution
                 * [ ] Add feedback as info box 
                 * 
                */

                // Get input from user
                turnTextToHiddenFields(text);

                while (!gameFinished) {

                    // Show the header line
                    showHeader();

                    // Show lifes
                    showLife();

                    // Show uset letters
                    showUsedLetters();

                    // Write the current state of the hidden text
                    showHiddenText();

                    // Show info text
                    showInfoText();

                    // Check if text contains the given letter
                    isInputContainsLetter();

                    // Show hidden text with 
                    showSelectedLettersInString(letter);

                    // Clear the console
                    Console.Clear();

                    // Check if game finished
                    isGameFinished();
                }
            }

            int getRandomNumberBetween(int min, int max){
                Random rnd = new Random();
                int nr = rnd.Next(min, max);
                return nr;
            }

            void showInfoText() {

                Console.WriteLine("(IT technical term)");

                //if (isErr) 
                //    Console.ForegroundColor = ConsoleColor.Red;

                //Console.WriteLine(infoText);

                //if (isErr)
                //    Console.ResetColor();
            }

            void showHiddenText(){
                Console.WriteLine($"\n\n{hiddenText}");
            }

            string turnTextToHiddenFields(string text) {
                for (int index = 0; index < text.Length; index++) {
                    hiddenText += text[index] != ' ' ? '_' : ' ';
                }
                return hiddenText;
            }

            char getLetterFromUser() {
                Console.Write("\n\n\nWrite a letter: ");
                letter = Console.ReadLine()[0];
                return letter;
            }

            void showUsedLetters() {
                Console.WriteLine("\tLetters: " + usedLetters);
            }

            void showLife() {
                string lifes = "";

                // Add hearts to lifes
                for (int i = 0; i < life; i++) {
                    lifes += heart;
                }

                Console.Write("Lifes: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(lifes);
                Console.ResetColor();
            }

            void showSelectedLettersInString(char letter) {
                bool isLetterMatches = false;

                string comma = usedLetters.Length > 0 ? "," : "";

                usedLettersArr.Add(letter);

                usedLetters += comma + letter.ToString();



                for (int i = 0; i < text.Length; i++) {

                    char letterOrig = text[i];
                    string letterString = letterOrig.ToString().ToLower();
                    bool isLetterStringUpper = char.IsUpper(letterOrig);
                    string letterInput = letter.ToString().ToLower();

                    if (letterString == letterInput) {
                        char[] ch = hiddenText.ToCharArray();
                        if (isLetterStringUpper) {
                            ch[i] = char.ToUpper(letter);
                        } else {
                            ch[i] = char.ToLower(letter);
                        }
                        hiddenText = new string(ch);
                        isLetterMatches = true;
                    }
                }

                if (!isLetterMatches) {
                    if (life > 0)
                        life--;
                    else
                        gameOver();
                }

                Console.WriteLine(hiddenText);
            }

            bool isInputContainsLetter() {
                char letter = getLetterFromUser();

                Console.WriteLine(usedLettersArr.Count);

                //infoText = "";
                //isErr = false;

                //if (usedLettersArr.Contains(letter)) {
                //    infoText = "Letter already used";
                //    isErr = true;
                //    return false;
                //}

                return text.Contains(letter);
            }

            string showHeadlineText(string text) {
                string headerPatternLineWithTitle = "";
                for (int i = 0; i < headerWidth - text.Length - 1; i++) {
                    if (i == (headerWidth - text.Length) / 2) {
                        headerPatternLineWithTitle += ' ' + text + ' ';
                    }
                    else {
                        headerPatternLineWithTitle += '*';
                    }
                }
                return headerPatternLineWithTitle;
            }

            void showHeader() {

                string headerPatternLine = "";

                for (int i = 0; i < headerWidth; i++) {
                    headerPatternLine += '*';
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(headerPatternLine);
                Console.WriteLine(showHeadlineText(welcomeText));
                Console.WriteLine(headerPatternLine);
                Console.WriteLine(showHeadlineText(gitLink));
                Console.WriteLine(headerPatternLine);
                Console.ResetColor();
            }

            void isGameFinished() {
                if (!hiddenText.Contains('_')) {
                    gameFinished = true;
                    Console.WriteLine("Congratulations!");
                }
            }

            void gameOver() {
                Console.WriteLine("You lost!");
                gameFinished = true;
            }

            init();
        }
    }
}