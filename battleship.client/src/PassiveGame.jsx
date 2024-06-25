/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This component contains renders the BaseGame component
// with a parameter that allows only for the computer
// to control actions on both boards.

import './App.css';
import BaseGame from './components/BaseGame';
function PassiveGame() {
    return (
        <BaseGame gameType={0} />
    );
}

export default PassiveGame;