using BloodTypes.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BloodTypes.Infrastructure
{
    public static class DbSeeder
    {
        public async static void Seed(CassandraDbContext context)
        {
            await AddPeople(context);
            await AddBloodAmounts(context);
        }

        private static async Task AddPeople(CassandraDbContext context)
        {
            List<Person> people = new List<Person>();
            foreach (string line in File.ReadLines(@"..\BloodTypes.Infrastructure\SampleData\data.csv"))
            {
                string[] separated = line.Split(';');

                var usCulture = new CultureInfo("en-US");

                Person person = new Person();
                person.Gender = separated[0] == "female" ? Gender.Female : Gender.Male;
                person.Name = separated[1];
                person.Surname = separated[2];
                person.City = separated[3];
                person.Country = separated[4];
                string test = separated[5];
                person.Birthdate = DateTime.Parse(separated[5], usCulture);
                person.Telephone = separated[6];
                person.BloodType = separated[7];
                person.Weight = Double.Parse(separated[8], usCulture);
                person.Height = Double.Parse(separated[9], usCulture);

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

        private static async Task AddBloodAmounts(CassandraDbContext context)
        {
            await Task.Run(() =>
            {
                var cities = context.People.GetAll().Select(p => p.City).Distinct();
                List<BloodAmount> bloodAmounts = new List<BloodAmount>();

                Random random = new Random();

                foreach (string city in cities)
                {
                    bloodAmounts.Add(new BloodAmount
                    {
                        City = city,
                        Aplus = random.Next(10, 150),
                        Aminus = random.Next(10, 150),
                        Bplus = random.Next(10, 150),
                        Bminus = random.Next(10, 150),
                        ABplus = random.Next(10, 150),
                        ABminus = random.Next(10, 150),
                        Oplus = random.Next(10, 150),
                        Ominus = random.Next(10, 150)
                    });
                }

                foreach (var item in bloodAmounts)
                {
                    context.BloodAmounts.Add(item);
                }
            });
        }
    }
}
