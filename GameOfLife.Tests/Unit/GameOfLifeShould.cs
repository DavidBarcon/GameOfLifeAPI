using NUnit.Framework;
using GameOfLifeKata.Business;
using FluentAssertions;
using NSubstitute;

namespace GameOfLifeKata.Tests.Unit
{
    [TestFixture]
    public class GameOfLifeShould
    {
        private BoardRepository boardRepository;
        private GameOfLife gameOfLife;

        [SetUp]
        public void SetUp()
        {
            boardRepository = Substitute.For<BoardRepository>();
            gameOfLife = new GameOfLife(boardRepository);

        }

        [Test]
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



            gameOfLife.NewGame(board);
            gameOfLife.next();

            GameOfLife expected = new GameOfLife(boardRepository);
            expected.NewGame(boardExpected);

            gameOfLife.Equals(expected).Should().BeTrue();
        }

        [Test]
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



            gameOfLife.NewGame(board);
            gameOfLife.next();

            GameOfLife expected = new GameOfLife(boardRepository);
            expected.NewGame(boardExpected);

            gameOfLife.Equals(expected).Should().BeTrue();
        }

        [Test]
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



            gameOfLife.NewGame(board);
            gameOfLife.next();

            GameOfLife expected = new GameOfLife(boardRepository);
            expected.NewGame(boardExpected);

            gameOfLife.Equals(expected).Should().BeTrue();
        }

        [Test]
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



            gameOfLife.NewGame(board);
            gameOfLife.next();

            GameOfLife expected = new GameOfLife(boardRepository);
            expected.NewGame(boardExpected);

            gameOfLife.Equals(expected).Should().BeTrue();
        }

        [Test]
        public void create_a_new_board_with_the_given_values()
        {

            bool[,] board =
            {
                { false, false, false, false},
                { false, true, true, false},
                { false, true, true, false},
                { false, false, false, false},
            };

            gameOfLife.NewGame(board);

            gameOfLife.ToArray().Should().BeEquivalentTo(board);
        }

        [Test]
        public void save_the_new_game()
        {

            bool[,] values =
            {
                { false, false, false, false},
                { false, true, true, false},
                { false, true, true, false},
                { false, false, false, false},
            };

            gameOfLife.NewGame(values);


            boardRepository.Received(1).Save(Arg.Is<Board>(board => board.Equals(new Board(values))));

        }
    }
}