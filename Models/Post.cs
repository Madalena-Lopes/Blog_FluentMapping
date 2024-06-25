using System;
using System.Collections.Generic;

namespace Blog.Models
{
    //1 post possui n tags
    //1 tag pode estar em n posts
    public class Post
    {
        public int Id { get; set; }
        //public int CategoryId { get; set; }
        //public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        //estas 2 propriedades tem relações de 1 para muitos
        public Category Category { get; set; } //1 Category tem muitos Posts
        public User Author { get; set; } //1 Author tem muitos Posts

        //esta propriedade tem relação de muitos para muitos 
        public List<Tag> Tags { get; set; }
    }
}