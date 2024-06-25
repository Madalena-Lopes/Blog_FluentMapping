using Blog.Data.Mappings;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<PostWithTagsCount> PostsWithTagsCount { get; set; } //esta tabela não existe

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap()); 
            modelBuilder.ApplyConfiguration(new PostMap());

            //Mapeando querys puras e views
            modelBuilder.Entity<PostWithTagsCount>(x => 
            {
                //aqui podia ter uma view para a query - melhor q ter este código todo aqui
                //[não sei se as views entram nas migrations?...]
                //importante manter os nomes q dei na class PostWithTagsCount com alias no SQL se necessário
                x.ToSqlQuery(@"
                    SELECT [Post].[Title] as [Name],
                        COUNT([PostTag].[PostId]) as [Count]
                    FROM [Post]
                    INNER JOIN [PostTag]
                    ON [Post].[Id] = [PostTag].[PostId]
                    GROUP BY [Post].[Title]").HasNoKey(); //HasNoKey para não me obrigar a definir uma key 
            });
        }
    }
}