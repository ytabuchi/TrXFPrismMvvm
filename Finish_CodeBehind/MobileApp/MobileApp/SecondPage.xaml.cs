using System;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class SecondPage : ContentPage
    {
        public SecondPage(string message)
        {
            InitializeComponent();

            messaage.Text = message;
        }

        async void OkButtonOnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}