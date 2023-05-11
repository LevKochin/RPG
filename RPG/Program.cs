using System.Numerics;

namespace RPG
{
    class Program
    {
        static string[,] field = new string[,]
            {
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
                { "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_", "_" },
            };
        static Random random = new Random();
        static int yFieldLength = field.GetLength(0);
        static int xFieldLength = field.GetLength(1);
        static string enemy = "O";
        static char player = (char)17;
        static int xPlayerCoord = 0;
        static int yPlayerCoord = 0;
        static int enemyXCoord = 0;
        static int enemyYCoord = 0;

        static bool isGameContinue = true;

        static void Main()
        {
            InitPlayer();
            InitEnemy();

            // Вызов метода
            BrowsMap();
            while (isGameContinue)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (yPlayerCoord != 0)
                        {
                            player = (char)30;
                            Console.SetCursorPosition(xPlayerCoord, yPlayerCoord);
                            Console.WriteLine("_");
                            Console.SetCursorPosition(xPlayerCoord, yPlayerCoord -= 1);
                            Console.WriteLine(player);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (yPlayerCoord != yFieldLength - 1)
                        {
                            player = (char)31;
                            Console.SetCursorPosition(xPlayerCoord, yPlayerCoord);
                            Console.WriteLine("_");
                            Console.SetCursorPosition(xPlayerCoord, yPlayerCoord += 1);
                            Console.WriteLine(player);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (xPlayerCoord != 0)
                        {
                            player = (char)17;
                            Console.SetCursorPosition(xPlayerCoord, yPlayerCoord);
                            Console.WriteLine("_");
                            Console.SetCursorPosition(xPlayerCoord -= 1, yPlayerCoord);
                            Console.WriteLine(player);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (xPlayerCoord != xFieldLength - 1)
                        {
                            player = (char)16;
                            Console.SetCursorPosition(xPlayerCoord, yPlayerCoord);
                            Console.WriteLine("_");
                            Console.SetCursorPosition(xPlayerCoord += 1, yPlayerCoord);
                            Console.WriteLine(player);
                        }
                        break;
                }
            }
        }

        // Определение метода
        static void BrowsMap()
        {
            for (int y = 0; y < yFieldLength; y++)
            {
                for (int x = 0; x < xFieldLength; x++)
                {
                    Console.Write(field[y, x]);
                }
                Console.WriteLine();
            }
        }


        static void InitPlayer()
        {
            xPlayerCoord = random.Next(xFieldLength);
            yPlayerCoord = random.Next(yFieldLength);
            field[yPlayerCoord, xPlayerCoord] = player.ToString();
        }

        static void InitEnemy()
        {
            enemyXCoord = random.Next(xFieldLength);
            enemyYCoord = random.Next(yFieldLength);
            while (xPlayerCoord == enemyXCoord && enemyYCoord == yPlayerCoord)
            {
                enemyXCoord = random.Next(xFieldLength);
                enemyYCoord = random.Next(yFieldLength);
            }
            field[enemyYCoord, enemyXCoord] = enemy;
        }
    }
}