using todolist.Domain.Model;
using System;
using System.Collections.Generic;

namespace todolist.Domain.Repository
{
    public interface ICardRepository
    {
        IList<Card> GetCards();
        Card GetBy(Guid id);
        void DeleteBy(Guid id);
        void SaveOrUpdate(Card card);
    }
}