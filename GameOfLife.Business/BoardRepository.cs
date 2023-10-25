namespace GameOfLifeKata.Business
{
    public interface BoardRepository
    {
        void Save(Board board, int id);
        Board Load(int id);
    }
}