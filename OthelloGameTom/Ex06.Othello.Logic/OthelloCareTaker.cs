using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Logic
{
    public class OthelloDiskCareTaker
    {
        private readonly int r_Row = 0;
        private readonly int r_Column = 0;
        private readonly OthelloDiskMemento r_Memento = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="OthelloDiskCareTaker"/> class.
        /// </summary>
        /// <param name="i_Row">The i_ row.</param>
        /// <param name="i_Column">The i_ column.</param>
        /// <param name="i_Memento">The i_ memento.</param>
        public OthelloDiskCareTaker(int i_Row, int i_Column, OthelloDiskMemento i_Memento)
        {
            r_Row = i_Row;
            r_Column = i_Column;
            r_Memento = i_Memento;
        }

        /// <summary>
        /// Gets the memento.
        /// </summary>
        public OthelloDiskMemento Memento
        {
            get { return r_Memento; }
        }

        /// <summary>
        /// Gets the row.
        /// </summary>
        public int Row 
        {
            get { return r_Row; } 
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        public int Column 
        {
            get { return r_Column;} 
        }
    }
}
