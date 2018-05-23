using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodTypes.Core.Interfaces;
using BloodTypes.Core.Models;
using Cassandra;

namespace BloodTypes.Infrastructure.Repositories
{
    public class BloodAmountRepository : IRepository<BloodAmount>
    {
        private ISession session;

        public BloodAmountRepository(ISession session)
        {
            this.session = session;
        }

        //TODO
        public bool Add(BloodAmount item)
        {
            throw new NotImplementedException();
        }

        public bool AddMany(IEnumerable<BloodAmount> items)
        {
            throw new NotImplementedException();
        }

        public BloodAmount Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BloodAmount> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(BloodAmount item)
        {
            throw new NotImplementedException();
        }

        public bool Update(BloodAmount item)
        {
            throw new NotImplementedException();
        }
    }
}
