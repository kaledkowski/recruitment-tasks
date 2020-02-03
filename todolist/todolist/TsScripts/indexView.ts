/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />

class IndexView {
    private _dom: JQuery;

    public constructor() {
        let dom = $(this.render());
        let form = new CardForm();
        let container = new CardContainer();

        this._dom = dom;

        dom.find("form").replaceWith(form.view.dom);
        dom.find("#cardContainer").replaceWith(container.view.dom);
    }

    public get dom(): JQuery {
        return this._dom;
    }

    private render(): string {
        return `<div class="jumbotron">
                    <h1>Todo list</h1>
                    <p class="lead">This is your TODO list! You can add cards right away. Just fill in the card and click 'Add card' button!</p>
                    <form />
                </div>
                <div class="card">
                    <div class="card-body">
                        <div id="cardContainer" />
                    </div>
                </div>`;
    }
}