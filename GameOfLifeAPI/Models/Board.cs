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
        public Board(bool[,] values)
        {
            initialize(values);
            sizeX = values.GetLength(0);
            sizeY = values.GetLength(1);

        }


        //ignore cells that are on the borders and change the ones on the middle
        //when all cells are checked all changes are applied at the same time to avoid overlapping between changes.
        public void next()
        {
            foreach (var cell in board)
            {
                if (isBorder(cell))
                {
                    continue;
                }
                else
                {
                    if (cell.getState() == states.alive)
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

        public bool[,] ToArray()
        {
            bool[,] values = new bool[sizeX, sizeY];


            foreach (var cell in board)
            {
                if (cell.getState() == states.alive)
                {
                    values[cell.getPosition()[0], cell.getPosition()[1]] = true;
                }
                else
                {
                    values[cell.getPosition()[0], cell.getPosition()[1]] = false;
                } 
            }

            return values;
        }

        //initialize board with a 2d bool array
        private void initialize(bool[,] bools)
        {
            board = new List<Cell>();

            for (int x = 0; x < bools.GetLength(0); x++)
            {
                for (int y = 0; y < bools.GetLength(1); y++)
                {
                    if (bools[x, y] == true)
                    {
                        board.Add(new Cell(states.alive, new int[] { x, y }));
                    }
                    else
                    {
                        board.Add(new Cell(states.dead, new int[] { x, y }));
                    }

                }
            }
        }

        public override bool Equals(object obj)
        {

            Board boardTemp = (Board)obj;
            return this.board.SequenceEqual(boardTemp.board);

        }

        private bool isBorder(Cell cell)
        {
            int x = cell.getPosition()[0];
            int y = cell.getPosition()[1];

            if (x == 0 || y == 0 ||
                x == sizeX - 1 || y == sizeY - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //applies every queued change
        private void updateBoard()
        {
            while (StackOff.Count > 0)
            {
                int[] item = StackOff.Pop();
                findCell(item).dead();
            }

            while (StackOn.Count > 0)
            {
                int[] item = StackOn.Pop();
                findCell(item).alive();
            }
        }

        private Cell findCell(int[] pos)
        {
            return board.Find(cell =>
                cell.getPosition().SequenceEqual(pos)
            );
        }


        //when a cell is dead, if there are exactly 3 alive adjacent pixels, this is resurrected
        private void reproduction(Cell cell)
        {
            int numberOfAdjacent = countAdjacent(cell);
            if (numberOfAdjacent == 3)
            {
                StackOn.Push(cell.getPosition());
            }
        }


        //when a cell is alive, if there are less than 2 adjacent alive cells, this is killed
        private void underpopulation(Cell cell)
        {
            int numberOfAdjacent = countAdjacent(cell);
            if (numberOfAdjacent < 2)
            {
                StackOff.Push(cell.getPosition());
            }
        }


        //when a cell is alive, if there are more than 3 adjacent alive cells, this is killed
        private void overpopulation(Cell cell)
        {
            int numberOfAdjacent = countAdjacent(cell);
            if (numberOfAdjacent > 3)
            {
                StackOff.Push(cell.getPosition());
            }
        }


        //check the number of adjacent alive cells to a said cell
        private int countAdjacent(Cell cell)
        {
            int res = 0;
            int x = cell.getPosition()[0];
            int y = cell.getPosition()[1];


            if (findCell(new int[] { x - 1, y - 1 }).getState() == states.alive) res++;
            if (findCell(new int[] { x - 1, y }).getState() == states.alive) res++;
            if (findCell(new int[] { x - 1, y + 1 }).getState() == states.alive) res++;

            if (findCell(new int[] { x + 1, y - 1 }).getState() == states.alive) res++;
            if (findCell(new int[] { x + 1, y }).getState() == states.alive) res++;
            if (findCell(new int[] { x + 1, y + 1 }).getState() == states.alive) res++;

            if (findCell(new int[] { x, y - 1 }).getState() == states.alive) res++;
            if (findCell(new int[] { x, y + 1 }).getState() == states.alive) res++;

            return res;
        }

    }
}
