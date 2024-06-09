using System;
using System.Collections.Generic;
using System.Text;
using AppMobile_MDW.Models;
using SQLite;

namespace AppMobile_MDW.Services
{
    public class ServiceDBEpi
    {
        SQLiteConnection conn;
        public string StatusMessage { get; set; }
        public ServiceDBEpi(string dbPath)
        {
            if (dbPath == "") dbPath = App.DbPath;
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<ModelEpi>();
        }
        public void Inserir(ModelEpi control)
        {
            try
            {
                if (string.IsNullOrEmpty(control.Epi))
                    throw new Exception("EPI não informado!");

                control.data_vencimento = DateTime.Now.AddDays(90);
                int result = conn.Insert(control);
                if (result != 0)
                {
                    this.StatusMessage = string.Format("{0} registro adicionado: {1}", result, control.Epi);
                }
                else
                {
                    this.StatusMessage = string.Format("0 registro! Por favor, informe a Epi e a data de validade!");
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<ModelEpi> Listar()
        {
            List<ModelEpi> lista = new List<ModelEpi>();
            try
            {
                lista = conn.Table<ModelEpi>().ToList();
                this.StatusMessage = "Listagem das EPI's";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lista;
        }
        public void Alterar(ModelEpi control)
        {
            try
            {
                if (string.IsNullOrEmpty(control.Epi))
                    throw new Exception("EPI não informado!");
                if (control.Id <= 0)
                    throw new Exception("Id do EPI não informado!");

                control.data_vencimento = DateTime.Now.AddDays(90);
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }
        public void Excluir(int id)
        {
            try
            {
                int result = conn.Table<ModelEpi>().Delete(r => r.Id == id);
                StatusMessage = string.Format("{0} registro deletado", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }
        public List<ModelEpi> Localizar(string titulo)
        {
            List<ModelEpi> lista = new List<ModelEpi>();
            try
            {
                var resp = from p in conn.Table<ModelEpi>() where p.Epi.ToLower().Contains(titulo.ToLower()) select p;
                lista = resp.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return lista;
        }
    }
}
