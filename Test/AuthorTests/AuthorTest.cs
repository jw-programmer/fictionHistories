using System;
using Src.Models;
using Xunit;
using FluentAssertions;
using System.Text.Json;

namespace Test.AuthorTests
{
    public class AuthorTest
    {
        [Theory]
        [InlineData("2013-06-01", 17)]
        [InlineData("2013-06-02", 18)]
        public void CanGetAgeByBithDate(string birthDate, int expectAge)
        {
        //Given
        Author author = new() { Id =1, Username="Jonh Doe", Email="jonhdoe@gmail.com", BirthDate = DateTime.Parse("1995-06-02")};
        //When
        var  age = author.CalcAge(DateTime.Parse(birthDate));
        //Then
        age.Should().Be(expectAge);
        }

        [Fact]
        public void CanSeriealizewithAge()
        {
        //Given
        Author author = new() { Id =1, Username="Jonh Doe", Email="jonhdoe@gmail.com", BirthDate = DateTime.Parse("1995-06-02")};
        //When
        string stringAuthor = JsonSerializer.Serialize<Author>(author);
        //Then
        stringAuthor.Should().Contain("\"Age\"");
        }
    }
}