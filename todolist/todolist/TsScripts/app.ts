/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />

class App {
    public static readonly bus: JQuery = $(new Object());

    public constructor() {
        let index = new Index();
        $("#app").replaceWith(index.view.dom);
    }

    public async init(): Promise<void> {
        let api = new Api();
        let cards = await api.getCards();
        cards.forEach(card => App.bus.trigger("cardAdd", card));
    }
}