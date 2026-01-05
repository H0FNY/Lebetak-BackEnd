

using CloudinaryDotNet;
using Lebetak.Models;
using Lebetak.Repository;
using Microsoft.AspNetCore.Identity;

namespace Lebetak.UnitOfWork
{
    public class UnitOFWork
    {
        public UserManager<User> _userManager;
        public SignInManager<User> _signInManager;


        public Cloudinary Cloudinary { get; }


        ChatRepository _chatRepo;
        public ChatRepository ChatRepo
        {
            get
            {
                if (_chatRepo == null) _chatRepo = new ChatRepository(db);
                return _chatRepo;
            }
        }
        MessageRepositry _messageRepo;
        public MessageRepositry MessageRepo
        {
            get
            {
                if (_messageRepo == null) _messageRepo = new MessageRepositry(db);
                return _messageRepo;
            }
        }

        WorkerRepositry _workerRepo;
        public WorkerRepositry WorkerRepo
        {
            get
            {
                if (_workerRepo == null) _workerRepo = new WorkerRepositry(db);
                return _workerRepo;
            }
        }
        WorkerSkillsRepositry _workerSkillRepo;
        public WorkerSkillsRepositry WorkerSkillRepo
        {
            get
            {
                if (_workerSkillRepo == null) _workerSkillRepo = new WorkerSkillsRepositry(db);
                return _workerSkillRepo;
            }
        }

        SkillRepositry _skillRepo;
        public SkillRepositry SkillRepo
        {
            get
            {
                if (_skillRepo == null) _skillRepo = new SkillRepositry(db);
                return _skillRepo;
            }
        }

        ProjectWorkerRepositry _projectWorkerRepo;
        public ProjectWorkerRepositry ProjectWorkerRepo
        {
            get
            {
                if (_projectWorkerRepo == null) _projectWorkerRepo = new ProjectWorkerRepositry(db);
                return _projectWorkerRepo;
            }
        }

        UrgencyRepositry _urgencyRepo;
        public UrgencyRepositry UrgencyRepo
        {
            get
            {
                if (_urgencyRepo == null) _urgencyRepo = new UrgencyRepositry(db);
                return _urgencyRepo;
            }
        }


        ClientRepositry _clientRepo;
        public ClientRepositry ClientRepo
        {
            get
            {
                if (_clientRepo == null) _clientRepo = new ClientRepositry(db);
                return _clientRepo;
            }
        }


        RatingRepositry _ratingRepo;
        public RatingRepositry RatingRepo
        {
            get
            {
                if (_ratingRepo == null) _ratingRepo = new RatingRepositry(db);
                return _ratingRepo;
            }
        }



        ServiceRepositry _serviceRepo;
        public ServiceRepositry ServiceRepo
        {
            get
            {
                if (_serviceRepo == null) _serviceRepo = new ServiceRepositry(db);
                return _serviceRepo;
            }
        }

        ServiceAnswerRepositry _serviceAnswerRepo;
        public ServiceAnswerRepositry ServiceAnswerRepo
        {
            get
            {
                if (_serviceAnswerRepo == null) _serviceAnswerRepo = new ServiceAnswerRepositry(db);
                return _serviceAnswerRepo;
            }
        }

        PostRepositry _postRepo;
        public PostRepositry PostRepo
        {
            get {
                if (_postRepo == null) _postRepo = new PostRepositry(db);
                return _postRepo;
            }
        }

        PostStatusRepositry _postStatusRepo;
        public PostStatusRepositry PostStatusRepo
        {
            get
            {
                if (_postStatusRepo == null) _postStatusRepo = new PostStatusRepositry(db);
                return _postStatusRepo;
            }
        }


        OwnerRepositry _ownerRepo;
        public OwnerRepositry OwnerRepo
        {
            get
            {
                if (_ownerRepo == null) _ownerRepo = new OwnerRepositry(db);
                return _ownerRepo;
            }
        }

        CompanyRepositry _companyRepo;
        public CompanyRepositry CompanyRepo
        {
            get
            {
                if (_companyRepo == null) _companyRepo = new CompanyRepositry(db);
                return _companyRepo;
            }
        }

        ProjectCompanyRepositry _projectCompanyRepo;
        public ProjectCompanyRepositry ProjectCompanyRepo
        {
            get
            {
                if (_projectCompanyRepo == null) _projectCompanyRepo = new ProjectCompanyRepositry(db);
                return _projectCompanyRepo;
            }
        }

        QuestionRepositry _QuestionRepo;
        public QuestionRepositry QuestionRepo
        {
            get
            {
                if (_QuestionRepo == null) _QuestionRepo = new QuestionRepositry(db);
                return _QuestionRepo;
            }
        }

        OptionRepositry _OptionRepo;
        public OptionRepositry OptionRepo
        {
            get
            {
                if (_OptionRepo == null) _OptionRepo = new OptionRepositry(db);
                return _OptionRepo;
            }
        }


        //RoleRepository _roleRepo;
        //public RoleRepository RoleRepo
        //{
        //    get
        //    {
        //        if (_roleRepo == null) _roleRepo = new RoleRepository(db);
        //        return _roleRepo;
        //    }
        //}


        AccountRepositry _userRepo;
        public AccountRepositry UserRepo
        {
            get
            {
                if (_userRepo == null) _userRepo = new AccountRepositry(db);
                return _userRepo;
            }
        }

        UserRepositry _appUserRepo;
        public UserRepositry AppUserRepo
        {
            get
            {
                if (_appUserRepo == null) _appUserRepo = new UserRepositry(db);
                return _appUserRepo;
            }
        }

        WalletRepository _wallerRepo;
        public WalletRepository WalletRepo
        {
            get
            {
                if (_wallerRepo == null) _wallerRepo = new WalletRepository(db);
                return _wallerRepo;
            }
        }


        //Category 
        CategoryRepository _categoryRepo;
        public CategoryRepository CategoryRepo
        {
            get
            {
                if (_categoryRepo == null) _categoryRepo = new CategoryRepository(db);
                return _categoryRepo;
            }
        }
        //PostRepository 
        //PostRepository _postRepo;
        //public PostRepository PostRepo
        //{
        //    get
        //    {
        //        if (_postRepo == null) _postRepo = new PostRepository(db);
        //        return _postRepo;
        //    }
        //}

        //ProposalRepository  
        ProposalRepository _propRepo;
        public ProposalRepository ProposalRepo
        {
            get
            {
                if (_propRepo == null) _propRepo = new ProposalRepository(db);
                return _propRepo;
            }
        }

        //NotificationRepository 
        ChatNotificationRepositry _chatNotificationRepo;
        public ChatNotificationRepositry ChatNotificationRepo
        {
            get
            {
                if (_chatNotificationRepo == null) _chatNotificationRepo = new ChatNotificationRepositry(db);
                return _chatNotificationRepo;
            }
        }

        ProposalNotificationRepositry _proposalNotificationRepo;
        public ProposalNotificationRepositry ProposalNotificationRepo
        {
            get
            {
                if (_proposalNotificationRepo == null) _proposalNotificationRepo = new ProposalNotificationRepositry(db);
                return _proposalNotificationRepo;
            }
        }

        AttPostRepositry _attPostRepo;
        public AttPostRepositry AttPostRepo
        {
            get
            {
                if (_attPostRepo == null) _attPostRepo = new AttPostRepositry(db);
                return _attPostRepo;
            }
        }



        private readonly LebetakContext db;
        public UnitOFWork(LebetakContext db, UserManager<User> userManager, SignInManager<User> signInManager, Cloudinary cloudinary)
        {
            this.db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            Cloudinary = cloudinary;
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
