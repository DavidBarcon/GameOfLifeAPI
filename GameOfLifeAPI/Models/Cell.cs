namespace GameOfLifeAPI.Models
{
    internal class Cell
    {
        public bool isAlive { get; set; }
        public int x { get; }
        public int y { get; }

        public Cell(bool isAlive, int x, int y)
        {
            this.isAlive = isAlive;
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            Cell cell = (Cell)obj;
            return this.isAlive == cell.isAlive && this.x == cell.x && this.y == cell.y;
        }

    }
}
