namespace GameOfLifeAPI.Models
{
    internal class Board
    {

        List<Cell> board;
        int sizeX;
        int sizeY;

        Stack<int[]> StackOn = new Stack<int[]>();
        Stack<int[]> StackOff = new Stack<int[]>();

        //initialize the board with a 2d array
        public Board(bool[,] bools)
        {
            initialize(bools);
            sizeX = bools.GetLength(0);
            sizeY = bools.GetLength(1);
        }


        //ignore cells that are on the borders and change the ones on the middle
        //when all cells are checked all changes are applied at the same time to avoid overlapping between changes.
        public void next()
        {
            foreach (var cell in board)
            {
                if (cell.x == 0 ||
                   cell.y == 0 ||
                   cell.x == sizeX - 1 ||
                   cell.y == sizeY - 1)
                {
                    continue;
                }
                else
                {
                    if (cell.isAlive)
                    {
                        underpopulation(cell);
                        overpopulation(cell);
                    }
                    else
                    {
                        reproduction(cell);
                    }
                }
            }
            updateBoard();
        }

        //initialize board with a 2d bool array
        private void initialize(bool[,] bools)
        {
            board = new List<Cell>();

            for (int x = 0; x < bools.GetLength(0); x++)
            {
                for (int y = 0; y < bools.GetLength(1); y++)
                {
                    board.Add(new Cell(bools[x, y], x, y));
                }
            }
        }

        public override bool Equals(object obj)
        {

            Board boardTemp = (Board)obj;
            return this.board.SequenceEqual(boardTemp.board);

        }

        //applies every queued change
        private void updateBoard()
        {
            while (StackOff.Count > 0)
            {
                int[] item = StackOff.Pop();
                findCell(item[0], item[1]).isAlive = false;
            }

            while (StackOn.Count > 0)
            {
                int[] item = StackOn.Pop();
                findCell(item[0], item[1]).isAlive = true;
            }
        }

        private Cell findCell(int x, int y)
        {
            return board.Find(cell =>
                cell.x == x && cell.y == y
            );
        }


        //when a cell is dead, if there are exactly 3 alive adjacent pixels, this is resurrected
        private void reproduction(Cell cell)
        {
            int numberOfAdjacent = countAdjacent(cell);
            if (numberOfAdjacent == 3)
            {
                StackOn.Push(new[] { cell.x, cell.y });
            }
        }


        //when a cell is alive, if there are less than 2 adjacent alive cells, this is killed
        private void underpopulation(Cell cell)
        {
            int numberOfAdjacent = countAdjacent(cell);
            if (numberOfAdjacent < 2)
            {
                StackOff.Push(new[] { cell.x, cell.y });
            }
        }


        //when a cell is alive, if there are more than 3 adjacent alive cells, this is killed
        private void overpopulation(Cell cell)
        {
            int numberOfAdjacent = countAdjacent(cell);
            if (numberOfAdjacent > 3)
            {
                StackOff.Push(new[] { cell.x, cell.y });
            }
        }


        //check the number of adjacent alive cells to a said cell
        private int countAdjacent(Cell cell)
        {
            int res = 0;

            if (findCell(cell.x - 1, cell.y - 1).isAlive) res++;
            if (findCell(cell.x - 1, cell.y).isAlive) res++;
            if (findCell(cell.x - 1, cell.y + 1).isAlive) res++;

            if (findCell(cell.x + 1, cell.y - 1).isAlive) res++;
            if (findCell(cell.x + 1, cell.y).isAlive) res++;
            if (findCell(cell.x + 1, cell.y + 1).isAlive) res++;

            if (findCell(cell.x, cell.y - 1).isAlive) res++;
            if (findCell(cell.x, cell.y + 1).isAlive) res++;

            return res;
        }

    }
}
