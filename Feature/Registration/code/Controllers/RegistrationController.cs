using System;
using Adventure.Feature.Registration.Models;
using Adventure.Feature.Registration.Services.Interfaces;
using Adventure.Feature.Registration.Validators;
using log4net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Adventure.Foundation.Assets.Services.Interfaces;
using Adventure.Foundation.Common.Controllers;
using Adventure.Foundation.Common.ValidationServices;
using Feature.Registration;
using Foundation.EmailProvider;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;

namespace Adventure.Feature.Registration.Controllers
{
    public class RegistrationController : BaseController
    {
        private readonly ILog _logger;
        private readonly IRegistrationService _registrationService;
        private readonly IMvcContext _mvcContext;
        private readonly ISitecoreService _sitecoreService;
        private readonly IEmailService _emailService;
        private readonly IRenderAssetsService _assetsService;

        public RegistrationController(
            ILog logger,
            IRegistrationService registrationService,
            IMvcContext mvcContext,
            ISitecoreService sitecoreService,
            IEmailService emailService,
            IRenderAssetsService renderAssetsService)
        {
            Throw.IfNull(logger, nameof(logger));
            Throw.IfNull(registrationService, nameof(registrationService));
            Throw.IfNull(mvcContext, nameof(mvcContext));
            Throw.IfNull(sitecoreService, nameof(sitecoreService));
            Throw.IfNull(emailService, nameof(emailService));
            Throw.IfNull(renderAssetsService, nameof(renderAssetsService));

            _logger = logger;
            _registrationService = registrationService;
            _mvcContext = mvcContext;
            _sitecoreService = sitecoreService;
            _emailService = emailService;
            _assetsService = renderAssetsService;
        }

        [HttpGet]
        public ActionResult RegisterForm()
        {
            _logger.Debug($"{nameof(RegisterForm)} has been called.");

            var model = new RegistrationPageViewModel
            {
                RegistrationPage = _mvcContext.GetContextItem<IRegistrationPage>(),
                AssetsService = _assetsService,
                DataSourceId = _mvcContext.GetDataSourceItem<IEmail>()?.Id ?? Guid.Empty
            };

            return ExecuteSafe(() => View(model));
        }

        [HttpPost]
        public async Task<JsonResult> RegisterUser(
            NewUserViewModel newUserViewModel,
            NewUserValidator newUserValidator,
            Guid dataSourceId)
        {
            Throw.IfNull(newUserViewModel, nameof(newUserViewModel));
            Throw.IfNull(newUserValidator, nameof(newUserValidator));

            _logger.Debug($"{nameof(RegisterUser)} has been called with model" +
                          $" {newUserViewModel.UserName} name, {newUserViewModel.Email} email.");

            newUserValidator.SetUserNameUniqueValidation(_registrationService.IsUserNameUnique);
            newUserValidator.SetEmailUniqueValidation(_registrationService.IsEmailUnique);

            var modelState = await newUserValidator.ValidateAsync(newUserViewModel);
            var validation = new List<string>();
            var success = false;
            var emailSent = false;

            if (modelState.IsValid)
            {
                _logger.Debug($"{nameof(RegisterUser)} has been called with valid model.");

                if (_registrationService.CreateUser(
                    newUserViewModel.UserName,
                    newUserViewModel.Email,
                    newUserViewModel.Password))
                {
                    success = true;

                    var emailDataSource = _sitecoreService.GetItem<IWelcomingEmail>(dataSourceId);

                    if (!(emailDataSource is null))
                    {
                        _emailService.SetDataSource(emailDataSource);
                    }

                    emailSent = await _emailService.SendEmail(newUserViewModel.Email);
                }
            }
            else
            {
                _logger.Warn($"{nameof(RegisterUser)} has been called with invalid model.");

                foreach (var error in modelState.Errors)
                {
                    validation.Add(error.ErrorMessage);
                }
            }

            return Json(new { validation, success, emailSent });
        }
    }
}
