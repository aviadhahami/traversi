using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Othello.Logic;

namespace Othello.UI
{
    public class Player
    {
        public Player(ePlayerColor i_PlayerColor, ePlayerMode i_PlayerMode)
        {
            PlayerColor = i_PlayerColor;
            PlayerMode = i_PlayerMode;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", this.PlayerColor, this.PlayerMode);
        }

        /// <summary>
        /// Gets or sets the color of the player.
        /// </summary>
        /// <value>
        /// The color of the player.
        /// </value>
        public ePlayerColor PlayerColor { get; set; }

        /// <summary>
        /// Gets or sets the player mode.
        /// </summary>
        /// <value>
        /// The player mode.
        /// </value>
        public ePlayerMode PlayerMode { get; set; }
    }
}
