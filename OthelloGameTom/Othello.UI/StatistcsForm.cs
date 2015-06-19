using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Othello.Logic;
using Othello.UI;

namespace Othello.UI
{
    public partial class StatistcsForm : Form
    {
        private GroupBox m_TopGroupBox = new GroupBox();
        private GroupBox m_BottomGroupBox = new GroupBox();
        private Button m_CloseButton = new Button();
        private Button m_ResetButton = new Button();
        private Label m_OverallWhiteScoreLabel = new Label();
        private Label m_OverallBlackScoreLabel = new Label();
        private PictureBox m_BlackCoinPictureBox = new PictureBox();
        private PictureBox m_WhiteCoinPictureBox = new PictureBox();
        private Label m_OverallWhiteTotalLabel = new Label();
        private Label m_OverallBlackTotalLabel = new Label();
        private Label m_TopTotalScoreLabel = new Label();
        private Label m_BlackWinsLabel = new Label();
        private Label m_DrawsLabel = new Label();
        private Label m_WhiteWinsLabel = new Label();
        private Label m_VsCompDrawsLabel = new Label();
        private Label m_UserWinsLabel = new Label();
        private Label m_ComputerWinsLabel = new Label();
        private Label m_UserTotalScoreLabel = new Label();
        private Label m_ComputerTotalScoreLabel = new Label();
        private Label m_BottomTotalScoreLabel = new Label();
        private Label m_OverallDrawsLabel = new Label();
        private Label m_OverallWhiteWinsLabel = new Label();
        private Label m_OverallBlackWinsLabel = new Label();
        private Label m_VsComputerDrawsLabel = new Label();
        private Label m_VsComputerUserWinsLabel = new Label();
        private Label m_VsComputerComputerWinsLabel = new Label();
        private Label m_VsComputerUserScoreLabel = new Label();
        private Label m_VsComputerComputerScoreLabel = new Label();
        private static readonly string sr_Zero = "0";
        private Statistics m_LastStatistics = null;
        
        public event EventHandler OnStatistcsFormClose = null;

        public StatistcsForm()
        {
            m_LastStatistics = new Statistics();
            initializeComponents();
            reset();
        }

        /// <summary>
        /// Initializes the components.
        /// </summary>
        private void initializeComponents()
        {
            m_TopGroupBox.BackColor = SystemColors.Control;
            m_TopGroupBox.Controls.Add(m_OverallDrawsLabel);
            m_TopGroupBox.Controls.Add(m_OverallWhiteWinsLabel);
            m_TopGroupBox.Controls.Add(m_OverallBlackWinsLabel);
            m_TopGroupBox.Controls.Add(m_DrawsLabel);
            m_TopGroupBox.Controls.Add(m_WhiteWinsLabel);
            m_TopGroupBox.Controls.Add(m_WhiteCoinPictureBox);
            m_TopGroupBox.Controls.Add(m_BlackWinsLabel);
            m_TopGroupBox.Controls.Add(m_BlackCoinPictureBox);
            m_TopGroupBox.Controls.Add(m_OverallWhiteScoreLabel);
            m_TopGroupBox.Controls.Add(m_OverallBlackScoreLabel);
            m_TopGroupBox.Controls.Add(m_OverallWhiteTotalLabel);
            m_TopGroupBox.Controls.Add(m_OverallBlackTotalLabel);
            m_TopGroupBox.Controls.Add(m_TopTotalScoreLabel);
            m_TopGroupBox.Location = new Point(12, 12);
            m_TopGroupBox.Size = new Size(337, 137);
            m_TopGroupBox.TabIndex = 1;
            m_TopGroupBox.TabStop = false;
            m_TopGroupBox.Text = "Overall";
            m_OverallDrawsLabel.AutoSize = true;
            m_OverallDrawsLabel.Location = new Point(150, 107);
            m_OverallDrawsLabel.Size = new Size(13, 13);
            m_OverallWhiteWinsLabel.AutoSize = true;
            m_OverallWhiteWinsLabel.Location = new Point(150, 79);
            m_OverallWhiteWinsLabel.Size = new Size(13, 13);
            m_OverallWhiteWinsLabel.Text = "3";
            m_OverallBlackWinsLabel.AutoSize = true;
            m_OverallBlackWinsLabel.Location = new Point(150, 50);
            m_OverallBlackWinsLabel.Size = new Size(13, 13);
            m_OverallBlackWinsLabel.Text = "4";
            m_DrawsLabel.Location = new Point(66, 107);
            m_DrawsLabel.Size = new Size(40, 13);
            m_DrawsLabel.TabIndex = 9;
            m_DrawsLabel.Text = "Draws:";
            m_WhiteWinsLabel.AutoSize = true;
            m_WhiteWinsLabel.Location = new Point(42, 79);
            m_WhiteWinsLabel.Size = new Size(65, 13);
            m_WhiteWinsLabel.TabIndex = 8;
            m_WhiteWinsLabel.Text = "White Wins:";
            m_WhiteCoinPictureBox.Image = OthelloResources.CoinWhite;
            m_WhiteCoinPictureBox.Location = new Point(18, 76);
            m_WhiteCoinPictureBox.Size = new Size(20, 20);
            m_WhiteCoinPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            m_WhiteCoinPictureBox.TabIndex = 7;
            m_WhiteCoinPictureBox.TabStop = false;
            m_BlackWinsLabel.AutoSize = true;
            m_BlackWinsLabel.Location = new Point(42, 50);
            m_BlackWinsLabel.Size = new Size(64, 13);
            m_BlackWinsLabel.TabIndex = 6;
            m_BlackWinsLabel.Text = "Black Wins:";
            m_BlackCoinPictureBox.Image = OthelloResources.CoinBlack;
            m_BlackCoinPictureBox.Location = new Point(18, 47);
            m_BlackCoinPictureBox.Size = new Size(20, 20);
            m_BlackCoinPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            m_BlackCoinPictureBox.TabIndex = 5;
            m_BlackCoinPictureBox.TabStop = false;
            m_OverallWhiteScoreLabel.AutoSize = true;
            m_OverallWhiteScoreLabel.Location = new Point(294, 79);
            m_OverallWhiteScoreLabel.Size = new Size(25, 13);
            m_OverallWhiteScoreLabel.TabIndex = 4;
            m_OverallWhiteScoreLabel.Text = "309";
            m_OverallBlackScoreLabel.AutoSize = true;
            m_OverallBlackScoreLabel.Location = new Point(294, 50);
            m_OverallBlackScoreLabel.Size = new Size(25, 13);
            m_OverallBlackScoreLabel.TabIndex = 3;
            m_OverallBlackScoreLabel.Text = "195";
            m_OverallWhiteTotalLabel.AutoSize = true;
            m_OverallWhiteTotalLabel.Location = new Point(221, 79);
            m_OverallWhiteTotalLabel.Size = new Size(38, 13);
            m_OverallWhiteTotalLabel.TabIndex = 2;
            m_OverallWhiteTotalLabel.Text = "White:";
            m_OverallBlackTotalLabel.AutoSize = true;
            m_OverallBlackTotalLabel.Location = new Point(221, 50);
            m_OverallBlackTotalLabel.Size = new Size(37, 13);
            m_OverallBlackTotalLabel.TabIndex = 1;
            m_OverallBlackTotalLabel.Text = "Black:";
            m_TopTotalScoreLabel.AutoSize = true;
            m_TopTotalScoreLabel.Location = new Point(193, 19);
            m_TopTotalScoreLabel.Size = new Size(62, 13);
            m_TopTotalScoreLabel.TabIndex = 0;
            m_TopTotalScoreLabel.Text = "TotalScore:";
            m_BottomGroupBox.Controls.Add(m_VsComputerDrawsLabel);
            m_BottomGroupBox.Controls.Add(m_VsComputerUserWinsLabel);
            m_BottomGroupBox.Controls.Add(m_VsComputerComputerWinsLabel);
            m_BottomGroupBox.Controls.Add(m_VsCompDrawsLabel);
            m_BottomGroupBox.Controls.Add(m_UserWinsLabel);
            m_BottomGroupBox.Controls.Add(m_ComputerWinsLabel);
            m_BottomGroupBox.Controls.Add(m_VsComputerUserScoreLabel);
            m_BottomGroupBox.Controls.Add(m_VsComputerComputerScoreLabel);
            m_BottomGroupBox.Controls.Add(m_UserWinsLabel);
            m_BottomGroupBox.Controls.Add(m_ComputerWinsLabel);
            m_BottomGroupBox.Controls.Add(m_BottomTotalScoreLabel);
            m_BottomGroupBox.Controls.Add(m_UserTotalScoreLabel);
            m_BottomGroupBox.Controls.Add(m_ComputerTotalScoreLabel);
            m_BottomGroupBox.Controls.Add(m_VsCompDrawsLabel);
            m_BottomGroupBox.Location = new Point(12, 162);
            m_BottomGroupBox.Size = new Size(337, 137);
            m_BottomGroupBox.TabIndex = 2;
            m_BottomGroupBox.TabStop = false;
            m_BottomGroupBox.Text = "vs. Computer";
            m_VsComputerDrawsLabel.AutoSize = true;
            m_VsComputerDrawsLabel.Location = new Point(150, 105);
            m_VsComputerDrawsLabel.Size = new Size(13, 13);
            m_VsComputerDrawsLabel.TabIndex = 25;
            m_VsComputerDrawsLabel.Text = "0";
            m_VsComputerUserWinsLabel.AutoSize = true;
            m_VsComputerUserWinsLabel.Location = new Point(150, 78);
            m_VsComputerUserWinsLabel.Size = new Size(13, 13);
            m_VsComputerUserWinsLabel.TabIndex = 24;
            m_VsComputerUserWinsLabel.Text = "0";
            m_VsComputerComputerWinsLabel.AutoSize = true;
            m_VsComputerComputerWinsLabel.Location = new Point(150, 49);
            m_VsComputerComputerWinsLabel.Size = new Size(13, 13);
            m_VsComputerComputerWinsLabel.TabIndex = 23;
            m_VsComputerComputerWinsLabel.Text = "4";
            m_VsComputerDrawsLabel.AutoSize = true;
            m_VsCompDrawsLabel.Location = new Point(61, 105);
            m_VsCompDrawsLabel.Size = new Size(40, 13);
            m_VsCompDrawsLabel.TabIndex = 22;
            m_VsCompDrawsLabel.Text = "Draws:";
            m_UserWinsLabel.AutoSize = true;
            m_UserWinsLabel.Location = new Point(42, 78);
            m_UserWinsLabel.Size = new Size(59, 13);
            m_UserWinsLabel.TabIndex = 21;
            m_UserWinsLabel.Text = "User Wins:";
            m_ComputerWinsLabel.AutoSize = true;
            m_ComputerWinsLabel.Location = new Point(19, 49);
            m_ComputerWinsLabel.Size = new Size(82, 13);
            m_ComputerWinsLabel.TabIndex = 19;
            m_ComputerWinsLabel.Text = "Computer Wins:";
            m_VsComputerUserScoreLabel.AutoSize = true;
            m_VsComputerUserScoreLabel.Location = new Point(295, 78);
            m_VsComputerUserScoreLabel.Size = new Size(13, 13);
            m_VsComputerUserScoreLabel.TabIndex = 17;
            m_VsComputerUserScoreLabel.Text = "1";
            m_VsComputerComputerScoreLabel.AutoSize = true;
            m_VsComputerComputerScoreLabel.Location = new Point(294, 49);
            m_VsComputerComputerScoreLabel.Size = new Size(25, 13);
            m_VsComputerComputerScoreLabel.TabIndex = 16;
            m_VsComputerComputerScoreLabel.Text = "183";
            m_UserTotalScoreLabel.AutoSize = true;
            m_UserTotalScoreLabel.Location = new Point(243, 78);
            m_UserTotalScoreLabel.Size = new Size(32, 13);
            m_UserTotalScoreLabel.TabIndex = 15;
            m_UserTotalScoreLabel.Text = "User:";
            m_ComputerTotalScoreLabel.AutoSize = true;
            m_ComputerTotalScoreLabel.Location = new Point(221, 49);
            m_ComputerTotalScoreLabel.Size = new Size(55, 13);
            m_ComputerTotalScoreLabel.TabIndex = 14;
            m_ComputerTotalScoreLabel.Text = "Computer:";
            m_BottomTotalScoreLabel.AutoSize = true;
            m_BottomTotalScoreLabel.Location = new Point(193, 18);
            m_BottomTotalScoreLabel.Size = new Size(62, 13);
            m_BottomTotalScoreLabel.TabIndex = 13;
            m_BottomTotalScoreLabel.Text = "TotalScore:";
            m_CloseButton.Location = new Point(274, 308);
            m_CloseButton.Size = new Size(75, 23);
            m_CloseButton.TabIndex = 3;
            m_CloseButton.Text = "Close";
            m_CloseButton.UseVisualStyleBackColor = true;
            m_CloseButton.Click += new EventHandler(btnClose_Click);
            m_ResetButton.Location = new Point(186, 308);
            m_ResetButton.Size = new Size(75, 23);
            m_ResetButton.TabIndex = 4;
            m_ResetButton.Text = "Reset";
            m_ResetButton.UseVisualStyleBackColor = true;
            m_ResetButton.Click += new EventHandler(btnReset_Click);
            AcceptButton = m_CloseButton;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(364, 341);
            ControlBox = false;
            Controls.Add(m_ResetButton);
            Controls.Add(m_CloseButton);
            Controls.Add(m_TopGroupBox);
            Controls.Add(m_BottomGroupBox);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Statistcs";
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (OnStatistcsFormClose != null)
            {
                OnStatistcsFormClose(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the scores.
        /// </summary>
        /// <param name="statistics">The statistics.</param>
        public void UpdateScores(Statistics statistics)
        {
            m_LastStatistics = m_LastStatistics + statistics;

            m_OverallBlackScoreLabel.Text = m_LastStatistics.BlackStatistics.TotalScore.ToString();
            m_OverallWhiteScoreLabel.Text = m_LastStatistics.WhiteStatistics.TotalScore.ToString();
            m_OverallBlackWinsLabel.Text = m_LastStatistics.BlackStatistics.TotalWins.ToString();
            m_OverallWhiteWinsLabel.Text = m_LastStatistics.WhiteStatistics.TotalWins.ToString();
            m_OverallDrawsLabel.Text = m_LastStatistics.OverallDraws.ToString();

            m_VsComputerComputerScoreLabel.Text = m_LastStatistics.ComputerStatistics.TotalScore.ToString();
            m_VsComputerUserScoreLabel.Text = m_LastStatistics.UserStatistics.TotalScore.ToString();
            m_VsComputerComputerWinsLabel.Text = m_LastStatistics.ComputerStatistics.TotalWins.ToString();
            m_VsComputerUserWinsLabel.Text = m_LastStatistics.UserStatistics.TotalWins.ToString();
            m_VsComputerDrawsLabel.Text = m_LastStatistics.AgainstComputerDraws.ToString();
        }

        /// <summary>
        /// Handles the Click event of the btnReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        private void reset()
        {
            m_OverallBlackScoreLabel.Text = sr_Zero;
            m_OverallWhiteScoreLabel.Text = sr_Zero;
            m_OverallBlackWinsLabel.Text = sr_Zero;
            m_OverallWhiteWinsLabel.Text = sr_Zero;
            m_OverallDrawsLabel.Text = sr_Zero;

            m_VsComputerComputerScoreLabel.Text = sr_Zero;
            m_VsComputerUserScoreLabel.Text = sr_Zero;
            m_VsComputerComputerWinsLabel.Text = sr_Zero;
            m_VsComputerUserWinsLabel.Text = sr_Zero;
            m_VsComputerDrawsLabel.Text = sr_Zero;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StatistcsForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "StatistcsForm";
            this.Load += new System.EventHandler(this.StatistcsForm_Load);
            this.ResumeLayout(false);

        }

        private void StatistcsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
