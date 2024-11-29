using System; // Importa o namespace base do .NET que contém classes fundamentais, como tipos de dados básicos e funcionalidades básicas.
using System.Collections.Generic; // Permite o uso de coleções genéricas, como listas e dicionários.
using System.ComponentModel.DataAnnotations; // Fornece atributos para validação de dados, como [Required] e [StringLength].
using System.Linq; // Habilita consultas em coleções, como filtros e agregações, usando LINQ (Language Integrated Query).

namespace SalesWebMvc.Models // Define o namespace, agrupando classes relacionadas ao modelo do projeto SalesWebMvc.
{
    public class Seller // Declara a classe Seller, que representa um vendedor no sistema.
    {
        public int id { get; set; } // Propriedade que armazena o identificador único do vendedor.

        [Required(ErrorMessage = "{0} required")] // Valida que o campo 'Name' é obrigatório; exibe a mensagem personalizada em caso de erro.
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1} ")] // Especifica que o tamanho do nome deve ser entre 3 e 60 caracteres.
        public string Name { get; set; } // Propriedade que armazena o nome do vendedor.

        [Required(ErrorMessage = "{0} required")] // Valida que o campo 'Email' é obrigatório.
        [EmailAddress(ErrorMessage = "Enter a valid email")] // Valida que o formato do e-mail fornecido é válido.
        [DataType(DataType.EmailAddress)] // Define o tipo de dados como um endereço de e-mail, útil para formatação em exibições.
        public string Email { get; set; } // Propriedade que armazena o e-mail do vendedor.

        [Required(ErrorMessage = "{0} required")] // Valida que o campo 'BirthDate' é obrigatório.
        [Display(Name = "Birth Date")] // Define o rótulo que será exibido em interfaces para essa propriedade.
        [DataType(DataType.Date)] // Define o tipo de dados como uma data, útil para formatação e controle.
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] // Define o formato de exibição da data como dia/mês/ano.
        public DateTime BirthDate { get; set; } // Propriedade que armazena a data de nascimento do vendedor.

        [Required(ErrorMessage = "{0} required")] // Valida que o campo 'BaseSalary' é obrigatório.
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")] // Especifica que o salário base deve estar entre 100.0 e 50000.0.
        [DisplayFormat(DataFormatString = "{0:F2}")] // Define o formato de exibição como decimal com duas casas decimais.
        [Display(Name = "Base Salary")] // Define o rótulo que será exibido para essa propriedade.
        public double BaseSalary { get; set; } // Propriedade que armazena o salário base do vendedor.

        public Departament Departament { get; set; } // Relaciona o vendedor a um departamento, representado pela classe Departament.
        public int DepartamentId { get; set; } // Representa a chave estrangeira para o departamento ao qual o vendedor pertence.

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>(); // Lista de registros de vendas associados ao vendedor, inicializada para evitar valores nulos.

        public Seller() // Construtor padrão da classe, necessário para inicialização sem parâmetros.
        {
        }

        // Construtor com parâmetros, utilizado para inicializar um vendedor com valores específicos.
        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Departament departament)
        {
            this.id = id; // Define o ID do vendedor.
            Name = name; // Define o nome do vendedor.
            Email = email; // Define o e-mail do vendedor.
            BirthDate = birthDate; // Define a data de nascimento do vendedor.
            BaseSalary = baseSalary; // Define o salário base do vendedor.
            Departament = departament; // Define o departamento ao qual o vendedor pertence.
        }

        // Método para adicionar um registro de venda à lista de vendas do vendedor.
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr); // Adiciona o registro fornecido à coleção Sales.
        }

        // Método para remover um registro de venda da lista de vendas do vendedor.
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr); // Remove o registro fornecido da coleção Sales.
        }

        // Método que calcula o total de vendas em um intervalo de datas especificado.
        public double TotalSales(DateTime initial, DateTime final)
        {
            // Filtra os registros de vendas que estão no intervalo especificado e soma os valores.
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
