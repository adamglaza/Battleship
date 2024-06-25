/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This component renders the navbar and provides routes
// to the rest of the web page.
import './App.css';
import Navbar from "./components/Navbar";
import ActiveGame from "./ActiveGame"
import PassiveGame from "./PassiveGame"
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import About from './About';
function App() {
    return (
        <Router>
            <div className="App">
                <Navbar />
                <Routes>
                    <Route path="/" element={<PassiveGame />} />
                    <Route path="/ActiveGame" element={<ActiveGame />} />
                    {<Route path="/About" element={<About />} />}
                </Routes>
            </div>
        </Router>
    );
}

export default App;