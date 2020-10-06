using System;
using System.Runtime.CompilerServices;

namespace TicTacToe
{
    public class Program
    {
        #region Pola
        static string Player1Name = null;
        static string Player2Name = null;
        static bool Winner = false;
        static string CurrentPlayer = null;
        static int TableDimension = 0;
        #endregion

        /// <summary>
        /// Pole gry
        /// </summary>
        static string[] GameField;

        /// <summary>
        /// Wybór modelu gry
        /// </summary>
        static int GameMode = 0;

        public static void Main(string[] args)
        {
            SetNames();

            GameModeChose();

            GameFieldChose();

            Game();



        }

        /// <summary>
        /// Pozwala wybrać rodzaj gry (1 to 3x3 ; 2 to 4x4 ; 3 to 5x5)
        /// </summary>
        static void GameModeChose()
        {
            Console.WriteLine("Witaj!");
            Console.WriteLine("W jaką wersję kółka i krzyrzyk chcesz zagrać?");
            Console.WriteLine("1. Pole 3x3 - wciśnij 1");
            Console.WriteLine("2. Pole 4x4 - wciśnij 2");
            Console.WriteLine("3. Pole 5x5 - wciśnij 3");
            Console.WriteLine("4. Podaj wymiar tablicy kwadratowej");

            string a = Console.ReadLine();

            try
            {
                GameMode = Int32.Parse(a);
                if (GameMode > 4)
                {
                    Console.WriteLine("Wybrałeś złą liczbę. Pamiętaj wybierz 1, 2, 3 lub 4");
                    GameModeChose();
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("Litery są niedozwolone. Wybierz spośród 1,2,3 lub 4");

            }

            switch (GameMode)
            {
                case 1:
                    Console.WriteLine("Gracie na 3x3");
                    TableDimension = 3;
                    break;
                case 2:
                    Console.WriteLine("Gracie na 4x4");
                    TableDimension = 4;
                    break;
                case 3:
                    Console.WriteLine("Gracie na 5x5");
                    TableDimension = 5;
                    break;
                case 4:
                    Console.WriteLine("Podaj ilość wierszy tablicy, przy czym ilość kolumn będzie identyczna");
                    string b = Console.ReadLine();
                    try
                    {
                        TableDimension = Int32.Parse(b);                    
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Litery są niedozwolone.");

                    }
                    break;
                default:
                    GameModeChose();
                    break;
            }


        }

        /// <summary>
        /// Rozpoczęcie gry
        /// </summary>
        static void Game()
        {
            while (Winner == false)
            {
                if (CurrentPlayer == null)
                {
                    CurrentPlayer = Player1Name;
                }

                Move();
                CheckIfWin();
                PlayerChange();


            }
            PlayAgainQuestion();

        }

        /// <summary>
        /// Zmiana gracza
        /// </summary>
        private static void PlayerChange()
        {
            if (CurrentPlayer == Player2Name)
            {
                CurrentPlayer = Player1Name;

            }
            else
            {
                CurrentPlayer = Player2Name;
            }
        }

        /// <summary>
        /// Ruch gracza
        /// </summary>
        static void Move()
        {
            if (CurrentPlayer == Player1Name)
            {
                DisplayField(TableDimension);

                Console.WriteLine(" \n \n Wybierz pole na, które chcesz postawić X");
                int a = Convert.ToInt32(Console.ReadLine());
                if (CheckIfFieldTaken(a) == false)
                {
                    GameField[a - 1] = "x";
                }
                else
                {
                    Move();
                }

            }
            else
            {
                DisplayField(TableDimension);
                Console.WriteLine("\n \n Wybierz pole na, które chcesz postawić O");
                int a = Convert.ToInt32(Console.ReadLine());
                if (CheckIfFieldTaken(a) == false)
                {
                    GameField[a - 1] = "o";
                }
                else
                {
                    Move();
                }


            }
        }

        static void SetNames()
        {
            Console.WriteLine("Podaj imię gracza pierwszego:");
            Player1Name = Console.ReadLine();

            Console.WriteLine("Podaj imię gracza drugiego:");
            Player2Name = Console.ReadLine();

            Console.WriteLine("Witaj : " + Player1Name + " i " + Player2Name);
            Console.WriteLine("Gracz " + Player1Name + " to X a " + Player2Name + " to O");
        }

        static bool CheckIfFieldTaken(int pole)
        {
            if (GameField[pole - 1] == "x" || GameField[pole - 1] == "o")
            {
                Console.WriteLine("Niestety te pole jest już zajęte, spróbuj ponownie");
                return true;

            }
            else
            {

                return false;

            }
        }

        /// <summary>
        /// Sprawdzenie czy wygrana (sprawdzamy czy są 3 znaki identyczne)
        /// </summary>
        static void CheckIfWin()
        {
            
            try
            {
                for (int i = 0; i < GameField.Length; i++)
                {
                    //Warunek Poziomy
                    if (GameField[i] == GameField[i + 1] && GameField[i + 1] == GameField[i + 2])
                    {
                        Winner = true;
                        Console.WriteLine("Wygral gracz : " + CurrentPlayer);
                    }
                    // Warunek Pionowy
                    else if (GameField[i] == GameField[i + TableDimension] && GameField[i + TableDimension] == GameField[i + (2 * TableDimension)])
                    {
                        Winner = true;
                        Console.WriteLine("Wygral gracz : " + CurrentPlayer);
                    }
                    // Warunek skośny w prawo
                    else if (GameField[i] == GameField[i + (TableDimension + 1)] && GameField[i + (TableDimension + 1)] == GameField[i + (2 * (TableDimension + 1))])
                    {
                        Winner = true;
                        Console.WriteLine("Wygral gracz : " + CurrentPlayer);
                    }
                    // Warunek skośny w lewo
                    else if (GameField[i] == GameField[i + (TableDimension - 1)] && GameField[i + (TableDimension - 1)] == GameField[i + (2 * (TableDimension - 1))])
                    {
                        Winner = true;
                        Console.WriteLine("Wygral gracz : " + CurrentPlayer);
                    }
                }
            }
            catch (System.IndexOutOfRangeException)
            {

            }
            
        }


        static void ResetGame()
        {
            for (int i = 0; i < GameField.Length; i++)
            {
                GameField[i] = Convert.ToString(i + 1);
            }
            TableDimension = 0;
            CurrentPlayer = null;
            Winner = false;
        }

        static void PlayAgainQuestion()
        {
            Console.WriteLine("Chcecie zagrać ponownie? T / N");
            string b = Console.ReadLine();
            if (b == "t" || b == "T")
            {
                Console.WriteLine("Ci sami gracze? T / N");
                b = Console.ReadLine();
                if (b == "t" || b == "T")
                {
                    ResetGame();
                    GameModeChose();
                    GameFieldChose();
                    Game();

                }
                else if (b == "n" || b =="N")
                {
                    Console.WriteLine("Dzięki za gre" + Player1Name + " " + Player2Name);
                    ResetGame();
                    SetNames();
                    GameModeChose();
                    GameFieldChose();
                    Game();

                }
                else
                {
                    Console.WriteLine("Wciśnij T lub N");
                    PlayAgainQuestion();
                }
            }
            else if (b == "n" || b == "N")
            {
                Console.WriteLine("Dzięki za gre");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Wciśnij T lub N");
                PlayAgainQuestion();
            }
        }


        static void GameFieldChose()
        {
            
            GameField = new string[TableDimension * TableDimension];
            for (int i = 1; i <= TableDimension*TableDimension; i++)
            {
                GameField[i-1] = Convert.ToString(i);
            }

        }
        static void DisplayField(int a)
        {
            for (int i = 1; i <= a; i++)
            {
                Console.WriteLine("\n ---------");
                for (int j = 1; j <= a; j++)
                {
                    Console.Write(" | " + GameField[(i * a - (a - j)) - 1]);
                }

            }
        }
        

    }



}

