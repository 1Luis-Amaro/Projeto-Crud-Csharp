// Importa os namespaces necessários.
using SalesWebMvc.Models; // Contém as classes do modelo, como Seller e Departament.
using System; // Fornece tipos básicos, como Exception.
using System.Collections.Generic; // Necessário para trabalhar com coleções, como List.
using System.Linq; // Suporta consultas LINQ.
using System.Threading.Tasks; // Suporte para programação assíncrona com Task.
using Microsoft.EntityFrameworkCore; // Usado para manipulação de dados com o Entity Framework Core.
using SalesWebMvc.Services.Exceptions; // Contém exceções personalizadas usadas neste serviço.

namespace SalesWebMvc.Services // Define o namespace do serviço.
{
    // Define a classe `SellerService`, responsável por operações relacionadas a vendedores no sistema.
    public class SellerService
    {
        // Campo somente leitura para acessar o contexto do banco de dados.
        private readonly SalesWebMvcContext _context;

        // Construtor que recebe o contexto do banco de dados como dependência.
        public SellerService(SalesWebMvcContext context)
        {
            _context = context; // Inicializa o campo com o contexto recebido.
        }

        // Método para buscar todos os vendedores de forma assíncrona.
        public async Task<List<Seller>> FindAllAsync()
        {
            // Retorna todos os registros da tabela Seller como uma lista.
            return await _context.Seller.ToListAsync();
        }

        // Método para inserir um novo vendedor no banco de dados.
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj); // Adiciona o objeto Seller ao contexto.
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados de forma assíncrona.
        }

        // Método para buscar um vendedor pelo ID de forma assíncrona.
        public async Task<Seller> FindByIdAsync(int id)
        {
            // Busca um vendedor pelo ID e inclui o Departament relacionado na consulta.
            return await _context.Seller.Include(obj => obj.Departament).FirstOrDefaultAsync(obj => obj.id == id);
        }

        // Método para remover um vendedor pelo ID de forma assíncrona.
        public async Task RemoveAsync(int id)
        {
            try
            {
                // Busca o vendedor pelo ID.
                var obj = await _context.Seller.FindAsync(id);

                // Remove o objeto Seller do contexto.
                _context.Seller.Remove(obj);

                // Salva as alterações no banco de dados de forma assíncrona.
                await _context.SaveChangesAsync();
            }
            // Captura uma exceção do tipo DbUpdateException, que ocorre ao tentar remover um vendedor que possui dependências (como vendas).
            catch (DbUpdateException e)
            {
                // Lança uma exceção personalizada com uma mensagem explicativa.
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        // Método para atualizar os dados de um vendedor no banco de dados.
        public async Task UpdateAsync(Seller obj)
        {
            // Verifica se existe algum registro com o ID especificado.
            bool hasAny = await _context.Seller.AnyAsync(x => x.id == obj.id);

            // Se não encontrar o registro, lança uma exceção personalizada.
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                // Atualiza os dados do vendedor no contexto.
                _context.Update(obj);

                // Salva as alterações no banco de dados de forma assíncrona.
                await _context.SaveChangesAsync();
            }
            // Captura uma exceção de concorrência, que ocorre quando há conflito de versões no banco de dados.
            catch (DbUpdateConcurrencyException e)
            {
                // Lança uma exceção personalizada com a mensagem da exceção original.
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}



//Dependência SalesWebMvcContext:
//
//O contexto do Entity Framework Core é usado para interagir com o banco de dados. Ele permite realizar operações CRUD nas tabelas.
//Métodos CRUD:
//
//FindAllAsync: Retorna todos os registros de vendedores no banco.
//InsertAsync: Adiciona um novo vendedor ao banco.
//FindByIdAsync: Busca um vendedor pelo ID e inclui informações relacionadas ao departamento.
//RemoveAsync: Remove um vendedor do banco, tratando exceções quando ele possui dependências (vendas).
//UpdateAsync: Atualiza os dados de um vendedor, verificando se o registro existe e tratando exceções de concorrência.
//Programação Assíncrona:
//
//Todos os métodos utilizam async e await para evitar bloqueios no servidor enquanto as operações no banco de dados estão em andamento.
//Exceções Personalizadas:
//
//IntegrityException: Lançada quando há uma tentativa de remover um vendedor que possui dependências.
//NotFoundException: Lançada quando o ID fornecido para atualização não é encontrado.
//DbConcurrencyException: Lançada em caso de conflitos de concorrência ao atualizar registros.
//Boas Práticas:
//
//Validação do ID antes de operações críticas, como remoção e atualização.
//Uso de exceções personalizadas para melhorar a compreensão dos erros.
//Inclusão de relacionamentos na consulta com .Include para facilitar o acesso a dados relacionados (como o departamento).