using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodTypes.Core.Models;
using Cassandra;

namespace BloodTypes.Infrastructure.Repositories
{
    public class BloodAmountRepository
    {
        private ISession session;

        public BloodAmountRepository(ISession session)
        {
            this.session = session;
        }

        public IEnumerable<BloodAmount> GetAll()
        {

        }
    }
}
