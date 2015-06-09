using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Logic
{
    public class OthelloDiskMemento
    {   
        private eDiskMode m_DiskMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="OthelloDiskMemento"/> class.
        /// </summary>
        /// <param name="i_DiskMode">The i_ disk mode.</param>
        public OthelloDiskMemento(eDiskMode i_DiskMode)
        {
            m_DiskMode = i_DiskMode;
        }

        /// <summary>
        /// Gets the disk mode.
        /// </summary>
        public eDiskMode DiskMode
        {
            get { return m_DiskMode; }
        }
    }
}
