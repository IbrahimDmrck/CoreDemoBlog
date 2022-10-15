using DataAccess.Abstract;
using DataAccess.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMessage2Repository : GenericRepository<Message2>, IMessage2Dal
    {
        public List<Message2> GetInboxWithMessageByWriter(int id)
        {
            using (var context = new BlogContext())
            {
                return context.Message2s.Include(p => p.SenderUser).Where(x => x.ReceiverID == id).ToList();
            }
        }

        public List<Message2> GetSendboxWithMessageByWriter(int id)
        {
            using (var context = new BlogContext())
            {
                return context.Message2s.Include(p => p.ReceiverID).Where(x => x.SenderID == id).ToList();
            }
        }
    }
}
