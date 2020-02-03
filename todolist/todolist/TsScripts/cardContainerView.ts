/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />

class CardContainerView {
    private _dom: JQuery;

    public constructor() {
        this._dom = $(this.render());
    }

    public get dom(): JQuery {
        return this._dom;
    }

    public addCard(card: TodoItemView) {
        this._dom.append(card.dom);
    }

    private render(): string {
        return `<div class="d-flex flex-wrap"></div>`;
    }
}