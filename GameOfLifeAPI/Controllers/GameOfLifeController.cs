using GameOfLifeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameOfLifeAPI.Controllers
{
    [Route("api/gameoflife")]
    [ApiController]
    public class GameOfLifeController : ControllerBase
    {
        GameOfLife gameOfLife;

        /*// GET: api/<GameOfLifeController>
        [HttpGet]
        public string Get()
        {
            if (gameOfLife == null) return "Not initialized";
            return gameOfLife.ToString();
        }*/

        

        // POST api/gameoflife
        [HttpPost]
        public IActionResult initializeBoard([FromBody] bool[][] values)
        {

            if (values == null) return BadRequest("Bad request: input is null");

            //convert from bool[][] to bool[,]
            bool[,] values2d = stageredTo2d(values);

            gameOfLife = new GameOfLife(values2d);

            gameOfLife.next();
            return Ok();
        }

        /*// PUT api/gameoflife
        [HttpPut]
        public IActionResult updateBoard()
        {
            if(gameOfLife == null)
            {
                return BadRequest("Bad request: Not initialized");
            }

            gameOfLife.next();
            return Ok();
        }*/

        private bool[,] stageredTo2d(bool[][] values)
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
    }
}
