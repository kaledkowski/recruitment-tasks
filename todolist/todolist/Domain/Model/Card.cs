using System;

namespace todolist.Domain.Model
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}