using AutoMapper;
using Lebetak.Common;
using Lebetak.DTOs;
using Lebetak.DTOs.Account;
using Lebetak.Models;
using Lebetak.Services;
using Lebetak.UnitOfWork;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Lebetak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UnitOFWork _unitOFWork;
        EmailService emailService = new EmailService();
        private readonly IMapper imapper;

        public AccountController(UnitOFWork unitOFWork, IMapper _imappe)
        {
            _unitOFWork = unitOFWork;
            imapper = _imappe;
        }


        // Login
        #region Login

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AccountLoginDTO user)
        {
                var ExitstingUser = await _unitOFWork._userManager.FindByNameAsync(user.Text);
                if (ExitstingUser == null)
                {
                    ExitstingUser = await _unitOFWork._userManager.FindByEmailAsync(user.Text);
                }

                if (ExitstingUser == null)
                {
                    return NotFound("Email not Found");
                }

                var SignInResult = await _unitOFWork._signInManager.PasswordSignInAsync(ExitstingUser, user.Password, user.RememberMe, lockoutOnFailure: true);

                if (SignInResult.Succeeded)
                {
                    var roles = await _unitOFWork._userManager.GetRolesAsync(ExitstingUser);
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, ExitstingUser.Id),
                        new Claim(ClaimTypes.Email, ExitstingUser.Email),
                        new Claim(ClaimTypes.Name, $"{ExitstingUser.F_Name} {ExitstingUser.L_Name}"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    #region  secert key + signingCredentials
                    var key = "THIS_IS_MY_SUPER_SECRET_KEY_12345";
                    var secertkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    var signingcer = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);
                    #endregion
                    var tokenobject = new JwtSecurityToken(
                   claims: claims,
                   expires: DateTime.Now.AddDays(1),
                   signingCredentials: signingcer
                   );
                    var Token = new JwtSecurityTokenHandler().WriteToken(tokenobject);
                    if (roles[0]=="Owner")
                    return new JsonResult(new {id=ExitstingUser.Id,token=Token,email=ExitstingUser.Email,userName=ExitstingUser.UserName,image=ExitstingUser.Owner.Company.Logo,role=roles[0] });
                return new JsonResult(new {id=ExitstingUser.Id,token=Token,email=ExitstingUser.Email,userName=ExitstingUser.UserName,image=ExitstingUser.profileImageUrl,role=roles[0] });
                }
                else if (SignInResult.IsLockedOut || SignInResult.IsNotAllowed)
                {
                    return NotFound("Your Account is Under Reviw Will Connet with you Later Stay tunned");
                }
                else
                {
                    
                    return NotFound("Invalid User Name Or Password");
                }
        }
        #endregion

        #region Google login
        [HttpGet("google-login")]
        public IActionResult GoogleLogin() => Challenge(new AuthenticationProperties { RedirectUri = "/api/account/google-response" }, "Google");

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync();
            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var user = await _unitOFWork._userManager.FindByEmailAsync(email);

            if (user == null)
            {
                //ned to add new register here
            }

            return Ok("JWT token");
        }

        #endregion

        // Register Accounts
        #region Client Register End point

        [HttpPost("client-register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ClientRegister([FromForm] ClinetRegisterDTO account)
        {
            if (account.password != account.Confirm_Password)
            {
                return BadRequest("Password isn't match");
            }

            //Email + Password validation & hashing
            if (!Regex.IsMatch(account.email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return BadRequest("Invalid email format");

            if (account.password.Length < 6)
                return BadRequest("Password must be at least 6 characters");


            // Check if email already exists
            var userFromDb = await _unitOFWork._userManager.FindByEmailAsync(account.email);
            if (userFromDb != null)
            {
                return BadRequest(Response<string>.Failure("User already exists"));
            }
            
            var user = imapper.Map<User>(account);
            
            string SSN_F = "";
            string SSN_B = "";
            string profile = "";

            if (account.SSN_FrontImageURL != null)
            {
                var res = await FileUpload.UploadAsync(account.SSN_FrontImageURL, _unitOFWork.Cloudinary,"SSN");
                SSN_F = res.Url.ToString();
                user.SSN_FrontURL = SSN_F;

            }
            if (account.SSN_BackImageURL != null)
            {
                 var res = await FileUpload.UploadAsync(account.SSN_BackImageURL, _unitOFWork.Cloudinary, "SSN");
                SSN_B = res.Url.ToString();
                user.SSN_BackURL = SSN_B;
            }
            if (account.profileImage != null)
            {
                 var res = await FileUpload.UploadAsync(account.profileImage, _unitOFWork.Cloudinary,"ProfileImages");
                profile = res.Url.ToString();
                user.profileImageUrl = profile;
            }

            var result = await _unitOFWork._userManager.CreateAsync(user, account.password);

            if (result.Succeeded)
            {
                await _unitOFWork._userManager.AddToRoleAsync(user, account.Role);

                var wallet = new Wallet
                {
                    Balance = 0,
                    UserId = user.Id
                };
                user.Wallet = wallet;
                _unitOFWork.WalletRepo.Add(wallet);

                var client = new Client
                {
                    UserId = user.Id,
                };
                _unitOFWork.ClientRepo.Add(client);
                _unitOFWork.Save();


                // Email verification
                var emailToken = await _unitOFWork._userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = System.Web.HttpUtility.UrlEncode(emailToken);
                var confirmationLink = $"{Request.Scheme}://{Request.Host}/api/account/confirm-email?userId={user.Id}&token={encodedToken}";
                emailService.SendMessage(user.Email, "Confirm your email", $"Click here to confirm your email: {confirmationLink}");

                return Ok(Response<string>.Success("User created successfully"));


            }

            return BadRequest(Response<string>.Failure("Error creating user"));
        }

        #endregion

        #region Worker Register End point

        [HttpPost("worker-register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> WorkerRegister([FromForm] WorkerRegisterDTO account)
        {
            if (account.password != account.Confirm_Password)
            {
                return BadRequest("Password isn't match");
            }
            // Check if email already exists
            var userFromDb = await _unitOFWork._userManager.FindByEmailAsync(account.email);
            if (userFromDb != null)
            {
                return BadRequest(Response<string>.Failure("Worker already exists"));
            }

            var user = imapper.Map<User>(account);
            string SSN_F = "";
            string SSN_B = "";
            string profile = "";

            if (account.SSN_FrontImageURL != null)
            {
                var res = await FileUpload.UploadAsync(account.SSN_FrontImageURL, _unitOFWork.Cloudinary, "SSN");
                SSN_F = res.Url.ToString();
                user.SSN_FrontURL = SSN_F;

            }
            if (account.SSN_BackImageURL != null)
            {
                var res = await FileUpload.UploadAsync(account.SSN_BackImageURL, _unitOFWork.Cloudinary, "SSN");
                SSN_B = res.Url.ToString();
                user.SSN_BackURL = SSN_B;
            }
            if (account.profileImage != null)
            {
                var res = await FileUpload.UploadAsync(account.profileImage, _unitOFWork.Cloudinary, "ProfileImages");
                profile = res.Url.ToString();
                user.profileImageUrl = profile;
            }


            var result = await _unitOFWork._userManager.CreateAsync(user, account.password);

            if (result.Succeeded)
            {
                await _unitOFWork._userManager.AddToRoleAsync(user, account.Role);

                var wallet = new Wallet
                {
                    Balance = 0,
                    UserId = user.Id
                };
                user.Wallet = wallet;
                _unitOFWork.WalletRepo.Add(wallet);


                

                var worker = new Worker
                {
                    UserId = user.Id,
                    Description = account.Description,
                    CategoryId = account.CategoryId,
                    ExperienceYears = account.ExperienceYears,
                    HourlyPrice = account.HourlyPrice,
                };
                _unitOFWork.WorkerRepo.Add(worker);
                _unitOFWork.Save();

                if (account.WorkerSkills != null && account.WorkerSkills.Any())
                {
                    foreach (var skillName in account.WorkerSkills)
                    {
                        // Normalize skill name
                        var normalizedName = skillName.Trim().ToLower();

                        // Check if skill exists
                        var skill = _unitOFWork.SkillRepo.FirstOrDefualt(normalizedName).FirstOrDefault();

                        // Create skill if not exists
                        if (skill == null)
                        {
                            skill = new Skill
                            {
                                Name = skillName.Trim()
                            };

                            _unitOFWork.SkillRepo.Add(skill);
                            _unitOFWork.Save();
                        }


                        var workerSkill = new WorkerSkills
                        {
                            WorkerId = worker.UserId,
                            SkillId = skill.Id
                        };

                        _unitOFWork.WorkerSkillRepo.Add(workerSkill);
                    }

                    _unitOFWork.Save();
                }

                // Email verification
                var emailToken = await _unitOFWork._userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = System.Web.HttpUtility.UrlEncode(emailToken);
                var confirmationLink = $"{Request.Scheme}://{Request.Host}/api/account/confirm-email?userId={user.Id}&token={encodedToken}";
                emailService.SendMessage(user.Email, "Confirm your email", $"Click here to confirm your email: {confirmationLink}");

                return Ok(Response<string>.Success("Worker created successfully"));
            }

            return BadRequest(Response<string>.Failure("Error creating user"));
        }

        #endregion

        #region Owner Register End point

        [HttpPost("owner-register")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> OwnerRegister([FromForm] OwnerRegisterDTO account)
        {
            if (account.password != account.Confirm_Password)
            {
                return BadRequest("Password isn't match");
            }
            // Check if email already exists
            var userFromDb = await _unitOFWork._userManager.FindByEmailAsync(account.email);
            if (userFromDb != null)
            {
                return BadRequest(Response<string>.Failure("User already exists"));
            }

            var user = imapper.Map<User>(account);
            string SSN_F = "";
            string SSN_B = "";
            string profile = "";
            string CompanyLogo = "";
            string CompanyPanner = "";

            if (account.SSN_FrontImageURL != null)
            {
                var res = await FileUpload.UploadAsync(account.SSN_FrontImageURL, _unitOFWork.Cloudinary, "SSN");
                SSN_F = res.Url.ToString();
                user.SSN_FrontURL = SSN_F;

            }
            if (account.SSN_BackImageURL != null)
            {
                var res = await FileUpload.UploadAsync(account.SSN_BackImageURL, _unitOFWork.Cloudinary, "SSN");
                SSN_B = res.Url.ToString();
                user.SSN_BackURL = SSN_B;
            }
            if (account.profileImage != null)
            {
                var res = await FileUpload.UploadAsync(account.profileImage, _unitOFWork.Cloudinary, "ProfileImages");
                profile = res.Url.ToString();
                user.profileImageUrl = profile;
            }
            if (account.LogoImageUrl != null)
            {
                var res = await FileUpload.UploadAsync(account.LogoImageUrl, _unitOFWork.Cloudinary, "other");
                CompanyLogo = res.Url.ToString();
                
            }
            if (account.PannerUrl != null)
            {
                var res = await FileUpload.UploadAsync(account.PannerUrl, _unitOFWork.Cloudinary, "other");
                CompanyPanner = res.Url.ToString();
                
            }


            var result = await _unitOFWork._userManager.CreateAsync(user, account.password);

            if (result.Succeeded)
            {
                await _unitOFWork._userManager.AddToRoleAsync(user, account.Role);

                var wallet = new Wallet
                {
                    Balance = 0,
                    UserId = user.Id
                };
                user.Wallet = wallet;
                _unitOFWork.WalletRepo.Add(wallet);

                // Note : relation sith categores 
                var owner = new Owner
                {
                    UserID = user.Id,
                    Company = new Company
                    {
                        CategoryId = account.CategoryId,
                        Name = account.CompaneyName,
                        Description = account.Descreption,
                        Logo = CompanyLogo,
                        Panner = CompanyPanner,
                    }
                };
                _unitOFWork.OwnerRepo.Add(owner);
                _unitOFWork.Save();

                Console.WriteLine(owner);

                // Email verification
                var emailToken = await _unitOFWork._userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = System.Web.HttpUtility.UrlEncode(emailToken);
                var confirmationLink = $"{Request.Scheme}://{Request.Host}/api/account/confirm-email?userId={user.Id}&token={encodedToken}";
                emailService.SendMessage(user.Email, "Confirm your email", $"Click here to confirm your email: {confirmationLink}");


                return Ok(Response<string>.Success("Owner created successfully"));
            }

            return BadRequest(Response<string>.Failure("Error creating user"));
        }

        #endregion


        [HttpPut("update-user-info/{userId}")]
        public async Task<IActionResult> UpdateUserInfo(string userId, [FromForm] UpdateUserInfoDTO dto)
        {
            var user = await _unitOFWork._userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            // update only non-null
            if (dto.F_Name != null) user.F_Name = dto.F_Name;
            if (dto.L_Name != null) user.L_Name = dto.L_Name;
            if (dto.PhoneNumber != null) user.PhoneNumber = dto.PhoneNumber;

            // upload profile image
            if (dto.ProfileImage != null)
            {
                if(user.profileImageUrl!=null)
                await FileUpload.DeleteImageAsync(user.profileImageUrl, _unitOFWork.Cloudinary, "ProfileImage");
                var res = await FileUpload.UploadAsync(dto.ProfileImage, _unitOFWork.Cloudinary,"ProfileImage");
                user.profileImageUrl = res.Url.ToString();
            }
            await _unitOFWork._userManager.UpdateAsync(user);

            return Ok(user);
        }

        [HttpPut("update-location/{userId}")]
        public async Task<IActionResult> UpdateLocation(string userId, [FromBody] UpdateLocationDTO dto)
        {
            var user = await _unitOFWork._userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (dto.City != null) user.City = dto.City;
            if (dto.Region != null) user.Region = dto.Region;
            if (dto.Street != null) user.Street = dto.Street;
            if (dto.ApartmentNumber != null) user.ApartmentNumber = dto.ApartmentNumber;

            await _unitOFWork._userManager.UpdateAsync(user);
            return Ok(user);
        }

        [HttpPut("update-ssn/{userId}")]
        public async Task<IActionResult> UpdateSSN(string userId, [FromForm] UpdateSSNDTO dto)
        {
            var user = await _unitOFWork._userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (dto.SSN_Front != null)
            {
                var res = await FileUpload.UploadAsync(dto.SSN_Front, _unitOFWork.Cloudinary,"SSN");
                user.SSN_FrontURL = res.Url.ToString();
            }

            if (dto.SSN_Back != null)
            {
                    var res = await FileUpload.UploadAsync(dto.SSN_Back, _unitOFWork.Cloudinary, "SSN");
                    user.SSN_BackURL = res.Url.ToString();
            }

            await _unitOFWork._userManager.UpdateAsync(user);

            return Ok(user);
        }




        #region UpdatePassword
        [Authorize]
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(AccountChangePasswordDTO model)
        {

            var user = await _unitOFWork._userManager.GetUserAsync(User);

            if (user == null)
                return NotFound(new { message = "User not found." });


            var result = await _unitOFWork._userManager.ChangePasswordAsync(
                user,
                model.OldPassword,
                model.NewPassword
            );


            if (result.Succeeded)
            {
                return Ok(Response<string>.Failure("Password was changed successfully."));
            }


            return BadRequest(new
            {
                message = "Password update failed.",
                errors = result.Errors.Select(e => e.Description)
            });
        }
        #endregion

        #region ForgetPassword
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromForm] AccountForgetPasswordDTO dto)
        {
            var user = await _unitOFWork._userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Ok(new { message = "This Email Not Found" });


            var token = await _unitOFWork._userManager.GeneratePasswordResetTokenAsync(user);


            emailService.SendMessage(dto.Email, "Reset Your Password", $"Your verification code: {token}");

            return Ok(new
            {
                message = "Reset code sent to email."
            });
        }

        [HttpPost("VerifyReset")]
        public async Task<IActionResult> VerifyForgetPassword([FromForm] AccountVerfiyCodeDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data." });

            var user = await _unitOFWork._userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return BadRequest(new { message = "User not found." });

            var result = await _unitOFWork._userManager.ResetPasswordAsync(user, dto.Code, dto.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    message = "Password changed successfully."
                });
            }

            // Return identity errors
            return BadRequest(new
            {
                message = "Password reset failed.",
                errors = result.Errors.Select(e => e.Description)
            });
        }

        #endregion



        #region Abdo Forget Password
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDTO model)
        {

            var user = await _unitOFWork._userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Ok(Response<string>.Success("If the email exists, a reset link will be sent."));


            var token = await _unitOFWork._userManager.GeneratePasswordResetTokenAsync(user);


            var encodedToken = System.Web.HttpUtility.UrlEncode(token);
            var resetLink = $"{Request.Scheme}://{Request.Host}/api/account/reset-password?email={model.Email}&token={encodedToken}";
            emailService.SendMessage(model.Email, "Reset Password", $"Click here to reset your password: {resetLink}");

            return Ok( Response<string>.Success("Reset link sent to your email"));
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var user = await _unitOFWork._userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return BadRequest("User not found");

            if (!user.EmailConfirmed)
                return BadRequest("You must confirm your email before resetting password");



            var decodedToken = System.Web.HttpUtility.UrlDecode(model.Token);

            var result = await _unitOFWork._userManager.ResetPasswordAsync(
                user,
                decodedToken,
                model.NewPassword
            );

            if (result.Succeeded)
                return Ok(Response<string>.Success("Password reset successfully"));

            return BadRequest(result.Errors);
        }


        #endregion

        #region Logout
        [Authorize]
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _unitOFWork._signInManager.SignOutAsync();
            return Ok("Logged out");
        }
        #endregion
    }
}
