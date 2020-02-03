using todolist.Domain.Model;
using todolist.Domain.Repository;
using todolist.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace todolist.Controllers
{
    public class ApiController : Controller
    {
        private readonly ICardRepository _repository;

        public ApiController(ICardRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult Card()
        {
            var cards = _repository.GetCards();
            var results = cards.Select(x => new CardModel
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content
            });
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult Card(Guid id)
        {
            _repository.DeleteBy(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult AddCard(CardModel card)
        {
            var newCard = new Card
            {
                Id = Guid.NewGuid(),
                Title = card.Title,
                Content = card.Content
            };

            _repository.SaveOrUpdate(newCard);
            return Json(newCard.Id, JsonRequestBehavior.DenyGet);
        }

        [HttpPut]
        public ActionResult UpdateCard(CardModel card)
        {
            var toUpdate = _repository.GetBy(card.Id);
            if(toUpdate == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            toUpdate.Title = card.Title;
            toUpdate.Content = card.Content;

            _repository.SaveOrUpdate(toUpdate);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}