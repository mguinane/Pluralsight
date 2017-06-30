using System;

namespace MessageBoard.Data
{
    public class Reply
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public int TopicId { get; set; }
    }
}