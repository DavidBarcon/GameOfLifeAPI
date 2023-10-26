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
            string path = TestContext.CurrentContext.TestDirectory;
            string filePath = Path.Combine(path, "1.json");

            FileSystemBoardRepository fileSystemBoardRepository = new FileSystemBoardRepository(path);
            bool[,] values = new bool[2, 2] { { true, true},{ true , true } };
            Board board = new Board(values);

            fileSystemBoardRepository.Save(board,1);

            string json = File.ReadAllText(filePath);

            BoardDTO result = JsonConvert.DeserializeObject<BoardDTO>(json);

            result.Should().BeEquivalentTo(board.toDTO());
        }

        [Test]
        public void Load_the_saved_board()
        {
            string path = TestContext.CurrentContext.TestDirectory;
            string filePath = Path.Combine(path, "1.json");

            FileSystemBoardRepository fileSystemBoardRepository = new FileSystemBoardRepository(path);
            bool[,] values = new bool[2, 2] { { true, true }, { true, true } };
            Board boardExpected = new Board(values);

            fileSystemBoardRepository.Save(boardExpected,1);

            Board boardLoaded = fileSystemBoardRepository.Load(1);

            boardLoaded.Should().BeEquivalentTo(boardExpected);

        }

        [Test]
        public void Throw_file_not_found_exeption_if_file_doesnt_exist()
        {
            string path = TestContext.CurrentContext.TestDirectory;

            FileSystemBoardRepository fileSystemBoardRepository = new FileSystemBoardRepository(path);


            try
            {
                fileSystemBoardRepository.Load(2);
            }
            catch (FileNotFoundException e)
            {
                Assert.Pass(e.Message);
            }
            Assert.Fail("File exists");
            
        }
    }
}
