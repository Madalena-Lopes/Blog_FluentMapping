using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog
{
    class Program
    {
        /* este Main aqui é 1 método syncrono
         * static void Main(string[] args)
        {
            //Testar relacionamentos
            using var context = new BlogDataContext();

            //context.Users.Add(new User 
            //{
            //    Name = "Madalena Lopes",
            //    Slug = "madalena-lopes",
            //    Email = "email@gmail.com",
            //    Bio = "LMCC",
            //    Image = "https://balta.io",
            //    PasswordHash = "1234"
            //});
            //context.SaveChanges();

            //var user = context.Users.FirstOrDefault();
            //var post = new Post {
            //    Author = user,
            //    Body = "Meu artigo",
            //    Category = new Category { 
            //        Name = "FullStack",
            //        Slug = "fullstack"
            //    },
            //    CreateDate = DateTime.Now,
            //    //LastUpdateDate=
            //    Slug = "meu-artigo",
            //    Summary = "Neste artigo vamos..",
            //    //Tags = null,
            //    Title = "Meu artigo",
            //};

            //context.Posts.Add(post);
            //context.SaveChanges();

            //Ver erro do campo GitHub q ainda não está criado na BD
            //Com migrations a BD tem q estar sempre atualizada com o código!
            var user = new User
            {
                Name = "André Baltieri",
                Slug = "andre-baltieri",
                Email = "a@a.com",
                Bio = "a",
                Image = "https://",
                PasswordHash = "123",
                GitHub = "andrebaltieri"
            };

            context.Users.Add(user);
            context.SaveChanges();
        }
        */

        //Task - para permitir executar tarefas em paralelo
        //async - posso executar as instruções por partes
        /*static async Task Main(string[] args)
        {
            using var context = new BlogDataContext();

            //FirstOrDefault - retorna um Post
            //var post = context.Posts.FirstOrDefault( x=> x.Id == 1);

            //FirstOrDefaultAsync - retorna um Task<Post>
            //var post = context.Posts.FirstOrDefaultAsync(x => x.Id == 1);
           
            //Console.WriteLine("Teste"); //vai executar em paralelo. Não vai esperar q a tarefa de cima termine

            //---------------------
            //AWAIT - para aguardar
            var posts = await context.Posts.ToListAsync(); //vai buscar os posts
            var tags = await  context.Users.ToListAsync(); //não vai esperar e vai buscarbos tags
                                                           // apesar do Main ser async e nas linhas acima ter await, o método continua a ser asyncronio
                                                           // pq vai continuar a executar as tarefas em paralelo. Contudo vai esperar pelo resultado.  

            //pots.wait() - outra forma em vez de usar await como acima. E aqui pode-se configurar

            //var posts = await GetPosts(context);
            Console.WriteLine("Feito");
            //foreach (var post in posts)
            //{
            //    Console.WriteLine($"{post.Id} - {post.Title}"); //não imprime nada
            //}

            //Eager Loding vs LAsy Loading
            //Skip(pular), Take(pegar) e Paginação de dados
            var posts1 = GetPosts(context, 0, 25);
            //var posts1 = GetPosts(context, 25 25);
            //var posts1 = GetPosts(context, 50, 25);
            //var posts1 = GetPosts(context, 75, 25);

            //ThenInclude - Usar com moderação! Ou fazer antes query manual.
            //var posts2 = context.Posts
            //    .Include(x => x.Author)
            //        .ThenInclude(x => x.Roles)//desce 1 nivel. Executa um SUBSELECT. Tomar muito cuidado com a performance na BD. Provavelmente será melhor uma query manual.
            //    .Include(x => x.Category);
            //foreach (var post in posts2)
            //{
            //    foreach (var tag in post.Tags)
            //    { 
            //    }
            //}

            //Mapeando querys puras e views
        }*/

        static void Main(string[] args)
        {
            using var context = new BlogDataContext();

            var posts = context.PostsWithTagsCount
                .AsNoTracking()
                .ToList();
            Console.WriteLine("Nome do Post - Nº tags associadas:");
            foreach (var item in posts)
            {
                Console.WriteLine($"{item.Name} - {item.Count}");
            }

            //agora vamos usar o ef com asp.net, apis
        }



        static async Task<IEnumerable<Post>> GetPosts(BlogDataContext context)
        { 
            return await context.Posts.ToListAsync();
        }

        static List<Post> GetPosts(BlogDataContext context, int skip = 0, int take = 25)
        {
            var posts = context
                .Posts
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .ToList();//sem as 3 linhas acima ia buscar todos os registos. Se fosse 1 milhão estava tramada.
            return posts;
        }
    }
}
