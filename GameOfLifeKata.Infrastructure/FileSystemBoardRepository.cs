using GameOfLifeKata.Business;


namespace GameOfLifeKata.Infrastructure
{
    public class FileSystemBoardRepository : BoardRepository
    {
        private string path;

        public FileSystemBoardRepository(string path)
        {
            this.path = path;
        }

        public void Save(Board board)
        {

            BoardDTO boardDTO = board.toDTO();
            string json = System.Text.Json.JsonSerializer.Serialize(boardDTO); 
            System.IO.File.WriteAllText(path, json);


        }
    }

    public class BoardDTO
    {
        public IEnumerable<CellDTO> Cells { get; set; }

    }

    public class CellDTO
    {
        public bool State {  get; set; }
        public int x { get; set; }
        public int y { get; set; }


    }

    public static class BoardExtensions
    {
        public static BoardDTO toDTO(this Board board)
        {
            var cells = new List<CellDTO>();
            var array = board.ToArray();
            for (int i = 0;i<array.GetLength(0); i++)
            {
                for(int j = 0; j < array.GetLength(1); j++)
                {
                    cells.Add(new CellDTO() { State = array[i,j], x = i, y=j });    
                } 
            }
            return new BoardDTO() { Cells = cells };


        }
    }
}