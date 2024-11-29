// Importa os namespaces necessários para o funcionamento do controlador.
using Microsoft.AspNetCore.Mvc; // Inclui recursos para criar controladores e ações em uma aplicação ASP.NET Core MVC.
using System; // Fornece tipos fundamentais, como DateTime, usados para manipulação de datas e horas.
using System.Collections.Generic; // Necessário para trabalhar com coleções, como List e Dictionary.
using System.Linq; // Suporta consultas LINQ para coleções.
using System.Threading.Tasks; // Fornece suporte a programação assíncrona usando a classe Task.
using SalesWebMvc.Services; // Importa o namespace que contém o serviço usado para buscar registros de vendas.

namespace SalesWebMvc.Controllers // Define o namespace do controlador, agrupando o código de forma lógica.
{
    // Define a classe `SalesRecordsController`, que herda de `Controller`.
    // Esta classe gerencia as ações relacionadas aos registros de vendas.
    public class SalesRecordsController : Controller
    {
        // Campo somente leitura para armazenar uma instância do serviço de registros de vendas.
        private readonly SalesRecordService _salesRecordService;

        // Construtor da classe que recebe o serviço `SalesRecordService` como dependência.
        // Esse serviço é injetado automaticamente pelo sistema de injeção de dependência.
        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService; // Inicializa o campo com a instância recebida.
        }

        // Ação responsável por exibir a página inicial do controlador.
        public IActionResult Index()
        {
            return View(); // Retorna a View associada à ação (Index.cshtml).
        }

        // Ação para realizar uma busca simples de registros de vendas com base em um intervalo de datas.
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            // Verifica se a data mínima foi fornecida. Caso contrário, define o início do ano atual como padrão.
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1); // Define 1º de janeiro do ano atual.
            }

            // Verifica se a data máxima foi fornecida. Caso contrário, define a data atual como padrão.
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now; // Define a data e hora atuais como limite máximo.
            }

            // Usa o `ViewData` para passar os valores das datas para a View.
            // Essas informações serão exibidas nos campos de entrada de data na interface.
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd"); // Formata a data mínima no formato "yyyy-MM-dd".
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd"); // Formata a data máxima no formato "yyyy-MM-dd".

            // Chama o serviço para buscar os registros de vendas no intervalo de datas fornecido.
            // O método `FindByDateAsync` é assíncrono e retorna os resultados em forma de uma lista.
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);

            // Retorna a View associada à ação (SimpleSearch.cshtml) e passa os resultados para ela.
            return View(result);
        }

        // Ação para realizar uma busca agrupada de registros de vendas com base em um intervalo de datas.
        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            // Verifica se a data mínima foi fornecida. Caso contrário, define o início do ano atual como padrão.
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1); // Define 1º de janeiro do ano atual.
            }

            // Verifica se a data máxima foi fornecida. Caso contrário, define a data atual como padrão.
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now; // Define a data e hora atuais como limite máximo.
            }

            // Usa o `ViewData` para passar os valores das datas para a View.
            // Essas informações serão exibidas nos campos de entrada de data na interface.
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd"); // Formata a data mínima no formato "yyyy-MM-dd".
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd"); // Formata a data máxima no formato "yyyy-MM-dd".

            // Chama o serviço para buscar os registros de vendas agrupados no intervalo de datas fornecido.
            // O método `FindByDateGroupingAsync` é assíncrono e retorna os resultados agrupados.
            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);

            // Retorna a View associada à ação (GroupingSearch.cshtml) e passa os resultados para ela.
            return View(result);
        }
    }
}


//Dependência SalesRecordService:
//
//O serviço SalesRecordService é utilizado para encapsular a lógica de negócios e acesso aos dados relacionados aos registros de vendas. Ele é injetado na classe por meio do construtor.
//Ações SimpleSearch e GroupingSearch:
//
//Essas ações realizam consultas baseadas em intervalos de datas e retornam resultados que podem ser exibidos em suas respectivas Views.
//A lógica de definição de datas padrão garante que a aplicação funcione corretamente mesmo quando o usuário não fornece um intervalo de datas.
//Os dados de entrada (minDate e maxDate) são formatados e passados para as Views por meio do ViewData, permitindo que sejam exibidos nos campos de entrada da interface do usuário.
//Programação Assíncrona:
//
//As ações utilizam async e await para chamadas assíncronas ao serviço. Isso melhora o desempenho da aplicação ao não bloquear a thread principal enquanto aguarda a conclusão das consultas.