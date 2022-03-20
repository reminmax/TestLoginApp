using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using PropertyChanged;
using TestLoginApp.Helpers;
using TestLoginApp.Helpers.Validations;
using TestLoginApp.Helpers.Validations.Rules;
using TestLoginApp.Repository;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TestLoginApp.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageModel : FreshBasePageModel
    {
        public bool ShowPassword { get; private set; }
        public bool ShowErrorMessage { get; private set; }
        public string ErrorMessageText { get; private set; }
        public bool IsBusy { get; set; }
        public ValidatableObject<string> Email { get; set; }
        public ValidatableObject<string> Password { get; set; }

        private IRestService _restService { get; }

        public MainPageModel(IRestService restService)
        {
            _restService = restService;

            ShowHidePasswordCommand = CommandFactory.Create(ShowHidePassword);
            CredentialsTextChangedCommand = CommandFactory.Create(CredentialsTextChangedCommandHandler);
            LoginAsyncCommand = CommandFactory.Create(LoginAsync);
            ShowPassword = false;

            AddValidationRules();
        }

        public ICommand ShowHidePasswordCommand { get; }
        public ICommand CredentialsTextChangedCommand { get; }
        public IAsyncCommand LoginAsyncCommand { get; }
        
        private void ShowHidePassword() => ShowPassword = !ShowPassword;

        private void AddValidationRules()
        {
            Email = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();

            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Email не может быть пустым" });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Пароль не может быть пустым" });
        }

        private bool AreFieldsValid()
        {
            bool isEmailValid = Email.Validate();
            bool isPasswordValid = Password.Validate();

            return isEmailValid && isPasswordValid;
        }

        private void CredentialsTextChangedCommandHandler() => ShowErrorMessage = false;

        private async Task LoginAsync()
        {
            if (!AreFieldsValid()) return;

            IsBusy = true;

            try
            {
                var result = await _restService.LoginAsync(Email.Value, Password.Value);
                if (result is null)
                    throw new ArgumentNullException(nameof(result));

                if (!string.IsNullOrEmpty(result.Error))
                {
                    ErrorMessageText = ErrorMessageHelper.GetErrorMessageText(result.Error);
                    ShowErrorMessage = true;
                    return;
                }

                ClearCredentials();

                // Save Token into app settings
                AppSettings.Token = result.Token;

                // Navigate to UserProfilePage
                await CoreMethods.PushPageModel<UserProfilePageModel>();
            }
            catch (Exception ex)
            {
                ErrorMessageText = ex.Message;
                ShowErrorMessage = true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ClearCredentials() => Email.Value = Password.Value = String.Empty;
    }
}
