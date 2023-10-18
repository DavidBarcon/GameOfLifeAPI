namespace GameOfLifeAPI.Models
{
    enum states
    {
        alive,
        dead
    }
    internal class Cell
    {
        private states state;
        private int[] position;

        public Cell(states isAlive, int[] position)
        {
            this.state = isAlive;
            this.position = position;
        }
        public void dead()
        {
            state = states.dead;
        }
        public void alive()
        {
            state = states.alive;
        }

        public int[] getPosition()
        {
            return this.position;
        }

        public states getState()
        {
            return state;
        }

        public override bool Equals(object obj)
        {
            Cell cell = (Cell)obj;
            return this.state == cell.state && this.position[0] == cell.position[0] && this.position[1] == cell.position[1];
        }

    }
}
