using todolist.Domain.Model;
using todolist.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.IntegrationTests
{
    public class CardRepositoryTests
    {
        [Fact]
        public void ShouldDeleteItem()
        {
            // Arrange
            var id = Guid.NewGuid();
            var list = new List<Card>
            {
                new Card {Id = Guid.NewGuid()},
                new Card {Id = id},
                new Card {Id = Guid.NewGuid()},
            };

            // SUT
            var sut = new CardRepository(list);

            // Act
            sut.DeleteBy(id);

            // Assert
            Assert.Equal(2, list.Count);
            Assert.Null(list.FirstOrDefault(x => x.Id == id));
        }

        [Fact]
        public void ShouldNotDeleteIfItemNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var list = new List<Card>
            {
                new Card {Id = Guid.NewGuid()},
                new Card {Id = Guid.NewGuid()},
                new Card {Id = Guid.NewGuid()},
            };

            // SUT
            var sut = new CardRepository(list);

            // Act
            sut.DeleteBy(id);

            // Assert
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void ShouldReturnCard()
        {
            // Arrange
            var id = Guid.NewGuid();
            var list = new List<Card>
            {
                new Card {Id = Guid.NewGuid()},
                new Card {Id = id},
                new Card {Id = Guid.NewGuid()},
            };

            // SUT
            var sut = new CardRepository(list);

            // Act
            var card = sut.GetBy(id);

            // Assert
            Assert.Equal(id, card.Id);
        }

        [Fact]
        public void ShouldReturnNullIfNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var list = new List<Card>
            {
                new Card {Id = Guid.NewGuid()},
                new Card {Id = Guid.NewGuid()},
                new Card {Id = Guid.NewGuid()},
            };

            // SUT
            var sut = new CardRepository(list);

            // Act
            var card = sut.GetBy(id);

            // Assert
            Assert.Null(card);
        }

        [Fact]
        public void ShouldReturnCards()
        {
            // Arrange
            var list = new List<Card>
            {
                new Card {Id = Guid.NewGuid()},
                new Card {Id = Guid.NewGuid()},
                new Card {Id = Guid.NewGuid()},
            };

            // SUT
            var sut = new CardRepository(list);

            // Act
            var cards = sut.GetCards();

            // Assert
            Assert.Equal(list, cards);
        }

        [Fact]
        public void ShouldAddNewCard()
        {
            // Arrange
            var list = new List<Card>
            {
                new Card {Id = Guid.NewGuid()},
                new Card {Id = Guid.NewGuid()},
                new Card {Id = Guid.NewGuid()},
            };

            var newCard = new Card { Id = Guid.NewGuid() };

            // SUT
            var sut = new CardRepository(list);

            // Act
            sut.SaveOrUpdate(newCard);

            // Assert
            Assert.Equal(4, list.Count);
            Assert.Equal(newCard.Id, list.Last().Id);
        }

        [Fact]
        public void ShouldUpdateCard()
        {
            // Arrange
            var id = Guid.NewGuid();
            const string title = "fake title";
            const string content = "fake content";

            var list = new List<Card>
            {
                new Card {Id = Guid.NewGuid()},
                new Card {Id = id},
                new Card {Id = Guid.NewGuid()},
            };

            // SUT
            var sut = new CardRepository(list);

            // Act
            sut.SaveOrUpdate(new Card { Id = id, Title = title, Content = content });

            // Assert
            var card = list.First(x => x.Id == id);
            Assert.Equal(title, card.Title);
            Assert.Equal(content, card.Content);
            Assert.Equal(3, list.Count);
        }
    }
}
