using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Logic
{
    public class OthelloDiskChangedEventArgs : EventArgs
    {   
        private readonly eDiskMode r_Mode;
        private readonly Location r_Location;

        /// <summary>
        /// Initializes a new instance of the <see cref="OthelloDiskChangedEventArgs"/> class.
        /// </summary>
        /// <param name="i_Mode">The disk mode.</param>
        /// <param name="i_Location">The location of the disk.</param>
        public OthelloDiskChangedEventArgs(eDiskMode i_Mode, Location i_Location)
        {
            r_Mode = i_Mode;
            r_Location = i_Location;
        }

        /// <summary>
        /// Gets the disk mode.
        /// </summary>
        public eDiskMode Mode
        {
            get { return r_Mode; }
        }

        /// <summary>
        /// Gets the location of the disk.
        /// </summary>
        public Location Location
        {
            get { return r_Location; }
        }
    }
}
