using AutoMapper;
using E_learning_platform.DTOs.Requests;
using E_learning_platform.DTOs.Responses;
using E_learning_platform.Models;
using E_learning_platform.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Services
{
    public class ExamAttemptService : IExamAttemptService
    {
        private readonly IExamAttemptRepository _attemptRepository;
        private readonly IExamRepository _examRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public ExamAttemptService(
            IExamAttemptRepository attemptRepository,
            IExamRepository examRepository,
            IQuestionRepository questionRepository,
            IMapper mapper)
        {
            _attemptRepository = attemptRepository;
            _examRepository = examRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<ExamAttemptResponse> StartAttemptAsync(long userId, StartExamAttemptRequest request)
        {
            var exam = await _examRepository.GetExamByIdAsync(request.ExamId);
            if (exam == null || !exam.IsActive)
                throw new Exception("Exam không tồn tại hoặc đã vô hiệu hóa");

            var maxScore = await _attemptRepository.GetMaxScoreForExamAsync(request.ExamId);
            var attempts = await _attemptRepository.GetAttemptsByUserAsync(userId, request.ExamId);
            var attemptNumber = attempts.Count() + 1;
            if (exam.MaxAtttempts > 0 && attemptNumber > exam.MaxAtttempts)
                throw new Exception("Bạn đã vượt quá số lần làm bài tối đa");

            var attempt = await _attemptRepository.StartAttemptAsync(userId, request.ExamId, attemptNumber, maxScore);
            var response = _mapper.Map<ExamAttemptResponse>(attempt);
            response.ExamTitle = exam.Title ?? string.Empty;
            return response;
        }

        public async Task<bool> SubmitAnswerAsync(long userId, SubmitAnswerRequest request)
        {
            var attempt = await _attemptRepository.GetAttemptByIdAsync(request.AttemptId);
            if (attempt == null || attempt.UserId != userId)
                throw new Exception("Không tìm thấy bài làm hoặc bạn không có quyền");
            if (attempt.Status != "InProgress")
                throw new Exception("Bài làm đã nộp, không thể sửa");

            var question = await _questionRepository.GetQuestionByIdAsync(request.QuestionId);
            if (question == null)
                throw new Exception("Câu hỏi không tồn tại");

            bool? isCorrect = null;
            decimal? score = null;
            if (request.OptionId.HasValue)
            {
                var isOptionCorrect = await _questionRepository.IsOptionCorrectAsync(request.QuestionId, request.OptionId.Value);
                isCorrect = isOptionCorrect;
                score = isOptionCorrect ? question.Points : 0;
            }

            var answer = new UserAnswer
            {
                AttemptId = request.AttemptId,
                QuestionId = request.QuestionId,
                OptionId = request.OptionId,
                AnswerText = request.AnswerText,
                IsCorrect = isCorrect,
                Score = score,
                SubmittedAt = DateTime.UtcNow
            };
            await _attemptRepository.SubmitAnswerAsync(answer);
            return true;
        }

        public async Task<ExamAttemptResponse> FinishAttemptAsync(long userId, long attemptId)
        {
            var attempt = await _attemptRepository.GetAttemptByIdAsync(attemptId);
            if (attempt == null || attempt.UserId != userId)
                throw new Exception("Không tìm thấy bài làm hoặc bạn không có quyền");

            var answers = await _attemptRepository.GetAnswersByAttemptAsync(attemptId);
            var totalScore = answers.Sum(a => a.Score ?? 0);
            var isPassed = attempt.MaxScore > 0 && totalScore >= attempt.MaxScore * (attempt.Exam?.PassingScore ?? 0) / (attempt.Exam?.TotalMarks ?? attempt.MaxScore);
            await _attemptRepository.FinishAttemptAsync(attemptId, totalScore, 0, isPassed);
            var updated = await _attemptRepository.GetAttemptByIdAsync(attemptId);
            var response = _mapper.Map<ExamAttemptResponse>(updated);
            response.ExamTitle = updated?.Exam?.Title ?? string.Empty;
            return response;
        }

        public async Task<IEnumerable<ExamAttemptResponse>> GetMyAttemptsAsync(long userId, long? examId)
        {
            var attempts = await _attemptRepository.GetAttemptsByUserAsync(userId, examId);
            var result = _mapper.Map<IEnumerable<ExamAttemptResponse>>(attempts);
            foreach (var r in result)
            {
                var attempt = attempts.First(a => a.Id == r.Id);
                r.ExamTitle = attempt.Exam?.Title ?? string.Empty;
            }
            return result;
        }

        public async Task<IEnumerable<UserAnswerResponse>> GetAttemptAnswersAsync(long userId, long attemptId)
        {
            var attempt = await _attemptRepository.GetAttemptByIdAsync(attemptId);
            if (attempt == null || attempt.UserId != userId)
                throw new Exception("Không tìm thấy bài làm hoặc bạn không có quyền");
            var answers = await _attemptRepository.GetAnswersByAttemptAsync(attemptId);
            return _mapper.Map<IEnumerable<UserAnswerResponse>>(answers);
        }
    }
}
