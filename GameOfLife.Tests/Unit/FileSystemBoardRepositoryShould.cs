using FluentAssertions;
using GameOfLifeKata.Business;
using GameOfLifeKata.Infrastructure;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GameOfLifeKata.Tests.Unit
{

    [TestFixture]
    internal class FileSystemBoardRepositoryShould
    {

        [Test]
        public void Save_the_given_board() {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Data.json");

            FileSystemBoardRepository fileSystemBoardRepository = new FileSystemBoardRepository(path);
            bool[,] values = new bool[1, 1] { { true} };
            Board board = new Board(values);

            fileSystemBoardRepository.Save(board);

            string json = File.ReadAllText(path);

            BoardDTO result = JsonConvert.DeserializeObject<BoardDTO>(json);

            result.Should().BeEquivalentTo(board.toDTO());
        }
    }
}
