using FreshMvvm;
using PropertyChanged;
using System;
using System.Threading.Tasks;
using TestLoginApp.Helpers;
using TestLoginApp.Repository;
using Xamarin.CommunityToolkit.ObjectModel;

namespace TestLoginApp.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class UserProfilePageModel : FreshBasePageModel
    {
        public string MessageText { get; set; }
        private IRestService _restService { get; }

        public UserProfilePageModel(IRestService restService)
        {
            _restService = restService;

            LogoutAsyncCommand = CommandFactory.Create(LogoutAsync);
        }

        public IAsyncCommand LogoutAsyncCommand { get; }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            await GetUserProfileAsync();
        }

        private async Task LogoutAsync()
        {
            try
            {
                var result = await _restService.LogoutAsync(AppSettings.Token);

                if (!result)
                {
                    MessageText = ErrorMessageHelper.GetErrorMessageText(ConstantValues.TockenNotFound);
                    return;
                }

                AppSettings.Token = string.Empty;

                // Navigate back
                await CoreMethods.PopPageModel();
            }
            catch (Exception ex)
            {
                MessageText = ex.Message;
            }
        }
    
        private async Task GetUserProfileAsync()
        {
            try
            {
                var result = await _restService.GetUserProfileAsync(AppSettings.Token);
                if (result is null)
                    throw new ArgumentNullException(nameof(result));

                if (!string.IsNullOrEmpty(result.Error))
                {
                    MessageText = ErrorMessageHelper.GetErrorMessageText(result.Error);
                    return;
                }

                MessageText = string.Join(",", ConstantValues.HelloText, result.Name);
            }
            catch (Exception ex)
            {
                MessageText = ex.Message;
            }
        }
    }
}
