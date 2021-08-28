using System.Collections;
using System.Collections.Generic;
using System;
using FluentAssertions;
using Src.Models;
using Xunit;
using Moq;
using Src.Repositories;
using Src.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Test
{
    public class AuthorControllerTest
    {
        private readonly IList<Author> Authors = new List<Author>{
            new Author{Id =1, Username="Jonh Doe", Email="jonhdoe@gmail.com", BirthDate = DateTime.Parse("1995-06-02")},
            new Author{Id =2, Username="Jane Doe", Email="janedoe@gmail.com", BirthDate = DateTime.Parse("1996-06-02")},
            new Author{Id =3, Username="Will Doe", Email="willdoe@gmail.com", BirthDate = DateTime.Parse("1997-06-02")}
        };

        [Fact]
        public async Task CanGetAllAuthorsAsync()
        {
            //Given
            var repo = new Mock<IGenericRepository<Author>>();
            repo.Setup(x => x.GetAllAsync()).ReturnsAsync(Authors);
            //When
            var controller = new AuthorController(repo.Object);
            var result = await controller.GetAsync();
            //Then
            result.Should().BeOfType<ActionResult<IList<Author>>>("Because a response is expect", typeof(IList));

            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().Be(Authors);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task CanGetAuthorsById(int id)
        {
            //Given
            var repo = new Mock<IGenericRepository<Author>>();
            repo.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(Authors.Where(a => a.Id == id).First());
            //When
            var controller = new AuthorController(repo.Object);
            var result = await controller.GetById(id);
            //Then
            result.Should().BeOfType<ActionResult<Author>>("Because is a single object", typeof(Author));

            result.Value.Should().Be(Authors.Where(a => a.Id == id).First());
        }

    }
}