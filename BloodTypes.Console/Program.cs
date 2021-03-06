﻿using BloodTypes.Core.Models;
using BloodTypes.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;

namespace BloodTypes.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<Person> people = new List<Person>();
                foreach (string line in File.ReadLines(@"C:\Users\wormst\Desktop\data.csv"))
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

                CassandraDbContext dbContext = new CassandraDbContext();
                foreach (var item in people)
                {
                    dbContext.People.Add(item);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
