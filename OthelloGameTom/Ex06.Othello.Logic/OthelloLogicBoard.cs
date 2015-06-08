using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Othello.Logic
{
    public class OthelloLogicBoard
    {
        private readonly int r_Size = 0;
        private OthelloDisk[,] m_OthelloDisks = null;
        private List<OthelloDisk> m_OptionalMoves = null;
        private static readonly int sr_NoOptionalMoves = 0;
        private eDiskMode m_CurrentDiskMode = eDiskMode.Black;

        public event EventHandler<EndGameEventArgs> OnGameOver = null;
        public event EventHandler<PassTurnEventArgs> OnTurnPassed = null;
        public event EventHandler<PlayerChangedEventArgs> OnPlayerChanged = null;
        private Queue<OthelloDiskCareTaker> m_OthelloDisksCareTaker = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="OthelloLogicBoard"/> class.
        /// </summary>
        /// <param name="i_Size">Size of the board (Size x Size).</param>
        public OthelloLogicBoard(int i_Size)
        {
            r_Size = i_Size;
            m_OthelloDisksCareTaker = new Queue<OthelloDiskCareTaker>();
            m_OptionalMoves = new List<OthelloDisk>();
            this.m_OthelloDisks = new OthelloDisk[i_Size, i_Size];

            for (int row = 0; row < r_Size; row++)
            {
                for (int col = 0; col < r_Size; col++)
                {
                    Location diskLocation = new Location(row, col);
                    OthelloDisk disk = new OthelloDisk(diskLocation);
                    this.m_OthelloDisks[row, col] = disk;
                }
            }
        }

        /// <summary>
        /// Gets the Current Player.
        /// </summary>
        public ePlayerColor CurrentPlayer
        {
            get
            {
                return parsePlayerColor(m_CurrentDiskMode);
            }
        }

        /// <summary>
        /// Gets the optional moves.
        /// </summary>
        public List<OthelloDisk> OptionalMoves
        {
            get { return m_OptionalMoves; }
        }

        /// <summary>
        /// Gets the <see cref="Othello.Logic.OthelloDisk"/> with the specified column/row.
        /// </summary>
        public OthelloDisk this[int i_Column, int i_Row]
        {
            get { return m_OthelloDisks[i_Column, i_Row]; }
        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        public void NewGame()
        {
            int halfSize = r_Size / 2;

            for (int i = 0; i < r_Size; i++)
            {
                for (int j = 0; j < r_Size; j++)
                {
                    this.m_OthelloDisks[i, j].DiskMode = eDiskMode.IlegalMove;
                }
            }

            m_CurrentDiskMode = eDiskMode.Black;
            raisePlayerChangedEvent();

            //Intialize disks according to the chosen size.
            this.m_OthelloDisks[halfSize - 1, halfSize - 1].DiskMode = eDiskMode.White;
            this.m_OthelloDisks[halfSize - 1, halfSize].DiskMode = eDiskMode.Black;
            this.m_OthelloDisks[halfSize, halfSize - 1].DiskMode = eDiskMode.Black;
            this.m_OthelloDisks[halfSize, halfSize].DiskMode = eDiskMode.White;

            m_OptionalMoves.Clear();
            m_OptionalMoves.Add(this.m_OthelloDisks[halfSize - 1, halfSize - 2]);
            m_OptionalMoves.Add(this.m_OthelloDisks[halfSize - 2, halfSize - 1]);
            m_OptionalMoves.Add(this.m_OthelloDisks[halfSize, halfSize + 1]);
            m_OptionalMoves.Add(this.m_OthelloDisks[halfSize + 1, halfSize]);

            foreach (OthelloDisk disk in m_OptionalMoves)
            {
                disk.DiskMode = eDiskMode.OptionalMove;
            }
        }

        /// <summary>
        /// Makes the move.
        /// </summary>
        /// <param name="i_Column">The i_ column.</param>
        /// <param name="i_Row">The i_ row.</param>
        public void MakeMove(int i_Column, int i_Row, bool i_IsVirtual)
        {
            int row = 0;
            int column = 0;
            OthelloDisk othelloDisk = this[i_Column, i_Row];
            eDiskMode oppositeMode = getOppositeMode(m_CurrentDiskMode);

            if (i_IsVirtual)
            {
                addToDiskCare(othelloDisk);
            }

            othelloDisk.DiskMode = m_CurrentDiskMode;

            //Iterate all direction in order to find all options to flip the disks.
            for (int rowIncrementor = -1; rowIncrementor <= 1; rowIncrementor++)
            {
                for (int columnIncrementor = -1; columnIncrementor <= 1; columnIncrementor++)
                {
                    if (!(rowIncrementor == 0 && columnIncrementor == 0) && canConvertTo(m_CurrentDiskMode, i_Row, i_Column, rowIncrementor, columnIncrementor))
                    {
                        row = i_Row + rowIncrementor;
                        column = i_Column + columnIncrementor;

                        while (this[column, row].DiskMode == oppositeMode)
                        {
                            othelloDisk = this[column, row];

                            if (i_IsVirtual)
                            {
                                addToDiskCare(othelloDisk);
                            }

                            othelloDisk.DiskMode = m_CurrentDiskMode;
                            row += rowIncrementor;
                            column += columnIncrementor;
                        }
                    }
                }
            }

            if (!i_IsVirtual)
            {
                m_CurrentDiskMode = oppositeMode;
                raisePlayerChangedEvent();
                calculateValidMoves();
            }
        }

        private void addToDiskCare(OthelloDisk i_OthelloDisk)
        {
            OthelloDiskMemento memento = i_OthelloDisk.Memento;
            Location location = i_OthelloDisk.DiskLocation;
            OthelloDiskCareTaker careTaker = new OthelloDiskCareTaker(location.Row, location.Column, memento);

            m_OthelloDisksCareTaker.Enqueue(careTaker);
            i_OthelloDisk.IsVirtual = true;
        }

        public void RestoreFromLocation(int i_Column, int i_Row, bool i_IsClicked)
        {
            if (m_OthelloDisksCareTaker.Count > 0)
            {
                OthelloDiskCareTaker diskCareTaker = null;

                if (i_IsClicked)
                {
                    diskCareTaker = m_OthelloDisksCareTaker.Dequeue();

                    if (diskCareTaker.Column == i_Column && diskCareTaker.Row == i_Row)
                    {
                        OthelloDisk othelloDisk = this[i_Column, i_Row];

                        othelloDisk.DiskMode = m_CurrentDiskMode;
                        othelloDisk.IsVirtual = false;
                        othelloDisk.RaiseDiskChangeEvent();
                    }
                }

                Restore();
            }
        }

        public void Restore()
        {
            while (m_OthelloDisksCareTaker.Count > 0)
            {
                OthelloDiskCareTaker diskCareTaker = m_OthelloDisksCareTaker.Dequeue();
                OthelloDisk othelloDisk = this[diskCareTaker.Column, diskCareTaker.Row];
                othelloDisk.Memento = diskCareTaker.Memento;
            }

            m_OthelloDisksCareTaker.Clear();
        }

        private void raisePlayerChangedEvent()
        {
            if (this.OnPlayerChanged != null)
            {
                ePlayerColor currentPlayer = parsePlayerColor(m_CurrentDiskMode);
                PlayerChangedEventArgs args = new PlayerChangedEventArgs(currentPlayer);

                OnPlayerChanged(this, args);
            }
        }

        /// <summary>
        /// Determines whether [is valid move] [the specified i_ disk mode].
        /// </summary>
        /// <param name="i_DiskMode">The disk mode.</param>
        /// <param name="i_Column">The column.</param>
        /// <param name="i_Row">The row.</param>
        /// <returns>
        ///   <c>true</c> if [is valid move] [the specified disk mode]; otherwise, <c>false</c>.
        /// </returns>
        private bool isValidMove(eDiskMode i_DiskMode, int i_Column, int i_Row)
        {
            if (this[i_Column, i_Row].DiskMode == eDiskMode.IlegalMove)
            {
                for (int rowIncrementor = -1; rowIncrementor <= 1; rowIncrementor++)
                {
                    for (int columnIncrementor = -1; columnIncrementor <= 1; columnIncrementor++)
                    {
                        if (!(rowIncrementor == 0 && columnIncrementor == 0) && this.canConvertTo(i_DiskMode, i_Row, i_Column, rowIncrementor, columnIncrementor))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Calculates the valid moves.
        /// </summary>
        private void calculateValidMoves()
        {
            bool hasOptionalMoves = false;
            eDiskMode initialDiskMode = m_CurrentDiskMode;
            clearValidMoves();

            hasOptionalMoves = tryCalculateValidMoves(m_CurrentDiskMode);

            if (!hasOptionalMoves)
            {
                //Check the other player's optional moves.
                m_CurrentDiskMode = getOppositeMode(m_CurrentDiskMode);
                raisePlayerChangedEvent();
                hasOptionalMoves = tryCalculateValidMoves(m_CurrentDiskMode);

                //Other player has optional move notify for passing the turn to the other player.
                if (hasOptionalMoves)
                {
                    if (OnTurnPassed != null)
                    {
                        ePlayerColor initialPlayerMode = parsePlayerColor(initialDiskMode);
                        ePlayerColor currentPlayerMode = parsePlayerColor(m_CurrentDiskMode);
                        PassTurnEventArgs eventArgs = new PassTurnEventArgs(initialPlayerMode, currentPlayerMode);

                        OnTurnPassed(this, eventArgs);
                    }
                }
            }

            //Both players don't have optional moves, hence, end the game.
            if (m_OptionalMoves.Count == sr_NoOptionalMoves)
            {
                handleGameOver();
            }
        }

        /// <summary>
        /// Tries the calculate valid moves.
        /// </summary>
        /// <param name="i_DiskMode">The disk mode.</param>
        /// <returns></returns>
        private bool tryCalculateValidMoves(eDiskMode i_DiskMode)
        {
            for (int row = 0; row < r_Size; row++)
            {
                for (int column = 0; column < r_Size; column++)
                {
                    if (this.isValidMove(i_DiskMode, column, row))
                    {
                        OthelloDisk disk = this[column, row];

                        disk.DiskMode = eDiskMode.OptionalMove;
                        m_OptionalMoves.Add(disk);
                    }
                }
            }

            return m_OptionalMoves.Count > 0;
        }

        /// <summary>
        /// Handles the game over.
        /// </summary>
        private void handleGameOver()
        {
            if (OnGameOver != null)
            {
                int winnerCount;
                int loserCount;
                ePlayerColor winnerDiskMode;
                EndGameEventArgs gameEndEventArgs = null;

                winnerDiskMode = CountDisks(out winnerCount, out loserCount);
                gameEndEventArgs = new EndGameEventArgs(winnerDiskMode, winnerCount, loserCount);
                OnGameOver(this, gameEndEventArgs);
            }
        }

        /// <summary>
        /// Counts the disks.
        /// </summary>
        /// <param name="o_WinnerCount">The winner's disk count.</param>
        /// <param name="o_LoserCount">The loser's disk count.</param>
        /// <returns></returns>
        public ePlayerColor CountDisks(out int o_WinnerCount, out int o_LoserCount)
        {
            int blackCount = 0;
            int whiteCount = 0;
            eDiskMode winnerDiskMode;

            o_WinnerCount = 0;
            o_LoserCount = 0;

            for (int row = 0; row < r_Size; row++)
            {
                for (int column = 0; column < r_Size; column++)
                {
                    OthelloDisk disk = this[column, row];

                    if (disk.DiskMode == eDiskMode.Black)
                    {
                        blackCount++;
                    }
                    else if (disk.DiskMode == eDiskMode.White)
                    {
                        whiteCount++;
                    }
                }
            }

            if (blackCount > whiteCount)
            {
                winnerDiskMode = eDiskMode.Black;
                o_WinnerCount = blackCount;
                o_LoserCount = whiteCount;
            }
            else
            {
                winnerDiskMode = eDiskMode.White;
                o_WinnerCount = whiteCount;
                o_LoserCount = blackCount;
            }

            return parsePlayerColor(winnerDiskMode);
        }

        /// <summary>
        /// Clears the valid moves.
        /// </summary>
        private void clearValidMoves()
        {
            foreach (OthelloDisk disk in m_OptionalMoves)
            {
                if (disk.DiskMode == eDiskMode.OptionalMove)
                {
                    disk.DiskMode = eDiskMode.IlegalMove;
                }
            }

            m_OptionalMoves.Clear();
        }

        /// <summary>
        /// Determines whether this instance [can convert to] the specified disk mode.
        /// </summary>
        /// <param name="i_DiskMode">The disk mode.</param>
        /// <param name="i_StartRow">The start row.</param>
        /// <param name="i_StartColumn">The start column.</param>
        /// <param name="i_RowIncrementor">The row incrementor.</param>
        /// <param name="i_ColumnIncrementor">The column incrementor.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can convert to] the specified disk mode; otherwise, <c>false</c>.
        /// </returns>
        private bool canConvertTo(eDiskMode i_DiskMode, int i_StartRow, int i_StartColumn, int i_RowIncrementor, int i_ColumnIncrementor)
        {
            // Try to find oponenet's disk
            int row = i_StartRow + i_RowIncrementor;
            int column = i_StartColumn + i_ColumnIncrementor;

            eDiskMode oppositeMode = getOppositeMode(i_DiskMode);

            while (row >= 0 && row < r_Size && column >= 0 && column < r_Size && this[column, row].DiskMode == oppositeMode)
            {
                row += i_RowIncrementor;
                column += i_ColumnIncrementor;
            }

            //An oponenet's disk has not been flipped
            if (row < 0 || row > r_Size - 1 || column < 0 || column > r_Size - 1 || (row - i_RowIncrementor == i_StartRow && column - i_ColumnIncrementor == i_StartColumn) || this[column, row].DiskMode != i_DiskMode)
                return false;

            return true;
        }

        /// <summary>
        /// Gets the opposite mode of Disk (Black/White).
        /// </summary>
        /// <param name="i_DiskMode">The disk mode.</param>
        /// <returns></returns>
        private eDiskMode getOppositeMode(eDiskMode i_DiskMode)
        {
            return i_DiskMode == eDiskMode.Black ? eDiskMode.White : eDiskMode.Black;
        }

        /// <summary>
        /// Parses the player mode according to the specified diskMode.
        /// </summary>
        /// <param name="i_DiskMode">The disk mode.</param>
        /// <returns></returns>
        private ePlayerColor parsePlayerColor(eDiskMode i_DiskMode)
        {
            return parseEnum<eDiskMode, ePlayerColor>(i_DiskMode);
        }

        /// <summary>
        /// Parses the disk mode according to the player color.
        /// </summary>
        /// <param name="i_DiskMode">The plyer color.</param>
        /// <returns></returns>
        private eDiskMode parseDiskMode(ePlayerColor i_PlayerColor)
        {
            return parseEnum<ePlayerColor, eDiskMode>(i_PlayerColor);
        }

        private TOut parseEnum<TIn, TOut>(TIn i_value)
        {
            return (TOut)Enum.Parse(typeof(TIn), i_value.ToString());
        }
    }
}
