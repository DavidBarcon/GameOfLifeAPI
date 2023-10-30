using Microsoft.AspNetCore.Mvc;
using GameOfLifeKata.Business;
using Asp.Versioning;

namespace GameOfLifeKata.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/gameoflife")]
    public class GameOfLifeControllerV1 : ControllerBase
    {
        private GameOfLife _gameOfLife;
        public GameOfLifeControllerV1(GameOfLife gameOfLife)
        {
            this._gameOfLife = gameOfLife;
        }


        /// <summary>
        /// Update the saved instance of the selected game.
        /// </summary>
        /// 
        /// <response code="200">The game was found and successfully updated</response>
        /// <response code="404">The game was not found</response>
        /// 
        //"api/gameoflife/5"
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Put(int id)
        {
            try
            {
                _gameOfLife.next(id);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Initialize a game with a 2D array.
        /// If a board with that id doesn´t exist, its created. 
        /// </summary>
        /// 
        /// <response code="201">Returns the newly created game id</response>
        /// <response code="400">The input values are not valid</response>
        /// 
        //"api/v1.0/gameoflife"
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(int),201)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] bool[][] values)
        {
            if (values.Length == 0 || values == null) return BadRequest();
            int id = _gameOfLife.NewGame(jaggedTo2d(values));
            return Created(nameof(_gameOfLife),id);
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
    }
}
