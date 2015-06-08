using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Othello.Logic;

namespace Othello.Logic
{
    public class EndGameEventArgs : EventArgs
    {
        private readonly bool r_HasWinner;
        private readonly int r_LoserCount;
        private readonly int r_WinnerCount;
        private readonly ePlayerColor r_Winner;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndGameEventArgs"/> class.
        /// </summary>
        /// <param name="i_Winner">The winner.</param>
        /// <param name="i_WinnerCount">The winner's point count.</param>
        /// <param name="i_LoserCount">The loser's point count.</param>
        public EndGameEventArgs(ePlayerColor i_Winner, int i_WinnerCount, int i_LoserCount)
        {
            r_Winner = i_Winner;
            r_WinnerCount = i_WinnerCount;
            r_LoserCount = i_LoserCount;
            r_HasWinner = i_WinnerCount != i_LoserCount;
        }

        /// <summary>
        /// Gets the winner.
        /// </summary>
        public ePlayerColor Winner
        {
            get { return r_Winner; }
        }

        /// <summary>
        /// Gets the winner count.
        /// </summary>
        public int WinnerCount
        {
            get { return r_WinnerCount; }
        }

        /// <summary>
        /// Gets the loser count.
        /// </summary>
        public int LoserCount
        {
            get { return r_LoserCount; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has winner.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has winner; otherwise, <c>false</c>.
        /// </value>
        public bool HasWinner
        {
            get { return r_HasWinner; }
        }
    }
}
