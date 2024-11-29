using System; // Importa funcionalidades básicas do .NET, como tipos de dados fundamentais e manipulação de datas.
using System.Collections.Generic; // Importa coleções genéricas, como List<T> e ICollection<T>, usadas na propriedade 'Sellers'.
using System.Linq; // Fornece métodos LINQ, como 'Sum', usados para operações em coleções.

namespace SalesWebMvc.Models // Define o namespace, agrupando classes relacionadas ao modelo do projeto SalesWebMvc.
{
    public class Departament // Declara a classe 'Departament', que representa um departamento no sistema.
    {
        public int Id { get; set; } // Propriedade que armazena o identificador único do departamento.
        public string Name { get; set; } // Propriedade que armazena o nome do departamento.

        // Lista de vendedores associados a esse departamento. 
        // A inicialização com 'new List<Seller>()' garante que a coleção não seja nula.
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        // Construtor padrão da classe, necessário para inicialização sem parâmetros.
        public Departament()
        {
        }

        // Construtor com parâmetros, usado para criar um departamento com ID e nome específicos.
        public Departament(int id, string name)
        {
            Id = id; // Define o ID do departamento.
            Name = name; // Define o nome do departamento.
        }

        // Método para adicionar um vendedor à lista de 'Sellers' do departamento.
        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller); // Adiciona o vendedor recebido como argumento à coleção.
        }

        // Método para calcular o total de vendas realizadas por todos os vendedores do departamento 
        // em um intervalo de tempo especificado.
        public double TotalSales(DateTime initial, DateTime final)
        {
            // Usa LINQ para somar as vendas de cada vendedor no intervalo definido.
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}


//Classe Departament:
//Representa um departamento da empresa, responsável por agrupar vendedores e calcular as vendas totais dentro de um intervalo de tempo.

//Propriedade Sellers:
//É uma coleção de vendedores (Seller) que pertencem ao departamento.

//Inicializada como uma lista vazia para evitar problemas de referência nula.

//Método AddSeller:
//Permite adicionar um vendedor ao departamento de forma controlada.

//Método TotalSales:
//Usa a função Sum do LINQ para calcular a soma das vendas realizadas por todos os vendedores no intervalo fornecido.
//Chama o método TotalSales da classe Seller para obter o total de cada vendedor.

//Construtores:
//O construtor padrão é necessário para inicializar objetos sem passar parâmetros.
//O construtor parametrizado permite criar objetos Departament já configurados com um ID e um nome.