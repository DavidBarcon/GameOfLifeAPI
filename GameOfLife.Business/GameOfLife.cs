namespace GameOfLifeKata.Business
{
    public class GameOfLife
    {

        private Board board;
        private readonly BoardRepository boardRepository;
        private int id;

        public GameOfLife(BoardRepository boardRepository)
        {
            this.boardRepository = boardRepository;
        }

        public void next()
        {
            this.board = boardRepository.Load(this.id);
            board.next();
            boardRepository.Save(board, id);
        }

        public bool Equals(GameOfLife game)
        {
            return game.board.Equals(board);

        }

        public bool[,] ToArray()
        {
            return board.ToArray();
        }

        public int NewGame(bool[,] values)
        {
            this.id = new Random().Next(1, 100);
            board = new Board(values);
            boardRepository.Save(board, id);
            return id;
        }
    }


}
