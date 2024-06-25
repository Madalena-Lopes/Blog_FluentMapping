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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //Fluent Mapping - fluent API

            //Tabela
            builder.ToTable("Category"); //[Table("Category")] - com as data anotations

            //Chave Primária
            builder.HasKey(x => x.Id);
            
            //Identity - necessário se quiser fazer migrations! BD gerada pelo programa.
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(); //PRIMARY KEY IDENTITY(1,1) 

            //Propriedades
            builder.Property(x => x.Name)
                .IsRequired() //NOT NULL
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);


            //Indices
            builder.HasIndex(x => x.Slug, "IX_Category_Slug") //propriedade q vai receber o indice e o nome do indice
                .IsUnique(); //único para não poder criar 2 categorias com o mesmo slug

        }
    }
}
