using SalesWebMvc.Models.Enums; // Importa o namespace que contém o enum 'SalesStatus', usado para definir o status de vendas.
using System; // Importa funcionalidades básicas do .NET, como tipos de dados fundamentais e manipulação de datas.
using System.ComponentModel.DataAnnotations; // Fornece atributos para validação e formatação de dados, como [DisplayFormat].

namespace SalesWebMvc.Models // Define o namespace, agrupando classes relacionadas ao modelo do projeto SalesWebMvc.
{
    public class SalesRecord // Declara a classe 'SalesRecord', que representa um registro de venda no sistema.
    {
        public int Id { get; set; } // Propriedade que armazena o identificador único do registro de venda.

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] // Define o formato de exibição da data como dia/mês/ano.
        public DateTime Date { get; set; } // Propriedade que armazena a data em que a venda foi realizada.

        [DisplayFormat(DataFormatString = "{0:F2}")] // Define o formato de exibição do valor monetário com duas casas decimais.
        public double Amount { get; set; } // Propriedade que armazena o valor da venda.

        public SalesStatus Status { get; set; } // Enum que indica o status da venda (ex: Pending, Billed, Canceled).

        public Seller Seller { get; set; } // Relaciona o registro de venda a um vendedor específico.

        // Construtor padrão da classe, necessário para inicialização sem parâmetros.
        public SalesRecord()
        {
        }

        // Construtor com parâmetros, utilizado para criar um registro de venda com valores específicos.
        public SalesRecord(int id, DateTime date, double amount, SalesStatus status, Seller seller)
        {
            Id = id; // Define o ID do registro de venda.
            Date = date; // Define a data da venda.
            Amount = amount; // Define o valor da venda.
            Status = status; // Define o status da venda.
            Seller = seller; // Define o vendedor associado a essa venda.
        }
    }
}
