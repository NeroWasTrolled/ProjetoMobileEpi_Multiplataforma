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
    public partial class PagePrincipal : MasterDetailPage
    {
        public PagePrincipal()
        {
            InitializeComponent();
            btHome_Clicked(new Object(), new EventArgs());
        }

        private void btHome_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageHome());
            IsPresented = false;
        }

        private void btCadastrar_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageCadastrar());
            IsPresented = false;
        }

        private void btLocalizar_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageListar());
            IsPresented = false;
        }

        private void btSobre_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageSobre());
            IsPresented = false;
        }

        private void btEpi_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageEpi());
            IsPresented = false;
        }

        private void btEntrega_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageEntrega());
            IsPresented = false;
        }
    }
}