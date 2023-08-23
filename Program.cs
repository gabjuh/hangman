

using System.Text;

namespace Hangman {

    internal class Program {

        static void Main(string[] args) {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool gameFinished = false;
            string text = "This is a hidden text";
            char letter = ' ';
            string hiddenText = string.Empty;
            string welcomeText = "Welcome to my HANGMAN Game";
            string gitLink = "https://github.com/gabjuh/hangman";
            int headerWidth = welcomeText.Length + 50;
            char heart = '\u2665';
            string usedLetters = "";
            int life = 5;

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
                 * [ ] Add an array of possible texts and set them random
                 * [ ] Ask at the end if the game should be repeated
                 * 
                */

                // Get input from user
                turnTextToHiddenFields(text);

                while (!gameFinished) {

                    // Show the header line
                    showHeader();

                    // Show lifes
                    showLife();

                    // Write the current state of the hidden text
                    Console.WriteLine(hiddenText);

                    // Show uset letters
                    showUsedLetters();

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

            string turnTextToHiddenFields(string text) {
                for (int index = 0; index < text.Length; index++) {
                    hiddenText += text[index] != ' ' ? '_' : ' ';
                }
                return hiddenText;
            }

            char getLetterFromUser() {
                Console.Write("Write a letter: ");
                // letter = char.IsLetter(Console.ReadKey().KeyChar) ? Console.ReadKey().KeyChar : '_';
                letter = Console.ReadLine()[0];
                return letter;
            }

            void showUsedLetters() {
                Console.WriteLine("Used letters: " + usedLetters);
            }

            void showLife() {
                string lifes = "";

                for (int i = 0; i < life; i++) {
                    lifes += heart;
                }

                Console.Write("Lifes: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(lifes + "\n");
                Console.ResetColor();
            }

            void showSelectedLettersInString(char letter) {
                bool isLetterMatches = false;

                string comma = usedLetters.Length > 0 ? ", " : "";

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