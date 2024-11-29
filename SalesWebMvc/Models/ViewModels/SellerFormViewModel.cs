using System.Collections.Generic; // Importa coleções genéricas, como 'ICollection<T>', usada para armazenar uma lista de departamentos.
using System.Linq; // Fornece métodos LINQ, caso sejam necessários para manipulação de coleções.
using System.Threading.Tasks; // Permite operações assíncronas, embora não seja utilizado diretamente nesse código.

namespace SalesWebMvc.Models.ViewModels // Define o namespace para agrupar classes de ViewModel relacionadas ao projeto SalesWebMvc.
{
    // Classe usada para representar o modelo de dados necessário para o formulário de cadastro/edição de vendedores.
    public class SellerFormViewModel
    {
        // Propriedade que armazena os dados de um vendedor, permitindo que ele seja exibido ou editado no formulário.
        public Seller Seller { get; set; }

        // Lista de departamentos disponíveis, usada para preencher, por exemplo, um dropdown no formulário.
        // Facilita a seleção de um departamento ao associar um vendedor a ele.
        public ICollection<Departament> Departaments { get; set; }
    }
}


//Classe SellerFormViewModel:
//
//É uma classe ViewModel, usada para encapsular os dados que serão enviados para uma View (formulário).
//Combina os dados do vendedor (Seller) com a lista de departamentos (Departaments) para facilitar a manipulação desses dados na interface do usuário.
//Propriedade Seller:
//Representa os dados de um vendedor (Seller) no formulário.
//Pode conter informações como nome, e-mail, salário base, data de nascimento, e departamento ao qual o vendedor está associado.

//Propriedade Departaments:
//Armazena uma coleção de objetos Departament, que representam os departamentos disponíveis na empresa.
//Essa coleção pode ser usada em elementos da interface como dropdowns ou listas para que o usuário escolha o departamento correto para o vendedor.

//Finalidade da classe:
//
//Centraliza e organiza os dados necessários para que a View possa renderizar corretamente um formulário de vendedor.
//Permite uma separação de responsabilidades, já que a classe ViewModel é independente das classes de domínio (Seller e Departament), mas combina informações delas para a View.

