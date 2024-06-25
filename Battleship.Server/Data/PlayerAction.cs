/*
 * Copyright 2024 Adam Glaza (adamglaza@outlook.com)
 * Use of this source code is governed by an MIT-style
 * license that can be found in the LICENSE file or at
 * https://opensource.org/licenses/MIT.
 */

// This class contains the data format that is received by the
// GameController which allows it to create or modify an appropriate
// Game object.

namespace Battleship.Server.Data
{
    public class PlayerAction
    {
        public Guid ID { get; set; }
        public int? i { get; set; }
        public int? j { get; set; }
        public int? GameType { get; set; }
        public int? Size { get; set; }
    }
}
