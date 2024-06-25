/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This controller is responsible for communicating with the React client.
// It receives data about a game which it then creates or modifies and
// sends the result back afterwards.

using Battleship.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private static Dictionary<Guid, Game> _games = new Dictionary<Guid, Game>();
        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }



        static readonly object _lock = new object();

        // This function receives data from a post request containing description
        // of the appropriate game. If the game in question doesn't exist, it is then
        // created and its information is sent back to client. If the game does
        // exist, an action is executed on a player's board depending on the game type
        // and the turn order. Afterwards the resulting game is sent back to the client.
        [HttpPost]
        public ActionResult<Game> TakeAction(PlayerAction playerAction)
        {
            lock (_lock)
            {
                if (!_games.ContainsKey(playerAction.ID))
                {
                    if (playerAction.GameType == null)
                    {
                        playerAction.GameType = 0;
                    }

                    Game newGame;

                    if(playerAction.Size != null)
                    {
                        newGame = new Game(playerAction.ID, (int)playerAction.GameType,(int)playerAction.Size); 
                    }
                    else
                    {
                        newGame = new Game(playerAction.ID, (int)playerAction.GameType);
                    }

                    _games.Add(newGame.ID, newGame);
                    Console.WriteLine(playerAction.ID);

                    if (playerAction.GameType % 2 == 0)
                    {
                        return newGame;
                    }
                    else
                    {
                        return newGame.Censor();
                    }
                }
                else
                {
                    if (playerAction.GameType == null)
                    {
                        if (_games[playerAction.ID].gameType % 2 == 0)
                        {
                            _games[playerAction.ID].TakeBotAction();
                        }
                        else if (playerAction.i != null && playerAction.j != null)
                        {
                            _games[playerAction.ID].TakeAction((int)playerAction.i, (int)playerAction.j);
                        }
                    }
                    if (_games[playerAction.ID].gameType % 2 == 0)
                    {
                        return _games[playerAction.ID];
                    }
                    else
                    {
                        return _games[playerAction.ID].Censor();
                    }
                }
            }
        }
    }
}
