using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Logic
{
    public class PassTurnEventArgs : EventArgs
    {
        private ePlayerColor m_PassedTo;
        private ePlayerColor m_PassedFrom;

        /// <summary>
        /// Initializes a new instance of the <see cref="PassTurnEventArgs"/> class.
        /// </summary>
        /// <param name="i_PassedFrom">The from mode.</param>
        /// <param name="i_PassedTo">The to mode.</param>
        public PassTurnEventArgs(ePlayerColor i_PassedFrom, ePlayerColor i_PassedTo)
        {
            m_PassedTo = i_PassedTo;
            m_PassedFrom = i_PassedFrom;
        }

        /// <summary>
        /// Gets the passed from.
        /// </summary>
        public ePlayerColor PassedFrom
        {
            get { return m_PassedFrom; }
        }

        /// <summary>
        /// Gets the passed to.
        /// </summary>
        public ePlayerColor PassedTo
        {
            get { return m_PassedTo; }
        }
    }
}
