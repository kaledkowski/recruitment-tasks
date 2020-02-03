/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />

class Api {
    private static readonly API_CARD_URL = "/api/card/";

    public async getCards(): Promise<Card[]> {
        let cards = await $.ajax(Api.API_CARD_URL);
        return cards.map(c => new Card(c.Id, c.Title, c.Content));
    }

    public async addCard(card: Card): Promise<void> {
        let id = await $.ajax({
            url: Api.API_CARD_URL,
            method: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(card)
        });

        card.id = id;
    }

    public async update(card: Card): Promise<void> {
        await $.ajax({
            url: Api.API_CARD_URL,
            method: "PUT",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(card)
        });
    }

    public async removeCard(card: Card): Promise<void> {
        await $.ajax({
            url: `${Api.API_CARD_URL}${card.id}`,
            method: "DELETE"
        });
    }
}