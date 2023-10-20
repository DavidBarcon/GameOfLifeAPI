using GameOfLifeKata.Business;
using Newtonsoft.Json;

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
            string json = JsonConvert.SerializeObject(boardDTO);
            
            System.IO.File.WriteAllText(path, json);

        }

        public Board Load()
        {
            string json = System.IO.File.ReadAllText(path);
            BoardDTO boardDTO = JsonConvert.DeserializeObject<BoardDTO>(json);

            return boardDTO.toBoard();
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

        public static Board toBoard(this BoardDTO boardDTO) {
            IEnumerable<CellDTO> cells = boardDTO.Cells;
           
            int maxX=0, maxY=0;
            foreach(CellDTO cell in cells) { 
                if(cell.x > maxX) maxX = cell.x;
                if(cell.y > maxY) maxY = cell.y;
            }

            bool[,] values = new bool[maxX+1,maxY+1];
            foreach (CellDTO cell in cells)
            {
                int x = cell.x;
                int y = cell.y;
                bool state = cell.State;

                values[x,y] = state;
            }

            return new Board(values);
        }
    }
}