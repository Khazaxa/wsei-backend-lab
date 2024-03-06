using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace BackendLab01.Pages
{
    
    public class QuizModel : PageModel
    {
        private readonly IQuizUserService _userService;

        private readonly ILogger _logger;
        public QuizModel(IQuizUserService userService, ILogger<QuizModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [BindProperty]
        public string Question { get; set; }
        [BindProperty]
        public List<string> Answers { get; set; }
        
        [BindProperty]
        public String UserAnswer { get; set; }
        
        [BindProperty]
        public int QuizId { get; set; }
        
        [BindProperty]
        public int ItemId { get; set; }
        
        public void OnGet(int quizId, int itemId)
        {
            QuizId = quizId;
            ItemId = itemId;
            var quiz = _userService.FindQuizById(quizId);
            if (itemId > 0 && itemId <= quiz?.Items.Count)
            {
                var quizItem = quiz?.Items[itemId - 1];
                Question = quizItem?.Question;
                Answers = new List<string>();
                if (quizItem is not null)
                {
                    Answers.AddRange(quizItem?.IncorrectAnswers);
                    Answers.Add(quizItem?.CorrectAnswer);
                }
            }
        }

        [BindProperty]
        public int CorrectAnswers { get; set; }

        public IActionResult OnPost()
        {
            var quiz = _userService.FindQuizById(QuizId);
            if (ItemId == quiz?.Items.Count)
            {
                return RedirectToPage("./Summary", new { quizId = QuizId, itemId = ItemId, correctAnswers = CorrectAnswers, totalQuestions = quiz?.Items.Count });
            }
            else
            {
                return RedirectToPage("Item", new {quizId = QuizId, itemId = ItemId + 1});
            }
        }
    }
}
