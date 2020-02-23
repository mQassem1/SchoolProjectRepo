using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using SchoolProject.Models;
using SchoolProject.Models.Helpers;
using SchoolProject.ViewModels;

namespace SchoolProject.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplictionUser> userManger;
        private readonly SignInManager<ApplictionUser> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly GoogleReCAPTCHAService googleReCAPTCHAService;
        private readonly IEmailSender emailSender;
        private readonly ISendSMS sendSMS;

        public AccountController(UserManager<ApplictionUser> userManger,
                                 SignInManager<ApplictionUser> signInManager,
                                 ILogger<AccountController> logger,
                                 GoogleReCAPTCHAService googleReCAPTCHAService,
                                 IEmailSender emailSender,
                                 ISendSMS sendSMS)
                                
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
            this.logger = logger;
            this.googleReCAPTCHAService = googleReCAPTCHAService;
            this.emailSender = emailSender;
            this.sendSMS = sendSMS;
        }
        
        [HttpGet]
        public IActionResult login(string ReturnUrl = null) 
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(LoginViewModel model,string ReturnUrl = null)
        {
           var returnUrl = ReturnUrl ?? Url.Content("~/");


            //Token verification
            var ReCAPTCHA = googleReCAPTCHAService.ResponseVerification(model.Token);

            if (ModelState.IsValid)
            {
                if (!ReCAPTCHA.Result.success && ReCAPTCHA.Result.score <= 0.5)
                {
                    ModelState.AddModelError(string.Empty, "You are Not Human");
                    return View(model);
                }
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RemeberMe, false);
                if (result.Succeeded)
                {
                    logger.LogInformation("Success Login", DateTime.UtcNow);
                    await emailSender.SendEmailAsync(model.Email, "Login", "Login Success");
                    sendSMS.SendSMSMessage("00966533112780","Hello") ;
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login Fail try Agin");
                    logger.LogInformation("Fail to Login", DateTime.UtcNow);
                    return View(model);
                }
            }
              return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            logger.LogInformation($"User logged out..{DateTime.UtcNow}");
            return RedirectToAction("Index", "home");
        }
    }
}