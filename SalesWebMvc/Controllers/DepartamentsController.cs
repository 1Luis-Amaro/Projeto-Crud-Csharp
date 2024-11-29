// Declaração de namespaces necessários para o controlador
using System; // Importa namespaces do .NET para trabalhar com tipos básicos, como 'DateTime', 'String', etc.
using System.Collections.Generic; // Usado para coleções como listas, dicionários, etc.
using System.Linq; // Para consultas LINQ.
using System.Threading.Tasks; // Para trabalhar com tarefas assíncronas, como as consultas ao banco de dados.
using Microsoft.AspNetCore.Mvc; // Fornece classes e métodos necessários para trabalhar com controllers e views no ASP.NET Core.
using Microsoft.AspNetCore.Mvc.Rendering; // Utilizado para gerar HTML de controle de formulários, como listas suspensas, etc.
using Microsoft.EntityFrameworkCore; // Usado para interagir com o Entity Framework, permitindo acesso ao banco de dados.
using SalesWebMvc.Models; // Referência ao namespace onde os modelos de dados (como o modelo Departament) estão definidos.

namespace SalesWebMvc.Controllers // Define o namespace para o controlador, o que organiza o código em diferentes áreas de funcionalidade.
{
    // Define o controlador para a entidade 'Departament'. 
    // Ele herda de Controller, a classe base que facilita a criação de ações (metodos) que manipulam as requisições HTTP.
    public class DepartamentsController : Controller
    {
        // Declaração de uma variável de contexto do banco de dados para manipular os dados da tabela 'Departament' no banco.
        private readonly SalesWebMvcContext _context;

        // Construtor que injeta uma instância do contexto de banco de dados (_context) para acesso aos dados.
        public DepartamentsController(SalesWebMvcContext context)
        {
            _context = context;
        }

        // Ação assíncrona para exibir a lista de departamentos.
        // A consulta para obter todos os departamentos é feita assíncronamente com 'ToListAsync()' do Entity Framework.
        // A consulta ao banco de dados é feita de forma assíncrona para evitar bloqueios na thread principal.
        // A lista é passada para a view que vai renderizar os dados.
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departament.ToListAsync()); // Retorna a lista de departamentos para a view.
        }

        // Ação para exibir os detalhes de um departamento. Recebe um ID opcional na URL (id).
        // Se o ID for nulo, retorna um erro "NotFound" para informar que o departamento não foi encontrado.
        // Caso contrário, busca o departamento com o ID fornecido no banco e o passa para a view.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Se o ID for nulo, retorna uma resposta 'NotFound' (404).
            }

            var departament = await _context.Departament
                .FirstOrDefaultAsync(m => m.Id == id); // Busca o departamento no banco de dados usando o ID.
            if (departament == null)
            {
                return NotFound(); // Se o departamento não for encontrado, retorna 'NotFound'.
            }

            return View(departament); // Se o departamento for encontrado, retorna os detalhes na view.
        }

        // Ação para exibir o formulário de criação de um departamento. 
        // Essa ação apenas retorna a view que contém o formulário para inserir um novo departamento.
        public IActionResult Create()
        {
            return View(); // Retorna a view de criação de departamento.
        }

        // Ação POST que recebe os dados do formulário de criação (usando o método HTTP POST).
        // O atributo 'Bind' especifica quais propriedades do modelo podem ser vinculadas no formulário.
        // O modelo de departamento é validado, e se for válido, ele é adicionado ao contexto do banco de dados e salvo.
        // Após salvar, o usuário é redirecionado para a página de listagem de departamentos (Index).
        [HttpPost]
        [ValidateAntiForgeryToken] // Protege contra ataques CSRF (Cross-Site Request Forgery).
        public async Task<IActionResult> Create([Bind("Id,Name")] Departament departament)
        {
            if (ModelState.IsValid) // Verifica se o modelo de departamento passou na validação.
            {
                _context.Add(departament); // Adiciona o novo departamento ao contexto (ainda não foi salvo no banco de dados).
                await _context.SaveChangesAsync(); // Salva as alterações no banco de dados de forma assíncrona.
                return RedirectToAction(nameof(Index)); // Redireciona para a ação 'Index' após salvar.
            }
            return View(departament); // Se o modelo não for válido, retorna a view de criação com os dados que foram enviados.
        }

        // Ação para exibir o formulário de edição de um departamento.
        // Recebe o ID do departamento e retorna a view de edição com os dados do departamento a ser editado.
        // Caso o ID não seja encontrado, retorna a resposta 'NotFound'.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Se o ID for nulo, retorna 'NotFound'.
            }

            var departament = await _context.Departament.FindAsync(id); // Busca o departamento pelo ID.
            if (departament == null)
            {
                return NotFound(); // Se o departamento não for encontrado, retorna 'NotFound'.
            }
            return View(departament); // Retorna a view de edição com os dados do departamento.
        }

        // Ação POST que atualiza um departamento existente no banco de dados.
        // Se o ID do departamento não corresponder ao ID fornecido na URL, retorna 'NotFound'.
        // Se o modelo for válido, tenta atualizar o departamento no banco de dados e salvar as mudanças.
        // Caso ocorra um erro de concorrência (por exemplo, o departamento foi modificado por outra pessoa), 
        // o código lida com a exceção.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Departament departament)
        {
            if (id != departament.Id) // Verifica se o ID da URL corresponde ao ID do departamento.
            {
                return NotFound(); // Se os IDs não coincidirem, retorna 'NotFound'.
            }

            if (ModelState.IsValid) // Se o modelo for válido, tenta salvar as alterações.
            {
                try
                {
                    _context.Update(departament); // Atualiza o departamento no banco de dados.
                    await _context.SaveChangesAsync(); // Salva as alterações no banco de dados.
                }
                catch (DbUpdateConcurrencyException) // Captura erros de concorrência durante o processo de atualização.
                {
                    if (!DepartamentExists(departament.Id)) // Verifica se o departamento existe no banco.
                    {
                        return NotFound(); // Se o departamento não existir, retorna 'NotFound'.
                    }
                    else
                    {
                        throw; // Caso contrário, lança a exceção novamente.
                    }
                }
                return RedirectToAction(nameof(Index)); // Após salvar com sucesso, redireciona para a página de listagem.
            }
            return View(departament); // Se o modelo não for válido, retorna a view de edição com os dados.
        }

        // Ação para exibir a confirmação de exclusão de um departamento.
        // Recebe o ID do departamento e, se encontrado, retorna a view de exclusão. Caso contrário, retorna 'NotFound'.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Se o ID for nulo, retorna 'NotFound'.
            }

            var departament = await _context.Departament
                .FirstOrDefaultAsync(m => m.Id == id); // Busca o departamento pelo ID.
            if (departament == null)
            {
                return NotFound(); // Se o departamento não for encontrado, retorna 'NotFound'.
            }

            return View(departament); // Se encontrado, retorna a view de exclusão com os dados do departamento.
        }

        // Ação POST que confirma a exclusão de um departamento.
        // Remove o departamento do banco de dados e salva as alterações.
        // Após excluir, redireciona para a página de listagem (Index).
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departament = await _context.Departament.FindAsync(id); // Encontra o departamento pelo ID.
            _context.Departament.Remove(departament); // Remove o departamento do contexto.
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados.
            return RedirectToAction(nameof(Index)); // Redireciona para a página de listagem de departamentos.
        }

        // Método privado para verificar se um departamento com o ID fornecido existe no banco de dados.
        private bool DepartamentExists(int id)
        {
            return _context.Departament.Any(e => e.Id == id); // Verifica se existe algum departamento com o ID especificado.
        }
    }
}
