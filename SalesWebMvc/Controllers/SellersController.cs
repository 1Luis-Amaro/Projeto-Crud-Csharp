// Importa os namespaces necessários para o funcionamento do controlador.
using Microsoft.AspNetCore.Mvc; // Fornece classes e métodos para criar controladores no ASP.NET Core MVC.
using System; // Fornece tipos básicos e funcionalidades essenciais.
using System.Collections.Generic; // Para trabalhar com listas e coleções.
using System.Linq; // Suporte para LINQ.
using System.Threading.Tasks; // Suporte para programação assíncrona.
using SalesWebMvc.Services; // Contém os serviços SellerService e DepartamentService.
using SalesWebMvc.Models; // Contém modelos como Seller e Departament.
using SalesWebMvc.Models.ViewModels; // Contém o ViewModel SellerFormViewModel.
using SalesWebMvc.Services.Exceptions; // Contém exceções personalizadas.
using System.Diagnostics; // Fornece classes para rastreamento de atividades e logs.

namespace SalesWebMvc.Controllers // Define o namespace do controlador.
{
    // Define o controlador responsável por gerenciar vendedores.
    public class SellersController : Controller
    {
        // Dependências do serviço.
        private readonly SellerService _sellerService; // Serviço para gerenciar vendedores.
        private readonly DepartamentService _departamentService; // Serviço para gerenciar departamentos.

        // Construtor que injeta os serviços.
        public SellersController(SellerService sellerService, DepartamentService departamentService)
        {
            _sellerService = sellerService; // Inicializa o serviço de vendedores.
            _departamentService = departamentService; // Inicializa o serviço de departamentos.
        }

        // Método para exibir a lista de vendedores.
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync(); // Busca todos os vendedores de forma assíncrona.
            return View(list); // Retorna a lista para a view.
        }

        // Método para exibir o formulário de criação de um vendedor.
        public async Task<IActionResult> Create()
        {
            var departaments = await _departamentService.FindAllAsync(); // Busca todos os departamentos.
            var viewModel = new SellerFormViewModel { Departaments = departaments }; // Cria o ViewModel.
            return View(viewModel); // Retorna o formulário preenchido.
        }

        // Método para criar um novo vendedor (requisição POST).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid) // Valida o estado do modelo.
            {
                var departaments = await _departamentService.FindAllAsync(); // Busca departamentos novamente em caso de erro.
                var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departaments };
                return View(viewModel); // Retorna o formulário com os erros.
            }
            await _sellerService.InsertAsync(seller); // Insere o vendedor no banco.
            return RedirectToAction(nameof(Index)); // Redireciona para a lista de vendedores.
        }

        // Método para exibir a tela de confirmação de exclusão.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) // Verifica se o ID foi fornecido.
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); // Busca o vendedor pelo ID.
            if (obj == null) // Verifica se o vendedor foi encontrado.
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj); // Retorna o objeto para a view de exclusão.
        }

        // Método para processar a exclusão de um vendedor (requisição POST).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id); // Remove o vendedor pelo ID.
                return RedirectToAction(nameof(Index)); // Redireciona para a lista.
            }
            catch (IntegrityException e) // Captura erros de integridade referencial.
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // Método para exibir os detalhes de um vendedor.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) // Verifica se o ID foi fornecido.
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); // Busca o vendedor pelo ID.
            if (obj == null) // Verifica se o vendedor foi encontrado.
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj); // Retorna o objeto para a view de detalhes.
        }

        // Método para exibir o formulário de edição de um vendedor.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) // Verifica se o ID foi fornecido.
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value); // Busca o vendedor pelo ID.
            if (obj == null) // Verifica se o vendedor foi encontrado.
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var departaments = await _departamentService.FindAllAsync(); // Busca todos os departamentos.
            var viewModel = new SellerFormViewModel { Seller = obj, Departaments = departaments }; // Cria o ViewModel.
            return View(viewModel); // Retorna o formulário preenchido.
        }

        // Método para processar a edição de um vendedor (requisição POST).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid) // Valida o estado do modelo.
            {
                var departaments = await _departamentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departaments };
                return View(viewModel);
            }

            if (id != seller.id) // Verifica se o ID da URL corresponde ao ID do modelo.
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller); // Atualiza o vendedor no banco.
                return RedirectToAction(nameof(Index)); // Redireciona para a lista.
            }
            catch (ApplicationException e) // Captura exceções personalizadas.
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // Método para exibir uma página de erro.
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message, // Define a mensagem de erro.
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // Define o ID da requisição.
            };
            return View(viewModel); // Retorna o modelo para a view de erro.
        }
    }
}
