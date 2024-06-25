using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //Tabela
            builder.ToTable("Post");

            //Chave Primária
            builder.HasKey(x => x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            //Propriedades
            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);

            builder.Property(x => x.Summary)
                .IsRequired()
                .HasColumnName("Summary")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.Body)
                .IsRequired()
                .HasColumnName("Body")
                .HasColumnType("TEXT");            

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.CreateDate)
                .IsRequired()
                .HasColumnName("CreateDate")
                .HasColumnType("SMALLDATETIME")            
                .HasDefaultValueSql("GETDATE()");
                //.HasDefaultValue(DateTime.Now.ToUniversalTime()); //este datetime é 1 struct -> por defeito é 01/01/1900 - nunca é vazio! A não ser q o coloque nulable -> i.e. colocar com ?
                //esta ultima é o datetime do .NET q o Balta particularmente recomenda

            builder.Property(x => x.LastUpdateDate)
                .IsRequired()
                .HasColumnName("LastUpdateDate")
                .HasColumnType("SMALLDATETIME")              
                .HasDefaultValueSql("GETDATE()");

            //Indices
            builder.HasIndex(x => x.Slug, "IX_Post_Slug")
                .IsUnique();

            //Relacionamentos
            //Relacionamentos de 1 para muitos
            builder.HasOne(x => x.Author)
                .WithMany(x => x.Posts) //e esse Author tem muitos Posts
                .HasConstraintName("FK_Post_Author")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Posts) //e essa Categoria tem muitos Posts
                .HasConstraintName("FK_Post_Category")
                .OnDelete(DeleteBehavior.Cascade);

            //Relacionamentos de muitos para muitos 
            //UsingEntity - gera uma tabela virtual com base no Dictionary
            /*Sempre que temos um relacionamento muitos para muitos, criamos uma terceira tabela, 
              chamada tabela associativa e as chaves primárias vão para esta tabela, fazendo assim 
              o relacionamento.
              Então no fim, temos uma terceira tabela com dois relacionamentos um para um.*/
            builder.HasMany(x => x.Tags) //Post tem n Tags
                .WithMany(x => x.Posts)//e 1 Tag tem n Posts - Posts de Tag
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",
                    post => post.HasOne<Tag>() //o post tem 1 Tag
                        .WithMany() //e essa Tag tem n Posts
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostTag_PostId")
                        .OnDelete(DeleteBehavior.Cascade),
                    tag => tag.HasOne<Post>() //a tag tem 1 Post
                        .WithMany() //e esse Post tem n Tags
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_PostTag_TagId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
