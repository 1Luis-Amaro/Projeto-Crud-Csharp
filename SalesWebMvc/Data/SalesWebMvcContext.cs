// Importação dos namespaces necessários para o código
using System; // Importa classes e tipos básicos do .NET, como DateTime, String, etc.
using System.Collections.Generic; // Necessário para trabalhar com coleções, como List, Dictionary, etc.
using System.Linq; // Usado para consultas LINQ, que permitem fazer operações de consulta em coleções.
using System.Threading.Tasks; // Necessário para operações assíncronas com tarefas (Task).
using Microsoft.EntityFrameworkCore; // Fornece classes e métodos necessários para interagir com o Entity Framework Core, permitindo o acesso ao banco de dados.

namespace SalesWebMvc.Models // Define o namespace do código, que organiza a estrutura do projeto.
{
    // A classe SalesWebMvcContext herda de DbContext e representa o contexto de banco de dados do aplicativo.
    // Ela é usada para interagir com o banco de dados e manipular as tabelas e entidades associadas.
    public class SalesWebMvcContext : DbContext
    {
        // O construtor recebe DbContextOptions como argumento, que permite configurar o contexto de banco de dados.
        // O construtor passa essas opções para a classe base DbContext para que o Entity Framework possa ser configurado adequadamente.
        public SalesWebMvcContext(DbContextOptions<SalesWebMvcContext> options)
            : base(options) // Passa as opções para a classe base (DbContext).
        {
        }

        // Definição de propriedades DbSet, que representam as tabelas no banco de dados.
        // DbSet é usado pelo Entity Framework para mapear as classes de modelo para as tabelas do banco de dados.
        // Cada DbSet corresponde a uma tabela no banco de dados e permite realizar operações de CRUD (Create, Read, Update, Delete).

        public DbSet<Departament> Departament { get; set; } // Representa a tabela de Departamentos no banco de dados.
        public DbSet<Seller> Seller { get; set; } // Representa a tabela de Vendedores (Sellers) no banco de dados.
        public DbSet<SalesRecord> SalesRecord { get; set; } // Representa a tabela de Registros de Vendas (SalesRecord) no banco de dados.
    }
}
