using E_learning_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // === SYSTEM ===
        public DbSet<SystemSetting> SystemSettings { get; set; }

        // === AUTH & USER ===
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // === PERMISSIONS & FEATURES ===
        public DbSet<Feature> Features { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        // === POSTS ===
        public DbSet<Post> Posts { get; set; }

        // === LANGUAGES & LEVELS ===
        public DbSet<Language> Languages { get; set; }
        public DbSet<Level> Levels { get; set; }

        // === COURSES ===
        public DbSet<Course> Courses { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

        // === ORDERS & PAYMENTS ===
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        // === EXAMS ===
        public DbSet<Exam> Exams { get; set; }
        public DbSet<CourseExam> CourseExams { get; set; }
        public DbSet<ExamSection> ExamSections { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<MediaQuestion> MediaQuestions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<ExamAttempt> ExamAttempts { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        // === TRANSCRIPTS ===
        public DbSet<TranscriptPractice> TranscriptPractices { get; set; }

        // === VOCABULARY ===
        public DbSet<VocabularySet> VocabularySets { get; set; }
        public DbSet<VocabularyCard> VocabularyCards { get; set; }
        public DbSet<VocabularySetCard> VocabularySetCards { get; set; }
        public DbSet<UserFlashcardProgress> UserFlashcardProgress { get; set; }

        // === REVIEWS & NOTIFICATIONS ===
        public DbSet<CourseReview> CourseReviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        // === LEARNING PROGRESS ===
        public DbSet<LessonProgress> LessonProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === COMPOSITE KEYS ===
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.FeatureId });

            modelBuilder.Entity<VocabularySetCard>()
                .HasKey(vsc => new { vsc.SetId, vsc.CardId });

            modelBuilder.Entity<UserFlashcardProgress>()
                .HasIndex(u => new { u.UserId, u.CardId })
                .IsUnique();

            // === UNIQUE CONSTRAINTS ===
            modelBuilder.Entity<CourseReview>()
                .HasIndex(r => new { r.UserId, r.CourseId })
                .IsUnique();

            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.UserId, e.CourseId })
                .IsUnique();

            // === SELF-REFERENCE ===
            modelBuilder.Entity<Exam>()
                .HasOne<Exam>()
                .WithMany()
                .HasForeignKey(e => e.ParentExamId)
                .OnDelete(DeleteBehavior.Restrict);

            // === USERS ===
            modelBuilder.Entity<User>()
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne<Branch>()
                .WithMany()
                .HasForeignKey(u => u.BranchId)
                .OnDelete(DeleteBehavior.SetNull);

            // === COURSES ===
            modelBuilder.Entity<Course>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Course>()
                .HasOne<Language>()
                .WithMany()
                .HasForeignKey(c => c.LanguageId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Course>()
                .HasOne<Branch>()
                .WithMany()
                .HasForeignKey(c => c.BranchId)
                .OnDelete(DeleteBehavior.SetNull);

            // === CHAPTERS & LESSONS ===
            modelBuilder.Entity<Chapter>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lesson>()
                .HasOne<Chapter>()
                .WithMany()
                .HasForeignKey(l => l.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            // === ENROLLMENTS ===
            modelBuilder.Entity<Enrollment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // === EXAM RELATIONS ===
            modelBuilder.Entity<CourseExam>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(ce => ce.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseExam>()
                .HasOne<Exam>()
                .WithMany()
                .HasForeignKey(ce => ce.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAnswer>()
                .HasOne<ExamAttempt>()
                .WithMany()
                .HasForeignKey(a => a.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            // === VOCABULARY RELATIONS ===
            modelBuilder.Entity<VocabularySet>()
                .HasOne<Language>()
                .WithMany()
                .HasForeignKey(v => v.LanguageId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<VocabularySet>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(v => v.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull);

            // === FLASHCARD RELATIONS ===
            modelBuilder.Entity<UserFlashcardProgress>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserFlashcardProgress>()
                .HasOne<VocabularyCard>()
                .WithMany()
                .HasForeignKey(u => u.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            // === NOTIFICATIONS ===
            modelBuilder.Entity<Notification>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // === COURSE REVIEWS ===
            modelBuilder.Entity<CourseReview>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseReview>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
