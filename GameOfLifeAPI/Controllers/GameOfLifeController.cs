using GameOfLifeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net;

namespace GameOfLifeAPI.Controllers
{
    [Route("api/gameoflife")]
    [ApiController]
    public class GameOfLifeController : ControllerBase
    {

        public GameOfLifeController() { }
        /// <summary>
        /// Update the saved instance of the board
        /// </summary>

        //"api/gameoflife"
        [HttpPut]
        public ActionResult updateBoard() {
            bool[][] board = readFile();
            if (board == null) return BadRequest("Bad Request: File was not initialized");

            GameOfLife gameOfLife = new GameOfLife(jaggedTo2d(board));

            gameOfLife.next();

            string json = gameOfLife.ToArray().ToJson();
            System.IO.File.WriteAllText(@"c:\dotNetKataGoL\GameOfLifeAPI\Data.json", json);
            return Ok();

        }

        /// <summary>
        /// Initialize board with an array
        /// </summary>
        
        //"api/gameoflife"
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool[][]), (int)HttpStatusCode.OK)]
        public ActionResult initializeBoard([FromBody] bool[][] values)
        {
            if (values == null) return BadRequest("Bad request: input is null");

            writeFile(values);

            return Ok();
        }

        //method that converts and array in the form of <T>[][] to <T>[,]
        //this is because GameOfLife constructor only accepts arrays in that form
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

        //Methods for persistance. They read and write from a local .json file
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
