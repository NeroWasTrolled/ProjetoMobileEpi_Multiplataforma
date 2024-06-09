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
    public partial class PageEntrega : ContentPage
    {
        private ModelEntrega entrega;
        private ServiceDBFuncs dbFuncs;
        private ServiceDBEpi dbEpi;
        // Adicione este construtor à classe PageEntrega
        public PageEntrega(ModelEntrega entrega)
        {
            InitializeComponent();
            this.entrega = entrega;
            btSalvar.Text = "Alterar";
            txtCodigo.IsVisible = true;
            btExcluir.IsVisible = true;
            txtCodigo.Text = entrega.id.ToString();

            // Definir a matrícula selecionada no Picker
            ServiceDBFuncs dbFuncs = new ServiceDBFuncs(App.DbPath);
            var funcionario = dbFuncs.Listar().FirstOrDefault(f => f.Matricula == entrega.matricula);
            if (funcionario != null)
            {
                pickerMatricula.SelectedItem = funcionario;
            }

            // Definir o EPI selecionado no Picker
            ServiceDBEpi dbEpi = new ServiceDBEpi(App.DbPath);
            var epi = dbEpi.Listar().FirstOrDefault(ep => ep.Epi == entrega.epi);
            if (epi != null)
            {
                pickerEpi.SelectedItem = epi;
            }

            labelValidade.Text = entrega.Data_Vencimento.ToString(); // Corrigido para usar ToString() para converter a data
            datePickerEntrega.Date = entrega.data_entrega; // Corrigido para definir a Data, não o Texto
            PreencherPickers();
        }


        public PageEntrega()
        {
            InitializeComponent();
            dbFuncs = new ServiceDBFuncs(App.DbPath);
            dbEpi = new ServiceDBEpi(App.DbPath);
            PreencherPickers();
        }

        private void PreencherPickers()
        {
            ServiceDBFuncs dbMatricula = new ServiceDBFuncs(App.DbPath);
            ServiceDBEpi dbEpi = new ServiceDBEpi(App.DbPath);

            try
            {
                List<ModelFuncionario> matriculas = dbMatricula.Listar();
                pickerMatricula.ItemsSource = matriculas;
                pickerMatricula.ItemDisplayBinding = new Binding("Matricula");

                List<ModelEpi> epis = dbEpi.Listar();
                pickerEpi.ItemsSource = epis;
                pickerEpi.ItemDisplayBinding = new Binding("Epi");

                // Verifique se há um registro selecionado
                if (entrega != null)
                {
                    // Defina o item selecionado nos pickers
                    pickerMatricula.SelectedItem = matriculas.FirstOrDefault(m => m.Matricula == entrega.matricula);
                    pickerEpi.SelectedItem = epis.FirstOrDefault(e => e.Epi == entrega.epi);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao preencher pickers: {ex.Message}");
            }
        }

        private void pickerEpi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Evento SelectedIndexChanged acionado");

            if (pickerEpi.SelectedItem != null && pickerEpi.SelectedItem is ModelEpi epiSelecionado)
            {
                Console.WriteLine("Item selecionado é válido");

                if (epiSelecionado.data_vencimento != null && epiSelecionado.data_vencimento != DateTime.MinValue)
                {
                    Console.WriteLine("Data de vencimento válida encontrada");
                    labelValidade.Text = $"Validade: {epiSelecionado.data_vencimento:d}";
                }
                else
                {
                    Console.WriteLine("Data de vencimento não definida");
                    labelValidade.Text = "Validade não definida";
                }
            }
            else
            {
                Console.WriteLine("Nenhum item selecionado ou item inválido");
                labelValidade.Text = "Selecione um EPI";
            }
        }

        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageHome());
        }

        private async void btExcluir_Clicked(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("Excluir Entrega", "Deseja EXCLUIR esta entrega selecionada?", "Sim", "Não");
            if (resp == true)
            {
                ServiceDBEntrega dbEntrega = new ServiceDBEntrega(App.DbPath);
                int id = Convert.ToInt32(txtCodigo.Text); 
                dbEntrega.Excluir(id);
                DisplayAlert("Entrega excluída com sucesso", dbEntrega.StatusMessage, "OK");

                // Redirecionar para a página inicial após a exclusão
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
        }


        private void btSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                ModelEntrega entrega = new ModelEntrega();

                ModelFuncionario funcionarioSelecionado = (ModelFuncionario)pickerMatricula.SelectedItem;
                if (funcionarioSelecionado != null)
                {
                    entrega.matricula = funcionarioSelecionado.Matricula;
                }
                else
                {
                    throw new Exception("Selecione uma matrícula.");
                }

                ModelEpi epiSelecionado = (ModelEpi)pickerEpi.SelectedItem;
                if (epiSelecionado != null)
                {
                    entrega.epi = epiSelecionado.Epi; 
                    entrega.Data_Vencimento = epiSelecionado.data_vencimento;
                }
                else
                {
                    throw new Exception("Selecione um EPI.");
                }

                ServiceDBEntrega dbEntrega = new ServiceDBEntrega(App.DbPath);
                dbEntrega.Inserir(entrega);
                DisplayAlert("Resultado", "Entrega registrada com sucesso!", "OK");
                
                LimparCampos();
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private void LimparCampos()
        {
            pickerMatricula.SelectedItem = null;
            pickerEpi.SelectedItem = null;
        }

    }
}