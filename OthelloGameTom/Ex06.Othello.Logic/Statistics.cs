using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello.Logic
{
    public class Statistics
    {
        public Statistics()
        {
            BlackStatistics = new PlayerAggregatedStatistcs();
            WhiteStatistics = new PlayerAggregatedStatistcs();
            UserStatistics = new PlayerAggregatedStatistcs();
            ComputerStatistics = new PlayerAggregatedStatistcs();
        }

        /// <summary>
        /// Gets or sets the overall draws.
        /// </summary>
        /// <value>
        /// The overall draws.
        /// </value>
        public int OverallDraws { get; set; }

        /// <summary>
        /// Gets or sets the against computer draws.
        /// </summary>
        /// <value>
        /// The against computer draws.
        /// </value>
        public int AgainstComputerDraws { get; set; }

        /// <summary>
        /// Gets or sets the black statistics.
        /// </summary>
        /// <value>
        /// The black statistics.
        /// </value>
        public PlayerAggregatedStatistcs BlackStatistics { get; set; }

        /// <summary>
        /// Gets or sets the white statistics.
        /// </summary>
        /// <value>
        /// The white statistics.
        /// </value>
        public PlayerAggregatedStatistcs WhiteStatistics { get; set; }

        /// <summary>
        /// Gets or sets the user statistics.
        /// </summary>
        /// <value>
        /// The user statistics.
        /// </value>
        public PlayerAggregatedStatistcs UserStatistics { get; set; }

        /// <summary>
        /// Gets or sets the computer statistics.
        /// </summary>
        /// <value>
        /// The computer statistics.
        /// </value>
        public PlayerAggregatedStatistcs ComputerStatistics { get; set; }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <param name="other">The other.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Statistics operator +(Statistics first, Statistics other)
        {
            return new Statistics()
            {
                OverallDraws = first.OverallDraws + other.OverallDraws,
                AgainstComputerDraws = first.AgainstComputerDraws + other.AgainstComputerDraws,
                BlackStatistics = new PlayerAggregatedStatistcs()
                                  {
                                      TotalScore = first.BlackStatistics.TotalScore + other.BlackStatistics.TotalScore,
                                      TotalWins = first.BlackStatistics.TotalWins + other.BlackStatistics.TotalWins
                                  },

                WhiteStatistics = new PlayerAggregatedStatistcs()
                                  {
                                      TotalScore = first.WhiteStatistics.TotalScore + other.WhiteStatistics.TotalScore,
                                      TotalWins = first.WhiteStatistics.TotalWins + other.WhiteStatistics.TotalWins
                                  },

                ComputerStatistics = new PlayerAggregatedStatistcs()
                                     {
                                         TotalScore = first.ComputerStatistics.TotalScore + other.ComputerStatistics.TotalScore,
                                         TotalWins = first.ComputerStatistics.TotalWins + other.ComputerStatistics.TotalWins
                                     },

                UserStatistics = new PlayerAggregatedStatistcs()
                                 {
                                     TotalScore = first.UserStatistics.TotalScore + other.UserStatistics.TotalScore,
                                     TotalWins = first.UserStatistics.TotalWins + other.UserStatistics.TotalWins
                                 }
            };
        }
    }
}
