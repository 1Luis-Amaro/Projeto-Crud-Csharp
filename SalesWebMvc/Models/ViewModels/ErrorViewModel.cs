// Importação do namespace 'System', que fornece funcionalidades fundamentais para a aplicação.
// Este namespace contém tipos básicos como String, DateTime, entre outros, usados para manipulação de dados e controle do fluxo da aplicação.
using System;

// Definindo o namespace da classe. O namespace 'SalesWebMvc.Models.ViewModels' indica que a classe 'ErrorViewModel' 
// pertence ao modelo de dados que será utilizado na aplicação MVC, especificamente no contexto de erros.
namespace SalesWebMvc.Models.ViewModels
{
    // Definição da classe 'ErrorViewModel', que serve como um modelo de dados para transferir informações de erro 
    // para a camada de exibição (view) no padrão MVC. O 'ViewModel' contém as propriedades que serão utilizadas na view 
    // para mostrar o erro ao usuário.
    public class ErrorViewModel
    {
        // Propriedade 'RequestId' do tipo 'string' que armazena o identificador único da requisição. 
        // Esse ID é utilizado para rastrear e identificar uma requisição específica, o que é útil em logs de erros 
        // ou quando se precisa diagnosticar o problema. Ele pode ser gerado automaticamente pelo framework em casos de exceções.
        public string RequestId { get; set; }

        // Propriedade 'Message' também do tipo 'string', que armazena uma mensagem de erro descritiva.
        // Essa mensagem será passada do controlador para a view, permitindo ao usuário final entender a natureza do erro ocorrido.
        public string Message { get; set; }

        // Propriedade calculada 'ShowRequestId', que determina se o 'RequestId' deve ser mostrado ao usuário.
        // A expressão retorna 'true' se o 'RequestId' não for nulo nem vazio (isto é, se o identificador da requisição existe),
        // caso contrário, retorna 'false'. Isso permite que o RequestId seja mostrado na interface do usuário apenas quando houver
        // um valor válido, dando controle sobre o que é exibido. 
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
