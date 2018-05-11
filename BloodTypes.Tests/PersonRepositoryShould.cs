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
            bool result = personRepository.Add(new Person());

            //assert
            Assert.False(result); //should return false because RowSet is empty; this should be fixed
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
            Assert.False(result);
        }

        //TODO add more tests
    }
}
