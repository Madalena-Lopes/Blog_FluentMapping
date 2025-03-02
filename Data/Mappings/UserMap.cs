﻿using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Tabela
            builder.ToTable("User"); 

            //Chave Primária
            builder.HasKey(x => x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            //Propriedades
            builder.Property(x => x.Name)
                .IsRequired() 
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Bio)
                .IsRequired()
                .HasColumnName("Bio")
                .HasColumnType("TEXT");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(200);

            builder.Property(x => x.Image)
                .IsRequired()
                .HasColumnName("Image")
                .HasColumnType("VARCHAR")
                .HasMaxLength(2000);

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.GitHub);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            //Indices
            builder.HasIndex(x => x.Slug, "IX_User_Slug") 
                .IsUnique();

            builder.HasIndex(x => x.Email, "IX_User_Email") 
                .IsUnique();


            //Relacionamentos de muitos para muitos
            //UsingEntity - gera uma tabela virtual com base no Dictionary
            builder
                .HasMany(x => x.Roles)  //1 User tem n Roles (perfis)
                .WithMany(x => x.Users) // e 1 Role tem n Users
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    role => role
                        .HasOne<Role>() //role tem 1 Role
                        .WithMany() //com n Users
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRole_RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    user => user
                        .HasOne<User>() //user tem 1 User
                        .WithMany() //com n Roles?????
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRole_UserId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
