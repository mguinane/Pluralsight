using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MessageBoard.Data
{
    public class MessageBoardMigrationsConfiguration : DbMigrationsConfiguration<MessageBoardContext>
    {
        public MessageBoardMigrationsConfiguration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;

        }

        protected override void Seed(MessageBoardContext context)
        {
            base.Seed(context);

#if DEBUG
            if (context.Topics.Count() == 0)
            {
                var topic = new Topic()
                {
                    Title = "I love MVC",
                    Body = "I really, really love it man!",
                    Created = DateTime.Now,
                    Replies = new List<Reply>()
                    {
                        new Reply()
                        {
                            Body = "Me too man!",
                            Created = DateTime.Now
                        },
                        new Reply()
                        {
                            Body = "And me goddamnit",
                            Created = DateTime.Now
                        },
                        new Reply()
                        {
                            Body = "Nah hate it",
                            Created = DateTime.Now
                        }
                    }
                };

                context.Topics.Add(topic);

                var anotherTopic = new Topic()
                {
                    Title = "I like Ruby too",
                    Body = "RoR is god stuff innit",
                    Created = DateTime.Now
                };

                context.Topics.Add(anotherTopic);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
#endif
        }
    }
}