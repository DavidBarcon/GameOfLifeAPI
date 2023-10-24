using FluentAssertions;
using GameOfLifeKata.API.Controllers;
using GameOfLifeKata.Business;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace GameOfLifeKata.Tests.Unit
{
    [TestFixture]
    public class GameOfLifeAPIShould
    {
        private BoardRepository boardRepository;
        private GameOfLife gameOfLife;
        private GameOfLifeController controller;

        
        [SetUp]
        public void SetUp()
        {
            boardRepository = Substitute.For<BoardRepository>();

            gameOfLife = new GameOfLife(boardRepository);

            controller = new GameOfLifeController(gameOfLife);
        }

        [Test]
        public void return_Created_when_post_is_called_with_not_empty_array()
        {
            bool[][] values = new bool[2][];
            values[0] = new[] { true, true };
            values[1] = new[] { true, true };

            var result =controller.Post(values);

            result.Should().BeOfType<CreatedResult>();

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
            int id = gameOfLife.NewGame(values2d);
            boardRepository.Load(id).Returns(new Board(values2d));

            var result = controller.Put(id);

            result.Should().BeOfType<OkResult>();

        }

        [Test]
        public void trow_Expection_when_put_is_called_with_null_repository()
        {
            bool[,] values2d = null;
            

            ActionResult result;

            try
            {
                int id = gameOfLife.NewGame(values2d);

                boardRepository.Load(id).Returns(new Board(values2d));
                result = controller.Put(id);
            }
            catch (Exception ex)
            {
                Assert.Pass();
            }

        }

        [Test]
        public void return_NotFound_when_put_is_called_with_a_nonexistent_id()
        {
            boardRepository.Load(101).Throws(new FileNotFoundException());
            ActionResult result = controller.Put(101);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
