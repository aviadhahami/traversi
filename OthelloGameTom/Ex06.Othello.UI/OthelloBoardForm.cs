using System;
using System.Windows.Forms;
using Othello.Logic;
using System.ComponentModel;
using System.Drawing;

namespace Ex06.Othello.UI
{
    public partial class OthelloBoardForm : Form
    {
        private static readonly int sr_DefaultSize = 8;
        public Statistics m_Statistics = null;
        private StatistcsForm m_StatistcsForm = null;
        private Player m_BlackPlayer = null;
        private Player m_WhitePlayer = null;
        private string m_MoveFormat = "{0}'s move...";
        private bool m_ShowPreviewMoves = true;
        private bool m_ShowValidMoves = true;
        private MenuStrip othelloMenu;
        private Panel m_Panel1 = new Panel();
        private Label m_Label5 = new Label();
        private Label m_Label4 = new Label();
        private Label m_Label3 = new Label();
        private Label m_Label2 = new Label();
        private PictureBox m_PictureBox3 = new PictureBox();
        private Label m_Label1 = new Label();
        private PictureBox m_PictureBox1 = new PictureBox();
        private ToolStripMenuItem m_GameMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_OptionsMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_PlayersMenuItem = new ToolStripMenuItem();
        private ToolStrip m_OthelloToolStrip = new ToolStrip();
        private ToolStripButton m_ToolStripNewGame = new ToolStripButton();
        private ToolStripSeparator m_ToolStripSeparator1 = new ToolStripSeparator();
        private ToolStripButton m_ToolStripShowValidMoves = new ToolStripButton();
        private ToolStripButton m_ToolStripShowPreviewMoves = new ToolStripButton();
        private ToolStripSeparator m_ToolStripSeparator2 = new ToolStripSeparator();
        private ToolStripLabel m_ToolStripLabel1 = new ToolStripLabel();
        private ToolStripDropDownButton m_BlackCoinMenu = new ToolStripDropDownButton();
        private ToolStripDropDownButton m_WhiteCoinMenu = new ToolStripDropDownButton();
        private ToolStripMenuItem m_ToolStripBlackUserMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_ToolStripBlackComputerMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_ToolStripWhiteUserMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_ToolStripWhiteComputerMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_BlackToolStripMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_WhiteToolStripMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_WhitePlayerUserMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_WhitePlayerComputerMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_BlackPlayerUserMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_BlackPlayerComputerMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_ShowValidMovesToolStripMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_ShowPreviewMovesToolStripMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem m_NewGameToolStripMenuItem = new ToolStripMenuItem();
        private ToolStripSeparator m_ToolStripSeparator3 = new ToolStripSeparator();
        private ToolStripMenuItem m_StatisticsMenuItem = new ToolStripMenuItem();
        private ToolStripSeparator m_ToolStripSeparator4 = new ToolStripSeparator();
        private ToolStripMenuItem m_ExitMenuItem = new ToolStripMenuItem();
        private StatusStrip m_StatusStrip1 = new StatusStrip();
        private ToolStripStatusLabel m_GameStatusBar = new ToolStripStatusLabel();
        private OthelloBoardControl m_OthelloBoardControl = null;
        private OthelloMoves m_OthelloMoves1 = new OthelloMoves();

        /// <summary>
        /// Initializes a new instance of the <see cref="OthelloBoardForm"/> class.
        /// </summary>
        public OthelloBoardForm()
            : this(sr_DefaultSize) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OthelloBoardForm"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public OthelloBoardForm(int size)
        {
            m_OthelloBoardControl = new OthelloBoardControl(size);
            initializeComponents();
            m_ShowValidMoves = m_ToolStripShowValidMoves.Checked = true;
            m_ShowPreviewMoves = m_ToolStripShowPreviewMoves.Checked = false;
            m_Statistics = new Statistics();
            m_StatistcsForm = new StatistcsForm();
            m_OthelloBoardControl.OnGameOver += new EventHandler<EndGameEventArgs>(m_LogicBoard_OnGameOver);
            m_OthelloBoardControl.OnTurnPassed += new EventHandler<PassTurnEventArgs>(m_LogicBoard_OnTurnPassed);
            m_OthelloBoardControl.OnTurnEnded += new EventHandler<UserInteractionEndTurnEventArgs>(othelloBoardControl_OnTurnEnded);
            m_OthelloBoardControl.OnGameStarted += new EventHandler<EndTurnEventArgs>(othelloBoardControl_OnGameStarted);
            m_OthelloBoardControl.ShowOptionalMoves = m_ShowValidMoves;
            setInitialPlayersConfiguration();
            m_StatistcsForm.OnStatistcsFormClose += delegate(object sender, EventArgs e)
            {
                m_StatistcsForm.Hide();
            };
        }

        /// <summary>
        /// Initializes the components.
        /// </summary>
        private void initializeComponents()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(OthelloBoardForm));
            othelloMenu = new MenuStrip();
            m_GameMenuItem = new ToolStripMenuItem();
            m_NewGameToolStripMenuItem = new ToolStripMenuItem();
            m_ToolStripSeparator3 = new ToolStripSeparator();
            m_StatisticsMenuItem = new ToolStripMenuItem();
            m_ToolStripSeparator4 = new ToolStripSeparator();
            m_ExitMenuItem = new ToolStripMenuItem();
            m_OptionsMenuItem = new ToolStripMenuItem();
            m_ShowValidMovesToolStripMenuItem = new ToolStripMenuItem();
            m_ShowPreviewMovesToolStripMenuItem = new ToolStripMenuItem();
            m_PlayersMenuItem = new ToolStripMenuItem();
            m_BlackToolStripMenuItem = new ToolStripMenuItem();
            m_BlackPlayerUserMenuItem = new ToolStripMenuItem();
            m_BlackPlayerComputerMenuItem = new ToolStripMenuItem();
            m_WhiteToolStripMenuItem = new ToolStripMenuItem();
            m_WhitePlayerUserMenuItem = new ToolStripMenuItem();
            m_WhitePlayerComputerMenuItem = new ToolStripMenuItem();
            m_OthelloToolStrip = new ToolStrip();
            m_ToolStripNewGame = new ToolStripButton();
            m_ToolStripSeparator1 = new ToolStripSeparator();
            m_ToolStripShowValidMoves = new ToolStripButton();
            m_ToolStripShowPreviewMoves = new ToolStripButton();
            m_ToolStripSeparator2 = new ToolStripSeparator();
            m_ToolStripLabel1 = new ToolStripLabel();
            m_BlackCoinMenu = new ToolStripDropDownButton();
            m_ToolStripBlackUserMenuItem = new ToolStripMenuItem();
            m_ToolStripBlackComputerMenuItem = new ToolStripMenuItem();
            m_WhiteCoinMenu = new ToolStripDropDownButton();
            m_ToolStripWhiteUserMenuItem = new ToolStripMenuItem();
            m_ToolStripWhiteComputerMenuItem = new ToolStripMenuItem();
            m_StatusStrip1 = new StatusStrip();
            m_GameStatusBar = new ToolStripStatusLabel();
            m_OthelloMoves1 = new OthelloMoves();
            m_Panel1 = new Panel();
            m_Label5 = new Label();
            m_Label4 = new Label();
            m_Label3 = new Label();
            m_Label2 = new Label();
            m_PictureBox3 = new PictureBox();
            m_Label1 = new Label();
            m_PictureBox1 = new PictureBox();
            othelloMenu.BackColor = Color.CadetBlue;
            othelloMenu.Items.AddRange(new ToolStripItem[] {
            m_GameMenuItem,
            m_OptionsMenuItem,
            m_PlayersMenuItem});
            othelloMenu.Location = new Point(0, 0);
            othelloMenu.Name = "othelloMenu";
            othelloMenu.Size = new Size(649, 24);
            othelloMenu.TabIndex = 0;
            othelloMenu.Text = "othelloMenu";
            m_GameMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            m_NewGameToolStripMenuItem,
            m_ToolStripSeparator3,
            m_StatisticsMenuItem,
            m_ToolStripSeparator4,
            m_ExitMenuItem});
            m_GameMenuItem.Name = "gameMenuItem";
            m_GameMenuItem.Size = new Size(50, 20);
            m_GameMenuItem.Text = "Game";
            m_NewGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            m_NewGameToolStripMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.N)));
            m_NewGameToolStripMenuItem.Size = new Size(175, 22);
            m_NewGameToolStripMenuItem.Text = "New Game";
            m_NewGameToolStripMenuItem.Click += new EventHandler(newGameMenuItem_Click);
            m_ToolStripSeparator3.Name = "toolStripSeparator3";
            m_ToolStripSeparator3.Size = new Size(172, 6);
            m_StatisticsMenuItem.Name = "statisticsMenuItem";
            m_StatisticsMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.S)));
            m_StatisticsMenuItem.Size = new Size(175, 22);
            m_StatisticsMenuItem.Text = "Statistics...";
            m_StatisticsMenuItem.Click += new EventHandler(statisticsMenuItem_Click);
            m_ToolStripSeparator4.Name = "toolStripSeparator4";
            m_ToolStripSeparator4.Size = new Size(172, 6);
            m_ExitMenuItem.Name = "exitMenuItem";
            m_ExitMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.X)));
            m_ExitMenuItem.Size = new Size(175, 22);
            m_ExitMenuItem.Text = "Exit";
            m_ExitMenuItem.Click += new EventHandler(exitMenuItem_Click);
            m_OptionsMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            m_ShowValidMovesToolStripMenuItem,
            m_ShowPreviewMovesToolStripMenuItem});
            m_OptionsMenuItem.Name = "optionsMenuItem";
            m_OptionsMenuItem.Size = new Size(61, 20);
            m_OptionsMenuItem.Text = "Options";
            m_ShowValidMovesToolStripMenuItem.Checked = true;
            m_ShowValidMovesToolStripMenuItem.CheckState = CheckState.Checked;
            m_ShowValidMovesToolStripMenuItem.Image = OthelloResources.IconShowValidMoves;
            m_ShowValidMovesToolStripMenuItem.Name = "showValidMovesToolStripMenuItem";
            m_ShowValidMovesToolStripMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.I)));
            m_ShowValidMovesToolStripMenuItem.Size = new Size(207, 22);
            m_ShowValidMovesToolStripMenuItem.Text = "Show Valid Moves";
            m_ShowValidMovesToolStripMenuItem.Click += new EventHandler(menuShowValidMoves_Click);
            m_ShowPreviewMovesToolStripMenuItem.Image = OthelloResources.IconShowPreview;
            m_ShowPreviewMovesToolStripMenuItem.Name = "showPreviewMovesToolStripMenuItem";
            m_ShowPreviewMovesToolStripMenuItem.ShortcutKeys = ((Keys)((Keys.Control | Keys.P)));
            m_ShowPreviewMovesToolStripMenuItem.Size = new Size(207, 22);
            m_ShowPreviewMovesToolStripMenuItem.Text = "&Preview Moves";
            m_ShowPreviewMovesToolStripMenuItem.Click += new EventHandler(menuShowPreviewMoves_Click);
            m_PlayersMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            m_BlackToolStripMenuItem,
            m_WhiteToolStripMenuItem});
            m_PlayersMenuItem.Name = "playersMenuItem";
            m_PlayersMenuItem.Size = new Size(56, 20);
            m_PlayersMenuItem.Text = "Players";
            m_BlackToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            m_BlackPlayerUserMenuItem,
            m_BlackPlayerComputerMenuItem});
            m_BlackToolStripMenuItem.Image = OthelloResources.CoinBlack;
            m_BlackToolStripMenuItem.Name = "blackToolStripMenuItem";
            m_BlackToolStripMenuItem.Size = new Size(105, 22);
            m_BlackToolStripMenuItem.Text = "Black";
            m_BlackPlayerUserMenuItem.Checked = true;
            m_BlackPlayerUserMenuItem.CheckState = CheckState.Checked;
            m_BlackPlayerUserMenuItem.Name = "blackPlayerUserMenuItem";
            m_BlackPlayerUserMenuItem.Size = new Size(128, 22);
            m_BlackPlayerUserMenuItem.Text = "User";
            m_BlackPlayerUserMenuItem.Click += new EventHandler(blackUserMenuItem_Click);
            m_BlackPlayerComputerMenuItem.Name = "blackPlayerComputerMenuItem";
            m_BlackPlayerComputerMenuItem.Size = new Size(128, 22);
            m_BlackPlayerComputerMenuItem.Text = "Computer";
            m_BlackPlayerComputerMenuItem.Click += new EventHandler(blackComputerMenuItem_Click);
            m_WhiteToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            m_WhitePlayerUserMenuItem,
            m_WhitePlayerComputerMenuItem});
            m_WhiteToolStripMenuItem.Image = OthelloResources.CoinWhite;
            m_WhiteToolStripMenuItem.Name = "whiteToolStripMenuItem";
            m_WhiteToolStripMenuItem.Size = new Size(105, 22);
            m_WhiteToolStripMenuItem.Text = "White";
            m_WhitePlayerUserMenuItem.Checked = true;
            m_WhitePlayerUserMenuItem.CheckState = CheckState.Checked;
            m_WhitePlayerUserMenuItem.Name = "whitePlayerUserMenuItem";
            m_WhitePlayerUserMenuItem.Size = new Size(128, 22);
            m_WhitePlayerUserMenuItem.Text = "User";
            m_WhitePlayerUserMenuItem.Click += new EventHandler(whiteUserMenuItem_Click);
            m_WhitePlayerComputerMenuItem.Name = "whitePlayerComputerMenuItem";
            m_WhitePlayerComputerMenuItem.Size = new Size(128, 22);
            m_WhitePlayerComputerMenuItem.Text = "Computer";
            m_WhitePlayerComputerMenuItem.Click += new EventHandler(whiteComputerMenuItem_Click);
            m_OthelloToolStrip.BackColor = Color.Honeydew;
            m_OthelloToolStrip.Items.AddRange(new ToolStripItem[] {
            m_ToolStripNewGame,
            m_ToolStripSeparator1,
            m_ToolStripShowValidMoves,
            m_ToolStripShowPreviewMoves,
            m_ToolStripSeparator2,
            m_ToolStripLabel1,
            m_BlackCoinMenu,
            m_WhiteCoinMenu});
            m_OthelloToolStrip.Location = new Point(0, 24);
            m_OthelloToolStrip.Name = "othelloToolStrip";
            m_OthelloToolStrip.RenderMode = ToolStripRenderMode.System;
            m_OthelloToolStrip.Size = new Size(649, 25);
            m_OthelloToolStrip.TabIndex = 1;
            m_OthelloToolStrip.Text = "toolStrip1";
            m_ToolStripNewGame.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_ToolStripNewGame.Image = OthelloResources.IconNewGame;
            m_ToolStripNewGame.ImageTransparentColor = Color.Magenta;
            m_ToolStripNewGame.Name = "toolStripNewGame";
            m_ToolStripNewGame.Size = new Size(23, 22);
            m_ToolStripNewGame.Text = "New Game";
            m_ToolStripNewGame.Click += new EventHandler(newGameMenuItem_Click);
            m_ToolStripSeparator1.Name = "toolStripSeparator1";
            m_ToolStripSeparator1.Size = new Size(6, 25);
            m_ToolStripShowValidMoves.CheckOnClick = true;
            m_ToolStripShowValidMoves.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_ToolStripShowValidMoves.Image = OthelloResources.IconShowValidMoves;
            m_ToolStripShowValidMoves.ImageTransparentColor = Color.Magenta;
            m_ToolStripShowValidMoves.Name = "toolStripShowValidMoves";
            m_ToolStripShowValidMoves.Size = new Size(23, 22);
            m_ToolStripShowValidMoves.Text = "Show Valid Moves";
            m_ToolStripShowValidMoves.Click += new EventHandler(menuShowValidMoves_Click);
            m_ToolStripShowPreviewMoves.DisplayStyle = ToolStripItemDisplayStyle.Image;
            m_ToolStripShowPreviewMoves.Image = OthelloResources.IconShowPreview;
            m_ToolStripShowPreviewMoves.ImageTransparentColor = Color.Magenta;
            m_ToolStripShowPreviewMoves.Name = "toolStripShowPreviewMoves";
            m_ToolStripShowPreviewMoves.Size = new Size(23, 22);
            m_ToolStripShowPreviewMoves.Text = "Show Preview Moves";
            m_ToolStripShowPreviewMoves.Click += new EventHandler(menuShowPreviewMoves_Click);
            m_ToolStripSeparator2.Name = "toolStripSeparator2";
            m_ToolStripSeparator2.Size = new Size(6, 25);
            m_ToolStripLabel1.Name = "toolStripLabel1";
            m_ToolStripLabel1.Size = new Size(0, 22);
            m_BlackCoinMenu.DropDownItems.AddRange(new ToolStripItem[] {
            m_ToolStripBlackUserMenuItem,
            m_ToolStripBlackComputerMenuItem});
            m_BlackCoinMenu.Image = OthelloResources.CoinBlack;
            m_BlackCoinMenu.ImageTransparentColor = Color.Magenta;
            m_BlackCoinMenu.Name = "blackCoinMenu";
            m_BlackCoinMenu.Size = new Size(64, 22);
            m_BlackCoinMenu.Text = "Black";
            m_ToolStripBlackUserMenuItem.Checked = true;
            m_ToolStripBlackUserMenuItem.CheckState = CheckState.Checked;
            m_ToolStripBlackUserMenuItem.Name = "toolStripBlackUserMenuItem";
            m_ToolStripBlackUserMenuItem.Size = new Size(128, 22);
            m_ToolStripBlackUserMenuItem.Text = "User";
            m_ToolStripBlackUserMenuItem.Click += new EventHandler(blackUserMenuItem_Click);
            m_ToolStripBlackComputerMenuItem.Name = "toolStripBlackComputerMenuItem";
            m_ToolStripBlackComputerMenuItem.Size = new Size(128, 22);
            m_ToolStripBlackComputerMenuItem.Text = "Computer";
            m_ToolStripBlackComputerMenuItem.Click += new EventHandler(blackComputerMenuItem_Click);
            m_WhiteCoinMenu.DropDownItems.AddRange(new ToolStripItem[] {
            m_ToolStripWhiteUserMenuItem,
            m_ToolStripWhiteComputerMenuItem});
            m_WhiteCoinMenu.Image = OthelloResources.CoinWhite;
            m_WhiteCoinMenu.ImageTransparentColor = Color.Magenta;
            m_WhiteCoinMenu.Name = "whiteCoinMenu";
            m_WhiteCoinMenu.Size = new Size(67, 22);
            m_WhiteCoinMenu.Text = "White";
            m_ToolStripWhiteUserMenuItem.Checked = true;
            m_ToolStripWhiteUserMenuItem.CheckState = CheckState.Checked;
            m_ToolStripWhiteUserMenuItem.Name = "toolStripWhiteUserMenuItem";
            m_ToolStripWhiteUserMenuItem.Size = new Size(128, 22);
            m_ToolStripWhiteUserMenuItem.Text = "User";
            m_ToolStripWhiteUserMenuItem.Click += new EventHandler(whiteUserMenuItem_Click);
            m_ToolStripWhiteComputerMenuItem.Name = "toolStripWhiteComputerMenuItem";
            m_ToolStripWhiteComputerMenuItem.Size = new Size(128, 22);
            m_ToolStripWhiteComputerMenuItem.Text = "Computer";
            m_ToolStripWhiteComputerMenuItem.Click += new EventHandler(whiteComputerMenuItem_Click);
            m_StatusStrip1.BackColor = SystemColors.Control;
            m_StatusStrip1.Items.AddRange(new ToolStripItem[] {
            m_GameStatusBar});
            m_StatusStrip1.Location = new Point(0, 509);
            m_StatusStrip1.Name = "statusStrip1";
            m_StatusStrip1.Size = new Size(649, 22);
            m_StatusStrip1.TabIndex = 2;
            m_StatusStrip1.Text = "statusStrip1";
            m_GameStatusBar.Margin = new Padding(18, 3, 0, 2);
            m_GameStatusBar.Name = "gameStatusBar";
            m_GameStatusBar.Size = new Size(0, 17);
            m_OthelloMoves1.BackColor = Color.SlateGray;
            m_OthelloMoves1.Controls.Add(m_Panel1);
            m_OthelloMoves1.Dock = DockStyle.Right;
            m_OthelloMoves1.Location = new Point(459, 49);
            m_OthelloMoves1.Margin = new Padding(0);
            m_OthelloMoves1.Name = "othelloMoves1";
            m_OthelloMoves1.Size = new Size(190, 460);
            m_OthelloMoves1.TabIndex = 4;
            m_Panel1.BackColor = Color.SlateGray;
            m_Panel1.Controls.Add(m_Label5);
            m_Panel1.Controls.Add(m_Label4);
            m_Panel1.Controls.Add(m_Label3);
            m_Panel1.Controls.Add(m_Label2);
            m_Panel1.Controls.Add(m_PictureBox3);
            m_Panel1.Controls.Add(m_Label1);
            m_Panel1.Controls.Add(m_PictureBox1);
            m_Panel1.Location = new Point(2, 0);
            m_Panel1.Name = "panel1";
            m_Panel1.Size = new Size(185, 460);
            m_Panel1.TabIndex = 1;
            m_Label5.AutoSize = true;
            m_Label5.Font = new Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(177)));
            m_Label5.Location = new Point(9, 126);
            m_Label5.Name = "label5";
            m_Label5.Size = new Size(54, 17);
            m_Label5.TabIndex = 7;
            m_Label5.Text = "Moves:";
            m_Label4.AutoSize = true;
            m_Label4.Font = new Font("Arial", 13F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(177)));
            m_Label4.Location = new Point(115, 30);
            m_Label4.Name = "label4";
            m_Label4.Size = new Size(30, 21);
            m_Label4.TabIndex = 6;
            m_Label4.Text = "32";
            m_Label3.AutoSize = true;
            m_Label3.Font = new Font("Arial", 13F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(177)));
            m_Label3.ForeColor = Color.White;
            m_Label3.Location = new Point(115, 68);
            m_Label3.Name = "label3";
            m_Label3.Size = new Size(30, 21);
            m_Label3.TabIndex = 5;
            m_Label3.Text = "32";
            m_Label2.AutoSize = true;
            m_Label2.Font = new Font("Arial", 13F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(177)));
            m_Label2.ForeColor = Color.White;
            m_Label2.Location = new Point(44, 67);
            m_Label2.Name = "label2";
            m_Label2.Size = new Size(67, 21);
            m_Label2.TabIndex = 4;
            m_Label2.Text = "White:";
            m_PictureBox3.Image = OthelloResources.CoinWhite;
            m_PictureBox3.Location = new Point(7, 62);
            m_PictureBox3.Name = "pictureBox3";
            m_PictureBox3.Size = new Size(30, 30);
            m_PictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            m_PictureBox3.TabIndex = 3;
            m_PictureBox3.TabStop = false;
            m_Label1.AutoSize = true;
            m_Label1.Font = new Font("Arial", 13F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(177)));
            m_Label1.Location = new Point(46, 30);
            m_Label1.Name = "label1";
            m_Label1.Size = new Size(65, 21);
            m_Label1.TabIndex = 2;
            m_Label1.Text = "Black:";
            m_PictureBox1.Image = OthelloResources.CoinBlack;
            m_PictureBox1.Location = new Point(7, 25);
            m_PictureBox1.Name = "pictureBox1";
            m_PictureBox1.Size = new Size(30, 30);
            m_PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            m_PictureBox1.TabIndex = 0;
            m_PictureBox1.TabStop = false;
            m_OthelloBoardControl.AutoSize = true;
            m_OthelloBoardControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            m_OthelloBoardControl.Dock = DockStyle.Left;
            m_OthelloBoardControl.Location = new Point(0, 49);
            m_OthelloBoardControl.Margin = new Padding(0);
            m_OthelloBoardControl.Name = "othelloBoardControl1";
            m_OthelloBoardControl.ShowOptionalMoves = false;
            m_OthelloBoardControl.ShowPreviewMoves = false;
            m_OthelloBoardControl.Size = new Size(460, 460);
            m_OthelloBoardControl.TabIndex = 3;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            ClientSize = new Size(649, 531);
            Controls.Add(m_OthelloMoves1);
            Controls.Add(m_OthelloBoardControl);
            Controls.Add(m_StatusStrip1);
            Controls.Add(m_OthelloToolStrip);
            Controls.Add(othelloMenu);
            Icon = OthelloResources.AppIcon;
            MainMenuStrip = othelloMenu;
            Name = "OthelloBoardForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Othello";
        }

        /// <summary>
        /// Handles the OnGameStarted event of the othelloBoardControl control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EndTurnEventArgs">The <see cref="Ex06.Othello.Logic.EndTurnEventArgs"/> instance containing the event data.</param>
        private void othelloBoardControl_OnGameStarted(object i_Sender, EndTurnEventArgs i_EndTurnEventArgs)
        {
            m_GameStatusBar.Text = string.Format(m_MoveFormat, i_EndTurnEventArgs.CurrentPlayer);
            m_OthelloMoves1.ClearMoves();
            m_OthelloMoves1.SetMove(i_EndTurnEventArgs.CurrentPlayer, i_EndTurnEventArgs.BlackCoinsCount, i_EndTurnEventArgs.WhiteCoinsCount);
        }

        /// <summary>
        /// Handles the OnTurnEnded event of the othelloBoardControl control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_UserInteractionEndTurnEventArgs">The <see cref="Ex06.Othello.Logic.UserInteractionEndTurnEventArgs"/> instance containing the event data.</param>
        private void othelloBoardControl_OnTurnEnded(object i_Sender, UserInteractionEndTurnEventArgs i_UserInteractionEndTurnEventArgs)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<UserInteractionEndTurnEventArgs>(handleTurnEnd), new[] { i_UserInteractionEndTurnEventArgs });
            }
            else
            {
                handleTurnEnd(i_UserInteractionEndTurnEventArgs);
            }
        }

        /// <summary>
        /// Handles the turn end.
        /// </summary>
        /// <param name="i_UserInteractionEndTurnEventArgs">The <see cref="Ex06.Othello.Logic.UserInteractionEndTurnEventArgs"/> instance containing the event data.</param>
        private void handleTurnEnd(UserInteractionEndTurnEventArgs i_UserInteractionEndTurnEventArgs)
        {
            m_GameStatusBar.Text = string.Format(m_MoveFormat, i_UserInteractionEndTurnEventArgs.NextPlayer);
            m_OthelloMoves1.SetUserMove(i_UserInteractionEndTurnEventArgs.CurrentPlayer, i_UserInteractionEndTurnEventArgs.NextPlayer, i_UserInteractionEndTurnEventArgs.BlackCoinsCount, i_UserInteractionEndTurnEventArgs.WhiteCoinsCount, i_UserInteractionEndTurnEventArgs.Position);
        }

        /// <summary>
        /// Sets the initial players configuration.
        /// </summary>
        private void setInitialPlayersConfiguration()
        {
            m_BlackPlayer = new Player(ePlayerColor.Black, ePlayerMode.User);
            m_WhitePlayer = new Player(ePlayerColor.White, ePlayerMode.User);
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the Click event of the statisticsMenuItem control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void statisticsMenuItem_Click(object i_Sender, EventArgs i_EventArgs)
        {
            m_StatistcsForm.ShowDialog();
            m_OthelloBoardControl.Refresh();
        }

        /// <summary>
        /// Handles the Click event of the newGameMenuItem control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void newGameMenuItem_Click(object i_Sender, EventArgs i_EventArgs)
        {
            m_GameStatusBar.Text = string.Format(m_MoveFormat, ePlayerColor.Black);
            m_OthelloBoardControl.NewGame(m_BlackPlayer, m_WhitePlayer);
        }

        /// <summary>
        /// Handles the OnTurnPassed event of the m_LogicBoard control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_PassTurnEventArgs">The <see cref="Ex06.Othello.Logic.PassTurnEventArgs"/> instance containing the event data.</param>
        private void m_LogicBoard_OnTurnPassed(object i_Sender, PassTurnEventArgs i_PassTurnEventArgs)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new Action<PassTurnEventArgs>(onTurnPassed), new[] { i_PassTurnEventArgs });
            }
            else
            {
                onTurnPassed(i_PassTurnEventArgs);
            }
        }

        /// <summary>
        /// Handles the OnGameOver event of the m_LogicBoard control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EndGameEventArgs">The <see cref="Ex06.Othello.Logic.EndGameEventArgs"/> instance containing the event data.</param>
        private void m_LogicBoard_OnGameOver(object i_Sender, EndGameEventArgs i_EndGameEventArgs)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new Action<EndGameEventArgs>(onGameOver), new[] { i_EndGameEventArgs });
            }
            else
            {
                onGameOver(i_EndGameEventArgs);
            }
        }

        /// <summary>
        /// Ons the turn passed.
        /// </summary>
        /// <param name="i_PassTurnEventArgs">The <see cref="Ex06.Othello.Logic.PassTurnEventArgs"/> instance containing the event data.</param>
        private void onTurnPassed(PassTurnEventArgs i_PassTurnEventArgs)
        {
            ePlayerColor passedFrom = i_PassTurnEventArgs.PassedFrom;
            ePlayerColor passedTo = i_PassTurnEventArgs.PassedTo;

            m_OthelloMoves1.SetNextPlayer(passedTo);
            m_GameStatusBar.Text = string.Format(m_MoveFormat, passedTo);
        }

        /// <summary>
        /// Ons the game over.
        /// </summary>
        /// <param name="i_EndGameEventArgs">The <see cref="Ex06.Othello.Logic.EndGameEventArgs"/> instance containing the event data.</param>
        private void onGameOver(EndGameEventArgs i_EndGameEventArgs)
        {
            int blackCount = 0;
            int whiteCount = 0;
            ePlayerColor winner = i_EndGameEventArgs.Winner;
            int winnerCount = i_EndGameEventArgs.WinnerCount;
            int loserCount = i_EndGameEventArgs.LoserCount;
            Statistics gameStatistics = new Statistics();
            Player winnerPlayer = null;
            bool againstComputer = m_BlackPlayer.PlayerMode == ePlayerMode.Computer || m_WhitePlayer.PlayerMode == ePlayerMode.Computer;

            if (i_EndGameEventArgs.HasWinner)
            {
                m_GameStatusBar.Text = string.Format("{0} Won", winner);

                if (winner == ePlayerColor.Black)
                {
                    blackCount = winnerCount;
                    whiteCount = loserCount;
                    gameStatistics.BlackStatistics.TotalScore = winnerCount;
                    gameStatistics.BlackStatistics.TotalWins = 1;
                    gameStatistics.WhiteStatistics.TotalScore = loserCount;
                    winnerPlayer = m_BlackPlayer;
                }
                else
                {
                    whiteCount = winnerCount;
                    blackCount = loserCount;
                    gameStatistics.WhiteStatistics.TotalScore = winnerCount;
                    gameStatistics.WhiteStatistics.TotalWins = 1;
                    gameStatistics.BlackStatistics.TotalScore = loserCount;
                    winnerPlayer = m_WhitePlayer;
                }

                if (againstComputer)
                {
                    if (winnerPlayer.PlayerMode == ePlayerMode.Computer)
                    {
                        gameStatistics.ComputerStatistics.TotalScore = winnerCount;
                        gameStatistics.ComputerStatistics.TotalWins = 1;
                        gameStatistics.UserStatistics.TotalScore = loserCount;
                    }
                    else
                    {
                        gameStatistics.UserStatistics.TotalScore = winnerCount;
                        gameStatistics.UserStatistics.TotalWins = 1;
                        gameStatistics.ComputerStatistics.TotalScore = loserCount;
                    }
                }
            }
            else
            {
                if (againstComputer)
                {
                    gameStatistics.ComputerStatistics.TotalScore = winnerCount;
                    gameStatistics.UserStatistics.TotalScore = winnerCount;
                    gameStatistics.AgainstComputerDraws = 1;
                }

                gameStatistics.WhiteStatistics.TotalScore = winnerCount;
                gameStatistics.BlackStatistics.TotalScore = winnerCount;
                gameStatistics.OverallDraws = 1;
                m_GameStatusBar.Text = "Draw";
            }

            m_StatistcsForm.UpdateScores(gameStatistics);
            m_OthelloMoves1.SetMove(winner, blackCount, whiteCount);
        }

        /// <summary>
        /// Handles the Click event of the blackUserMenuItem control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void blackUserMenuItem_Click(object i_Sender, EventArgs i_EventArgs)
        {
            m_BlackPlayer.PlayerMode = ePlayerMode.User;
            m_ToolStripBlackUserMenuItem.Checked = true;
            m_BlackPlayerUserMenuItem.Checked = true;

            m_ToolStripBlackComputerMenuItem.Checked = false;
            m_BlackPlayerComputerMenuItem.Checked = false;
        }

        /// <summary>
        /// Handles the Click event of the blackComputerMenuItem control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void blackComputerMenuItem_Click(object i_Sender, EventArgs i_EventArgs)
        {
            m_BlackPlayer.PlayerMode = ePlayerMode.Computer;
            m_ToolStripBlackComputerMenuItem.Checked = true;
            m_BlackPlayerComputerMenuItem.Checked = true;

            m_ToolStripBlackUserMenuItem.Checked = false;
            m_BlackPlayerUserMenuItem.Checked = false;

            if (m_WhitePlayer.PlayerMode == ePlayerMode.Computer)
            {
                m_ToolStripWhiteUserMenuItem.Checked = true;
                m_WhitePlayerUserMenuItem.Checked = true;
                m_ToolStripWhiteComputerMenuItem.Checked = false;
                m_WhitePlayerComputerMenuItem.Checked = false;
                m_WhitePlayer.PlayerMode = ePlayerMode.User;
            }
        }

        /// <summary>
        /// Whites the computer menu item_ click.
        /// </summary>
        /// <param name="i_Sender">The i_ sender.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void whiteComputerMenuItem_Click(object i_Sender, EventArgs i_EventArgs)
        {
            m_WhitePlayer.PlayerMode = ePlayerMode.Computer;
            m_ToolStripWhiteComputerMenuItem.Checked = true;
            m_WhitePlayerComputerMenuItem.Checked = true;

            m_ToolStripWhiteUserMenuItem.Checked = false;
            m_WhitePlayerUserMenuItem.Checked = false;

            if (m_BlackPlayer.PlayerMode == ePlayerMode.Computer)
            {
                m_ToolStripBlackUserMenuItem.Checked = true;
                m_BlackPlayerUserMenuItem.Checked = true;
                m_ToolStripBlackComputerMenuItem.Checked = false;
                m_BlackPlayerComputerMenuItem.Checked = false;
                m_BlackPlayer.PlayerMode = ePlayerMode.User;
            }
        }

        /// <summary>
        /// Handles the Click event of the whiteUserMenuItem control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void whiteUserMenuItem_Click(object i_Sender, EventArgs i_EventArgs)
        {
            m_WhitePlayer.PlayerMode = ePlayerMode.User;
            m_ToolStripWhiteUserMenuItem.Checked = true;
            m_WhitePlayerUserMenuItem.Checked = true;

            m_ToolStripWhiteComputerMenuItem.Checked = false;
            m_WhitePlayerComputerMenuItem.Checked = false;
        }

        /// <summary>
        /// Handles the Click event of the menuShowValidMoves control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuShowValidMoves_Click(object i_Sender, EventArgs i_EventArgs)
        {
            m_ShowValidMoves = !m_ShowValidMoves;
            m_ToolStripShowValidMoves.Checked = m_ShowValidMoves;
            m_OthelloBoardControl.ShowOptionalMoves = m_ShowValidMoves;
        }

        /// <summary>
        /// Handles the Click event of the menuShowPreviewMoves control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void menuShowPreviewMoves_Click(object i_Sender, EventArgs i_EventArgs)
        {
            m_ShowPreviewMoves = !m_ShowPreviewMoves;
            m_ToolStripShowPreviewMoves.Checked = m_ShowPreviewMoves;
            m_OthelloBoardControl.ShowPreviewMoves = m_ShowPreviewMoves;
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OthelloBoardForm));
            this.SuspendLayout();
            // 
            // OthelloBoardForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OthelloBoardForm";
            this.ResumeLayout(false);

        }
    }
}
