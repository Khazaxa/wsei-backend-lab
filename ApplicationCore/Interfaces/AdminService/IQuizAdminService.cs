﻿using ApplicationCore.Models.QuizAggregate;

namespace ApplicationCore.Interfaces.AdminService;

public interface IQuizAdminService
{
    public QuizItem AddQuizItem(string question, List<string> incorrectAnswers, string correctAnswer, int points);
    public void UpdateQuizItem(int id, string question, List<string> incorrectAnswers, string correctAnswer, int points);
    public Quiz AddQuiz(string title, List<QuizItem> items);
    public List<QuizItem> FindAllQuizItems();
    public List<Quiz> FindAllQuizzes();
    object? FindQuizById(int id);
    void DeleteQuiz(int id);
    object? FindQuizItemById(int itemId);
    void DeleteQuizItem(int itemId);
}