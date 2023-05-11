namespace HorizonFantazy
{
    class Program
    {
        static void BrowseMap()
        {
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    Console.Write("_");
                }
                Console.WriteLine();
            }
        }
        static void Main()
        {
            Console.CursorVisible = false;
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.BufferHeight = 101;


            Console.SetCursorPosition(0, 0);
            Console.Write((char)06);
            Console.WriteLine();
            while(true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.W:
                        if (Console.GetCursorPosition().Top != 0)
                        {
                            Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
                            Console.Write("_");
                            Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                            Console.Write((char)06);

                        }
                        break;

                    case ConsoleKey.S:
                        if (Console.GetCursorPosition().Top != Console.LargestWindowHeight)
                        {
                            Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
                            Console.Write("_");
                            Console.SetCursorPosition(0, Console.GetCursorPosition().Top + 1);
                            Console.Write((char)06);

                        }
                        break;


                }
            }
        } 
    }
}