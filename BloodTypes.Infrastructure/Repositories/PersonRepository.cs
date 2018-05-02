using BloodTypes.Core.Interfaces;
using BloodTypes.Core.Models;
using Cassandra;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodTypes.Infrastructure
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly ISession session;

        public PersonRepository(ISession session)
        {
            this.session = session;
        }

        public bool Add(Person item)
        {
            var result = this.session.Execute($"INSERT INTO people " +
                $"(id, name, surname) " +
                $"VALUES (uuid(), '{item.Name}', '{item.Surname}');");

            return result.Any();
        }

        public bool AddMany(IEnumerable<Person> items)
        {
            StringBuilder statement = new StringBuilder(items.Count() + 2);

            statement.Append("BEGIN BATCH");
            foreach (Person item in items)
            {
                statement.Append($"INSERT INTO people " +
                    $"(id, name, surname) " +
                    $"VALUES (uuid(), '{item.Name}', '{item.Surname}');");
            }
            statement.Append("APPLY BATCH;");

            this.session.Execute(statement.ToString());
            return true; //TODO get real result
        }

        public bool Delete(Person item)
        {
            this.session.Execute($"DELETE FROM people WHERE id = {item.Id} IF EXISTS");
            return true;
        }

        public Person Get(string id)
        {
            return ConvertRowToPerson(session.Execute($"SELECT * FROM people " +
                $"WHERE id = {id};").FirstOrDefault());
        }

        public IEnumerable<Person> GetAll()
        {
            RowSet people = session.Execute("SELECT * FROM people");
            return people.Select(row => ConvertRowToPerson(row));
        }

        public bool Update(Person item)
        {
            Row result = session.Execute($"UPDATE people " +
                $"SET name = '{item.Name}', surname = '{item.Surname}'" +
                $"WHERE id = {item.Id} IF EXISTS;").FirstOrDefault();

            if(result.Count() > 1 && bool.TryParse(result[0].ToString(), out bool value))
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
                Surname = row["surname"].ToString()
            };
        }
    }
}
