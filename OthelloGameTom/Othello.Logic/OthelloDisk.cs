using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Logic
{
    public class OthelloDisk
    {

        private Location m_Location = Location.Empty;
        private eDiskMode m_DiskMode = eDiskMode.IlegalMove;

        public event EventHandler<OthelloDiskChangedEventArgs> OnDiskChanged = null;
        public event EventHandler<OthelloDiskChangedEventArgs> OnDiskChangedToVirtual = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="OthelloDisk"/> class.
        /// </summary>
        /// <param name="i_Location">The location of the disk.</param>
        public OthelloDisk(Location i_Location)
        {
            this.m_Location = i_Location;
        }

        public bool IsVirtual { get; set; }

        public OthelloDiskMemento Memento
        {
            get
            {
                return new OthelloDiskMemento(this.DiskMode);
            }
            set
            {
                this.IsVirtual = false;
                this.DiskMode = value.DiskMode;
            }
        }

        /// <summary>
        /// Gets or sets the disk mode.
        /// </summary>
        /// <value>
        /// The disk mode.
        /// </value>
        public eDiskMode DiskMode
        {
            get { return m_DiskMode; }
            set
            {
                if (m_DiskMode != value)
                {
                    m_DiskMode = value;

                    // If we have listeneres 
                    if (OnDiskChanged != null || OnDiskChangedToVirtual != null)
                    {
                        OthelloDiskChangedEventArgs args = new OthelloDiskChangedEventArgs(m_DiskMode, m_Location);

                        if (IsVirtual)
                        {
                            if (OnDiskChangedToVirtual != null)
                            {
                                OnDiskChangedToVirtual(this, args);
                            }
                        }
                        else
                        {
                            if (OnDiskChanged != null)
                            {
                                OnDiskChanged(this, args);
                            }
                        }
                    }
                }
            }
        }

        public void RaiseDiskChangeEvent()
        {
            if (OnDiskChanged != null)
            {
                OnDiskChanged(this, new OthelloDiskChangedEventArgs(m_DiskMode, m_Location));
            }
        }

        /// <summary>
        /// Gets the disk location.
        /// </summary>
        public Location DiskLocation
        {
            get { return m_Location; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Mode: {0} Row: {1} Column: {2}", this.DiskMode, this.m_Location.Row, m_Location.Column);
        }
    }
}
