using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GameOfLifeKata.API.Controllers;
using GameOfLifeKata.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
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
        public void return_OK_when_post_is_called_with_not_empty_array()
        {
            bool[][] values = new bool[2][];
            values[0] = new[] { true, true };
            values[1] = new[] { true, true };

            var result =controller.Post(values);

            result.Should().BeOfType<OkResult>();

        }
    }
}
