using FluentAssertions;
using GameOfLifeKata.API.Controllers;
using GameOfLifeKata.Business;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace GameOfLifeKata.Tests.Unit
{
    [TestFixture]
    public class GameOfLifeAPIShould
    {
        private BoardRepository boardRepository;
        private GameOfLife gameOfLife;
        private GameOfLifeController controller;

        /*
        [SetUp]
        public void SetUp()
        {
            boardRepository = Substitute.For<BoardRepository>();

            gameOfLife = new GameOfLife(boardRepository);

            controller = new GameOfLifeController(gameOfLife);
        }

        [Test]
        public void return_OK_when_post_is_called_with_not_empty_array()
        {
            bool[][] values = new bool[2][];
            values[0] = new[] { true, true };
            values[1] = new[] { true, true };

            var result =controller.Post(values);

            result.Should().BeOfType<OkResult>();

        }

        [Test]
        public void return_BadRequest_when_post_is_called_with_empty_array()
        {
            bool[][] values = new bool[0][];

            var result = controller.Post(values);

            result.Should().BeOfType<BadRequestResult>();

        }

        [Test]
        public void return_Ok_when_put_is_called_with_valid_repository()
        {
            bool[,] values2d = { { false, false }, { false, true } };
            boardRepository.Load().Returns(new Board(values2d));

            var result = controller.Put();

            result.Should().BeOfType<OkResult>();

        }

        [Test]
        public void trow_Expection_when_put_is_called_with_null_repository()
        {
            bool[,] values2d = null;
            

            ActionResult result;

            try
            {
                boardRepository.Load().Returns(new Board(values2d));
                result = controller.Put();
            }
            catch (Exception ex)
            {
                Assert.Pass();
            }

        }*/
    }
}
