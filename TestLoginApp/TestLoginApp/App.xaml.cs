using System;
using FreshMvvm;
using TestLoginApp.PageModels;
using TestLoginApp.Repository;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("FontAwesomeLight.ttf", Alias = "FontAwesomeLight")]
[assembly: ExportFont("InterRegular.ttf", Alias = "InterRegular")]
[assembly: ExportFont("InterMedium.ttf", Alias = "InterMedium")]
[assembly: ExportFont("InterSemiBold.ttf", Alias = "InterSemiBold")]

namespace TestLoginApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            FreshIOC.Container.Register<IRestService, RestService>().AsSingleton();

            MainPage = new FreshNavigationContainer(
                FreshPageModelResolver.ResolvePageModel<MainPageModel>());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
