using Lebetak.Models.Attachments.Att_Worker_Project;
using Lebetak.Models.ChatModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lebetak.Models
{
    public class LebetakContext : IdentityDbContext<User>
    {
        public LebetakContext(DbContextOptions<LebetakContext> options) : base(options) { }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<AttPost> AttPosts { get; set; }
        public DbSet<PostStatus> PostStatuses { get; set; }
        public DbSet<Urgency> Urgencies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ProjectWorker> ProjectWorkers { get; set; }
        public DbSet<ProjectCompany> ProjectCompanies { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ServiceAnswer> ServiceAnswers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<AttWorkerProject> AttWorkerProjects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new WalletConfigration());
            modelBuilder.ApplyConfiguration(new UserConfigration());
            modelBuilder.ApplyConfiguration(new OwnerConfigration());
            modelBuilder.ApplyConfiguration(new ClientConfigration());
            modelBuilder.ApplyConfiguration(new WorkerConfigration());
            modelBuilder.ApplyConfiguration(new CategoryConfigration());
            modelBuilder.ApplyConfiguration(new CompanyConfigration());


            modelBuilder.ApplyConfiguration(new PostStatusConfiguration());
            modelBuilder.ApplyConfiguration(new UrgencyConfiguratiion());
            modelBuilder.ApplyConfiguration(new WorkerSkillsConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectWorkerConfigration());
            modelBuilder.ApplyConfiguration(new ProposalConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfigration());
            modelBuilder.ApplyConfiguration(new OptionConfigration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceAnswerConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new ComponyPhonesConfiguration());

            modelBuilder.ApplyConfiguration(new AttProposalConfiguration());
            modelBuilder.ApplyConfiguration(new AttPostConfiguration());
            modelBuilder.ApplyConfiguration(new AttAnswerConfiguration());
            modelBuilder.ApplyConfiguration(new AttWorkerProjectConfiguration());
            modelBuilder.ApplyConfiguration(new AttServiceConfiguration());

            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            //modelBuilder.ApplyConfiguration(new AttCompanyProjectConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());






            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedRole();
            modelBuilder.SeedCategorey();
            modelBuilder.SeedPostStatus();
            modelBuilder.SeedUrgency();
            modelBuilder.SeedQuestions();
            modelBuilder.SeedOptions();



        }
    }
}