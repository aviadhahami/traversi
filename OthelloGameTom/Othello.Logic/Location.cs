using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Logic
{
    public struct Location
    {
        private int m_Column;
        private int m_Row;

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> struct.
        /// </summary>
        /// <param name="i_Column">The column.</param>
        /// <param name="i_Row">The row.</param>
        public Location(int i_Column, int i_Row)
        {
            m_Column = i_Column;
            m_Row = i_Row;
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        public int Column
        {
            get { return m_Column; }
        }

        /// <summary>
        /// Gets the row.
        /// </summary>
        public int Row
        {
            get { return m_Row; }
        }

        /// <summary>
        /// Gets an empty location.
        /// </summary>
        public static Location Empty
        {
            get { return new Location(0, 0); }
        }
    }
}
