using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeKata.API.Controllers;
using GameOfLifeKata.Business;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Api;

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
        public void Test() { }
    }
}
