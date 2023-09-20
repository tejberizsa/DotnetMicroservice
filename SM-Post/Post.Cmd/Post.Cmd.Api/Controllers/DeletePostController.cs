using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Common.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/{controller}")]
    public class DeletePostController : ControllerBase
    {
        private readonly ILogger<DeletePostController> _logger;
        private readonly ICommandDispacher _commandDispacher;
        
        public DeletePostController(ILogger<DeletePostController> logger, ICommandDispacher commandDispacher)
        {
            _logger = logger;
            _commandDispacher = commandDispacher;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePostAsync(Guid id, DeletePostCommand command)
        {
            try
            {
                command.Id = id;
                await _commandDispacher.SendAsync(command);

                return Ok(new BaseResponse
                {
                    Message = "Delete post request completed successfully!"
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Client made a bad request!");
                return BadRequest(new BaseResponse
                {
                    Message = ex.Message
                });
            }
            catch (AggregateNotFoundException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Could not retreive aggregate, client passed an incorrect post ID targetting the aggregate!");

                return BadRequest(new BaseResponse
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to delete a post!";
                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}