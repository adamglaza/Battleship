# Battleship

Battleship is a project utilizing React for its frontend and a C# web API for its backend.

The game can be played on any major modern web browser.

The player can select between 2 game modes:
- Active: which allows the player to compete against the computer
- Passive: which allows the player to watch the computer battle against itself

Additionally, the player can choose 2 more parameters:
- The size of the board - from between 6 and 12
- The intelligence of the computer - either the random guess mode or the semi-intelligent one (which tries to make guesses near the previous correct guesses)

Each game is tied to the user for as long as the application is running.

Given the fact that each game is very short (ending in a few minutes at most) it was decided that the game data won't be stored in a database but rather in memory (after ensuring that access to it will be thread safe). 
## License

[MIT](https://choosealicense.com/licenses/mit/)