using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using Othello.Logic;

namespace Othello.UI
{
    public partial class OthelloBoardControl : UserControl
    {
        private bool m_UseAnimation = false;
        private int m_InitialCoinsCount = 2;
        private bool m_InteractionAllowed = false;
        private Padding m_Padding = new Padding(0);
        private int m_BoardSize = 8;
        private OthelloLogicBoard m_OthelloLogicBoard = null;
        private MethodInvoker m_MakeComputerMoveInvoker = null;
        private System.Timers.Timer m_ComputerTurnTimer = null;
        private System.Timers.Timer m_OutflankingMovesTimer = null;
        private static readonly int sr_MaxBoardSize = 14;
        private int m_firstUpperCaseAsciiChar = 65;
        private Player m_BlackPlayer = null;
        private Player m_WhitePlayer = null;
        private Player m_CurrentPlayer = null;
        private string m_PositionFormat = "{0}{1}";
        private bool m_IsGameOver = false;
        private bool m_IsTurnPassed = false;
        private bool m_ShowOptionalMoves = true;
        private bool m_ShowPreviewMoves = false;
        private EndGameEventArgs m_GameOverEventArgs = null;
        private PassTurnEventArgs m_PassTurnEventArgs = null;
        private Queue<Tuple<eDiskMode, OthelloButton>> m_OuflankedDisk = null;
        private Queue<UserInteractionEndTurnEventArgs> m_TurnEvents = null;
        private List<OthelloButton> m_IlegalMoveDisks = new List<OthelloButton>();
        private List<OthelloButton> m_OptionalMoveDisks = new List<OthelloButton>();
        private List<OthelloButton> m_OthelloButtons = new List<OthelloButton>();
        private TableLayoutPanel m_OthelloPanel;

        public event EventHandler<EndGameEventArgs> OnGameOver = null;
        public event EventHandler<PassTurnEventArgs> OnTurnPassed = null;
        public event EventHandler<UserInteractionEndTurnEventArgs> OnTurnEnded = null;
        public event EventHandler<EndTurnEventArgs> OnGameStarted = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="OthelloBoardControl"/> class.
        /// </summary>
        /// <param name="i_BoardSize">Size of the i_ board.</param>
        public OthelloBoardControl(int i_BoardSize)
        {
            int lastAsciiChar = m_firstUpperCaseAsciiChar + i_BoardSize;
            initializeComponents();
            initializeBoard(i_BoardSize);

            if (i_BoardSize > sr_MaxBoardSize)
            {
                throw new ArgumentException(string.Format("Size of the board can no be higher then {0}", sr_MaxBoardSize));
            }

            m_OuflankedDisk = new Queue<Tuple<eDiskMode, OthelloButton>>();
            m_TurnEvents = new Queue<UserInteractionEndTurnEventArgs>();
            m_MakeComputerMoveInvoker = makeComputerMove;
            m_ComputerTurnTimer = new System.Timers.Timer();
            m_ComputerTurnTimer.Enabled = false;
            m_ComputerTurnTimer.Interval = 500;
            m_ComputerTurnTimer.Elapsed += new ElapsedEventHandler(m_ComputerTurnTimer_Elapsed);
            m_OutflankingMovesTimer = new System.Timers.Timer();
            m_OutflankingMovesTimer.Enabled = false;
            m_OutflankingMovesTimer.Interval = 150;
            m_OutflankingMovesTimer.Elapsed += new ElapsedEventHandler(m_OuflankingMovesTimer_Elapsed);
            m_OthelloLogicBoard = new OthelloLogicBoard(i_BoardSize);
            m_OthelloLogicBoard.OnGameOver += new EventHandler<EndGameEventArgs>(m_OthelloLogicBoard_OnGameOver);
            m_OthelloLogicBoard.OnTurnPassed += new EventHandler<PassTurnEventArgs>(m_OthelloLogicBoard_OnTurnPassed);
            m_OthelloLogicBoard.OnPlayerChanged += new EventHandler<PlayerChangedEventArgs>(m_OthelloLogicBoard_OnPlayerChanged);

            for (int i = 0; i < i_BoardSize; i++)
            {
                char charValue = (char)(i + m_firstUpperCaseAsciiChar);
                int firstRowIndexer = i + 1;
                Label labelForColumn = new Label()
                                       {
                                           ForeColor = Color.White,
                                           TextAlign = ContentAlignment.MiddleCenter,
                                           Dock = DockStyle.Fill
                                       };

                Label labelForRow = new Label() { ForeColor = Color.White, TextAlign = ContentAlignment.MiddleCenter };

                labelForRow.Dock = DockStyle.Fill;
                labelForColumn.Text = charValue.ToString();
                labelForRow.Text = firstRowIndexer.ToString();

                m_OthelloPanel.Controls.Add(labelForColumn, firstRowIndexer, 0);
                m_OthelloPanel.Controls.Add(labelForRow, 0, firstRowIndexer);
            }

            for (int row = 1; row <= m_BoardSize; row++)
            {
                for (int column = 1; column <= m_BoardSize; column++)
                {
                    int logicBoardRow = row - 1;
                    int logicBoardColumn = column - 1;
                    OthelloDisk othelloDisk = null;
                    OthelloButton button = new OthelloButton(logicBoardColumn, logicBoardRow);

                    m_OthelloButtons.Add(button);
                    button.Margin = button.Padding = new Padding(0);
                    button.Dock = DockStyle.Fill;
                    button.Click += new EventHandler(button_Click);
                    button.OnEnabledDiskMouseEnter += new EventHandler(button_MouseEnter);
                    button.OnEnabledDiskMouseLeave += new EventHandler(button_MouseLeave);

                    othelloDisk = m_OthelloLogicBoard[logicBoardColumn, logicBoardRow];

                    othelloDisk.OnDiskChanged += delegate(object i_Sender, OthelloDiskChangedEventArgs i_EventArgs)
                    {
                        changeButtonDisplay(button, i_EventArgs.Mode);
                    };

                    othelloDisk.OnDiskChangedToVirtual += delegate(object i_Sender, OthelloDiskChangedEventArgs i_EventArgs)
                    {
                        changeButtonToVirtual(button, i_EventArgs.Mode);
                    };

                    this.Controls.Add(button);

                    button.Padding = button.Margin = m_Padding; ;
                    m_OthelloPanel.Controls.Add(button, column, row);
                }
            }
        }

        /// <summary>
        /// Initializes the components.
        /// </summary>
        private void initializeComponents()
        {
            this.m_OthelloPanel = new TableLayoutPanel();
            this.Controls.Add(this.m_OthelloPanel);
            this.Location = new Point(0, 0);
            this.Size = new Size(460, 460);
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.m_OthelloPanel);
            this.Margin = new Padding(0);
            this.Size = new Size(460, 460);

        }

        /// <summary>
        /// Initializes the board.
        /// </summary>
        /// <param name="i_Size">Size of the i_.</param>
        private void initializeBoard(int i_Size)
        {
            Single size = 55F;
            Single firstSize = 20F;
            int sizeOfBaord = (int)(i_Size * size + firstSize);

            m_OthelloPanel.Anchor = AnchorStyles.Left;
            m_OthelloPanel.AutoSize = true;
            m_OthelloPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            m_OthelloPanel.BackColor = Color.Gray;
            m_OthelloPanel.BackgroundImageLayout = ImageLayout.None;
            m_OthelloPanel.ColumnCount = m_OthelloPanel.RowCount = i_Size + 1;
            m_OthelloPanel.Location = new Point(0, 0);
            m_OthelloPanel.Margin = new Padding(0);
            m_OthelloPanel.Size = new Size(sizeOfBaord, sizeOfBaord);

            m_OthelloPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, firstSize));
            m_OthelloPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, firstSize));

            for (int i = 0; i <= i_Size; i++)
            {
                m_OthelloPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, size));
                m_OthelloPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, size));
            }
        }

        /// <summary>
        /// Handles the MouseLeave event of the button control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button_MouseLeave(object i_Sender, EventArgs i_EventArgs)
        {
            if (ShowPreviewMoves)
            {
                bool isClicked = false;
                OthelloButton button = i_Sender as OthelloButton;

                m_OthelloLogicBoard.RestoreFromLocation(button.Column, button.Row, isClicked);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the button control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button_MouseEnter(object i_Sender, EventArgs i_EventArgs)
        {
            if (ShowPreviewMoves && m_CurrentPlayer.PlayerMode == ePlayerMode.User)
            {
                bool isVirtual = true;
                OthelloButton button = i_Sender as OthelloButton;

                m_OthelloLogicBoard.MakeMove(button.Column, button.Row, isVirtual);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show preview moves].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show preview moves]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowPreviewMoves
        {

            get { return m_ShowPreviewMoves; }
            set
            {
                m_UseAnimation = false;
                m_ShowPreviewMoves = value;
                m_OthelloLogicBoard.Restore();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show optional moves].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show optional moves]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowOptionalMoves
        {
            get { return m_ShowOptionalMoves; }
            set
            {
                m_UseAnimation = false;
                m_ShowOptionalMoves = value;

                foreach (OthelloDisk disk in m_OthelloLogicBoard.OptionalMoves)
                {
                    disk.RaiseDiskChangeEvent();
                }
            }
        }

        /// <summary>
        /// Handles the OnPlayerChanged event of the m_OthelloLogicBoard control.
        /// </summary>
        /// <param name="s_Sender">The source of the event.</param>
        /// <param name="i_PlayerChangedEventArgs">The <see cref="Othello.Logic.PlayerChangedEventArgs"/> instance containing the event data.</param>
        void m_OthelloLogicBoard_OnPlayerChanged(object s_Sender, PlayerChangedEventArgs i_PlayerChangedEventArgs)
        {
            m_CurrentPlayer = m_OthelloLogicBoard.CurrentPlayer == ePlayerColor.Black ? m_BlackPlayer : m_WhitePlayer;
        }

        /// <summary>
        /// Handles the OnTurnPassed event of the m_OthelloLogicBoard control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_PassTurnEventArgs">The <see cref="Othello.Logic.PassTurnEventArgs"/> instance containing the event data.</param>
        private void m_OthelloLogicBoard_OnTurnPassed(object i_Sender, PassTurnEventArgs i_PassTurnEventArgs)
        {
            if (!m_IsGameOver)
            {
                m_IsTurnPassed = true;
                m_PassTurnEventArgs = i_PassTurnEventArgs;
            }
        }

        /// <summary>
        /// Handles the OnGameOver event of the m_OthelloLogicBoard control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EndGameEventArgs">The <see cref="Othello.Logic.EndGameEventArgs"/> instance containing the event data.</param>
        private void m_OthelloLogicBoard_OnGameOver(object i_Sender, EndGameEventArgs i_EndGameEventArgs)
        {
            m_ComputerTurnTimer.Enabled = false;
            m_OutflankingMovesTimer.Enabled = false;
            m_IsGameOver = true;
            m_GameOverEventArgs = i_EndGameEventArgs;
        }

        /// <summary>
        /// News the game.
        /// </summary>
        /// <param name="i_BlackPlayer">The i_ black player.</param>
        /// <param name="i_WhitePlayer">The i_ white player.</param>
        public void NewGame(Player i_BlackPlayer, Player i_WhitePlayer)
        {
            m_IsGameOver = false;
            m_InteractionAllowed = true;
            m_UseAnimation = false;
            m_BlackPlayer = i_BlackPlayer;
            m_WhitePlayer = i_WhitePlayer;
            m_CurrentPlayer = i_BlackPlayer;
            m_TurnEvents.Clear();
            m_OuflankedDisk.Clear();
            m_IlegalMoveDisks.Clear();
            m_OptionalMoveDisks.Clear();
            m_OthelloLogicBoard.NewGame();

            if (OnGameStarted != null)
            {
                EndTurnEventArgs args = new EndTurnEventArgs(m_InitialCoinsCount, m_InitialCoinsCount, ePlayerColor.Black);
                OnGameStarted(this, args);
            }

            if (m_BlackPlayer.PlayerMode == ePlayerMode.Computer)
            {
                m_ComputerTurnTimer.Enabled = true;
            }
        }

        /// <summary>
        /// Makes the computer move.
        /// </summary>
        private void makeComputerMove()
        {
            int column = 0;
            int row = 0;
            OthelloDisk computersDisk = getRandomMove();
            ePlayerColor currentPlayer = m_CurrentPlayer.PlayerColor;

            m_UseAnimation = true;
            m_InteractionAllowed = false;
            column = computersDisk.DiskLocation.Column;
            row = computersDisk.DiskLocation.Row;
            m_OthelloLogicBoard.MakeMove(column, row, false);
            InsertTurnEventArgsToQueue(row, column, currentPlayer);
            m_OutflankingMovesTimer.Enabled = true;
        }

        /// <summary>
        /// Handles the Elapsed event of the m_ComputerTurnTimer control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_ElapsedEventArgs">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void m_ComputerTurnTimer_Elapsed(object i_Sender, ElapsedEventArgs i_ElapsedEventArgs)
        {
            m_ComputerTurnTimer.Enabled = false;
            BeginInvoke(m_MakeComputerMoveInvoker);
        }

        /// <summary>
        /// Handles the Click event of the button control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_EventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button_Click(object i_Sender, EventArgs i_EventArgs)
        {
            OthelloButton button = i_Sender as OthelloButton;

            if (m_InteractionAllowed)
            {
                bool isClicked = true;
                ePlayerColor currentPlayer = m_CurrentPlayer.PlayerColor;

                m_UseAnimation = false;
                m_OthelloLogicBoard.RestoreFromLocation(button.Column, button.Row, isClicked);
                m_UseAnimation = true;
                m_OthelloLogicBoard.MakeMove(button.Column, button.Row, !isClicked);
                InsertTurnEventArgsToQueue(button.Row, button.Column, currentPlayer);
                m_InteractionAllowed = false;
                m_OutflankingMovesTimer.Enabled = true;
            }
        }

        /// <summary>
        /// Raises the turn end event.
        /// </summary>
        private void raiseTurnEndEvent()
        {
            if (OnTurnEnded != null)
            {
                if (m_TurnEvents.Count > 0)
                {
                    UserInteractionEndTurnEventArgs args = m_TurnEvents.Dequeue();
                    OnTurnEnded(this, args);
                }
            }
        }

        /// <summary>
        /// Inserts the turn event args to queue.
        /// </summary>
        /// <param name="i_Row">The i_ row.</param>
        /// <param name="i_Column">The i_ column.</param>
        /// <param name="i_PlayerColor">Color of the i_ player.</param>
        private void InsertTurnEventArgsToQueue(int i_Row, int i_Column, ePlayerColor i_PlayerColor)
        {
            int winnerCoinsCount = 0;
            int loserCoinsCount = 0;
            int blackCoinsCount = 0;
            int whiteCoinsCount = 0;
            int columnPosition = m_firstUpperCaseAsciiChar + i_Column;
            string position = string.Format(m_PositionFormat, i_Row + 1, (char)columnPosition);
            UserInteractionEndTurnEventArgs args = null;

            ePlayerColor winerColor = m_OthelloLogicBoard.CountDisks(out winnerCoinsCount, out loserCoinsCount);

            if (winerColor == ePlayerColor.Black)
            {
                blackCoinsCount = winnerCoinsCount;
                whiteCoinsCount = loserCoinsCount;
            }
            else
            {
                whiteCoinsCount = winnerCoinsCount;
                blackCoinsCount = loserCoinsCount;
            }

            args = new UserInteractionEndTurnEventArgs(blackCoinsCount, whiteCoinsCount, i_PlayerColor, position);
            m_TurnEvents.Enqueue(args);
        }

        /// <summary>
        /// Outflanks the disk.
        /// </summary>
        /// <param name="i_ButtonTuple">The i_ button tuple.</param>
        private void outflankDisk(Tuple<eDiskMode, OthelloButton> i_ButtonTuple)
        {
            if (i_ButtonTuple.Item1 == eDiskMode.Black)
            {
                i_ButtonTuple.Item2.ConvertToBlack();
            }
            else
            {
                i_ButtonTuple.Item2.ConvertToWhite();
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the m_OuflankingMovesTimer control.
        /// </summary>
        /// <param name="i_Sender">The source of the event.</param>
        /// <param name="i_ElapsedEventArgs">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void m_OuflankingMovesTimer_Elapsed(object i_Sender, ElapsedEventArgs i_ElapsedEventArgs)
        {
            if (m_OuflankedDisk.Count > 0)
            {
                Tuple<eDiskMode, OthelloButton> buttonTuple = m_OuflankedDisk.Dequeue();
                BeginInvoke(new Action<Tuple<eDiskMode, OthelloButton>>(outflankDisk), new[] { buttonTuple });
            }
            else
            {
                m_OutflankingMovesTimer.Enabled = false;
                List<OthelloDisk> optionalMoves = m_OthelloLogicBoard.OptionalMoves;

                foreach (OthelloButton button in m_IlegalMoveDisks)
                {
                    button.Disable();
                }

                foreach (OthelloButton button in m_OptionalMoveDisks)
                {
                    button.ConvertToOptional(this.ShowOptionalMoves);
                }

                m_IlegalMoveDisks.Clear();
                m_OptionalMoveDisks.Clear();

                if (m_IsGameOver)
                {
                    if (this.OnGameOver != null)
                    {
                        OnGameOver(this, m_GameOverEventArgs);
                        return;
                    }
                }
                else
                {
                    if (m_IsTurnPassed)
                    {
                        if (this.OnTurnPassed != null)
                        {
                            m_IsTurnPassed = false;
                            OnTurnPassed(this, m_PassTurnEventArgs);
                        }
                    }

                    if (m_CurrentPlayer.PlayerMode == ePlayerMode.Computer)
                    {
                        m_ComputerTurnTimer.Enabled = true;
                    }
                    else
                    {
                        m_InteractionAllowed = true;
                        m_OutflankingMovesTimer.Enabled = false;
                    }

                    raiseTurnEndEvent();
                    m_UseAnimation = false;
                }
            }
        }

        /// <summary>
        /// Gets the random move.
        /// </summary>
        /// <returns></returns>
        private OthelloDisk getRandomMove()
        {
            List<OthelloDisk> optionalMoves = m_OthelloLogicBoard.OptionalMoves;
            Random random = new Random();
            int randomIndex = random.Next(0, optionalMoves.Count - 1);

            return optionalMoves[randomIndex];
        }

        /// <summary>
        /// Changes the button display.
        /// </summary>
        /// <param name="i_OthelloButton">The othell button.</param>
        /// <param name="i_DiskMode">The disk mode.</param>
        private void changeButtonDisplay(OthelloButton i_OthelloButton, eDiskMode i_DiskMode)
        {
            switch (i_DiskMode)
            {
                case eDiskMode.IlegalMove:

                    if (m_UseAnimation)
                    {
                        m_IlegalMoveDisks.Add(i_OthelloButton);
                    }
                    else
                    {
                        i_OthelloButton.Disable();
                    }

                    break;

                case eDiskMode.White:

                    if (m_UseAnimation)
                    {
                        m_OuflankedDisk.Enqueue(Tuple.Create(i_DiskMode, i_OthelloButton));
                    }
                    else
                    {
                        i_OthelloButton.ConvertToWhite();
                    }

                    break;

                case eDiskMode.Black:

                    if (m_UseAnimation)
                    {
                        m_OuflankedDisk.Enqueue(Tuple.Create(i_DiskMode, i_OthelloButton));
                    }
                    else
                    {
                        i_OthelloButton.ConvertToBlack();
                    }

                    break;

                case eDiskMode.OptionalMove:

                    if (m_UseAnimation)
                    {
                        m_OptionalMoveDisks.Add(i_OthelloButton);
                    }
                    else
                    {
                        i_OthelloButton.ConvertToOptional(this.ShowOptionalMoves);
                    }

                    break;
            }
        }

        /// <summary>
        /// Changes the button to virtual.
        /// </summary>
        /// <param name="i_OthelloButton">The i_ othello button.</param>
        /// <param name="i_DiskMode">The i_ disk mode.</param>
        private void changeButtonToVirtual(OthelloButton i_OthelloButton, eDiskMode i_DiskMode)
        {
            if (i_DiskMode == eDiskMode.Black)
            {
                i_OthelloButton.ConvertToVirtualBlack();
            }
            else
            {
                i_OthelloButton.ConvertToVirtualWhite();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // OthelloBoardControl
            // 
            this.Name = "OthelloBoardControl";
            this.Load += new System.EventHandler(this.OthelloBoardControl_Load);
            this.ResumeLayout(false);

        }

        private void OthelloBoardControl_Load(object sender, EventArgs e)
        {

        }
    }
}
