using GameOfLifeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameOfLifeAPI.Controllers
{
    [Route("api/gameoflife")]
    [ApiController]
    public class GameOfLifeController : ControllerBase
    {
        GameOfLife gameOfLife;

        /// <summary>
        /// update the saved instance of the board
        /// </summary>
        [HttpPut]
        public ActionResult updateBoard() {
            bool[][] board = readFile();
            if (board == null) return BadRequest("Bad Request: File was not initialized");

            GameOfLife gameOfLife = new GameOfLife(jaggedTo2d(board));
            gameOfLife.next();

            string json = JsonConvert.SerializeObject(gameOfLife.ToArray());
            System.IO.File.WriteAllText(@"c:\dotNetKataGoL\GameOfLifeAPI\Data.json", json);
            return Ok();

        }

        /// <summary>
        /// Initialize board with an array
        /// </summary>
        
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool[][]), (int)HttpStatusCode.OK)]
        public ActionResult initializeBoard([FromBody] bool[][] values)
        {
            if (values == null) return BadRequest("Bad request: input is null");

            writeFile(values);

            return Ok();
        }

        /// <summary>
        /// Deletes the current board
        /// </summary>
        [HttpDelete]
        public ActionResult deleteBoard() {
            System.IO.File.WriteAllText(@"c:\dotNetKataGoL\GameOfLifeAPI\Data.json", String.Empty);
            return Ok();
        }

        private bool[,] jaggedTo2d(bool[][] values)
        {
            bool[,] values2d = new bool[values.Length, values[0].Length];
            for (var i = 0; i < values.Length; i++)
            {
                for (var j = 0; j < values[i].Length; j++)
                {
                    values2d[i, j] = values[i][j];
                }
            }
            return values2d;
        }

        private void writeFile(bool[][] values) {
            string json = JsonConvert.SerializeObject(values);
            System.IO.File.WriteAllText(@"c:\dotNetKataGoL\GameOfLifeAPI\Data.json", json);
        }
        private bool[][] readFile() {
            string json = System.IO.File.ReadAllText(@"c:\dotNetKataGoL\GameOfLifeAPI\Data.json");
            if (json.Length == 0) return null; 
            bool[][] board = JsonConvert.DeserializeObject<bool[][]>(json);

            return board;
        }
    }
}
