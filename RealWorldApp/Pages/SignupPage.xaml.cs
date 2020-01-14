using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {

            var response=await APIService.RegisterUser(EntName.Text, EntEmail.Text, EntPassword.Text);
            if(response)
            {
                await DisplayAlert("Hi", "Your Account has been Created", "AlRight");
            }
            else
            {
                await DisplayAlert("Hi", "Some thing went Wrong", "Cancel");
            }
        }
    }
}