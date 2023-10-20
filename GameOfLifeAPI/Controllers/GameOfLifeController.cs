using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net;
using GameOfLifeKata.Business;

namespace GameOfLifeKata.API.Controllers
{
    [Route("api/gameoflife")]
    [ApiController]
    public class GameOfLifeController : ControllerBase
    {
        private GameOfLife gameOfLife;
        public GameOfLifeController(GameOfLife gameOfLife)
        {
            this.gameOfLife = gameOfLife;
        }
        
        /// <summary>
        /// Update the saved instance of the board
        /// </summary>

        //"api/gameoflife"
        [HttpPut]
        public ActionResult Put() {

        }

        /// <summary>
        /// Initialize board with an array
        /// </summary>
        
        //"api/gameoflife"
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(bool[][]), (int)HttpStatusCode.OK)]
        public ActionResult Post([FromBody] bool[][] values)
        {

        }
        /*
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
        }*/
    }
}
