using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PhotoQuiz.Backend.Api.Controllers
{
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {
        [Authorize]
        [HttpGet("@id:Guid")]
        public async ValueTask<IActionResult> Get([FromServices] ICacheRoot<Guid, Quiz> cacheRoot, [FromRoute] Guid id)
        {
            var quiz = await cacheRoot.Get(id);

            switch (quiz)
            {
                case null:
                    return NotFound();
                default:
                    return Ok(quiz);
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] ICommandHandler<CreateQuizCommand> commandHandler,
            [FromBody] CreateQuizCommand command)
//            [FromServices] IForcingMessageProvider forcingMessageProvider,
//            [FromServices] ICurrentUserProvider currentUserProvider,
        {
//            var quiz = new Quiz
//            {
//                Id = Guid.NewGuid(),
//                Theme = command.Theme,
//                Photo = command.Photo,
//                Question = command.Question,
//                Answers = Answers,
//                ForcingMessage = await forcingMessageProvider.GetForcingMessage(command.Photo, command.Theme)
//            };

            try
            {
                var id = await commandHandler.ExecuteCommand(command);
                var value = new {id};
                return CreatedAtAction(nameof(Get), value, value);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, x);
            }
        }
    }

    public interface IForcingMessageProvider
    {
    }

    public class CreateQuizCommand
    {
    }

    public interface ICommandHandler<T>
    {
        Task<T> ExecuteCommand(CreateQuizCommand command);
    }

    public interface ICurrentUserProvider
    {
    }

    public interface IFileRoot
    {
    }

    public class Quiz
    {
    }

    public interface ICacheRoot<T, T1>
    {
        Task<object> Get(Guid id);
    }
}