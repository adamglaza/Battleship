/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This component contains the navbar that allows the user
// to navigate around this web page.
import React from "react";
import { Link } from "react-router-dom";

const Navbar = () => {
    return (
        <nav className="h1 navbar navbar-expand-lg navbar-expand-md navbar-dark bg-primary">
            <div className="collapse navbar-collapse" id="navbarNav">
                <ul className="navbar-nav">
                    <li className="nav-item active">
                        <Link to="/">
                            <div className={"nav-link"} href="#">
                                Passive Game
                            </div>
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to="/ActiveGame">
                            <div className={"nav-link"} href="#">
                                Active Game
                            </div>
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to="/About">
                            <div className={"nav-link"} href="#">
                                About
                            </div>
                        </Link>
                    </li>
                </ul>
            </div>
        </nav>
    );
};

export default Navbar;