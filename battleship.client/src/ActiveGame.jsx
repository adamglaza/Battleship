/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This component contains renders the BaseGame component
// with a parameter that allows the player to participate
// in the game.

import './App.css';
import BaseGame from './components/BaseGame';
function ActiveGame() {
    return (
        <BaseGame gameType={1} />
    );
}

export default ActiveGame;