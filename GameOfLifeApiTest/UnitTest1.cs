using GameOfLifeAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.Core;

namespace GameOfLifeApiTest
{
    [TestClass]
    public class GameOfLifeController_should
    {
        bool[][] testArray;
        public GameOfLifeController_should()
        {
            testArray = new bool[3][];
            testArray[0] = new bool[] { false, false, false };
            testArray[1] = new bool[] { false, false, false };
            testArray[2] = new bool[] { false, false, false };
        }

        [TestMethod]
        public void given_a_null_value_post_should_return_BadRequest()
        {
            var controller = new GameOfLifeController();

            ActionResult actionResult = controller.initializeBoard(null);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void given_an_array_post_should_return_OK()
        {
            var controller = new GameOfLifeController();

            ActionResult actionResult = controller.initializeBoard(testArray);

            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        

        [TestMethod]
        public void given_initialized_board_put_should_return_OK()
        {
            var controller = new GameOfLifeController();

            controller.initializeBoard(testArray);
            ActionResult actionResult = controller.updateBoard();

            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

    }
}