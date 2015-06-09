using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Logic
{
    public class PlayerChangedEventArgs : EventArgs
    {
        private ePlayerColor m_CurrentPlayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerChangedEventArgs"/> class.
        /// </summary>
        /// <param name="i_CurrentPlayer">The i_ current player.</param>
        public PlayerChangedEventArgs(ePlayerColor i_CurrentPlayer)
        {
            m_CurrentPlayer = i_CurrentPlayer;
        }

        /// <summary>
        /// Gets the current player.
        /// </summary>
        public ePlayerColor CurrentPlayer
        {
            get { return m_CurrentPlayer; }
        }
    }
}
