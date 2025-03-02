using System.Collections;
using System.Collections.Generic;

namespace Blog.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public string Bio { get; set; }
        public string GitHub { get; set; }

        public IList<Post> Posts { get; set; } //publicações q o user escreveu
        public IList<Role> Roles { get; set; } //lista de perfis a q ele pertence
    }
}