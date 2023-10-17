using GameOfLifeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        /// Generate a game of life instance from a 2D bool array and calculate the next generation.
        /// </summary>
        
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool[][]), (int)HttpStatusCode.OK)]
        public IActionResult post([FromBody] bool[][] values)
        {
            bool[,] values2d;
            if (values == null) return BadRequest("Bad request: input is null");

            try
            {
                //convert from bool[][] to bool[,]
                values2d = stageredTo2d(values);
            }
            catch (Exception ex)
            {
                return BadRequest("Bad request: input is in the wrong format");
            }

            gameOfLife = new GameOfLife(values2d);

            gameOfLife.next();
            return Ok();
        }

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
