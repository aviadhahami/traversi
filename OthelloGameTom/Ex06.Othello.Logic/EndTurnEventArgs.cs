using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Logic
{
    public class EndTurnEventArgs : EventArgs
    {
        private int m_BlackCount = 0;
        private int m_WhiteCount = 0;
        private ePlayerColor m_CurrentPlayer;
        private ePlayerColor m_NextPlayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndTurnEventArgs"/> class.
        /// </summary>
        /// <param name="i_BlackCount">The i_ black count.</param>
        /// <param name="i_WhiteCount">The i_ white count.</param>
        /// <param name="i_CurrentPlayer">The i_ current player.</param>
        public EndTurnEventArgs(int i_BlackCount, int i_WhiteCount, ePlayerColor i_CurrentPlayer)
        {
            m_BlackCount = i_BlackCount;
            m_WhiteCount = i_WhiteCount;
            m_CurrentPlayer = i_CurrentPlayer;
            m_NextPlayer = m_CurrentPlayer == ePlayerColor.Black ? ePlayerColor.White : ePlayerColor.Black;
        }

        /// <summary>
        /// Gets the black coins count.
        /// </summary>
        public int BlackCoinsCount
        {
            get { return m_BlackCount; }
        }

        /// <summary>
        /// Gets the white coins count.
        /// </summary>
        public int WhiteCoinsCount
        {
            get { return m_WhiteCount; }
        }

        /// <summary>
        /// Gets the current player.
        /// </summary>
        public ePlayerColor CurrentPlayer
        {
            get { return m_CurrentPlayer; }
        }

        /// <summary>
        /// Gets the next player.
        /// </summary>
        public ePlayerColor NextPlayer
        {
            get { return m_NextPlayer; }
        }

    }
}
