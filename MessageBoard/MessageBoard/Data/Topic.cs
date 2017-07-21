using System;
using System.Collections.Generic;

namespace MessageBoard.Data
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public bool Flagged { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}