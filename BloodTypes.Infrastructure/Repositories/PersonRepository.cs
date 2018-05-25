using BloodTypes.Core.Interfaces;
using BloodTypes.Core.Models;
using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodTypes.Infrastructure
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly ISession session;
        private readonly string tableName = "people";

        public PersonRepository(ISession session)
        {
            this.session = session;
        }

        public bool Add(Person item)
        {
            try
            {
                var row = this.session.Execute($"INSERT INTO {tableName} " +
                     $"(id, name, surname, city, country, birthday, telephone, bloodtype, weight, height) " +
                     $"VALUES (uuid(), '{item.Name}', '{item.Surname}', " +
                     $"'{item.City}', '{item.Country}', '{item.Birthdate.Value.Date.ToString("yyyy-MM-dd")}'," +
                     $"'{item.Telephone}', " +
                     $"'{item.BloodType}', {(int)item.Weight.Value}, {(int)item.Height.Value});");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddMany(IEnumerable<Person> items)
        {
            StringBuilder statement = new StringBuilder(items.Count() + 2);

            statement.Append("BEGIN BATCH");
            foreach (Person item in items)
            {
                statement.Append($"INSERT INTO {tableName} " +
                     $"(id, name, surname, city, country, birthday, telephone, bloodtype, weight, height) " +
                     $"VALUES (uuid(), '{item.Name}', '{item.Surname}', " +
                     $"'{item.City}', '{item.Country}', '{item.Birthdate.Value.Date.ToString("yyyy-MM-dd")}'," +
                     $"'{item.Telephone}', " +
                     $"'{item.BloodType}', {(int)item.Weight.Value}, {(int)item.Height.Value});");
            }
            statement.Append("APPLY BATCH;");

            var row = this.session.Execute(statement.ToString());
            bool.TryParse(row.FirstOrDefault()[0].ToString(), out bool result);
            return result;
        }

        public bool Remove(Person item)
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

        public Person Get(string id)
        {
            return ConvertRowToPerson(session.Execute($"SELECT * FROM {tableName} " +
                $"WHERE id = {id};").FirstOrDefault());
        }

        public IEnumerable<Person> GetAll()
        {
            RowSet people = session.Execute($"SELECT * FROM {tableName}");
            return people.Select(row => ConvertRowToPerson(row));
        }

        public bool Update(Person item)
        {
            Row result = session.Execute($"UPDATE {tableName} " +
                $"SET name = '{item.Name}', surname = '{item.Surname}'" +
                $"WHERE id = {item.Id} IF EXISTS;").FirstOrDefault();

            if (result.Count() > 1 && bool.TryParse(result[0].ToString(), out bool value))
            {
                return value;
            }
            return false;
        }

        private Person ConvertRowToPerson(Row row)
        {
            if (row == null)
                return null;

            return new Person
            {
                Id = row["id"].ToString(),
                Name = row["name"].ToString(),
                Surname = row["surname"].ToString(),
                City = row["city"].ToString(),
                Country = row["country"].ToString(),
                Birthdate = row["birthday"] != null ? DateTime.Parse(row["birthday"].ToString()) : DateTime.Now,
                Telephone = row["telephone"]?.ToString(),
                BloodType = row["bloodtype"]?.ToString(),
                Weight = row["weight"] != null ? Double.Parse(row["weight"].ToString()) : 0,
                Height = row["height"] != null ? Double.Parse(row["height"].ToString()) : 0
            };
        }
    }
}
