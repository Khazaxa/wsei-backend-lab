using ApplicationCore.Interfaces.AdminService;
using ApplicationCore.Models.QuizAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/v1/admin/quizzes")]
    [ApiController]
    public class QuizAdminController : ControllerBase
    {
        private readonly IQuizAdminService _service;

        public QuizAdminController(IQuizAdminService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Quiz>> GetAllQuizzes()
        {
            return Ok(_service.FindAllQuizzes());
        }

        [HttpGet("{id}")]
        public ActionResult<Quiz> GetQuiz(int id)
        {
            var quiz = _service.FindQuizById(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }

        [HttpPost]
        public ActionResult<Quiz> AddQuiz([FromBody] Quiz quiz)
        {
            var createdQuiz = _service.AddQuiz(quiz.Title, quiz.Items);
            return CreatedAtAction(nameof(GetQuiz), new { id = createdQuiz.Id }, createdQuiz);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuiz(int id)
        {
            _service.DeleteQuiz(id);
            return NoContent();
        }

        [HttpGet("{quizId}/items")]
        public ActionResult<List<QuizItem>> GetAllQuizItems(int quizId)
        {
            return Ok(_service.FindAllQuizItems());
        }

        [HttpGet("{quizId}/items/{itemId}")]
        public ActionResult<QuizItem> GetQuizItem(int quizId, int itemId)
        {
            var quizItem = _service.FindQuizItemById(itemId);
            if (quizItem == null)
            {
                return NotFound();
            }
            return Ok(quizItem);
        }

        [HttpPost("{quizId}/items")]
        public ActionResult<QuizItem> AddQuizItem(int quizId, [FromBody] QuizItem quizItem)
        {
            var createdQuizItem = _service.AddQuizItem(quizItem.Question, quizItem.IncorrectAnswers, quizItem.CorrectAnswer, quizItem.Points);
            return CreatedAtAction(nameof(GetQuizItem), new { quizId = quizId, itemId = createdQuizItem.Id }, createdQuizItem);
        }

        // [HttpDelete("{quizId}/items/{itemId}")]
        // public IActionResult DeleteQuizItem(int quizId, int itemId)
        // {
        //     _service.DeleteQuizItem(itemId);
        //     return NoContent();
        // }
    }
}