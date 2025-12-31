using AutoMapper;
using Lebetak.DTOs;
using Lebetak.DTOs.Account;
using Lebetak.DTOs.ProjectWorker;
using Lebetak.DTOs.Report;
using Lebetak.DTOs.Worker;
using Lebetak.Models;
using Lebetak.DTOs.Urgency;
using Lebetak.DTOs.Proposal;
using Lebetak.DTOs.Question;
using Lebetak.DTOs.Service;

namespace Lebetak.Profiles
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<User, AccountRegisterDTO>().ReverseMap();


            CreateMap<ClinetRegisterDTO, User>().AfterMap((src,des)=>
            {
                des.F_Name = src.First_Name;
                des.L_Name = src.Last_Name;
                des.UserName = src.User_Name;
                des.Email = src.email;
                des.PhoneNumber = src.Phone_Number;
                des.LocationURL = src.LocationURL;
                des.City = src.City;
                des.Region = src.Region;
                des.Street = src.Street;
                des.ApartmentNumber = src.ApartmentNumber;

            }).ReverseMap();


            CreateMap<WorkerRegisterDTO, User>().AfterMap((src, des) =>
            {
                des.F_Name = src.First_Name;
                des.L_Name = src.Last_Name;
                des.UserName = src.User_Name;
                des.Email = src.email;
                des.PhoneNumber = src.Phone_Number;
                des.LocationURL = src.LocationURL;
                des.City = src.City;
                des.Region = src.Region;
                des.Street = src.Street;
                des.ApartmentNumber = src.ApartmentNumber;
            }).ReverseMap();


            CreateMap<OwnerRegisterDTO, User>().AfterMap((src, des) =>
            {
                des.F_Name = src.First_Name;
                des.L_Name = src.Last_Name;
                des.UserName = src.User_Name;
                des.Email = src.email;
                des.PhoneNumber = src.Phone_Number;
                des.LocationURL = src.LocationURL;
                des.City = src.City;
                des.Region = src.Region;
                des.Street = src.Street;
                des.ApartmentNumber = src.ApartmentNumber;
            }).ReverseMap();


            #region Category

            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, CategoryViewDTO>();

            #endregion

            


            CreateMap<ProjectAddDTO, ProjectWorker>().AfterMap((src, des) => { 
                des.Title=src.TitleOfProject;
                des.Description=src.DescriptionOfProject;
                
            }).ReverseMap();

            CreateMap<ProjectAddDTO, ProjectCompany>().AfterMap((src, des) => {
                des.Title = src.TitleOfProject;
                des.Description = src.DescriptionOfProject;

            }).ReverseMap();

            

            CreateMap<Worker, WorkerCardsDTO>().AfterMap((src, des) =>
            {
                des.FirstName = src.User.F_Name;
                des.LastName = src.User.L_Name;
                des.ProfileImageUrl = src.User.profileImageUrl;
                des.Location = src.User.City + ", " + src.User.Region + ", " + src.User.Street + ", " +src.User.ApartmentNumber;
                des.CategoryName = src.Category.Name;
                des.Skills = src.WorkerSkills.Select(s => s.Skill.Name).ToList();
                
            }).ReverseMap();
            CreateMap<Worker, WorkerProfileDTO>().AfterMap((src, des) =>
            {
                des.First_Name = src.User?.F_Name;
                des.Last_Name = src.User?.L_Name;
                des.Profile_Image = src.User?.profileImageUrl;
                des.City = src.User?.City;
                des.Region = src.User?.Region;
                des.Street = src.User?.Street;
                des.ApartmentNumber = src.User?.ApartmentNumber;

                des.Category_Title = src.Category?.Name;
                des.Description = src.Description;
                des.Experience_Years = src.ExperienceYears;
                des.HourlyPrice = src.HourlyPrice;
                des.Rate = src.Rate;
                des.NumberOfRates = src.NumberOfRates;

                des.Skills = src.WorkerSkills != null
                    ? src.WorkerSkills.Select(s => s.Skill.Name).ToList()
                    : new List<string>();
            }).ForMember(d => d.Projects,
        o => o.MapFrom(s => s.Projects)).ReverseMap();

            CreateMap<Worker, MyProfileWorker>().AfterMap((src, des) =>
            {
                des.Email = src.User?.Email;
                des.UserName = src.User?.UserName;
                des.PhoneNumber = src.User?.PhoneNumber;
                des.First_Name = src.User?.F_Name;
                des.Last_Name = src.User?.L_Name;
                des.Profile_Image = src.User?.profileImageUrl;
                des.City = src.User?.City;
                des.Region = src.User?.Region;
                des.Street = src.User?.Street;
                des.ApartmentNumber = src.User?.ApartmentNumber;
                des.FSSN = src.User?.SSN_FrontURL;
                des.BSSN = src.User?.SSN_BackURL;
                des.WallertBalance = src.User?.Wallet != null ? src.User.Wallet.Balance : 0;
                
                des.Description = src.Description;
                des.Experience_Years = src.ExperienceYears;
                des.Rate = src.Rate;
                des.NumberOfRates = src.NumberOfRates;
                des.HourlyPrice = src.HourlyPrice;
                des.Category_Title = src.Category?.Name;
                des.Skills=src.WorkerSkills.Select(s=>s.Skill.Name).ToList();

                des.Skills = src.WorkerSkills != null
                    ? src.WorkerSkills.Select(s => s.Skill.Name).ToList()
                    : new List<string>();
            }).ForMember(d => d.Projects,
        o => o.MapFrom(s => s.Projects)).ReverseMap();



            CreateMap<ProjectWorker, ProjectCardDTO>().ForMember(d => d.Image,o => o.MapFrom(s => s.Images.Select(i => i.URL).FirstOrDefault()));

            CreateMap<ProjectWorker, ProjectProfileDTO>().ForMember(d=> d.Images,o=>o.MapFrom(s=>s.Images.Select(i=>i.URL).ToList()));

            CreateMap<Worker, WorkerMapDTO>().AfterMap((src, des) =>
            {
                des.Id=src.UserId;
                des.Name=src.User.F_Name;
                des.latitude=src.User.latitude;
                des.longitude=src.User.longitude;
            });

            CreateMap<Company, CompanyProfileDTO>().AfterMap((src, des) =>
            {
                des.Id = src.Id;
                des.Name = src.Name;
                des.Logo = src.Logo;
                des.Panner = src.Panner;
                des.Description = src.Description;
                des.IsVerified = src.IsVerified;
                des.CategoryId = src.CategoryId;
                des.CategoryName = src.Category.Name;
            }).ForMember(d => d.Projects,
        o => o.MapFrom(s => s.Projects)).ReverseMap();

            CreateMap<ProjectCompany, ProjectCardDTO>()
                .ForMember(d=> d.Image,o=>o.MapFrom(s=>s.Images.Select(i=>i.URL).FirstOrDefault()));
            CreateMap<ProjectCompany, ProjectProfileDTO>().ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(i => i.URL).ToList()));


            CreateMap<Report, AddReportDTO>().ReverseMap();

            
            CreateMap<PostAddDTO, Post>().AfterMap((src, des) =>{}).ReverseMap();
            
            CreateMap<Post, PostCardDTO>().AfterMap((src, des) =>
            {
                des.Id = src.Id;
                des.ClientName = src.Client.User.F_Name + " " + src.Client.User.L_Name;
                des.ClientPicUrl = src.Client.User.profileImageUrl;
                des.Title = src.Title;
                des.Description = src.Description;
                des.Location = src.Location;
                des.CreatedDate = src.CreatedDate;
                des.BudgetFrom = src.BudgetFrom;
                des.BudgetTo = src.BudgetTo;
                des.UrgencyLevel = src.Urgency.Title;
                des.CategoryName = src.Category.Name;
                des.PostStatus = src.Status.ToString();
            });
            CreateMap<Post, PostViewDTO>().AfterMap((src, des) =>
            {
                des.Id = src.Id;
                des.ClientId = src.ClientId;
                des.ClientName = src.Client.User.F_Name + " " + src.Client.User.L_Name;
                des.ClientPicUrl = src.Client.User.profileImageUrl;
                des.Title = src.Title;
                des.Description = src.Description;
                des.Location = src.Location;
                des.CreatedDate = src.CreatedDate;
                des.Latitude = src.Latitude;
                des.Longitude = src.Longitude;
                des.BudgetFrom = src.BudgetFrom;
                des.BudgetTo = src.BudgetTo;
                des.UrgencyLevel = src.Urgency.Title;
                des.CategoryName = src.Category.Name;
                des.PostStatus = src.Status.ToString();
            }).ForMember(d => d.Images, o => o.MapFrom(s => s.AttPosts.Select(i => i.URL).ToList()));
            CreateMap<ProposalAddDTO, Proposal>().AfterMap((src, des) =>{}).ReverseMap();
            CreateMap<Proposal, ProposalViewDTO>().AfterMap((src, des) =>
            {
                des.Id = src.Id;
                des.WorkerId = src.WorkerId;
                des.WorkerName = src.Worker.User.F_Name + " " + src.Worker.User.L_Name;
                des.WorkerPicUrl = src.Worker.User.profileImageUrl;
                des.Description = src.Description;
                des.Price = src.Price;
                des.StatusName = src.Status.ToString();
                des.Created_At = src.Created_At;
                des.PostId = src.PostId;
            }).ForMember(d => d.Images, o => o.MapFrom(s => s.Images.Select(i => i.URL).ToList()));

            CreateMap<Urgency, UrgencyViewDTO>().AfterMap((src, des) =>
            {
                des.Id = src.Id;
                des.Title = src.Title;
            }).ReverseMap();

            CreateMap<Company,CompanyCardDTO>().AfterMap((src,des)=>
            {
                des.Id = src.Id;
                des.IsVerified = src.IsVerified;
                des.Description = src.Description;
                des.Logo = src.Logo;
                des.Name = src.Name;
            }).ReverseMap();

            CreateMap<Question,QuestionViewDTO>().AfterMap((src,des)=>
            {
                des.Id = src.Id;
                des.Text=src.Text;

            }).ReverseMap();
            CreateMap<Service, ServiceCardDTO>().AfterMap((src, des) =>
            {
                des.Id = src.Id;
                des.ClientId = src.ClientId;
                des.ClientName = src.Client.User.F_Name + " " + src.Client.User.L_Name;
                des.ClientPic = src.Client.User.profileImageUrl;
                des.CreatedDate = src.CreatedDate;
                des.PreferredTime = src.PreferredTime;
                des.Description = src.Description;
                des.CompanyId = src.Company.Id;
                des.CompanyName = src.Company.Name;
                des.CompanyPic = src.Company.Logo;
            }).ReverseMap();
            CreateMap<Service, ServiceProfileDTO>().AfterMap((src, des) =>
            {
                des.Id = src.Id;
                des.ClientId = src.ClientId;
                des.ClientName = src.Client.User.F_Name + " " + src.Client.User.L_Name;
                des.ClientPic = src.Client.User.profileImageUrl;
                des.CreatedDate = src.CreatedDate;
                des.PreferredTime = src.PreferredTime;
                des.Description = src.Description;
                des.CompanyId = src.Company.Id;
                des.CompanyName = src.Company.Name;
                des.CompanyPic = src.Company.Logo;
                des.answers = src.ServiceAnswers.Select(qa => new QuestionAndAnswerDTO
                {
                    Question = qa.Question.Text,
                    Answer = qa.Option.Text,
                }).ToList();
            }).ForMember(d => d.Images, o => o.MapFrom(s => s.AttService.Select(i => i.URL).ToList())).ReverseMap();
        }
    }
}
