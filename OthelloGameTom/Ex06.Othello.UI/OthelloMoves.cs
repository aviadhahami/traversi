using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Othello.Logic;

namespace Ex06.Othello.UI
{
    public partial class OthelloMoves : UserControl
    {   
        private int m_MovesCounter = 0;
        private Panel m_MainPanel = null;
        private Label m_BlackScoreLabel = null;
        private Label m_WhiteScoreLabel = null;
        private ListView m_MovesListView = null;
        private Label m_CurrentMoveLabel = null;
        private PictureBox m_CurrentCoinPictureBox = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="OthelloMoves"/> class.
        /// </summary>
        public OthelloMoves()
        {
            this.Width = 172;
            this.Height = 460;
            this.Padding = this.Margin = new Padding(0);
            this.BackColor = Color.SlateGray;
            m_MainPanel = new Panel();
            m_MainPanel.Dock = DockStyle.Fill;
            this.Controls.Add(m_MainPanel);
            initializeListView();
            initializeTopPanelMoves();
        }

        /// <summary>
        /// Clears the moves.
        /// </summary>
        public void ClearMoves()
        {
            m_MovesCounter = 0;
            m_MovesListView.Items.Clear();
        }

        /// <summary>
        /// Sets the user move.
        /// </summary>
        /// <param name="i_CurrentPlayer">The i_ current player.</param>
        /// <param name="i_NextPlayer">The i_ next player.</param>
        /// <param name="i_BlackCoinsCount">The i_ black coins count.</param>
        /// <param name="i_WhiteCoinsCount">The i_ white coins count.</param>
        /// <param name="i_Position">The i_ position.</param>
        public void SetUserMove(ePlayerColor i_CurrentPlayer, ePlayerColor i_NextPlayer, int i_BlackCoinsCount, int i_WhiteCoinsCount, string i_Position)
        {   
            ListViewItem listViewItem = null;

            m_MovesCounter++;
            SetMove(i_NextPlayer, i_BlackCoinsCount, i_WhiteCoinsCount);
            listViewItem = new ListViewItem(m_MovesCounter.ToString());
            
            listViewItem.SubItems.Add(i_CurrentPlayer.ToString());
            listViewItem.SubItems.Add(i_Position);
            m_MovesListView.Items.Add(listViewItem);
            m_MovesListView.EnsureVisible(m_MovesListView.Items.Count - 1);
        }

        /// <summary>
        /// Sets the move.
        /// </summary>
        /// <param name="i_NextPlayer">The i_ next player.</param>
        /// <param name="i_BlackCoinsCount">The i_ black coins count.</param>
        /// <param name="i_WhiteCoinsCount">The i_ white coins count.</param>
        public void SetMove(ePlayerColor i_NextPlayer, int i_BlackCoinsCount, int i_WhiteCoinsCount)
        {
            m_BlackScoreLabel.Text = i_BlackCoinsCount.ToString();
            m_WhiteScoreLabel.Text = i_WhiteCoinsCount.ToString();
            SetNextPlayer(i_NextPlayer);
            m_CurrentMoveLabel.Visible = true;
        }

        /// <summary>
        /// Sets the next player.
        /// </summary>
        /// <param name="i_NextPlayer">The i_ next player.</param>
        public void SetNextPlayer(ePlayerColor i_NextPlayer)
        {
            m_CurrentCoinPictureBox.Image = i_NextPlayer == ePlayerColor.Black ? OthelloResources.CoinBlack : OthelloResources.CoinWhite;
        }

        /// <summary>
        /// Initializes the top panel moves.
        /// </summary>
        private void initializeTopPanelMoves()
        {
            PictureBox blackCoinPictureBox = new PictureBox();
            
            blackCoinPictureBox.Image = OthelloResources.CoinBlack;
            blackCoinPictureBox.Location = new Point(7, 25);
            blackCoinPictureBox.Size = new Size(32, 32);
            blackCoinPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            m_MainPanel.Controls.Add(blackCoinPictureBox);

            Label blackLabel = new Label();

            blackLabel.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point);
            blackLabel.Location = new Point(45, 32);
            blackLabel.ForeColor = Color.Black;
            blackLabel.Size = new Size(70, 21);
            blackLabel.Text = "Black:";
            m_MainPanel.Controls.Add(blackLabel);

            m_BlackScoreLabel = new Label();

            m_BlackScoreLabel.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point);
            m_BlackScoreLabel.Location = new Point(115, 32);
            m_BlackScoreLabel.ForeColor = Color.Black;
            m_BlackScoreLabel.Size = new Size(50, 21);
            m_MainPanel.Controls.Add(m_BlackScoreLabel);

            PictureBox whiteCoinPictureBox = new PictureBox();

            whiteCoinPictureBox.Image = OthelloResources.CoinWhite;
            whiteCoinPictureBox.Location = new Point(7, 63);
            whiteCoinPictureBox.Size = new Size(32, 32);
            whiteCoinPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            m_MainPanel.Controls.Add(whiteCoinPictureBox);

            Label whiteLabel = new Label();

            whiteLabel.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point);
            whiteLabel.Location = new Point(45, 70);
            whiteLabel.ForeColor = Color.White;
            whiteLabel.Size = new Size(75, 21);
            whiteLabel.Text = "White:";
            m_MainPanel.Controls.Add(whiteLabel);

            m_WhiteScoreLabel = new Label();

            m_WhiteScoreLabel.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point);
            m_WhiteScoreLabel.Location = new Point(115, 70);
            m_WhiteScoreLabel.ForeColor = Color.White;
            m_WhiteScoreLabel.Size = new Size(50, 21);
            m_MainPanel.Controls.Add(m_WhiteScoreLabel);

            m_CurrentMoveLabel = new Label();

            m_CurrentMoveLabel.Font = new Font("Tahoma", 11F, FontStyle.Underline, GraphicsUnit.Point);
            m_CurrentMoveLabel.Location = new Point(7, 105);
            m_CurrentMoveLabel.ForeColor = Color.Black;
            m_CurrentMoveLabel.Size = new Size(63, 21);
            m_CurrentMoveLabel.Text = "Current:";
            m_CurrentMoveLabel.Visible = false;
            m_MainPanel.Controls.Add(m_CurrentMoveLabel);

            m_CurrentCoinPictureBox = new PictureBox();

            m_CurrentCoinPictureBox.Location = new Point(70, 100);
            m_CurrentCoinPictureBox.Size = new Size(32, 32);
            m_CurrentCoinPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            m_MainPanel.Controls.Add(m_CurrentCoinPictureBox);
            
            Label movesLabel = new Label();

            movesLabel.Font = new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Point);
            movesLabel.Location = new Point(7, 140);
            movesLabel.ForeColor = Color.Black;
            movesLabel.Size = new Size(70, 21);
            movesLabel.Text = "Moves:";
            m_MainPanel.Controls.Add(movesLabel);
        }

        /// <summary>
        /// Initializes the list view.
        /// </summary>
        private void initializeListView()
        {
            m_MovesListView = new ListView();
            m_MovesListView.View = View.Details;

            m_MovesListView.Size = new Size(191, 296);
            m_MovesListView.Location = new Point(0, 164);
            m_MovesListView.Columns.Add(new ColumnHeader() { Text =  "#", Width = 50, TextAlign = HorizontalAlignment.Center });
            m_MovesListView.Columns.Add(new ColumnHeader() { Text = "Color", Width = 77, TextAlign = HorizontalAlignment.Center });
            m_MovesListView.Columns.Add(new ColumnHeader() { Text = "Position", Width = 56, TextAlign = HorizontalAlignment.Center });
            m_MainPanel.Controls.Add(m_MovesListView);
        }
    }
}
