/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This component contains description of how to play the Battleship game contained
// in this app and the options that are available to the player.
import './App.css';
function About() {
    return (
        <div style={{ margin: '20px', fontSize: '20px' }} >
            <p>Choose between a passive game which plays itself and an active game in which you fight against your opponent.</p>
            <p>Select between 2 computer modes: random (which makes computer guess randomly)
                and semi-intelligent (which makes the computer guess near its previous correct guesses)</p>
            <p>Choose the size of the board (between 6 and 12) and press the generate button to generate the chosen game</p>
        </div>

  );
}

export default About;