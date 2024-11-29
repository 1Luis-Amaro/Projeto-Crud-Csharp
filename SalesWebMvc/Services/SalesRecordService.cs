// Importa os namespaces necessários para o serviço.
using SalesWebMvc.Models; // Modelos como SalesRecord e Departament.
using System; // Tipos básicos, como DateTime.
using System.Collections.Generic; // Para listas e coleções.
using System.Linq; // LINQ para consultas.
using System.Threading.Tasks; // Operações assíncronas.
using Microsoft.EntityFrameworkCore; // Trabalhar com Entity Framework Core.

namespace SalesWebMvc.Services // Define o namespace do serviço.
{
    // Serviço responsável por operações relacionadas a registros de vendas.
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context; // Contexto do banco de dados.

        // Construtor com injeção do contexto do banco de dados.
        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Busca registros de vendas filtrados por data mínima e máxima.
        /// </summary>
        /// <param name="minDate">Data mínima para o filtro.</param>
        /// <param name="maxDate">Data máxima para o filtro.</param>
        /// <returns>Lista de registros de vendas ordenada por data.</returns>
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            // Inicia uma consulta básica à tabela SalesRecord.
            var result = from obj in _context.SalesRecord select obj;

            // Aplica o filtro para a data mínima, se fornecida.
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            // Aplica o filtro para a data máxima, se fornecida.
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            // Executa a consulta com os filtros aplicados.
            return await result
                .Include(x => x.Seller) // Inclui os dados do vendedor.
                .Include(x => x.Seller.Departament) // Inclui o departamento do vendedor.
                .OrderByDescending(x => x.Date) // Ordena os resultados por data, em ordem decrescente.
                .ToListAsync(); // Converte a consulta para uma lista de forma assíncrona.
        }

        /// <summary>
        /// Busca registros de vendas agrupados por departamento e filtrados por data mínima e máxima.
        /// </summary>
        /// <param name="minDate">Data mínima para o filtro.</param>
        /// <param name="maxDate">Data máxima para o filtro.</param>
        /// <returns>Lista de grupos de registros de vendas organizados por departamento.</returns>
        public async Task<List<IGrouping<Departament, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            // Inicia uma consulta básica à tabela SalesRecord.
            var result = from obj in _context.SalesRecord select obj;

            // Aplica o filtro para a data mínima, se fornecida.
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            // Aplica o filtro para a data máxima, se fornecida.
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            // Executa a consulta com agrupamento por departamento.
            return await result
                .Include(x => x.Seller) // Inclui os dados do vendedor.
                .Include(x => x.Seller.Departament) // Inclui o departamento do vendedor.
                .OrderByDescending(x => x.Date) // Ordena os resultados por data, em ordem decrescente.
                .GroupBy(x => x.Seller.Departament) // Agrupa os resultados pelo departamento do vendedor.
                .ToListAsync(); // Converte a consulta para uma lista de forma assíncrona.
        }
    }
}
