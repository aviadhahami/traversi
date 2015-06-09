using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Logic
{
    public class UserInteractionEndTurnEventArgs : EndTurnEventArgs
    {
        private string m_Position = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInteractionEndTurnEventArgs"/> class.
        /// </summary>
        /// <param name="i_BlackCount">The i_ black count.</param>
        /// <param name="i_WhiteCount">The i_ white count.</param>
        /// <param name="i_CurrentPlayer">The i_ current player.</param>
        /// <param name="i_Position">The i_ position.</param>
        public UserInteractionEndTurnEventArgs(int i_BlackCount, int i_WhiteCount, ePlayerColor i_CurrentPlayer, string i_Position)
            :base(i_BlackCount, i_WhiteCount, i_CurrentPlayer)
        {
            m_Position = i_Position;
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        public string Position
        {
            get { return m_Position; }
        }
    }
}
