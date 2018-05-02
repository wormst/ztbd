using Xunit;
using Cassandra;
using Moq;
using BloodTypes.Infrastructure;
using BloodTypes.Core.Models;

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
            Assert.True(result);
        }

        //TODO add more tests
    }
}
