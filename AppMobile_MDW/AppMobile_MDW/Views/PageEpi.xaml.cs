using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppMobile_MDW.Models;
using AppMobile_MDW.Services;

namespace AppMobile_MDW.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageEpi : ContentPage
	{
		public PageEpi()
		{
			InitializeComponent ();
        }

        public PageEpi(ModelEpi epi)
        {
            InitializeComponent();
            btSalvar.Text = "Alterar";
            txtCodigo.IsVisible = true;
            btExcluir.IsVisible = true;
            txtCodigo.Text = epi.Id.ToString();
            txtEpi.Text = epi.Epi;
            datePickerVencimento.Date = epi.data_vencimento;
        }

        private void btSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                ModelEpi epi = new ModelEpi();
                epi.Epi = txtEpi.Text;

                epi.data_vencimento = DateTime.Now.AddDays(90);

                ServiceDBEpi dBEpi = new ServiceDBEpi(App.DbPath);
                if (btSalvar.Text == "Inserir")
                {
                    dBEpi.Inserir(epi);
                    DisplayAlert("Resultado", dBEpi.StatusMessage, "OK");
                }
                else
                {
                    epi.Id = Convert.ToInt32(txtCodigo.Text);
                    dBEpi.Alterar(epi);
                    DisplayAlert("Epi alterada com sucesso!", dBEpi.StatusMessage, "OK");
                }
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void btExcluir_Clicked(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("Excluir Epi", "Deseja EXCLUIR esta Epi selecionada?", "Sim", "Não");
            if (resp == true)
            {
                ServiceDBEpi dBEpi = new ServiceDBEpi(App.DbPath);
                int id = Convert.ToInt32(txtCodigo.Text);
                dBEpi.Excluir(id);
                DisplayAlert("Matricula excluída com sucesso", dBEpi.StatusMessage, "OK");
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
        }

        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageHome());
        }

        private void DatePickerVencimento_DateSelected(object sender, DateChangedEventArgs e)
        {
            AtualizarContagemRegressiva();
        }

        private void AtualizarContagemRegressiva()
        {
            DateTime dataVencimento = datePickerVencimento.Date;

            DateTime dataEntrega = DateTime.Today;

            TimeSpan diff = dataVencimento - dataEntrega;
            int diasRestantes = (int)Math.Ceiling(diff.TotalDays);

            if (diasRestantes >= 0)
            {
                labelCountdown.Text = $"{diasRestantes} dias restantes para o vencimento.";
            }
            else
            {
                labelCountdown.Text = "Data de vencimento anterior à data de entrega.";
            }
        }

    }
}