
namespace Tanks
{
    using System.Runtime.InteropServices;
    class Program
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto),/* SetLastError = true*/]
        private static extern bool ShowWindow( IntPtr hWnd, int nCmdShow );
        private const int MAXIMIZE = 3;

        static Random random = new Random();
        // Двух мерный массив состоит из статического количества элементов.
        // Для создания динамической системы уровеней. Мы можем ввести динамическое количество элементов в массиве.
        static string[,] field =
            {
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
                   {"_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_","_" },
            };

        // Массив, который подразумевает 1 уровень, и содержит 10 рядов и 10 столбцов 
        static string[,] levelOneField = new string[10, 10];

        static int countOfMassiveYAxis = field.GetLength(0);
        static int countOfMassiveXAxis = field.GetLength(1);
        static string fieldElement = "_";
        static char player = (char)17;
        static string playersYBullet = "|";
        static string playersXBullet = "-";
        static int currentPlayerDirection = 0; // 1 - 4, например 1 - это влево, 2 - это вверх, 3 - это вправо, 4 - это вниз
        static int playerCoordX = 0;
        static int playerCoordY = 0;

        // Создание универсальных полей, которые содержат состояние нахождения субъекта на определённой границе "поля"
        static bool isSubjectOnToRightBorder;
        static bool isSubjectOnToLeftBorder;
        static bool isSubjectOnToTopBorder;
        static bool isSubjectOnToBottomBorder;

        static bool isGameContinue = true;

        /// <summary>
        /// Консольное приложение, которое выполняет игровые инструкции. Под игровыми инструкциями, подразумеваются методы, которые будут воплощать функционал игры.
        /// </summary>
        static void Main()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(GetConsoleWindow(), MAXIMIZE);

            // Первый шаг - добавление танка в случайное положение
            GetPlayer();

            Enemy[] enemies = new Enemy[3]
            {
                new Enemy(playerCoordX, playerCoordY, ref field),
                new Enemy(playerCoordX, playerCoordY, ref field),
                new Enemy(playerCoordX, playerCoordY, ref field),
            };

            int enemy1XCoord = enemies[1].xCoord;

            // Шаг второй - добавление соперников в случайное положение

            BrowsMap();
            // Прохождение игры
            while (isGameContinue)
            {
                ConsoleKey key = Console.ReadKey(false).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (playerCoordY != 0)
                        {
                            Console.SetCursorPosition(playerCoordX, playerCoordY);
                            Console.WriteLine(fieldElement);
                            currentPlayerDirection = 2;
                            GetPlayerDirection();
                            Console.SetCursorPosition(playerCoordX, playerCoordY = playerCoordY - 1);
                            Console.WriteLine(player);

                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (playerCoordY != countOfMassiveYAxis - 1)
                        {
                            Console.SetCursorPosition(playerCoordX, playerCoordY);
                            Console.WriteLine(fieldElement);
                            currentPlayerDirection = 4;
                            GetPlayerDirection();
                            Console.SetCursorPosition(playerCoordX, playerCoordY = playerCoordY + 1);
                            Console.WriteLine(player);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (playerCoordX != 0)
                        {
                            Console.SetCursorPosition(playerCoordX, playerCoordY);
                            Console.WriteLine(fieldElement);
                            currentPlayerDirection = 1;
                            GetPlayerDirection();
                            Console.SetCursorPosition(playerCoordX = playerCoordX - 1, playerCoordY);
                            Console.WriteLine(player);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (playerCoordX != countOfMassiveXAxis -1)
                        {
                            Console.SetCursorPosition(playerCoordX, playerCoordY);
                            Console.WriteLine(fieldElement);
                            currentPlayerDirection = 3;
                            GetPlayerDirection();
                            Console.SetCursorPosition(playerCoordX = playerCoordX + 1, playerCoordY);
                            Console.WriteLine(player);
                        }
                        break;
                    case ConsoleKey.W:
                        currentPlayerDirection = 2;
                        GetPlayerDirection();
                        Console.SetCursorPosition(playerCoordX, playerCoordY);
                        Console.WriteLine(fieldElement);
                        break;
                    case ConsoleKey.D:
                        currentPlayerDirection = 3;
                        GetPlayerDirection();
                        Console.SetCursorPosition(playerCoordX, playerCoordY);
                        Console.WriteLine(fieldElement);
                        break;
                    case ConsoleKey.S:
                        currentPlayerDirection = 4;
                        GetPlayerDirection();
                        Console.SetCursorPosition(playerCoordX, playerCoordY);
                        Console.WriteLine(fieldElement);
                        break;
                    case ConsoleKey.A:
                        currentPlayerDirection = 1;
                        GetPlayerDirection();
                        Console.SetCursorPosition(playerCoordX, playerCoordY);
                        Console.WriteLine(fieldElement);
                        break;
                    case ConsoleKey.Spacebar:
                        Shoot(currentPlayerDirection);
                        break;
                    default:
                        break;
                }
            }
        }

        static void BrowsMap()
        {

            for (int y = 0; y < countOfMassiveYAxis; y++)
            {
                for (int x = 0; x < countOfMassiveXAxis; x++)
                {
                    Console.Write(field[y, x]);
                }
                Console.WriteLine();
            }
        }

        static void GetPlayerDirection()
        {
            switch (currentPlayerDirection)
            {
                case 1:
                    player = (char)17;
                    break;
                case 2:
                    player = (char)30;
                    break;
                case 3:
                    player = (char)16;
                    break;
                case 4:
                    player = (char)31;
                    break;
                default:
                    break;
            }
        }

        static void GetPlayer()
        {
            playerCoordY = random.Next(0, countOfMassiveYAxis);
            playerCoordX = random.Next(0, countOfMassiveXAxis);

            // Направление от 1 до 4
            currentPlayerDirection = random.Next(1, 5);

            //    случайное число от 0 до 5           случайное число от 0 до 6
            field[playerCoordY, playerCoordX] = player.ToString();
        }

        static void Shoot( int currentDirection )
        {
            switch (currentDirection)
            {
                case 1:
                    if (playerCoordX != 0)
                    {
                        for (int i = 1; i < playerCoordX + 1; i++)
                        {
                            Console.SetCursorPosition(playerCoordX - i, playerCoordY);
                            Console.WriteLine(playersXBullet);
                            Thread.Sleep(30);
                            if (i != 1)
                            {
                                Console.SetCursorPosition(playerCoordX - i + 1,playerCoordY);
                                Console.WriteLine(fieldElement);
                                Thread.Sleep(30);
                                if (i == playerCoordX)
                                {
                                    Console.SetCursorPosition(playerCoordX - i, playerCoordY);
                                    Console.WriteLine(fieldElement);
                                }
                            }

                            Thread.Sleep(30);
                        }
                    }
                    break;
                case 2:
                    if (playerCoordY != 0)
                    {
                        for (int i = 1; i < playerCoordY + 1; i++)
                        {
                            Console.SetCursorPosition(playerCoordX, playerCoordY - i);
                            Console.WriteLine(playersYBullet);
                            Thread.Sleep(30);
                            if (i != 1)
                            {
                                Console.SetCursorPosition(playerCoordX, playerCoordY - i + 1);
                                Console.WriteLine(fieldElement);
                                if (i == playerCoordY)
                                {
                                    Console.SetCursorPosition(playerCoordX, playerCoordY - i);
                                    Console.WriteLine(fieldElement);
                                };
                            }
                            Thread.Sleep(30);
                        }
                    }
                    break;
                case 3:
                    if (playerCoordX != countOfMassiveXAxis -1)
                    {
                        for (int i = 1; i < (countOfMassiveXAxis - playerCoordX); i++)
                        {
                            Console.SetCursorPosition(playerCoordX + i, playerCoordY);
                            Console.WriteLine(playersXBullet);
                            Thread.Sleep(30);
                            if (i != 1)
                            {
                                Console.SetCursorPosition(playerCoordX + i - 1, playerCoordY);
                                Console.WriteLine(fieldElement);
                                if (i == (countOfMassiveXAxis - playerCoordX) - 1)
                                {
                                    Console.SetCursorPosition(playerCoordX + i, playerCoordY);
                                    Console.WriteLine(fieldElement);
                                }
                            }
                            Thread.Sleep(30);
                        }
                    }
                    break;
                case 4:
                    if (playerCoordY != countOfMassiveYAxis - 1)
                    {
                        for (int i = 1; i < (countOfMassiveYAxis - playerCoordY); i++)
                        {
                            Console.SetCursorPosition(playerCoordX, playerCoordY + i + 1);
                            Console.WriteLine(playersYBullet);
                            Thread.Sleep(30);
                            if (i != 1)
                            {
                                Console.SetCursorPosition(playerCoordX, playerCoordY + i);
                                Console.WriteLine(fieldElement);
                                if (i == (countOfMassiveYAxis - playerCoordY))
                                {
                                    Console.SetCursorPosition(playerCoordX, playerCoordY + i + 1);
                                    Console.WriteLine(fieldElement);

                                }
                            }
                            Thread.Sleep(30);
                        }
                    }
                    break;
            }
        }
    }
}