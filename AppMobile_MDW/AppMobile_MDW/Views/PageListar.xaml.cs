using AppMobile_MDW.Models;
using AppMobile_MDW.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMobile_MDW.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListar : ContentPage
    {
        private bool listarFuncionarios = false;
        private bool listarEpis = false;
        private bool listarEntregas = false;

        public PageListar()
        {
            InitializeComponent();
            AtualizaLista();
        }

        private void AtualizaLista()
        {
            if (listarFuncionarios)
            {
                ServiceDBFuncs dBFuncionario = new ServiceDBFuncs(App.DbPath);
                string nome = txtNome.Text ?? "";
                ListaFuncs.ItemsSource = dBFuncionario.Localizar(nome);
            }
            else if (listarEpis)
            {
                ServiceDBEpi dBEpi = new ServiceDBEpi(App.DbPath);
                ListaFuncs.ItemsSource = dBEpi.Listar();
            }
            else if (listarEntregas)
            {
                ServiceDBEntrega dbEntrega = new ServiceDBEntrega(App.DbPath);
                ListaFuncs.ItemsSource= dbEntrega.Listar();
            }
            else
            {
                ListaFuncs.ItemsSource = null;
            }
        }

        private void btLocalizar_Clicked(object sender, EventArgs e)
        {
            AtualizaLista();
        }

        private void ListaFuncs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listarEpis)
            {
                ModelEpi epi = (ModelEpi)e.SelectedItem;
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageEpi(epi));
            }
            else if (listarFuncionarios)
            {
                ModelFuncionario funcionario = (ModelFuncionario)e.SelectedItem;
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageCadastrar(funcionario));
            }
            else if (listarEntregas)
            {
                ModelEntrega entrega = (ModelEntrega)e.SelectedItem;
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageEntrega(entrega));
            }
        }


        private void checkboxFuncionarios_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            listarFuncionarios = e.Value;
            if (listarFuncionarios)
            {
                listarEpis = false;
                listarEntregas = false;
            }
            AtualizaLista();
        }

        private void checkboxEpis_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            listarEpis = e.Value;
            if (listarEpis)
            {
                listarFuncionarios = false;
                listarEntregas = false;
            }
            AtualizaLista();
        }

        private void checkboxEntregas_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            listarEntregas = e.Value;
            if (listarEntregas)
            {
                listarFuncionarios = false;
                listarEpis = false;
            }
            AtualizaLista();
        }
    }
}
