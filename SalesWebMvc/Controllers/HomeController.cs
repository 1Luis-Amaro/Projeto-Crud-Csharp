// Importação dos namespaces necessários para o código
using Microsoft.AspNetCore.Mvc; // Importa a classe base Controller do ASP.NET Core MVC, necessária para criar controladores de MVC.
using SalesWebMvc.Models; // Importa o namespace que contém os modelos de dados da aplicação.
using System; // Importa classes e tipos básicos do .NET, como DateTime, String, etc.
using System.Collections.Generic; // Necessário para trabalhar com coleções, como List, Dictionary, etc.
using System.Diagnostics; // Usado para monitoramento e depuração, como o acesso ao identificador de rastreamento de erro.
using System.Linq; // Usado para consultas LINQ, que permitem fazer operações de consulta em coleções.
using System.Threading.Tasks; // Necessário para operações assíncronas com tarefas (Task).
using SalesWebMvc.Models.ViewModels; // Importa o namespace com as classes de ViewModel que são usadas para passar dados entre o Controller e a View.

namespace SalesWebMvc.Controllers // Define o namespace do controlador.
{
    // A classe HomeController herda de Controller e é responsável por gerenciar as ações relacionadas às páginas da aplicação.
    // Esse controlador tem ações como Index, About, Contact, Privacy e Error.
    public class HomeController : Controller
    {
        // Ação responsável por exibir a página inicial (Index).
        // Ela retorna uma View sem nenhum dado específico.
        public IActionResult Index()
        {
            return View(); // Retorna a View associada a esta ação (Index.cshtml).
        }

        // Ação responsável por exibir a página "About" (Sobre).
        // Define algumas informações que serão passadas para a View, utilizando o ViewData.
        public IActionResult About()
        {
            // O ViewData é um dicionário utilizado para passar dados entre o Controller e a View.
            // Ele armazena informações temporárias que são acessadas na View.
            ViewData["Message"] = "Salles Web MVC App from C# Course"; // Mensagem para ser exibida na View.
            ViewData["Professor"] = "Nelio Alves"; // Nome do professor para ser exibido na View.

            return View(); // Retorna a View associada a esta ação (About.cshtml).
        }

        // Ação responsável por exibir a página "Contact" (Contato).
        public IActionResult Contact()
        {
            // Passa uma mensagem para a View usando o ViewData.
            ViewData["Message"] = "Your contact page."; // Mensagem para a View.

            return View(); // Retorna a View associada a esta ação (Contact.cshtml).
        }

        // Ação responsável por exibir a página de "Privacy" (Privacidade).
        public IActionResult Privacy()
        {
            return View(); // Retorna a View associada a esta ação (Privacy.cshtml).
        }

        // Ação responsável por exibir a página de erro.
        // Esta ação usa o ResponseCache para controlar o cache da resposta (neste caso, não há cache).
        // Quando ocorre um erro na aplicação, a View Error é exibida.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // A View recebe um objeto ErrorViewModel com o RequestId, que é usado para identificar a requisição com falha.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


//Importação de Namespaces:

//O código começa com a importação de namespaces necessários para o funcionamento da aplicação. Entre eles, Microsoft.AspNetCore.Mvc que contém a classe Controller (base para todos os controladores no ASP.NET MVC) e SalesWebMvc.Models.ViewModels que contém as classes de ViewModel usadas para passagem de dados entre o Controller e as Views.
//Classe HomeController:
//
//A classe HomeController herda de Controller, o que permite que ela seja usada como um controlador em uma aplicação MVC. Ela contém ações que correspondem a páginas específicas, como Index, About, Contact, Privacy, e Error.
//Método Index():
//
//Esta é uma ação simples que exibe a página inicial da aplicação. Ela retorna a View associada, que será renderizada pelo ASP.NET MVC. Não há dados específicos sendo passados para a View, apenas a View é retornada.
//Método About():
//
//Este método exibe uma página "Sobre" (About). Ele usa ViewData para passar duas informações para a View:
//Message: Mensagem sobre o aplicativo.
//Professor: Nome do professor que criou o curso.
//O ViewData é um dicionário que armazena dados temporários que podem ser acessados na View.
//Método Contact():
//
//Este método exibe a página de contato e também utiliza ViewData para passar uma mensagem para a View. A mensagem passa a informação de que esta é a página de contato do aplicativo.
//Método Privacy():
//
//Este método exibe a página de privacidade, que pode ter informações sobre o uso de dados pessoais ou políticas de privacidade do site.Não há dados específicos sendo passados para a View.
//Método Error():
//
//Esta ação é executada quando ocorre um erro no sistema. A anotação [ResponseCache] é usada para configurar como a resposta será armazenada em cache (neste caso, a resposta não será armazenada). O método retorna uma View de erro, passando um modelo ErrorViewModel, que contém o RequestId associado à requisição, o que pode ser útil para rastrear o erro.
//Activity.Current?.Id ?? HttpContext.TraceIdentifier: Isso tenta obter o ID da requisição atual, se disponível; caso contrário, usa o identificador de rastreamento do contexto HTTP para identificar a requisição.
//Resumo:
//Esse código define o HomeController que gerencia as páginas principais da aplicação (Index, About, Contact, Privacy, Error). Ele utiliza o ViewData para passar dados para as Views, como mensagens ou informações do sistema, e possui um método específico para lidar com erros e exibir uma página de erro personalizada.