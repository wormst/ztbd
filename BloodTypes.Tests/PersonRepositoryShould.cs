using Xunit;
using Cassandra;
using Moq;
using BloodTypes.Infrastructure;
using BloodTypes.Core.Models;
using System.Collections.Generic;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BloodTypes.Tests
{
    public class PersonRepositoryShould
    {
        public PersonRepositoryShould()
        {
            ISession session = new Mock<ISession>().Object;
        }

        [Fact]
        public void AddPersonProperly()
        {
            //arrange
            var mock = new Mock<ISession>();
            mock.Setup(s => s.Execute(It.IsAny<string>())).Returns(new RowSet());
            PersonRepository personRepository = new PersonRepository(mock.Object);

            //act
            bool result = personRepository.Add(new Person
            {
                Name = "andrzej",
                Birthdate = DateTime.Parse("12.02.1959"),
                BloodType = "A+",
                City = "Mozliwe",
                Country = "Ze tu",
                Gender = Gender.Male,
                Height = 184.0,
                Surname = "andrzej",
                Telephone = "2312312312",
                Weight = 99
            });

            //assert
            Assert.True(result); //should return false because RowSet is empty; this should be fixed
        }

        [Fact]
        public void RemovePersonProperly()
        {
            //arrange
            var mock = new Mock<ISession>();
            mock.Setup(s => s.Execute(It.IsAny<string>())).Returns(new RowSet());
            PersonRepository personRepository = new PersonRepository(mock.Object);

            Person item = new Person();
            personRepository.Add(item);

            //act
            bool result = personRepository.Remove(item);

            //assert
            Assert.True(result);
        }

        //TODO add more tests
    }
}
