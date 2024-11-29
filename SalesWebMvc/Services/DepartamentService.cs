// Importa os namespaces necessários para o funcionamento do serviço.
using SalesWebMvc.Models; // Contém os modelos, como Departament.
using System; // Fornece tipos e funcionalidades essenciais.
using System.Collections.Generic; // Para trabalhar com listas e coleções.
using System.Linq; // Suporte para consultas LINQ.
using System.Threading.Tasks; // Para operações assíncronas.
using Microsoft.EntityFrameworkCore; // Para trabalhar com o Entity Framework Core.

namespace SalesWebMvc.Services // Define o namespace do serviço.
{
    // Serviço responsável por gerenciar operações relacionadas a departamentos.
    public class DepartamentService
    {
        private readonly SalesWebMvcContext _context; // Contexto do banco de dados.

        // Construtor que injeta o contexto do banco de dados.
        public DepartamentService(SalesWebMvcContext context)
        {
            _context = context; // Inicializa o contexto.
        }

        /// <summary>
        /// Método para buscar todos os departamentos de forma assíncrona.
        /// </summary>
        /// <returns>Lista de departamentos ordenada por nome.</returns>
        public async Task<List<Departament>> FindAllAsync()
        {
            // Realiza uma consulta para buscar todos os departamentos e ordená-los por nome.
            return await _context.Departament.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
