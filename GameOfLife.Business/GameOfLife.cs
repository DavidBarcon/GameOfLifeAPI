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

        public void next(int id)
        {
            this.board = boardRepository.Load(id);
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

    public class GameofLifeNewGameRequestBuilder
    {
        private bool[,] values;

        public GameofLifeNewGameRequestBuilder(int width, int height)
        {
            values = new bool[width, height];
        }

        public GameofLifeNewGameRequestBuilder AddElement(int x, int y )
        {
            values[x,y] = true;
            return this;
        }

        public bool[,] build()
        {
            return values;
        }
    }


}
