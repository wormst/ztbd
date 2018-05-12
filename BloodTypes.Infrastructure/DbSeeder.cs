using BloodTypes.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BloodTypes.Infrastructure
{
    public static class DbSeeder
    {
        public async static void Seed(CassandraDbContext context)
        {
            List<Person> people = new List<Person>();
            foreach (string line in File.ReadLines(@"C:\Users\rxft84\Desktop\studia12maja\BloodTypesZTBD\BloodTypes.Infrastructure\SampleData\data.csv"))
            {
                string[] separated = line.Split(';');

                Person person = new Person();
                person.Gender = separated[0] == "female" ? Gender.Female : Gender.Male;
                person.Name = separated[1];
                person.Surname = separated[2];
                person.City = separated[3];
                person.Country = separated[4];
                string test = separated[5];
                person.Birthdate = DateTime.Parse(separated[5]);
                person.Telephone = separated[6];
                person.BloodType = separated[7];
                person.Weight = Double.Parse(separated[8]);
                person.Height = Double.Parse(separated[9]);

                people.Add(person);
            }

            await Task.Run(() =>
            {
                foreach (var item in people)
                {
                    context.People.Add(item);
                }
            });
        }
    }
}
