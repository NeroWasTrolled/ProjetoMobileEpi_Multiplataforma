using System;
using System.Collections.Generic;
using System.Text;
using AppMobile_MDW.Models;
using SQLite;

namespace AppMobile_MDW.Services
{
    public class ServiceDBFuncs
    {
        SQLiteConnection conn;
        public string StatusMessage { get; set; }
        public ServiceDBFuncs(string dbPath)
        {
            if (dbPath == "") dbPath = App.DbPath;
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<ModelFuncionario>();
        }
        public void Inserir(ModelFuncionario func)
        {
            try
            {
                if (!int.TryParse(func.Matricula, out int matricula))
                    throw new Exception("Matrícula inválida!");
                if (string.IsNullOrEmpty(func.Nome))
                    throw new Exception("Nome do Funcionário não informado!");
                if (string.IsNullOrEmpty(func.Cpf))
                    throw new Exception("CPF do Funcionário não informado!");
                if (string.IsNullOrEmpty(func.Celular))
                    throw new Exception("Celular do Funcionário não informado!");
                int result = conn.Insert(func);
                if (result != 0)
                {
                    this.StatusMessage = string.Format("{0} registro adicionado: {1}", result, func.Nome);
                }
                else
                {
                    this.StatusMessage = string.Format("0 registro adicionado! Por favor, informe o nome e o setor do funcionário!");
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<ModelFuncionario> Listar()
        {
            List<ModelFuncionario> lista = new List<ModelFuncionario>();
            try
            {
                lista = conn.Table<ModelFuncionario>().ToList();
                this.StatusMessage = "Listagem dos Funcionários";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lista;
        }
        public void Alterar(ModelFuncionario func)
        {
            try
            {
                if (!int.TryParse(func.Matricula, out int matricula))
                    throw new Exception("Matrícula inválida!");
                if (string.IsNullOrEmpty(func.Nome))
                    throw new Exception("Nome do funcionário não informado!");
                if (string.IsNullOrEmpty(func.Cpf))
                    throw new Exception("Cpf do Funcionário não informado!");
                if (string.IsNullOrEmpty(func.Celular))
                    throw new Exception("Celular do funcionário não informado!");
                if (func.Id <= 0)
                    throw new Exception("Id do funcionário não informado!");
                int result = conn.Update(func);
                StatusMessage = string.Format("{0} registro alterado!", result);

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
                int result = conn.Table<ModelFuncionario>().Delete(r => r.Id == id);
                StatusMessage = string.Format("{0} registro deletado", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }
        public List<ModelFuncionario> Localizar(string titulo)
        {
            List<ModelFuncionario> lista = new List<ModelFuncionario>();
            try
            {
                var resp = from p in conn.Table<ModelFuncionario>() where p.Nome.ToLower().Contains(titulo.ToLower()) select p;
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
