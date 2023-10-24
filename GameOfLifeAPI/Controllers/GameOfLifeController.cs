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
        private GameOfLife _gameOfLife;
        public GameOfLifeController(GameOfLife gameOfLife)
        {
            this._gameOfLife = gameOfLife;
        }

        
        /// <summary>
        /// Update the saved instance of the selected game.
        /// </summary>

        //"api/gameoflife/5"
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Initialize a game with an array. If it doesn´t exist, its created.
        /// </summary>
        
        //"api/gameoflife"
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] bool[][] values)
        {
            if (values.Length == 0 || values == null) return BadRequest();
            int id = _gameOfLife.NewGame(jaggedTo2d(values));
            return Created("Created successful: ",id);
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
