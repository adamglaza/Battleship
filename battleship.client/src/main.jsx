/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This is the main component of this React application.
// It renders the App component in which the rest of 
// the application is contained.

import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import App from './App'

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <App></App>
  </React.StrictMode>,
)
