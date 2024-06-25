/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This component contains a form for selecting the parameters
// of the new game and sending them to the parent component to
// request and render a new game.

import { useRef } from 'react';


function BoardForm({resetData, gameType}) {

    const RandomGameTypeRef = useRef();
    const SemiIntelligentGameTypeRef = useRef();
    const sizeRef = useRef();

    // This function normalizes the data from the form contained in this component
    // and calls a function of the parent component to reset the game data.
    const handleSubmit = (event) => {
        event.preventDefault();
        let size = sizeRef.current.value;
        let newGameType = gameType;
        let value = parseInt(size);
        if (!(typeof value === "number" && !isNaN(value)))
        {
            size = 8;
        }
        if (SemiIntelligentGameTypeRef.current.checked == true)
        {
            newGameType += 2;
        }
        resetData(newGameType, size);
    };

    return (
        <form onSubmit={handleSubmit} style={{ margin: '20px', fontSize: '20px' }}>
            <label style={{ marginRight: '10px' }}>
                <input type="radio" ref={RandomGameTypeRef} name="gameType" value={0} style={{ marginLeft: '5px' }} defaultChecked />
                Random mode
            </label>
            <label style={{ marginRight: '10px' }}>
                <input type="radio" ref={SemiIntelligentGameTypeRef} name="gameType" value={1} style={{ marginLeft: '5px' }} />
                Semi-intelligent mode
            </label>
            <label style={{ marginRight: '10px' }}>
                Size:
                <input type="text" ref={sizeRef} style={{ marginLeft: '5px' }} />
            </label>
            <br/>
            <button type="submit" style={{ marginLeft: '5px' }}>
                Generate
            </button>
        </form>
    );
}

export default BoardForm;