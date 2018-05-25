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
        private readonly string tableName = "cities";

        public BloodAmountRepository(ISession session)
        {
            this.session = session;
        }

        public bool Add(BloodAmount item)
        {
            try
            {
                var row = this.session.Execute($"INSERT INTO {this.tableName} (id, name, aPlus, aMinus, bPlus, bMinus, abPlus, abMinus, oPlus, oMinus) " +
                    $"VALUES(uuid(), '{item.City}', {item.Aplus}, {item.Aminus}, {item.Bplus}, {item.Bminus}, " +
                    $"{item.ABplus}, {item.ABminus}, {item.Oplus}, {item.Ominus});");

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
            return ConvertRowToBloodAmount(this.session.Execute($"SELECT * FROM {this.tableName} " +
                $"WHERE id = {id};").FirstOrDefault());
        }

        public IEnumerable<BloodAmount> GetAll()
        {
            RowSet people = this.session.Execute($"SELECT * FROM {this.tableName}");
            return people.Select(row => ConvertRowToBloodAmount(row));
        }

        public bool Remove(BloodAmount item)
        {
            try
            {
                var row = this.session.Execute($"DELETE FROM {this.tableName} WHERE id = {item.Id} IF EXISTS");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(BloodAmount item)
        {
            Row result = this.session.Execute($"UPDATE {this.tableName} " +
                $"SET amount = {item.City}" +
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
                City = row["name"].ToString(),
                Aplus = row["aplus"] != null ? Int32.Parse(row["aplus"].ToString()) : 0,
                Aminus = row["aminus"] != null ? Int32.Parse(row["aminus"].ToString()) : 0,
                Bplus = row["bplus"] != null ? Int32.Parse(row["bplus"].ToString()) : 0,
                Bminus = row["bminus"] != null ? Int32.Parse(row["bminus"].ToString()) : 0,
                ABplus = row["abplus"] != null ? Int32.Parse(row["abplus"].ToString()) : 0,
                ABminus = row["abminus"] != null ? Int32.Parse(row["abminus"].ToString()) : 0,
                Oplus = row["oplus"] != null ? Int32.Parse(row["oplus"].ToString()) : 0,
                Ominus = row["ominus"] != null ? Int32.Parse(row["ominus"].ToString()) : 0,
            };
        }
    }
}
