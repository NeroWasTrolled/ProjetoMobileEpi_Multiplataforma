using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppMobile_MDW.Models
{
    [Table("Funcionarios")]
    public class ModelFuncionario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public String Matricula { get; set; }

        [NotNull]
        public String Nome { get; set; }

        [NotNull]
        public String Cpf { get; set; }

        [NotNull]
        public String Celular { get; set; }

        public ModelFuncionario()
        {
            this.Id = 0;
            this.Matricula = "";
            this.Nome = "";
            this.Cpf = "";
            this.Celular = "";
        }
    }
    public class ModelEpi
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public String Epi { get; set; }
        [NotNull]
        public DateTime data_vencimento { get; set; }


        public ModelEpi()
        {
            this.Id = 0;
            this.Epi = "";
            this.data_vencimento = DateTime.Now.AddDays(90);
        }
    }

    public class ModelEntrega
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [NotNull]
        public string matricula { get; set; }

        [NotNull]
        public string epi { get; set; }

        [NotNull]
        public DateTime data_entrega { get; set; }

        [NotNull]
        public DateTime Data_Vencimento { get; set; }

        public ModelEntrega()
        {
            this.id = 0;
            this.matricula = "";
            this.epi = "";
            this.data_entrega = DateTime.Now;
            this.Data_Vencimento = DateTime.Now.AddDays(60);
        }
    }
}
