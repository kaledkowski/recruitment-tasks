using todolist.Controllers;
using todolist.Domain.Model;
using todolist.Domain.Repository;
using todolist.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Xunit;

namespace Tests.UnitTests
{
    public class ApiControllerTests
    {
        [Fact]
        public void ShouldReturnListOfCards()
        {
            // Arrange
            var cards = new List<Card>()
            {
                new Card() { Id = Guid.NewGuid() },
                new Card() { Id = Guid.NewGuid() },
                new Card() { Id = Guid.NewGuid() }
            };

            var repository = Substitute.For<ICardRepository>();
            repository.GetCards().Returns(cards);

            // SUT
            var sut = new ApiController(repository);

            // Act
            var result = sut.Card() as JsonResult;

            // Assert
            Assert.Equal(cards.Select(x => x.Id), (result.Data as IEnumerable<CardModel>).Select(x => x.Id));
            repository.Received().GetCards();
        }

        [Fact]
        public void ShouldCreateANewCard()
        {
            // Arrange
            const string title = "fake title";
            const string content = "fake content";

            var repository = Substitute.For<ICardRepository>();

            // SUT
            var sut = new ApiController(repository);

            // Act
            var result = sut.AddCard(new CardModel() { Title = title, Content = content }) as JsonResult;

            // Assert
            var newCardId = (Guid)result.Data;

            Assert.NotEqual(Guid.Empty, newCardId);
            repository.Received().SaveOrUpdate(Arg.Is<Card>(x => x.Title == title && x.Content == content));
        }

        [Fact]
        public void ShouldDeleteACard()
        {
            // Arrange
            var id = Guid.NewGuid();
            var repository = Substitute.For<ICardRepository>();

            // SUT
            var sut = new ApiController(repository);

            // Act
            var result = sut.Card(id) as HttpStatusCodeResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            repository.Received().DeleteBy(id);
        }

        [Fact]
        public void ShouldUpdateACard()
        {
            // Arrange
            var model = new CardModel
            {
                Id = Guid.NewGuid(),
                Title = "fake title",
                Content = "fake content"
            };

            var repository = Substitute.For<ICardRepository>();
            repository.GetBy(model.Id).Returns(new Card { Id = model.Id, Title = model.Title, Content = model.Content });

            // SUT
            var sut = new ApiController(repository);

            // Act
            var result = sut.UpdateCard(model) as HttpStatusCodeResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            repository.Received().SaveOrUpdate(Arg.Is<Card>(x => x.Id == model.Id && x.Title == model.Title && x.Content == model.Content));
        }

        [Fact]
        public void ShouldReturn404WhenCardUpdateNotPossible()
        {
            // Arrange
            var model = new CardModel
            {
                Id = Guid.NewGuid(),
                Title = "fake title",
                Content = "fake content"
            };

            var repository = Substitute.For<ICardRepository>();
            repository.GetBy(model.Id).Returns((Card)null);

            // SUT
            var sut = new ApiController(repository);

            // Act
            var result = sut.UpdateCard(model) as HttpStatusCodeResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
