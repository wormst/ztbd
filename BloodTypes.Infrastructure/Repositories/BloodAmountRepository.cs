using System;
using System.Collections.Generic;
using System.Linq;
using BloodTypes.Core.Interfaces;
using BloodTypes.Core.Models;
using Cassandra;

namespace BloodTypes.Infrastructure.Repositories
{
    public class BloodAmountRepository : IRepository<BloodAmount>
    {
        private ISession session;
        private readonly string tableName = "bloodAmounts";

        public BloodAmountRepository(ISession session)
        {
            this.session = session;
        }

        public bool Add(BloodAmount item)
        {
            try
            {
                var row = this.session.Execute($"INSERT INTO {tableName} (id, type, amount) " +
                    $"VALUES(uuid(), '{item.Type}', {item.Amount});");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddMany(IEnumerable<BloodAmount> items)
        {
            throw new NotImplementedException();
        }

        public BloodAmount Get(string id)
        {
            return ConvertRowToBloodAmount(session.Execute($"SELECT * FROM {tableName} " +
                $"WHERE id = {id};").FirstOrDefault());
        }

        public IEnumerable<BloodAmount> GetAll()
        {
            RowSet people = session.Execute($"SELECT * FROM {tableName}");
            return people.Select(row => ConvertRowToBloodAmount(row));
        }

        public bool Remove(BloodAmount item)
        {
            try
            {
                var row = this.session.Execute($"DELETE FROM {tableName} WHERE id = {item.Id} IF EXISTS");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(BloodAmount item)
        {
            Row result = this.session.Execute($"UPDATE {tableName} " +
                $"SET amount = {item.Amount}" +
                $"WHERE id = {item.Id} IF EXISTS;").FirstOrDefault();

            if (result.Count() > 1 && bool.TryParse(result[0].ToString(), out bool value))
            {
                return value;
            }
            return false;
        }

        private BloodAmount ConvertRowToBloodAmount(Row row)
        {
            if (row == null)
                return null;

            return new BloodAmount
            {
                Id = row["id"].ToString(),
                Type = row["type"].ToString(),
                Amount = row["amount"] != null ? Double.Parse(row["amount"].ToString()) : 0
            };
        }
    }
}
