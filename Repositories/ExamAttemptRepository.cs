using E_learning_platform.Data;
using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Repositories
{
    public class ExamAttemptRepository : IExamAttemptRepository
    {
        private readonly ApplicationDbContext _context;

        public ExamAttemptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ExamAttempt> StartAttemptAsync(long userId, long examId, int attemptNumber, decimal maxScore)
        {
            var attempt = new ExamAttempt
            {
                UserId = userId,
                ExamId = examId,
                AttemptNumber = attemptNumber,
                StartedAt = DateTime.UtcNow,
                Status = "InProgress",
                MaxScore = maxScore
            };
            _context.ExamAttempts.Add(attempt);
            await _context.SaveChangesAsync();
            return attempt;
        }

        public async Task<bool> FinishAttemptAsync(long attemptId, decimal totalScore, int timeSpentSeconds, bool isPassed)
        {
            var attempt = await _context.ExamAttempts.FindAsync(attemptId);
            if (attempt == null) return false;
            attempt.SubmittedAt = DateTime.UtcNow;
            attempt.TimeSpentSeconds = timeSpentSeconds;
            attempt.TotalScore = totalScore;
            attempt.IsPassed = isPassed;
            attempt.Status = "Submitted";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ExamAttempt?> GetAttemptByIdAsync(long attemptId)
        {
            return await _context.ExamAttempts
                .Include(a => a.Exam)
                .FirstOrDefaultAsync(a => a.Id == attemptId);
        }

        public async Task<IEnumerable<ExamAttempt>> GetAttemptsByUserAsync(long userId, long? examId)
        {
            var query = _context.ExamAttempts
                .Where(a => a.UserId == userId);
            if (examId.HasValue)
                query = query.Where(a => a.ExamId == examId.Value);
            return await query
                .Include(a => a.Exam)
                .OrderByDescending(a => a.StartedAt)
                .ToListAsync();
        }

        public async Task<UserAnswer> SubmitAnswerAsync(UserAnswer answer)
        {
            _context.UserAnswers.Add(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<IEnumerable<UserAnswer>> GetAnswersByAttemptAsync(long attemptId)
        {
            return await _context.UserAnswers
                .Where(a => a.AttemptId == attemptId)
                .Include(a => a.Question)
                .Include(a => a.Option)
                .ToListAsync();
        }

        public async Task<decimal> GetMaxScoreForExamAsync(long examId)
        {
            var points = await _context.Questions
                .Where(q => q.ExamSection != null && q.ExamSection.ExamId == examId)
                .Select(q => q.Points)
                .ToListAsync();
            if (!points.Any()) return 0;
            return points.Sum(p => p);
        }
    }
}
