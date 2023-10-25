namespace GameOfLifeKata.Business
{
    public enum states
    {
        alive,
        dead
    }
    public class Cell
    {
        private states state;
        private int[] position;

        public Cell(states isAlive, int[] position)
        {
            state = isAlive;
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
            return position;
        }

        public states getState()
        {
            return state;
        }

        public override bool Equals(object obj)
        {
            Cell cell = (Cell)obj;
            return state == cell.state && position[0] == cell.position[0] && position[1] == cell.position[1];
        }

    }
}
