

using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Hangman {

    internal class Program {

        static void Main(string[] args) {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            #region Declarations
            bool gameFinished = false;
            bool gameSolved = false;
            string letter = "";
            string hiddenText = string.Empty;
            string welcomeText = "Welcome to my HANGMAN Game";
            string promptTextDefault = "Give me a letter or try a possible solution: ";
            string promptTextNew = "Do you want another game? (y/n): ";
            string gitLink = "https://github.com/gabjuh/hangman";
            string infoText = "Good Luck!";
            bool isErr = false;
            bool isNewGameQuestion = false;
            bool guessedCorrectly = false;
            int headerWidth = welcomeText.Length + 20;
            char heart = '\u2665';
            string usedLetters = "";
            List<char> usedLettersArr = new List<char>();
            int maxLife = 8;
            int life = maxLife;
            string[] itBegriffe = {
                "Werzweigung",
                "Schleife",
                "Logische Operatoren",
                "Bedienung",
                "Wahrheitstabelle",
                "Variablen Deklaration",
                "Console",
                "Debugging",
            };
            string text = selectNewWord();
            bool isExit = false;

            #region For David
            string forDavid = "@&&&&%#/(##(/*****,**,*///**/(((#%%#((////*/%&&%%%&%###%%#(%&&@@@@@@&&&&&%&&&&&&\n" +
                "((((######((##%%%%%#*.,((((##(//#&&&&&@@@&%#(#&@@@&%##(%&@@@@@@@@@@@&&&&%%%%%&%%\n" +
                "..(%%(,,...*//#%%%%%%%###%%&&#*/###&@@@&%&&&&@@@&%%&&@@@@@@@@@@@@@@&&&%#(##%%%%#\n" +
                "..,,,......,/(#%%%%%#/..,,***/(#((###%%%&&&@@&&&@@&&&@@@@@@@@@@@@@@&((/(######%%\n" +
                "....,***,,........,,,,,,,,*/////****//*,/#&&@@@@@@@&&&@@@@@@@@@@@@@&****/#%%####\n" +
                "  ..........  .......,,,,,,*/*,,,    &&@@&&&&&&&&&&&&&&&&@@@@@@@&&&&%(#%(((#(///\n" +
                "    ...        ......,,...,//*,,.%&&&&&@&&&&&&&&&%&&&&&@@@&@@@&&&&&%(**/#(/**,,,\n" +
                "  ...................,,...,/%@&&@&&&&&&&&%&%%%%%%%%%&&&&&@@&@@&&&&%(****,,,.   .\n" +
                ".....................,,,.&&&&&&&&&&&&&%%###%##%%%%%#%&&&&&&&&&&%&%/,,,*,.       \n" +
                "...................,**,*%%%%&&%&&#%&&&&#%&(##(%%######%%%%%&%%%%%/......       .\n" +
                "................,***,,,#%&&&&&%%(#%**/##%&&&&((/*/(###%%&%%&%%%%#..............,\n" +
                ".....,,,,,,,,,,,**,,***/#%%%%&&&%#/(/,,,,,,*/((/*,/#%#%%&&&&%&%#,...,.......,***\n" +
                ",,,,,,,,***********///*/#%%%%&%(%%#/*,,,,,,,,,,,,,/#%%%&&&&%%%%**********,,,*///\n" +
                "*********//***/////////#%%%&&%%&%/(*,,,,,,,,,,,,,,%%%%%%%&&&%%%(((//////////////\n" +
                "**//////////////(//(///#%%%&&&&&%#,,,,,,,,,,,,,,,/%%%%%#%%%%%%#((((((((((((((///\n" +
                "////////////((((((((((%#%%%&&&&%%#,,,,****,,,,,,*%%%%###%##%%%(((((((((((((((((/\n" +
                "/////////(((((((((((%%%(%%&&&&%%#(,,,*******,,*(####%%%%%#%%%%(((((((((((((((/,,\n" +
                "/////((((((((((((((#&%#(#%%%&&&&&*...,,,,,/(((((##(####%######((((((((((((((/,,,\n" +
                "///((((((((((((((((%&%(*(#%%&&&&&&&&*,((((((((((##(#(((((((((((((((((((((((/,,,,\n" +
                "((((((((((((((((((%%&%(***(#%%%%&&&%%#((((((((((######(((###((((((((((((((/,..,,\n" +
                "(((((((((((((((((#%%%#/,,.,,*(##%%&&&&&&&&&&%%((##((#%%##(/(#(((((((((((((*..,,,\n" +
                "(((((((((((((((((#%%%#/,,,,,,.,*((##%%%%%&&&&&&(###%%%%%%%#((((((((((((((/*..,,,\n" +
                "(((((((((((((((((%%%%#/,,,,,...,(,,**//((###%%%###%%%%%%%##((((((/(////((/,..,,,\n" +
                "((((((((((((((((#%%%%#/*,,,...,*((((((((((*,,,,*,(##%%%%%#((((///////////*..,,,,\n" +
                "(((((((((((((((#%%##(/**,,.,,,*((((((((((((((((((((*/(##/(((((///////////,,,,,*,\n" +
                "((((///((((/(((#%%%%#(/,,,.,,((((((((((((((((((((((((((((((((////(//////*..,,***\n" +
                "(((((((((((///(#%%####(**/((((((((((((((((((((((/(((((((((((////////////,.,,,,,*\n" +
                "((((((((((((((#%#%####/**(((((((((((((((((((((((//(((((((((////////////*.,,,,,,,\n" +
                "((((((((((((((#%#####(,((((((((((((((((/((///(((((((((((//(////////////,,,,,,,,,\n" +
                "((((((((((((((*(((((*(((((((((((((((//(///(///(((////(/(//////////////*..,,,,,,,\n" +
                "(((((((((((((((((/((((((((((((((((((((/////(((////////////////////////,.,,,,,,,,\n";
            #endregion f
            #endregion

            #region Helpers
            void print(string text, string color = "white", bool line = true)
            {
                if (color != "white") {
                    if (color == "red") Console.ForegroundColor = ConsoleColor.Red;
                    if (color == "green") Console.ForegroundColor = ConsoleColor.Green;
                    if (color == "gray") Console.ForegroundColor = ConsoleColor.Gray;
                }

                if (line) {
                    Console.WriteLine(text);
                } else {
                    Console.Write(text);
                }

                Console.ResetColor();
            }

            string selectNewWord()
            {
                return itBegriffe[getRandomNumberBetween(0, itBegriffe.Length)];
            }

            int getRandomNumberBetween(int min, int max)
            {
                Random rnd = new Random();
                int nr = rnd.Next(min, max);
                return nr;
            }
            #endregion

            #region Initialisation
            void init() {
                play();
            }

            void play()
            {
                turnTextToHiddenFields(text);

                while (!isExit) {
                    setGameField();
                }

                print(forDavid, "green");

                print("Press any key to exit...", "green");
                string bla = Console.ReadLine();
            }

            void setGameField ()
            {
                // Show the header line
                showHeader();

                showMenu();

                // Show lifes
                showLife();

                // Show uset letters
                showUsedLetters();

                // Write the current state of the hidden text
                showHiddenText();

                // Show the type of the word or text that is hidden
                showTypeOfWord();

                // Show info text
                showInfoText();

                // Check if text contains the given letter
                isTextContainsInputLetter();

                // Show hidden text with 
                showSelectedLettersInString(letter);

                // Check if game finished
                isGameFinished();

                // Show Footer
                //showFooter();
            }
            #endregion

            #region Menu stuff
            void showMenu()
            {
                print("\n\t1. Restart Game");
                print("\t2. I give up, show the word!");
                print("\t3. Exit\n");
            }

            void restartGame()
            {
                Console.Clear();
                text = selectNewWord();
                turnTextToHiddenFields(text);
                hiddenText = string.Empty;
                guessedCorrectly = false;
                life = maxLife;
                usedLetters = "";
                usedLettersArr.Clear();
                infoText = "New game! Better Luck!";
                play();
            }

            void exitGame()
            {
                isExit = true;
            }
            #endregion

            #region Show game parts
            void showTypeOfWord()
            {
                print("\n\t(Programmierungsbegriffe)", "gray");
            }

            void showInfoText() {

                if (isErr) {
                    print("\t" + infoText, "red");
                }
                else 
                {
                    print("\t" + infoText, "green");
                }

                isErr = false;
                infoText = "";
            }

            void showHiddenText(){
                if (gameSolved) {
                    print($"\n\t{text}", "green");
                } else if (!gameSolved && life == 0) {
                    print($"\n\t{text}", "red");
                } else {
                    print($"\n\t{hiddenText}");
                }
            }

            void showUsedLetters()
            {
                print("\n\tUsed Letters: " + usedLetters, "gray");
            }

            void showLife()
            {
                string lifes = "";

                // Add hearts to lifes
                for (int i = 0; i < life; i++) {
                    lifes += heart;
                }

                print("\tLifes remain: ", "white", false);
                print(lifes, "red", false);
            }

            string showHeadlineText(string text)
            {
                string headerPatternLineWithTitle = "";
                for (int i = 0; i < headerWidth - text.Length; i++) {
                    if (i == (headerWidth - text.Length) / 2) {
                        headerPatternLineWithTitle += ' ' + text + ' ';
                    }
                    else {
                        headerPatternLineWithTitle += '-';
                    }
                }
                return headerPatternLineWithTitle;
            }

            void showHeader()
            {
                string headerPatternLine = "";

                for (int i = 0; i <= headerWidth; i++) {
                    headerPatternLine += '-';
                }

                print(headerPatternLine);
                print(showHeadlineText(welcomeText));
                print(headerPatternLine);
                print(showHeadlineText(gitLink));
                print(headerPatternLine);
            }

            void showFooter()
            {
                string headerPatternLine = "";

                for (int i = 0; i < headerWidth; i++) {
                    headerPatternLine += '*';
                }

                print(headerPatternLine);
                print(showHeadlineText(gitLink));
                print(headerPatternLine);
            }
            #endregion

            #region Game logic
            string turnTextToHiddenFields(string text) {
                for (int index = 0; index < text.Length; index++) {
                    hiddenText += text[index] != ' ' ? '_' : ' ';
                }
                return hiddenText;
            }

            string getLetterFromUser() {
                print($"\n\n\n{promptTextDefault}", "white", false);
                letter = Console.ReadLine();
                return letter;
            }

            string isAnotherGame()
            {
                print($"\n\n\n{promptTextNew}", "white", false);
                letter = Console.ReadLine();
                return letter;
            }

            void refresh()
            {
                Console.Clear();
                //setGameField();
            }

            void isWon()
            {
                if (!hiddenText.Contains('_') || guessedCorrectly) {
                    gameFinished = true;
                    gameSolved = true;
                }
            }

            void isGameFinished()
            {
                if (!hiddenText.Contains('_') || guessedCorrectly) {
                    gameSolved = true;
                    isNewGameQuestion = true;
                    infoText = "Congratulations!";
                }
                refresh();
            }

            void gameOver()
            {
                infoText = "You lost!";
                isErr = true;
                isNewGameQuestion = true;
                gameFinished = true;
                refresh();
            }

            void giveUp()
            {
                infoText = "You p*ssy!";
                isErr = true;
                infoText += " Solution: " + text;
                isNewGameQuestion = true;
                refresh();
            }
            #endregion

            #region Show game elements
            void showSelectedLettersInString(string letter)
            {
                bool isLetterMatches = false;

                string comma = usedLetters.Length > 0 ? "," : "";

                if (!usedLettersArr.Contains(letter[0])) {
                    usedLettersArr.Add(letter[0]);
                    usedLetters += comma + letter.ToString();
                }

                for (int i = 0; i < text.Length; i++) {

                    char letterOrig = text[i];
                    string letterString = letterOrig.ToString().ToLower();
                    bool isLetterStringUpper = char.IsUpper(letterOrig);
                    string letterInput = letter.ToString().ToLower();

                    if (letterString == letterInput) {
                        char[] ch = hiddenText.ToCharArray();
                        if (isLetterStringUpper) {
                            ch[i] = char.ToUpper(letter[0]);
                        }
                        else {
                            ch[i] = char.ToLower(letter[0]);
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

                print(hiddenText);
            }
            #endregion
                                    
            #region Input check
            bool isTextContainsInputLetter() {
                string letter = isNewGameQuestion ? isAnotherGame() : getLetterFromUser();

                bool isMatching = false;

                if (isNewGameQuestion && letter.Length > 0) {
                    if (letter[0] == 'y') {
                        isNewGameQuestion = false;
                        restartGame();
                    }
                    else if (letter[0] == 'n') {
                        exitGame();
                    }
                    else {
                        isErr = true;
                        infoText = "Stop doing that!";
                    }
                }
                else
                {
                    isMatching = text.ToLower().Contains(letter);

                    int matches = text.ToLower().Count(m => (m == letter[0]));

                    print(usedLettersArr.Count.ToString());

                    if (letter[0] == '1') restartGame();

                    if (letter[0] == '2') giveUp();

                    if (letter[0] == '3') exitGame();

                    if (letter == text) {
                        guessedCorrectly = true;
                        isGameFinished();

                    }

                    //switch(letter[0]) {
                    //    case '1':
                    //        restartGame ();
                    //        break;
                    //    case '2': 
                    //        giveUp();
                    //        break;
                    //    case '3':
                    //        exitGame();
                    //        break;
                    //    default: 
                    //        if (letter == text) {
                    //            guessedCorrectly = true;
                    //            isGameFinished();
                    //        } else if (letter.Length > 1 && letter != text) {
                    //            isErr = true;
                    //            infoText = "Nice try, but no. It consts two hearts. 💩";
                    //        }
                    //        break;
                    //}

                    if (usedLettersArr.Contains(letter[0])) {
                        infoText = "Letter already used";
                        isErr = true;
                        refresh();
                        isWon();
                        return false;
                    }

                    if (isMatching) {
                        infoText = $"Great! Matches for letter '{letter[0]}': {matches}.";
                    }
                    else if (!isExit) {
                        infoText = $"Sorry, no matches for letter '{letter[0]}'.";
                        isErr = true;
                    }
                }

                return isMatching;
            }
            #endregion

            init();
        }
    }
}