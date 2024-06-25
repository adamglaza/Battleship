/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This component renders board data provided by the BaseGame component

/* eslint-disable react/prop-types */
import { useEffect, useState } from 'react';

import './../App.css';

function Board(props) {

    const [game, setGame] = useState();

    useEffect(() => {
        setGame(props.board)
    }, [props]);

    const types = ["empty", "hit", "miss", "ship"];



    const contents = game === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <div >
            {game.lost == true
                ? <h1 id="tabelLabel">Player {props.name} Lost</h1>
                : <h1 id="tabelLabel">Player {props.name}</h1>}
            <table className="table table-striped" aria-labelledby="tabelLabel">
            
            <thead>

                <tr>
                    <th />
                    {game.squares.map((row, i) => (
                        <th key={i}>{String.fromCharCode(97 + i)}</th>
                    ))}
                </tr>
            </thead>
            <tbody>
                {game.squares.map((row, i) => (
                    <tr key={i}>
                        <td>{i + 1}</td>
                        {row.map((cell, j) => (
                            (props.name == "2" && props.gameType == 1)
                            ? <td key={j} className={types[cell]} onClick={() => props.betterPost(i, j)}></td>
                            : <td key={j} className={types[cell]}></td>
                        ))}
                    </tr>
                ))}
            </tbody>
            </table></div>;

    return (
        <div id="board">
            {contents}
        </div>
    );

}

export default Board;