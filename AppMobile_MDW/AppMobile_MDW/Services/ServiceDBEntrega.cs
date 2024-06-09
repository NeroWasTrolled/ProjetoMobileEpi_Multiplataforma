using AppMobile_MDW.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppMobile_MDW.Services
{
    public class ServiceDBEntrega
    {
        SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public ServiceDBEntrega(string dbPath)
        {
            if (dbPath == "") dbPath = App.DbPath;
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<ModelEntrega>();
        }

        public void Inserir(ModelEntrega entrega)
        {
            try
            {
                if (string.IsNullOrEmpty(entrega.matricula))
                    throw new Exception("Matrícula não informada!");
                if (string.IsNullOrEmpty(entrega.epi))
                    throw new Exception("EPI não informado!");
                if (entrega.Data_Vencimento == null)
                    throw new Exception("Data de validade do EPI não informada!");
                if (entrega.data_entrega == null)
                    throw new Exception("Data de entrega não informada!");

                int result = conn.Insert(entrega);
                if (result != 0)
                {
                    this.StatusMessage = string.Format("{0} registro adicionado", result);
                }
                else
                {
                    this.StatusMessage = "0 registro adicionado";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }


        public List<ModelEntrega> Listar()
        {
            List<ModelEntrega> lista = new List<ModelEntrega>();
            try
            {
                lista = conn.Table<ModelEntrega>().ToList();
                this.StatusMessage = "Listagem das Entregas";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lista;
        }

        public void Excluir(int id)
        {
            try
            {
                int result = conn.Table<ModelEntrega>().Delete(r => r.id == id);
                StatusMessage = string.Format("{0} registro deletado", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }

        public void Alterar(ModelEntrega entrega)
        {
            try
            {
                if (string.IsNullOrEmpty(entrega.matricula))
                    throw new Exception("Matrícula não informada!");
                if (string.IsNullOrEmpty(entrega.epi))
                    throw new Exception("EPI não informado!");
                if (entrega.Data_Vencimento == null)
                    throw new Exception("Data de validade do EPI não informada!");
                if (entrega.data_entrega == null)
                    throw new Exception("Data de entrega não informada!");
                if (entrega.id <= 0)
                    throw new Exception("Id da entrega não informado!");

                int result = conn.Update(entrega);
                StatusMessage = string.Format("{0} registro alterado", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }

        public List<ModelEntrega> LocalizarPorMatricula(string matricula)
        {
            List<ModelEntrega> lista = new List<ModelEntrega>();
            try
            {
                var resp = from p in conn.Table<ModelEntrega>() where p.matricula.ToLower().Contains(matricula.ToLower()) select p;
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
