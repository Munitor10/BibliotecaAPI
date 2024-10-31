using BibliotecaAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Data;

namespace BibliotecaAPI.Repository
{
    public class BibliotecaRepository
    {
        private readonly string _connectionString;

        public BibliotecaRepository(string connectionString) => _connectionString = connectionString;
        private IDbConnection Connection =>
            new MySqlConnection(_connectionString);


        public async Task<IEnumerable<Livros>> ListarLivros()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Livros;";

                return await conn.QueryAsync<Livros>(sql);
            }
        }

        public async Task<int> CadastrarLivro(Livros dados)
        {
            using (var conn = Connection)
            {
                var sql = "insert into Livros (Titulo,Autor,AnoPublicacao,Genero,Disponivel) values ('@Titulo','@Autor','@AnoPublicacao','@Genero','@Disponivel');";

                return await conn.ExecuteAsync(sql, new { Titulo = dados.Titulo, Autor = dados.Autor, AnoPublicacao = dados.AnoPublicacao, Genero = dados.Genero, Disponivel = dados.Disponivel });

            }
            ////  
            //   {

            //   }


        }

        public async Task<int> Atualizar(Livros dados)
        {
            var sql = "UPDATE Livros set Titulo = @Titulo, Autor = @Autor, AnoPublicacao = @AnoPublicacao, Genero = @Genero WHERE Id = @id";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, dados);
            }
        }

        public async Task<Livros> BuscarPorId(int id)
        {
            var sql = "SELECT * FROM Livros WHERE Id = @Id";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Livros>(sql, new { Id = id });
            }
        }

        public async Task<int> DeletarPorId(int id)
        {
            var sql = "DELETE FROM Livros WHERE Id = @Id";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<int> CadastrarUsuario(Usuarios dados)
        {
            using (var conn = Connection)
            {
                var sql = "insert into Usuarios (Nome,Email) values ('@Nome','@Email');";

                return await conn.ExecuteAsync(sql, new { Nome = dados.Nome, Email = dados.Email });

            }
        }

        public async Task<int> CadastrarEmprestimo(Emprestimo dados)
        {
            using (var conn = Connection)
            {
                var sql = "insert into Emprestimo (LivroId,UsuarioId,DataEmprestimo,DataDevolucao,Taxa) values ('@LivroId','@UsuarioId','@DataEmprestimo','@DataDevolucao','@Taxa');";

                return await conn.ExecuteAsync(sql, new { LivroId = dados.LivroId, UsuarioId = dados.UsuarioId, DataEmprestimo = dados.DataEmprestimo, DataDevolucao = dados.DataDevolucao, Taxa = dados.Taxa });
            }
        }

        public async Task<int> CadastrarDevolucao(Devolucoes dados)
        {
            using (var conn = Connection)
            {
                var sql = "insert into Devolucoes (NomeLivro,Devolucao) values ('@NomeLivro','@Devolucao');";

                return await conn.ExecuteAsync(sql, new { NomeLivro = dados.NomeLivro, Devolucao = dados.Devolucao });
            }
        }

        public async Task<IEnumerable<Emprestimo>> ListarEmprestimo()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Emprestimo;";

                return await conn.QueryAsync<Emprestimo>(sql);
            }

        }
        public async Task<int> ListarLivrosOcupadasDB()
        {
            using (var conn = Connection)
            {
                var sql = "select count(*) from Livros where Ocupada = 1";              

                return await conn.QueryFirstOrDefaultAsync<int>(sql);

            }
        }

        public async Task<IEnumerable<Livros>> BuscarPor()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT AnoPublicacao,Genero,Disponivel FROM Livros";

                return await conn.QueryAsync<Livros>(sql);
            }
        }

        public async Task<IEnumerable<Usuarios>> BuscarUsuarioPor()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT Nome,Email FROM Usuarios";

                return await conn.QueryAsync<Usuarios>(sql);
            }
        }
    }
}


//select count(*) from Vagas where Ocupada = 1