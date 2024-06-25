/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This component is responsible for sending and receiving game data from the web API
// as well as sending board data to the Board component to render.

import { useEffect, useState } from 'react';
import Cookies from 'universal-cookie';
import './../App.css';
import Board from './Board';

import BoardForm from './BoardForm';

function BaseGame({ gameType }) {
    const [gameData, setGameData] = useState();
    const cookies = new Cookies(null, { path: '/' });

    useEffect(() => {
        if (cookies.get("BattleshipGameID" + gameType) === undefined) {
            cookies.set("BattleshipGameID" + gameType, crypto.randomUUID());
        }
        console.log(cookies.get("BattleshipGameID" + gameType));

        if (window.sessionStorage.getItem("board" + gameType) != undefined) {
            console.log(window.sessionStorage.getItem("board" + gameType));
            setGameData(JSON.parse(window.sessionStorage.getItem("board" + gameType)));
        }
        else {
            console.log("a");
            sendGamePost(0, 0, gameType);
        }
    }, []);


    const contents = gameData === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <div id="game">
            <Board board={gameData.player1} name="1" betterPost={sendGamePost} gameType={gameType}></Board>
            <Board board={gameData.player2} name="2" betterPost={sendGamePost} gameType={gameType}></Board>
        </div>;
    return (
        <div>
            {contents}
            {gameType == 0 && <button onClick={() => sendGamePost()}>Next Move</button>}
            <BoardForm resetData={resetData} gameType={gameType}></BoardForm>
        </div>

    );

    // This function resets board data and generates a new one
    // by calling the sendGamePost function.
    function resetData(typeOfGame,size) {
        window.sessionStorage.removeItem('board' + gameType);
        cookies.set("BattleshipGameID" + gameType, crypto.randomUUID());
        sendGamePost(0, 0, typeOfGame, size);
    }

    // This function sends a post request to the web API
    // and sets the response on the page with a React Hook.
    async function sendGamePost(i,j,typeOfGame=null,size=null) {
        const response = await fetch('game', {
            method: "POST",
            body: JSON.stringify({
                ID: cookies.get("BattleshipGameID" + gameType),
                i: i,
                j: j,
                gameType: typeOfGame,
                size: size,
            }),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        });
        const data = await response.json();
        window.sessionStorage.setItem("board" + gameType, JSON.stringify(data));

        setGameData(data);
    }
}

export default BaseGame;