using System;

namespace UV7_Snake_C
{
	class Old
    {
        public void OldStartScreen()
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
                Console.BackgroundColor = ConsoleColor.DarkRed;

                while (Console.CursorLeft < W - (3 + Round))
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
                while (Console.CursorLeft > (4 + Round))
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

                if (Round == 6)
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

            Console.Title = "UV7 Snake Console Edition Pre-Release";
            Console.Clear();
            Console.SetWindowSize(80, 32);
            Console.SetBufferSize(80, 32);
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 1);

            CL = Console.CursorLeft;
            CT = Console.CursorTop;

            StartAnimation(0);
            StartAnimation(2);
            StartAnimation(4);
            StartAnimation(6);
            
            CL = 24;
            CT = 11;

            Console.SetCursorPosition(CL, CT);
            Console.WriteLine();

            Console.SetCursorPosition(CL, CT + 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("UV7 Snake C Pre-Release");
            Console.SetCursorPosition(CL, CT + 3);
            Console.WriteLine("Programmed Aug 2022 by Volvo7700");
            Console.WriteLine();

            Console.SetCursorPosition(CL, CT + 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Press ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(CL + 6, CT + 5);
            Console.Write("ENTER");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(CL + 11, CT + 5);
            Console.Write(" to Start the Game");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(CL - 9, CT + 7);
            Console.Write("For the best graphical results, press ");
            Console.ForegroundColor = ConsoleColor.Gray;
            //Console.SetCursorPosition(CL + 29, CT + 7);
            Console.Write("Alt + Space");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(",");
            Console.SetCursorPosition(CL - 10, CT + 8);
            Console.Write("go to ");
            Console.ForegroundColor = ConsoleColor.Gray;
            //Console.SetCursorPosition(CL - 4, CT + 8);
            Console.Write("Properties\\Font ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            //Console.SetCursorPosition(CL + 12, CT + 8);
            Console.Write("and select ");
            Console.ForegroundColor = ConsoleColor.Gray;
            //Console.SetCursorPosition(CL + 23, CT + 8);
            Console.Write("Raster Font 12 x 16");

            Console.SetCursorPosition(CL + 30, CT + 5);
            Console.ForegroundColor = ConsoleColor.Yellow;
            string StartParam = Console.ReadLine();
            if (StartParam.ToLower() == "in special mode")
            {
                SpecialMode = true;
            }
            Console.Clear();
        }

        public void OldGame()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(40, 16);
            while (true)
            {
                CL = Console.CursorLeft;
                CT = Console.CursorTop;

                var userInput = Console.ReadKey(true).Key;
                switch (userInput)
                {
                    case ConsoleKey.W:
                        D = 1;
                        break;
                    case ConsoleKey.A:
                        D = 2;
                        break;
                    case ConsoleKey.S:
                        D = 3;
                        break;
                    case ConsoleKey.D:
                        D = 4;
                        break;
                }

                if (userInput == ConsoleKey.Escape)
                {
                    break;
                }

                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("D  : 0" + D);
                if (CL.ToString().Length == 1)
                {
                    Console.WriteLine("CL : 0" + CL);
                }
                else
                {
                    Console.WriteLine("CL : " + CL);
                }
                if (CT.ToString().Length == 1)
                {
                    Console.WriteLine("CT : 0" + CT);
                }
                else
                {
                    Console.WriteLine("CT : " + CT);
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.SetCursorPosition(CL, CT);

                switch (D)
                {
                    case 1:
                        Console.SetCursorPosition(CL - 1, CT - 1);
                        break;
                    case 2:
                        Console.SetCursorPosition(CL - 2, CT);
                        break;
                    case 3:
                        Console.SetCursorPosition(CL - 1, CT + 1);
                        break;
                    case 4:
                        Console.SetCursorPosition(CL, CT);
                        break;
                }

                Console.Write("█");

                Thread.Sleep(S);
            }
        }

        public void OldTitle(ConsoleColor U, ConsoleColor S, int T)
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
    }

    
    int IX = Array.LastIndexOf(X, X[0]);                // Internal Collision Check [ O L D ]
    int IY = Array.LastIndexOf(Y, Y[0]);

    if (P > 5)
    {
        if (IX != 0 && IY != 0)
        {
            if (X[IX] == X[0] && Y[IY] == Y[0] && IX == IY)
            {
                GameOver = true;
                PauseScreen();
            }
        }
    }
                


    // Internal collision check (VERY OLD)
    if (P > 4)                                          // Only check for internal collision if the Snake has already grown once
    {
        int X_Match = X.Count(x => x == X[0]);          // Check if the Snake's head bumps into its body
        int Y_Match = Y.Count(x => x == Y[0]);      // --> Check if the Value Appears 2 times (first value X[0] / Y[0] and second value X[?] and Y[?])
        if (X_Match == 2 && Y_Match == 2 && GameOver == false)
        { 
            GameOver = true;
            PauseScreen();
        }
    }


    int CheckCoordsX(int[] arr, int sValue)                 // Define the two anti-collision methods
    {
        int count = 0;
        int ret_index = -1;

        for (int index = 0; index < arr.Length; index++)
        {
            if (arr[index] == sValue)
            {
                ret_index = index;
                count++;
                if (count == 2)
                {
                    return ret_index;
                }
            }

        }
        return -1;
    }
    int CheckCoordsY(int[] arr, int sValue)                 // Define the two anti-collision methods
    {
        int count = 0;
        int ret_index = -1;

        for (int index = 0; index < arr.Length; index++)
        {
            if (arr[index] == sValue)
            {
                ret_index = index;
                count++;
                if (count == 2)
                {
                    return ret_index;
                }
            }

        }
        return -1;
    }

    if (P > 4)                                          // Only check if the coordinates appear twice if the Snake has already grown once
    {                    
        int XD = CheckCoordsX(X, X[0]);
        int YD = CheckCoordsY(X, X[0]);

        if (XD != -1 && YD != -1 && XD > 4 && YD > 4)   // Check if there were second coordinates found AND if they were after index 4
        {
            if (XD == YD)                               // Check if the are both at the same index on the snake
            {
                if (XD == X[0] && YD == Y[0])           // Check if the coordinates match
                {
                    GameOver = true;
                    PauseScreen();
                }
            }
        }
    }
}