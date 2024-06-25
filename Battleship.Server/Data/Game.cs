/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This class contains game data which includes the ID of the client
// that is tied to a particular instance of this class, the boards
// representing both players, the turn count and the game type.

// This class also contains functions responsible for calling
// appropriate board functions to initiate player actions
// and a functoon that censors board data.

namespace Battleship.Server.Data
{
    public class Game
    {
        public Guid ID { get; set; }

        public Board Player1 { get; set; }
        public Board Player2 { get; set; }

        public int turn = 0;

        // Odd numbers represent a game that is partially controlled
        // by an actual player. The values below 2 represent a game,
        // in which the actions of the computer are chosen randomly.
        // The values above 1 represent a game in which the actions
        // of the computer are chosen in the TakeSemiIntelligentShot
        // function of the Board class.
        public int gameType = 0;

        public Game(Guid id, int gameType = 0, int size = 8)
        {
            ID = id;
            Player1 = new Board(id,size);
            Player2 = new Board(id,size);
            this.gameType = gameType;
        }
        public Game(Game originalGame)
        {
            ID = originalGame.ID;
            Player1 = new Board(originalGame.Player1);
            Player2 = new Board(originalGame.Player2);
            gameType = originalGame.gameType;
        }
        public Game Censor()
        {
            var censoredGame = new Game(this);
            censoredGame.Player2.Censor();
            return censoredGame;
        }

        // This function depending on the turn and chosen game type
        // calls appropriate Board functions utilizing client input
        // if it's their turn.
        public void TakeAction(int i, int j)
        {
            if (IsEndOfGame())
            {
                return;
            }

            if (turn % 2 == 0)
            {
                if(gameType < 2)
                {
                    Player1.TakeRandomShot();
                }
                else
                {
                    Player1.TakeSemiIntelligentShot();
                }
            }
            else
            {
                Player2.TakeAction(i, j);
            }

            turn++;
        }
        // This function depending on the turn and chosen game type
        // calls appropriate Board functions.
        public void TakeBotAction()
        {
            if (IsEndOfGame())
            {
                return;
            }

            if (gameType < 2)
            {
                if (turn % 2 == 0)
                {
                    Player1.TakeRandomShot();
                }
                else
                {
                    Player2.TakeRandomShot();
                }
            }
            else
            {
                if (turn % 2 == 0)
                {
                    Player1.TakeSemiIntelligentShot();
                }
                else
                {
                    Player2.TakeSemiIntelligentShot();
                }
            }


            turn++;
        }

        private bool IsEndOfGame()
        {
            if (Player1.Lost || Player2.Lost)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
