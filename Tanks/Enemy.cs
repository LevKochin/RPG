namespace Tanks
{
    internal class Enemy
    {
        internal Enemy( int playerCoordX, int playerCoordY, ref string[,] field )
        {
            InitializeEnemy(playerCoordX, playerCoordY, ref field);
        }

        string enemyForm { get; set; }
        internal int xCoord { get; set; }
        internal int yCoord { get; set; }
        internal int Lifes { get; set; }

        private void InitializeEnemy( int playerCoordX, int playerCoordY, ref string[,] field )
        {
            Random rand = new Random();
            this.xCoord = rand.Next(0, field.GetLength(1));
            this.yCoord = rand.Next(0, field.GetLength(0));

            // Соответствуют ли координаты соперника координатам игрока
            bool enemyCoordIsEqualPLayersCoord = playerCoordX == this.xCoord && playerCoordY == this.yCoord;


            bool isEnenmyIntoField = field[this.yCoord, this.xCoord] == this.enemyForm;


            bool isMiddleAreaIntoPlayerArea =
                playerCoordY == this.yCoord &&
                (playerCoordX + 1 == this.xCoord || playerCoordX - 1 == this.xCoord);
            bool isTopAreaIntoPlayerArea = playerCoordY == this.yCoord + 1 &&
                (playerCoordX == this.xCoord || playerCoordX == this.xCoord + 1 ||
                playerCoordX == this.xCoord - 1);
            bool isBottomAreaIntoPlayerArea = playerCoordY == this.yCoord - 1 &&
                (playerCoordX == this.xCoord || playerCoordX == this.xCoord + 1 ||
                playerCoordX == this.xCoord - 1);

            while (isMiddleAreaIntoPlayerArea || isTopAreaIntoPlayerArea || isBottomAreaIntoPlayerArea || enemyCoordIsEqualPLayersCoord || isEnenmyIntoField)
            {
                this.xCoord = rand.Next(0, field.GetLength(1));
                this.yCoord = rand.Next(0, field.GetLength(0));

                isBottomAreaIntoPlayerArea = playerCoordY == this.yCoord - 1 &&
                (playerCoordX == this.xCoord || playerCoordX == this.xCoord + 1 ||
                playerCoordX == this.xCoord - 1);

                isTopAreaIntoPlayerArea = playerCoordY == this.yCoord + 1 &&
                (playerCoordX == this.xCoord || playerCoordX == this.xCoord + 1 ||
                playerCoordX == this.xCoord - 1);

                isMiddleAreaIntoPlayerArea =
                playerCoordY == this.yCoord &&
                (playerCoordX + 1 == this.xCoord || playerCoordX - 1 == this.xCoord);

                isEnenmyIntoField = field[this.yCoord, this.xCoord] == this.enemyForm;

                enemyCoordIsEqualPLayersCoord = playerCoordX == this.xCoord && playerCoordY == this.yCoord;
            }
            //    случайное число от 0 до 5           случайное число от 0 до 6
            field[this.yCoord, this.xCoord] = this.enemyForm;
        }
    }
}
