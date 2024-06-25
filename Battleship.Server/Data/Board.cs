/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This class contains board data which includes the ID of the client
// that is tied to a particular instance of this class, whether
// the player to whom this board is tied to has lost the game 
// as well as the squares that represent the state of the board.

// This class also contains functions responsible for creating
// the initial board state and other functions that alter by
// taking certain actions.

using System.Drawing;
using System.Threading.Tasks;

namespace Battleship.Server.Data
{
    public class Board
    {
        private readonly int[] Directions = { -1, 0, 1, 0 };
        public Guid ID { get; set; }

        public int[][] Squares { get; set; }

        public bool Lost { get; set; }

        public Board(Guid ID, int size = 8)
        {
            if(size < 6)
            {
                size = 6;
            }
            else if(size > 12)
            {
                size = 12;
            }

            this.ID = ID;
            Lost = false;
            Squares = new int[size][];
            for (int i = 0; i < size; i++)
            {
                Squares[i] = new int[size];

            }
            CreateBoard();
        }

        public Board(Board originalBoard)
        {
            ID = originalBoard.ID;
            Lost = originalBoard.Lost;
            Squares = new int[originalBoard.Squares.Length][];
            for (int i = 0; i < Squares.Length; i++)
            {
                Squares[i] = (int[])originalBoard.Squares[i].Clone();
            }
        }

        // This function censors the board data so that the player
        // won't know the location of the ships.
        public void Censor()
        {
            for (int i = 0; i < Squares.Length; i++)
            {
                for (int j = 0; j < Squares.Length; j++)
                {
                    if (Squares[i][j] == 3)
                    {
                        Squares[i][j] = 0;
                    }
                }
            }
        }

        // This function creates 5 ships of different sizes (from 5 squares to 1).
        // The starting point of each ship and the direction it extends towards
        // is chosen randomly until the given ship can be placed within the board
        // and without intersecting with other ships.
        private void CreateBoard()
        {
            int shipNumber = 5;

            while (shipNumber > 0)
            {
                int i = Random.Shared.Next(Squares.Length);
                int j = Random.Shared.Next(Squares.Length);
                int direction = Random.Shared.Next(4);

                int shipPartsToPlace = shipNumber;

                int moveVertical = Directions[direction % 4];
                int moveHorizontal = Directions[(direction + 1) % 4];

                int currentI = i;
                int currentJ = j;

                while (shipPartsToPlace > 0)
                {
                    if (currentI < 0 || currentJ < 0 || currentI == Squares.Length || currentJ == Squares.Length || Squares[currentI][currentJ] == 3)
                    {
                        break;
                    }
                    currentI += moveVertical;
                    currentJ += moveHorizontal;
                    shipPartsToPlace--;
                }

                if (shipPartsToPlace == 0)
                {
                    for (int k = 0; k < shipNumber; k++)
                    {
                        Squares[i][j] = 3;
                        i += moveVertical;
                        j += moveHorizontal;
                    }
                    shipNumber--;
                }
            }

        }

        // A random spot (that was not shot before) is shot at.
        public bool TakeRandomShot()
        {
            if (Lost)
            {
                return true;
            }

            while (true)
            {
                int i = Random.Shared.Next(Squares.Length);
                int j = Random.Shared.Next(Squares.Length);

                if (Squares[i][j] == 0)
                {
                    Squares[i][j] = 2;
                    break;
                }
                if (Squares[i][j] == 3)
                {
                    Squares[i][j] = 1;
                    break;
                }
            }

            if (DefeatCheck())
            {
                Lost = true;
                return true;
            }

            return false;
        }

        // A square near the previous correct guess is shot at.
        // If there are no such squares, a random square is chosen instead.
        public bool TakeSemiIntelligentShot()
        {
            if (Lost)
            {
                return true;
            }

            bool found = false;

            for(int i = 0; i < Squares.Length; i++)
            {
                for(int j = 0; j < Squares[i].Length; j++)
                {
                    if (Squares[i][j] == 1)
                    {
                        for (int k = 0; k < Directions.Length; k++)
                        {
                            int newI = Directions[k % 4] + i;
                            int newJ = Directions[(k + 1) % 4] + j;

                            if(newI >= 0 && newJ >= 0 && newI < Squares.Length && newJ < Squares.Length)
                            {
                                if (Squares[newI][newJ] == 0)
                                {
                                    Squares[newI][newJ] = 2;
                                    found = true;
                                    break;
                                }
                                if (Squares[newI][newJ] == 3)
                                {
                                    Squares[newI][newJ] = 1;
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (found)
                {
                    break;
                }
            }

            if (!found)
            {
                return TakeRandomShot();
            }

            if (DefeatCheck())
            {
                Lost = true;
                return true;
            }

            return false;
        }

        // The given position on the board (i,j) is shot at.
        public bool TakeAction(int i, int j)
        {
            if (Lost)
            {
                return true;
            }

            if (Squares[i][j] == 0)
            {
                Squares[i][j] = 2;
            }
            if (Squares[i][j] == 3)
            {
                Squares[i][j] = 1;
            }

            if (DefeatCheck())
            {
                Lost = true;
                return true;
            }

            return false;
        }
        private bool DefeatCheck()
        {
            for (int i = 0; i < Squares.Length; i++)
            {
                for (int j = 0; j < Squares.Length; j++)
                {
                    if (Squares[i][j] == 3)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
