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
    public partial class PageCadastrar : ContentPage
    {
        public PageCadastrar()
        {
            InitializeComponent();
        }

        public PageCadastrar(ModelFuncionario Func)
        {
            InitializeComponent();
            btSalvar.Text = "Alterar";
            txtCodigo.IsVisible = true;
            btExcluir.IsVisible = true;
            txtCodigo.Text = Func.Id.ToString();
            txtMatricula.Text = Func.Matricula;
            txtNome.Text = Func.Nome;
            txtCpf.Text = Func.Cpf;
            txtCelular.Text = Func.Celular;
        }


        private void btSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                ModelFuncionario Func = new ModelFuncionario();
                Func.Matricula = txtMatricula.Text;
                Func.Nome = txtNome.Text;
                Func.Cpf = txtCpf.Text;
                Func.Celular = txtCelular.Text;
                ServiceDBFuncs dBFuncs = new ServiceDBFuncs(App.DbPath);
                if (btSalvar.Text == "Inserir")
                {
                    dBFuncs.Inserir(Func);
                    DisplayAlert("Resultado", dBFuncs.StatusMessage, "OK");
                }
                else
                {
                    Func.Id = Convert.ToInt32(txtCodigo.Text);
                    dBFuncs.Alterar(Func);
                    DisplayAlert("Funcionario alterado com sucesso!", dBFuncs.StatusMessage, "OK");
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
            var resp = await DisplayAlert("Excluir Funcionario", "Deseja EXCLUIR este funcionario selecionado?", "Sim", "Não");
            if (resp == true)
            {
                ServiceDBFuncs dBNotas = new ServiceDBFuncs(App.DbPath);
                int id = Convert.ToInt32(txtCodigo.Text);
                dBNotas.Excluir(id);
                DisplayAlert("Funcionario excluído com sucesso", dBNotas.StatusMessage, "OK");
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
        }

        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageHome());
        }
    }
}