using todolist.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace todolist.Domain.Repository
{
    public class CardRepository : ICardRepository
    {
        private IList<Card> _cards;

        public CardRepository(IList<Card> cards)
        {
            _cards = cards;
        }

        public void DeleteBy(Guid id)
        {
            var toRemove = _cards.FirstOrDefault(x => x.Id == id);
            if (toRemove == null)
            {
                return;
            }
            _cards.Remove(toRemove);
        }

        public Card GetBy(Guid id)
        {
            return _cards.FirstOrDefault(x => x.Id == id);
        }

        public IList<Card> GetCards()
        {
            return _cards;
        }

        public void SaveOrUpdate(Card card)
        {
            var toUpdate = _cards.FirstOrDefault(x => x.Id == card.Id);
            if(toUpdate == null)
            {
                _cards.Add(card);
                return;
            }
            toUpdate.Title = card.Title;
            toUpdate.Content = card.Content;
        }
    }
}