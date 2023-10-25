using GameOfLifeAPI.Controllers;
using GameOfLifeAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace GameOfLifeApiTest
{
    [TestClass]
    public class GameOfLifeShould
    {

        [TestMethod]
        public void activate_a_cell_when_surrounded_by_exactly_three()
        {
            bool[,] board =
            {
                { false, false, false, false},
                { false, false, true, false},
                { false, true, true, false},
                { false, false, false, false},
            };

            bool[,] boardExpected =
            {
                { false, false, false, false},
                { false, true, true, false},
                { false, true, true, false},
                { false, false, false, false},
            };



            GameOfLife gameOfLife =
                new GameOfLife.GameOfLife(board);
            gameOfLife.next();

            GameOfLife.GameOfLife gameOfLifeExpected =
                new GameOfLife.GameOfLife(boardExpected);

            Assert.IsTrue(gameOfLife.Equals(gameOfLifeExpected));
        }

        [TestMethod]
        public void kill_a_cell_when_surrounded_by_less_than_two()
        {
            bool[,] board =
            {
                { false, false, false},
                { false, true, false},
                { false, false , false},
            };

            bool[,] boardExpected =
            {
                { false, false, false},
                { false, false, false},
                { false, false , false},
            };



            GameOfLife.GameOfLife gameOfLife =
                new GameOfLife.GameOfLife(board);
            gameOfLife.next();

            GameOfLife.GameOfLife gameOfLifeExpected =
                new GameOfLife.GameOfLife(boardExpected);

            Assert.IsTrue(gameOfLife.Equals(gameOfLifeExpected));
        }

        [TestMethod]
        public void kill_a_cell_when_surrounded_by_more_than_three()
        {

            // As a side effect, cells in (1,1), (1,3), (3,1), (3,3) should be activated because of the reproduction rule
            bool[,] board =
            {
                { false, false, false, false, false},
                { false, false, true, false, false},
                { false, true, true, true, false},
                { false, false, true, false, false},
                { false, false, false, false, false},
            };

            bool[,] boardExpected =
            {
                { false, false, false, false, false},
                { false, true, true, true, false},
                { false, true, false, true, false},
                { false, true, true, true, false},
                { false, false, false, false, false},
            };



            GameOfLife.GameOfLife gameOfLife =
                new GameOfLife.GameOfLife(board);
            gameOfLife.next();

            GameOfLife.GameOfLife gameOfLifeExpected =
                new GameOfLife.GameOfLife(boardExpected);

            Assert.IsTrue(gameOfLife.Equals(gameOfLifeExpected));
        }

        [TestMethod]
        public void not_change_a_cell_when_surrounded_by_two_or_three()
        {
            bool[,] board =
            {
                { false, false, false, false},
                { false, true, true, false},
                { false, true, true, false},
                { false, false, false, false},
            };

            bool[,] boardExpected =
            {
                { false, false, false, false},
                { false, true, true, false},
                { false, true, true, false},
                { false, false, false, false},
            };



            GameOfLife.GameOfLife gameOfLife =
                new GameOfLife.GameOfLife(board);
            gameOfLife.next();

            GameOfLife.GameOfLife gameOfLifeExpected =
                new GameOfLife.GameOfLife(boardExpected);

            Assert.IsTrue(gameOfLife.Equals(gameOfLifeExpected));
        }



    }
}