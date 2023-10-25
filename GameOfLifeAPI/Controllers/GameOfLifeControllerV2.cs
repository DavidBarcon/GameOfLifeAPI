using Microsoft.AspNetCore.Mvc;
using GameOfLifeKata.Business;
using Asp.Versioning;

namespace GameOfLifeKata.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v2.0/gameoflife")]
    [ApiController]
    public class GameOfLifeControllerV2 : ControllerBase
    {
        private GameOfLife _gameOfLife;
        public GameOfLifeControllerV2(GameOfLife gameOfLife)
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
        [MapToApiVersion("2.0")]
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
        /// Initialize a game with a list of numbers.
        /// First 2 represent height and width, the rest, each pair represents an active cell.
        /// It must be at lest 2 numbers as it is needed to create the board.
        /// If a board with that id doesn´t exist, its created. 
        /// </summary>
        /// 
        /// <response code="201">Returns the newly created game id</response>
        /// <response code="400">The input values are not valid</response>
        /// 
        //"api/v2/gameoflife"
        [HttpPost]
        [MapToApiVersion("2.0")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(int), 201)]
        [ProducesResponseType(400)]
        public ActionResult Post2([FromBody] int[] values)
        {
            if(values.Length % 2 != 0 || values.Length < 2) return BadRequest();

            GameOfLifeBuilder builder = new GameOfLifeBuilder(_gameOfLife, values[0], values[1]);
            for (int i = 2; i < values.Length; i += 2)
            {
                builder.AddElement(values[i], values[i + 1]);
            }

            _gameOfLife = builder.build();
            int id = _gameOfLife.getId();

            return Created(nameof(_gameOfLife), id);
        }

    }
}
