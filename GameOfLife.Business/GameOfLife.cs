namespace GameOfLifeKata.Business
{
    public class GameOfLife
    {

        private Board board;
        private readonly BoardRepository boardRepository;

        public GameOfLife(BoardRepository boardRepository)
        {
            this.boardRepository = boardRepository;
        }

        public void next()
        {
            this.board = boardRepository.Load();
            board.next();
            boardRepository.Save(board);
        }

        public bool Equals(GameOfLife game)
        {
            return game.board.Equals(board);

        }

        public bool[,] ToArray()
        {
            return board.ToArray();
        }

        public void NewGame(bool[,] values)
        {
            board = new Board(values);
            boardRepository.Save(board);
        }
    }


}
