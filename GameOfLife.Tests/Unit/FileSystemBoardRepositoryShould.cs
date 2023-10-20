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
            bool[,] values = new bool[2, 2] { { true, true},{ true , true } };
            Board board = new Board(values);

            fileSystemBoardRepository.Save(board);

            string json = File.ReadAllText(path);

            BoardDTO result = JsonConvert.DeserializeObject<BoardDTO>(json);

            result.Should().BeEquivalentTo(board.toDTO());
        }

        [Test]
        public void Load_the_saved_board()
        {
            string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Data.json");

            FileSystemBoardRepository fileSystemBoardRepository = new FileSystemBoardRepository(path);
            bool[,] values = new bool[2, 2] { { true, true }, { true, true } };
            Board boardExpected = new Board(values);

            fileSystemBoardRepository.Save(boardExpected);

            Board boardLoaded = fileSystemBoardRepository.Load();

            boardLoaded.Should().BeEquivalentTo(boardExpected);

        }
    }
}
