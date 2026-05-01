using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime;

namespace UV7_Snake_C
{
    class SnakeGame
    {
        public int H = 32;                          // Height
        public int W = 80;                          // Width

        public int[] X = new int[2340];              // Snake X 
        public int[] Y = new int[2340];              // Snake Y

        public int FX;                              // Fruit X
        public int FY;                              // Fruit Y

        public int P = 5;                           // Amount of Parts the Snake has (Snake Length)

        public Random R = new Random();             // Random Value Generator

        public int D = 0;                           // Direction

        public bool O = false;                      // Opposite Movement Detection

        public bool SCS = false;                    // Start Animation Color Switcher;

        public int CL = Console.CursorLeft;         // Cursor Coordinates
        public int CT = Console.CursorTop;          // CL Cursor Left  |  CT Cursor Top
                                                    // Horizontal      |  Vertical

        public int S = 1;                           // Speed ( Delay [ms] )
        public string S_Text = "";

        public bool SecretMode = false;

        public bool GameOver = false;
        public bool Error = false;

        Thread titleThread = new Thread(new ThreadStart(TitleAnimation));


        private const int MF_BYCOMMAND = 0x00000000;                                    // Disable Window Resizing and Maximizing
        public const int SC_CLOSE = 0xF060;                                             // using the Win32 API
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        #region TitleStuff
        public static void TitleAnimation()
        {
            SnakeGame Snake = new SnakeGame();
            int i = 1;
            int T = 16;
            ConsoleColor U;
            ConsoleColor S;
            while (true)
            {
                switch (i)
                {
                    case 1:
                        U = ConsoleColor.DarkRed;
                        S = ConsoleColor.Red;
                        Snake.WriteTitle(U, S, T);
                        break;
                    case 2:
                        U = ConsoleColor.DarkYellow;
                        S = ConsoleColor.Yellow;
                        Snake.WriteTitle(U, S, T);
                        break;
                    case 3:
                        U = ConsoleColor.DarkGreen;
                        S = ConsoleColor.Green;
                        Snake.WriteTitle(U, S, T);
                        break;
                    case 4:
                        U = ConsoleColor.DarkCyan;
                        S = ConsoleColor.Cyan;
                        Snake.WriteTitle(U, S, T);
                        break;
                    case 5:
                        U = ConsoleColor.DarkBlue;
                        S = ConsoleColor.Blue;
                        Snake.WriteTitle(U, S, T);
                        break;
                    case 6:
                        U = ConsoleColor.DarkMagenta;
                        S = ConsoleColor.Magenta;
                        Snake.WriteTitle(U, S, T);
                        i = 0;
                        break;
                }

                i++;
                Console.SetCursorPosition(55, 23 );
                Console.ForegroundColor = ConsoleColor.Yellow;
                Thread.Sleep(4000);
            }
        }

        public void WriteTitle(ConsoleColor U, ConsoleColor S, int T)
        {
            CL = 26;
            CT = 5;

            Console.ForegroundColor = U;
            Console.SetCursorPosition(CL, CT);
            Console.WriteLine("██    ██  ██    ██  ████████");
            Thread.Sleep(T);
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine("██    ██   ██  ██        ██");
            Thread.Sleep(T);
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine("██    ██    ████        ██ ");
            Thread.Sleep(T); 
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine(" ██████      ██        ██   ");
            Thread.Sleep(T);
            Console.WriteLine();
            Thread.Sleep(T);
            CL = 7;
            Console.ForegroundColor = S;
            Console.SetCursorPosition(CL, Console.CursorTop);
            
            Console.WriteLine(" ████████     ██      ██        ██        ██     ███    ██████████");
            Thread.Sleep(T);
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine("██████████    ███     ██        ██        ██    ███     ██████████");
            Thread.Sleep(T);
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine("██       █    ████    ██       ████       ██   ███      ██        ");
            Thread.Sleep(T);
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine("██            █████   ██       ████       ██  ███       ██        ");
            Thread.Sleep(T);
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine("█████████     ██ ███  ██      ██  ██      ██████        ██████████");
            Thread.Sleep(T);
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine(" █████████    ██  ███ ██      ██  ██      ██████        ██████████");
            Thread.Sleep(T);
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine("        ██    ██   █████     ████████     ██  ███       ██        ");
            Thread.Sleep(T);
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine("█       ██    ██    ████     ████████     ██   ███      ██        ");
            Thread.Sleep(T);
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine("██████████    ██     ███    ██      ██    ██    ███     ██████████");
            Thread.Sleep(T);
            Console.SetCursorPosition(CL, Console.CursorTop);
            Console.WriteLine(" ████████     ██      ██    ██      ██    ██     ███    ██████████");
            Console.WriteLine();

        }
        #endregion TitleStuff


        #region StartScreen
        public void StartScreen()
        {
            void StartAnimationColorSwitcher()
            {
                if (SCS == false)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    SCS = true;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    SCS = false;
                }
            }

            void StartAnimation(int Round)
            {
                while (Console.CursorLeft < W - (2 + Round))
                {
                    Console.SetCursorPosition(CL, CT);
                    Console.Write(" ");
                    CL += 1;
                    Thread.Sleep(S);
                    StartAnimationColorSwitcher();
                }
                while (Console.CursorTop < H - (3 + Round))
                {
                    Console.SetCursorPosition(CL, CT);
                    Console.Write(" ");
                    CT += 1;
                    Thread.Sleep(S);
                    StartAnimationColorSwitcher();
                }
                while (Console.CursorLeft > (3 + Round))
                {
                    Console.SetCursorPosition(CL, CT);
                    Console.Write(" ");
                    CL -= 1;
                    Thread.Sleep(S);
                    StartAnimationColorSwitcher();
                }
                while (Console.CursorTop > (4 + Round))
                {
                    Console.SetCursorPosition(CL, CT);
                    Console.Write(" ");
                    CT -= 1;
                    Thread.Sleep(S);
                    StartAnimationColorSwitcher();
                }

                if (Round == 2)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(CL, CT);          // The Snake's Head
                        Console.Write(" ");
                        StartAnimationColorSwitcher();
                        CL += 1;
                    }
                    
                    Console.BackgroundColor = ConsoleColor.DarkRed; // The Snake's Eyes
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(CL, CT);
                    Console.Write("'");

                    Console.ResetColor();                           // The Snake's Tongue
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.SetCursorPosition(CL + 1, CT);
                    Console.Write("──<");
                }
            }

            Console.Title = "UV7 Snake Console Edition - Release v1.0";
            Console.Clear();
            Console.SetWindowSize(80, 32);
            Console.SetBufferSize(80, 32);
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 1);

            CL = Console.CursorLeft;
            CT = Console.CursorTop;

            Console.SetWindowSize(80, 32);
            Console.SetBufferSize(80, 32);
            Console.CursorVisible = false;

            Console.SetCursorPosition(23, 15);
            Console.Write("In order to avoid graphic problems, ");
            Console.SetCursorPosition(22, 16);
            Console.Write("please do not change the console size");

            Console.BackgroundColor = ConsoleColor.DarkRed;
            StartAnimation(0);
            StartAnimation(2);

            Console.SetWindowSize(80, 32);
            Console.SetBufferSize(80, 32);
            Console.CursorVisible = false;
            
            Console.SetCursorPosition(23, 15);
            Console.Write("                                    ");
            Console.SetCursorPosition(23, 16);
            Console.Write("                                    ");

            WriteTitle(ConsoleColor.DarkRed, ConsoleColor.Red, 2);
            
            CL = 24;
            CT = 19;

            Console.SetCursorPosition(CL, CT);
            Console.WriteLine();

            string title_string = "UV7 Snake C v1.0 - Programmed Aug-Dec 2022, Feb 2023 by Volvo7700";
            Console.SetCursorPosition((80 - title_string.Length) / 2, CT + 2);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(title_string);
            Console.WriteLine();

            Console.SetCursorPosition(CL + 1, CT + 4);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Press ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(CL + 7, CT + 4);
            Console.Write("ENTER");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(CL + 12, CT + 4);
            Console.Write(" to Start the Game");


            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(CL - 17, CT + 6);
            Console.Write("Windows : ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(CL - 7, CT + 6);
            Console.Write("For the best graphical results, press ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Alt + Space");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(", go to");
            Console.SetCursorPosition(CL - 13, CT + 7);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Properties");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(", then go to ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Font");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" and select ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Raster Font 12 x 16");

            if (titleThread.IsAlive)
            {
                titleThread.Resume();
            }
            else
            {
                titleThread.Start();
            }
            titleThread.IsBackground = true;

            Console.SetCursorPosition(CL + 30, CT + 4);
            Console.ForegroundColor = ConsoleColor.Yellow;
            string StartParam = Console.ReadLine();
            if (StartParam.ToLower() == "in special mode" || StartParam.ToLower() == "/s" || StartParam.ToLower() == "-s")
            {
                SecretMode = true;
            }

            titleThread.Suspend();
            Console.Clear();

            bool validInput = false;
            while (validInput == false)
            {

                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, 1);
                if (SecretMode)
                {
                    Console.WriteLine("Secret Cheat Mode enabled!");
                    Console.ResetColor();
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(1, 3);
                    Console.WriteLine("You will be immortal");
                    Console.WriteLine();
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Game Speed Selection");
                    Console.ResetColor();
                    Console.SetCursorPosition(1, 3);
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Key Speed       Delay\n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("                200\n" +
                    "                100\n" +
                    "                 75\n" +
                    "                 50\n" +
                    "                 25\n");

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 6);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("    Yawn\n" +
                    "    Slow\n" +
                    "    Normal\n" +
                    "    Fast\n" +
                    "    Hyperspeed\n\n" +
                    "    Custom");

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 7);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" Y\n S\n N\n F\n H\n\n C\n\n");

                Console.WriteLine("Please enter game speed:");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(25, Console.CursorTop - 1);

                ConsoleKeyInfo speedSelection = new ConsoleKeyInfo();
                speedSelection = Console.ReadKey();

                switch (speedSelection.Key)
                {
                    case ConsoleKey.Y:
                        S = 200;
                        S_Text = "YAWN";
                        validInput = true;
                        break;
                    case ConsoleKey.S:
                        S = 100;
                        S_Text = "SLOW";
                        validInput = true;
                        break;
                    case ConsoleKey.N:
                        S = 75;
                        S_Text = "NORM";
                        validInput = true;
                        break;
                    case ConsoleKey.F:
                        S = 50;
                        S_Text = "FAST";
                        validInput = true;
                        break;
                    case ConsoleKey.H:
                        S = 25;
                        S_Text = "HYSP";
                        validInput = true;
                        break;
                    case ConsoleKey.C:
                        Console.ForegroundColor = ConsoleColor.White;
                        S_Text = "CSTM";
                        Console.Write(" Custom : ");
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            S = Int32.Parse(Console.ReadLine());
                            if (S < 513)
                            {
                                validInput = true;
                            }
                            else
                            {
                                validInput = false;
                            }
                        }
                        catch (FormatException)
                        {
                            validInput = false;
                        }
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.Write("  ");

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("OK");
            Console.SetCursorPosition(Console.CursorLeft + 2, Console.CursorTop);
            Console.Write("Starting Game...");
            Thread.Sleep(1000);

            Console.ResetColor();
            Console.Clear();
            Game(false);
        }
        #endregion StartScreen


        #region Game
        public void Game(Boolean resume)
        {
            titleThread.Suspend();

            if (SecretMode) Console.Title = "UV7 Snake Console Edition Release Candidate 2 - Secret Mode enabled";

            if (!resume)
            {
                CL = 0;
                CT = 0;

                X[0] = 40;
                Y[0] = 12;

                FX = R.Next(2, (W - 2));
                FY = R.Next(2, (H - 2));
            }

            ConsoleKeyInfo inputKey = new ConsoleKeyInfo();
            
            void WriteOuterBoard()
            {
                Console.Clear();
                CL = 0;
                CT = 0;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                while (Console.CursorLeft < W - 1)
                {
                    Console.SetCursorPosition(CL, CT);
                    Console.Write(" ");
                    CL += 1;
                }
                Console.SetBufferSize(81, 32);
                while (Console.CursorTop < H - 2)
                {
                    Console.SetCursorPosition(CL, CT);
                    Console.Write(" ");
                    CT += 1;
                }
                while (Console.CursorLeft > 2)
                {
                    Console.SetCursorPosition(CL, CT);
                    Console.Write(" ");
                    CL -= 1;
                }
                Console.SetBufferSize(80, 32);
                while (Console.CursorTop > 1)
                {
                    Console.SetCursorPosition(CL, CT);
                    Console.Write(" ");
                    CT -= 1;
                }

                Console.BackgroundColor = ConsoleColor.DarkGray;

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, 31);
                Console.Write("Speed : ");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(19, 31);
                Console.Write("│");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Head X :    ");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("│");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Head Y :    ");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("│");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Direction :   ");
                
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("│");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Length : ");

                Console.SetCursorPosition(1, 1);
                Console.BackgroundColor = ConsoleColor.Black;
                while (Console.CursorTop < (H - 1))
                {
                    Console.SetCursorPosition(1, Console.CursorTop);
                    Console.WriteLine("                                                                              ");
                }

                Console.ResetColor();
            }

            void WriteInnerBoard()
            {
                
                /*
                Console.BackgroundColor = ConsoleColor.Black;
                while (Console.CursorTop < (H - 1))
                {
                    Console.SetCursorPosition(1, Console.CursorTop);
                    Console.WriteLine("                                                                              ");
                }*/

                Console.BackgroundColor = ConsoleColor.DarkGray;
                
                if (SecretMode) Console.ForegroundColor = ConsoleColor.Green;
                else Console.ForegroundColor = ConsoleColor.Cyan;

                Console.SetCursorPosition(10, 31);
                if (S.ToString().Length == 1)
                {
                    Console.Write("00" + S);
                }
                else if (S.ToString().Length == 2)
                {
                    Console.Write("0" + S);
                }
                else
                {
                    Console.Write(S);
                }
                Console.SetCursorPosition(14, 31);
                Console.Write(S_Text);

                Console.SetCursorPosition(30, 31); // X Coordinate Head
                if (X[0].ToString().Length == 1)
                {
                    Console.Write("0" + X[0]);
                }
                else
                {
                    Console.Write(X[0]);
                }

                Console.SetCursorPosition(44, 31); // Y Coordinate Head
                if (Y[0].ToString().Length == 1)
                {
                    Console.Write("0" + Y[0]);
                }
                else
                {
                    Console.Write(Y[0]);
                }


                Console.SetCursorPosition(61, 31);  // Direction
                if (D == 1)
                {
                    Console.Write("▲");
                }
                else if (D == 2)
                {
                    Console.Write("◄");
                }
                else if (D == 3)
                {
                    Console.Write("▼");
                }
                else if (D == 4)
                {
                    Console.Write("►");
                }
                else
                {
                    Console.Write("■");
                }

                Console.SetCursorPosition(74, 31);  // Length
                if (P.ToString().Length == 1)
                {
                    Console.Write("000" + (P - 1));
                }
                else if (P.ToString().Length == 2)
                {
                    Console.Write("00" + (P - 1));
                }
                else if (P.ToString().Length == 3)
                {
                    Console.Write("0" + (P - 1));
                }
                else
                {
                    Console.Write(P - 1);
                }
            }

            void Input()
            {
                if (Console.KeyAvailable)
                {
                    inputKey = Console.ReadKey(true);
                }
            }

            void WritePoint(int x, int y)
            {
                if (x == 0 || y == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                Console.SetCursorPosition(x, y);
                Console.Write(" ");
                Console.ResetColor();
            }

            void Logic()
            {
                if (X[0] == FX && Y[0] == FY)       // Execute if the Snake's Head matches the Fruit's position
                {
                    P += 2;                         // Make the Snake 2 Parts longer
                    FX = R.Next(2, (W - 2));        // Generate the Fruit's Random Positions - X Coordinate
                    FY = R.Next(2, (H - 2));        // Y Coordinate
                    Console.Beep();
                }

                for (int i = P; i > 1; i--)          // Shift the Coordinates of the Snake back so it is able to follow [0]
                {
                    X[i - 1] = X[i - 2];
                    Y[i - 1] = Y[i - 2];
                    X[P + 1] = P;
                    Y[P + 1] = P;
                }

                X[P + 1] = P;
                Y[P + 1] = P;

                if (inputKey.Key == ConsoleKey.W || inputKey.Key == ConsoleKey.A || inputKey.Key == ConsoleKey.S || inputKey.Key == ConsoleKey.D)
                {
                                                        //                                   (W)-UP
                    switch (inputKey.Key)               // React to the Key Inputs: LEFT-(A) (S) (D)-RIGHT
                    {                                   //                               DOWN/
                        case ConsoleKey.W:              // Change the Snake's Head's Coordinates (up/left/down/right)
                            if (Y[0] == 1) Y[0] = 31;   // Make the Snake go through the gray wall if it's heading for it
                            if (D == 3)
                            {
                                Y[0]++;
                            }
                            else
                            {
                                Y[0]--;
                                D = 1;
                            }
                            break;
                        case ConsoleKey.A:
                            if (X[0] == 1) X[0] = 79;   // Make the Snake go through the gray wall if it's heading for it
                            if (D == 4)
                            {
                                X[0]++;
                            }
                            else
                            {
                                X[0]--;
                                D = 2;
                            }
                            break;
                        case ConsoleKey.S:
                            if (Y[0] == 30) Y[0] = 0;   // Make the Snake go through the gray wall if it's heading for it
                            if (D == 1)
                            {
                                Y[0]--;
                            }
                            else
                            {
                                Y[0]++;
                                D = 3;
                            }
                            break;
                        case ConsoleKey.D:
                            if (X[0] == 78) X[0] = 0;   // Make the Snake go through the gray wall if it's heading for it
                            if (D == 2)
                            {
                                 X[0]--;
                            }
                            else
                            {
                                X[0]++;
                                D = 4;
                            }
                            break;
                    }
                }
                else
                {
                    switch (D)                          // If a key other than WASD was pressed, the snake would stop.
                    {
                        case 1:                         // To prevent this from happening, D (Direction) is used as a backup value here.
                            if (Y[0] == 1) Y[0] = 31;   // This makes the Snake go the same direction as it did before as long as WASD is not pressed.
                            Y[0]--;
                            break;
                        case 2:
                            if (X[0] == 1) X[0] = 79;
                            X[0]--;
                            break;
                        case 3:
                            if (Y[0] == 30) Y[0] = 0;
                            Y[0]++;
                            break;
                        case 4:
                            if (X[0] == 78) X[0] = 0;
                            X[0]++;
                            break;
                    }
                }
                

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;

                try
                {
                    Console.SetCursorPosition(X[0], Y[0]);              // Draw the Snake's Head
                }
                catch (ArgumentOutOfRangeException)
                {
                    GameOver = true;
                    Error = true;
                    PauseScreen();
                }
                
                switch (D)
                {
                    case 1:
                        Console.Write(" ");
                        break;
                    case 2:
                        Console.Write("'");
                        break;
                    case 3:
                        Console.Write("\"");
                        break;
                    case 4:
                        Console.Write("'");
                        break;
                }

                for (int i = 1; i <= P - 1; i++)                    // Draw the rest of the Snake    
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    WritePoint(X[i], Y[i]);
                    if (SecretMode) Console.BackgroundColor = ConsoleColor.Cyan;
                    else Console.BackgroundColor = ConsoleColor.DarkGreen;
                    WritePoint(FX, FY);
                }

                if (!(X[P - 1] == X[0] && Y[P - 1] == Y[0]))
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    WritePoint(X[P - 1], Y[P - 1]);
                }

                if (P > 5)                                          // Internal Collision Check
                {
                    bool collision = false;
                    for (int i = 1; i < P; i++)
                    {
                        if (X[i] == X[0] && Y[i] == Y[0])
                        {
                            collision = true;
                        }

                        if (collision) break;
                    }

                    if (collision)
                    {
                        GameOver = true;
                        PauseScreen();
                    }
                }

                Thread.Sleep(S);
            }

            WriteOuterBoard();
            while (true)
            {
                WriteInnerBoard();
                Input();
                Logic();
                if (inputKey.Key == ConsoleKey.Escape)
                {
                    break;
                }
                if (inputKey.Key == ConsoleKey.Spacebar)
                {
                    Console.ReadKey(true);
                    continue;
                }
            }
            
            PauseScreen();
        }
        #endregion Game


        #region PauseScreen
        public void PauseScreen()
        {
            Console.ResetColor();
            Console.Clear();

            //WriteTitle(ConsoleColor.DarkRed, ConsoleColor.Red, 2);

            if (titleThread.IsAlive) titleThread.Resume();
            else titleThread.Start();

            void WriteGameStats()
            {
                CL = 0;
                CT = 22; 
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(CL, CT);
                Console.Write("       Your Current Length : ");
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                if (P.ToString().Length == 1)
                {
                    Console.Write("000" + (P + 1));
                }
                else if (P.ToString().Length == 2)
                {
                    Console.Write("00" + (P + 1));
                }
                else if (P.ToString().Length == 3)
                {
                    Console.Write("0" + (P + 1));
                }
                else
                {
                    Console.Write(P + 1);
                }

                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                CL = Console.CursorLeft + 25;
                CT = Console.CursorTop;
                Console.SetCursorPosition(CL, CT);

                if (GameOver)
                {
                    Console.WriteLine("  - Game Over -");
                }
                else
                {
                    Console.WriteLine("- Game Paused -");
                }
                
                if (Error)
                {
                    Console.SetCursorPosition(CL, CT);
                    Console.WriteLine(" - Game Error -");
                }

                Console.WriteLine();
                Console.WriteLine();
                if (!Error)
                {
                    if (GameOver && !SecretMode)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("       Continue            : Press ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Escape");
                    }
                    else
                    {
                        Console.Write("       Continue            : Press ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Escape");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("       Continue            : Press ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Escape");
                }
                

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.Write("       Return to Main Menu : Press ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Enter");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.Write("       Quit                : Press ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("E or Ctrl + C");
            }

            void EndScreen()
            {
                if (titleThread.IsAlive) titleThread.Abort();
                Console.Clear();
                CL = 30;
                CT = 8;
                Console.SetCursorPosition(CL, CT);
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("                    ");
                Console.SetCursorPosition(CL, CT + 1);
                Console.Write("    UV7 Tetris C    ");
                Console.SetCursorPosition(CL, CT + 2);
                Console.Write("                    ");

                CL = 29;
                CT = 16;

                Console.SetCursorPosition(CL, CT);
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Thank you for playing!");

                Console.ResetColor();
                Console.SetCursorPosition(0, 20);
                Console.CursorVisible = true;

                Thread.Sleep(3000);

                Environment.Exit(0);
            }

            Thread.Sleep(900);

            Console.Clear();
            WriteGameStats();

            
            Console.ForegroundColor = ConsoleColor.Black;
            Thread.Sleep(100);

            CL = Console.CursorLeft;
            CT = Console.CursorTop;

            bool validInput = false;
            while (validInput == false)
            {
                Console.SetCursorPosition(CL, CT);
                ConsoleKeyInfo pauseKey = Console.ReadKey();

                switch (pauseKey.Key)
                {
                    case ConsoleKey.Escape:
                        if (!Error)
                        {
                            if (GameOver && !(SecretMode))
                            {
                                validInput = false;
                            }
                            else
                            {
                                titleThread.Suspend();
                                validInput = true;
                                Game(true);
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        Array.Clear(X, 0, X.Length);
                        Array.Clear(X, 0, X.Length);
                        titleThread.Suspend();
                        FX = 0;
                        FY = 0;
                        P = 5;
                        D = 0;
                        S = 1;
                        S_Text = "";
                        SecretMode = false;
                        GameOver = false;
                        Error = false;

                        validInput = true;
                        Console.ResetColor();
                        StartScreen();
                        break;
                    case ConsoleKey.E:
                        validInput = true;
                        EndScreen();
                        break;
                }
            }
            
            Console.ResetColor();
        }
        #endregion PauseScreen


        public void BlueScreen(ConsoleColor backColor, ConsoleColor foreColor, string title, string t1, string t2, string t3, string t4, string t5)
        {
            Console.BackgroundColor = backColor;
            Console.Clear();
            Thread.Sleep(500);
            Console.BackgroundColor = foreColor;
            Console.ForegroundColor = backColor;
            Console.SetCursorPosition((W - title.Length) / 2, 6);
            Console.Write(title);
            Thread.Sleep(200);
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = foreColor;
            CL = 6;
            CT = 9;
            Console.SetCursorPosition(CL, CT);
            Console.Write("A fatal error INVALID_VID_MODE has occured in GAME_INIT");
            Console.SetCursorPosition(CL, CT + 1);
            Thread.Sleep(100);
            Console.Write("The Game has been stopped to prevent damage or something like that");
            Console.SetCursorPosition(CL, CT + 4);
            Thread.Sleep(100);
            Console.Write("The Problem seems to be caused by a font scaling which is too large");
            Console.SetCursorPosition(CL, CT + 5);
            Thread.Sleep(100);
            Console.Write("for your computer's screen to display.");
            Console.SetCursorPosition(CL, CT + 7);
            Thread.Sleep(100);
            Console.Write("Try changing the font scaling to a smaller template.");

            Console.SetCursorPosition(25, 24);
            Thread.Sleep(200);
            Console.Write("Press any key to continue ");
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.ReadKey();
            Console.ResetColor();
            Console.CursorVisible = false;
            StartScreen();
        }
        
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 32);
            Console.SetBufferSize(80, 32);
            var snake = new SnakeGame();
            //snake.BlueScreen(ConsoleColor.DarkBlue, ConsoleColor.Gray, "UV7 Snake C", "Error", "", "", "", "");
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                //DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);      
                //DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }

            var Snake = new SnakeGame();
            Snake.StartScreen();            
        }
    }
}
