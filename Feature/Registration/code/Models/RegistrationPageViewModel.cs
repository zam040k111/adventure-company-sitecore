using System;
using Adventure.Foundation.Assets.Services.Interfaces;
using Feature.Registration;

namespace Adventure.Feature.Registration.Models
{
    public class RegistrationPageViewModel
    {
        public IRegistrationPage RegistrationPage { get; set; }

        public Guid DataSourceId { get; set; }

        public IRenderAssetsService AssetsService { get; set; }
    }
}
