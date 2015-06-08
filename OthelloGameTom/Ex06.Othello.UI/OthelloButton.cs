using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Othello.Logic;

namespace Othello.UI
{
    public class OthelloButton : FlowLayoutPanel
    {
        private bool m_IsEnabled = false;
        private Pen m_BorderPen = new Pen(Color.FromArgb(194, 192, 255)) { Width = 1 };
        private PictureBox m_CoinPictureBox = new PictureBox();
        private readonly int sr_Column = 0;
        private readonly int sr_Row = 0;
        private SolidBrush m_CurrentBrush = null;
        private SolidBrush m_BackColorBrush = new SolidBrush(Color.FromArgb(129, 128, 255));
        private SolidBrush m_optionalBackColorBrush = new SolidBrush(Color.FromArgb(132, 0, 255));

        public event EventHandler OnEnabledDiskMouseEnter = null;
        public event EventHandler OnEnabledDiskMouseLeave = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="OthelloButton"/> class.
        /// </summary>
        /// <param name="i_Column">The i_ column.</param>
        /// <param name="i_Row">The i_ row.</param>
        public OthelloButton(int i_Column, int i_Row)
        {
            this.sr_Column = i_Column;
            this.sr_Row = i_Row;
            m_CurrentBrush = m_BackColorBrush;
            m_BorderPen.Width = 1;
            m_CoinPictureBox.BackColor = Color.Transparent;
            m_CoinPictureBox.Size = this.Size;
            m_CoinPictureBox.MouseEnter += new EventHandler(m_CoinPictureBox_MouseEnter);
            m_CoinPictureBox.MouseLeave += new EventHandler(m_CoinPictureBox_MouseLeave);
            m_CoinPictureBox.Click += new EventHandler(m_CoinPictureBox_Click);
            setImagePosition();
            this.Controls.Add(m_CoinPictureBox);
        }

        /// <summary>
        /// Handles the Paint event of the m_CoinPictureBox control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_PaintEventArgs">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void m_CoinPictureBox_Paint(object i_Sender, PaintEventArgs i_PaintEventArgs)
        {
            Point topLeft = new Point(0, 0);
            Point topRight = new Point(this.Width - 1, 0);
            Point bottomLeft = new Point(0, this.Height - 1);
            Point bottomRight = new Point(this.Width - 1, this.Height - 1);

            i_PaintEventArgs.Graphics.DrawLine(m_BorderPen, bottomLeft, topLeft);
            i_PaintEventArgs.Graphics.DrawLine(m_BorderPen, topLeft, topRight);
            i_PaintEventArgs.Graphics.DrawLine(m_BorderPen, bottomLeft, bottomRight);
            i_PaintEventArgs.Graphics.DrawLine(m_BorderPen, bottomRight, topRight);

            i_PaintEventArgs.Graphics.FillRectangle(m_CurrentBrush, 0, 0, this.Width - 1, this.Height - 1);
        }

        /// <summary>
        /// Handles the Click event of the m_CoinPictureBox control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void m_CoinPictureBox_Click(object i_Sender, EventArgs i_EventArgs)
        {
            //Prevent clicks when the button is not in optional mode
            if (m_IsEnabled)
            {
                this.OnClick(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the m_CoinPictureBox control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void m_CoinPictureBox_MouseEnter(object i_Sender, EventArgs i_EventArgs)
        {
            //Change the cursor to hand in order to show that the button is clickable.
            if (m_IsEnabled)
            {
                this.Cursor = Cursors.Hand;

                if (OnEnabledDiskMouseEnter != null)
                {
                    OnEnabledDiskMouseEnter(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Handles the MouseLeave event of the m_CoinPictureBox control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void m_CoinPictureBox_MouseLeave(object i_Sender, EventArgs i_EventArgs)
        {
            this.Cursor = Cursors.Default;

            if (m_IsEnabled)
            {
                if (OnEnabledDiskMouseLeave != null)
                {
                    OnEnabledDiskMouseLeave(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:PaintBackground"/> event.
        /// </summary>
        /// <param name="i_PaintEventArgs">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs i_PaintEventArgs)
        {
            Point topLeft = new Point(0, 0);
            Point topRight = new Point(this.Width - 1, 0);
            Point bottomLeft = new Point(0, this.Height - 1);
            Point bottomRight = new Point(this.Width - 1, this.Height - 1);

            i_PaintEventArgs.Graphics.DrawLine(m_BorderPen, bottomLeft, topLeft);
            i_PaintEventArgs.Graphics.DrawLine(m_BorderPen, topLeft, topRight);
            i_PaintEventArgs.Graphics.DrawLine(m_BorderPen, bottomLeft, bottomRight);
            i_PaintEventArgs.Graphics.DrawLine(m_BorderPen, bottomRight, topRight);

            i_PaintEventArgs.Graphics.FillRectangle(m_CurrentBrush, 2, 2, this.Width - 1, this.Height - 1);
        }

        /// <summary>
        /// Raises the <see cref="E:Resize"/> event.
        /// </summary>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnResize(EventArgs i_EventArgs)
        {
            base.OnResize(i_EventArgs);
            setImagePosition();
            this.Refresh();
        }

        /// <summary>
        /// Sets the image position.
        /// </summary>
        private void setImagePosition()
        {
            int x = this.Width / 2 - m_CoinPictureBox.Width / 2;
            int y = this.Height / 2 - m_CoinPictureBox.Height / 2;

            m_CoinPictureBox.Location = new Point(x, y);
        }

        /// <summary>
        /// Gets the row.
        /// </summary>
        public int Row
        {
            get { return sr_Row; }
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        public int Column
        {
            get { return sr_Column; }
        }

        /// <summary>
        /// Converts the button to black.
        /// </summary>
        public void ConvertToBlack()
        {
            this.m_IsEnabled = false;
            m_CurrentBrush = m_BackColorBrush;
            m_CoinPictureBox.Image = OthelloResources.CoinBlack;
        }

        /// <summary>
        /// Converts the button to white.
        /// </summary>
        public void ConvertToWhite()
        {
            this.m_IsEnabled = false;
            m_CurrentBrush = m_BackColorBrush;
            m_CoinPictureBox.Image = OthelloResources.CoinWhite;
        }

        /// <summary>
        /// Converts the button to optional.
        /// </summary>
        public void ConvertToOptional(bool i_ShowOptionalMoves)
        {
            this.m_IsEnabled = true;
            m_CurrentBrush = i_ShowOptionalMoves ? m_optionalBackColorBrush : m_BackColorBrush;
            m_CoinPictureBox.Image = null;
        }

        /// <summary>
        /// Converts the button to virtual move (optional)
        /// </summary>
        public void ConvertToVirtualWhite()
        {
            this.m_IsEnabled = true;
            m_CoinPictureBox.Image = OthelloResources.CoinWhitePreview;
        }

        /// <summary>
        /// Converts the button to virtual move (optional)
        /// </summary>
        public void ConvertToVirtualBlack()
        {
            this.m_IsEnabled = true;
            m_CoinPictureBox.Image = OthelloResources.CoinBlackPreview;
        }

        /// <summary>
        /// Disables this button.
        /// </summary>
        public void Disable()
        {
            this.m_IsEnabled = false;
            m_CurrentBrush = m_BackColorBrush;
            m_CoinPictureBox.Image = null;
        }

        /// <summary>
        /// Raises the <see cref="E:Click"/> event.
        /// </summary>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnClick(EventArgs i_EventArgs)
        {
            //Prevent clicks when the button is not in optional mode
            if (m_IsEnabled)
            {
                base.OnClick(i_EventArgs);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:MouseEnter"/> event.
        /// </summary>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnMouseEnter(EventArgs i_EventArgs)
        {
            //Change the cursor to hand in order to show that the button is clickable.
            if (m_IsEnabled)
            {
                this.Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:MouseLeave"/> event.
        /// </summary>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnMouseLeave(EventArgs i_EventArgs)
        {
            //Change the cursor back to narmal when the button lost focus.

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Column :{0} Row: {1}", this.Column, this.Row);
        }
    }
}
