using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile_MDW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHome : ContentPage
    {
        public PageHome()
        {
            InitializeComponent();
        }
        private void tapInserir_Tapped(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)App.Current.MainPage;
            p.Detail = new NavigationPage(new PageCadastrar());
            p.IsPresented = false;
        }

        private void tapLocalizar_Tapped(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)App.Current.MainPage;
            p.Detail = new NavigationPage(new PageListar());
            p.IsPresented = false;
        }
    }
}