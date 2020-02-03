/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />

class CardFormView {
    private readonly _dom: JQuery;
    private readonly _title: JQuery;
    private readonly _content: JQuery;
    private readonly _button: JQuery;

    public constructor() {
        let dom = $(this.render());
        this._dom = dom;

        this._title = dom.find("[name=title]");
        this._content = dom.find("[name=content]");
        this._button = dom.find("button");
    }

    public get dom(): JQuery {
        return this._dom;
    }

    public get title(): string {
        return this._title.val();
    }

    public get content(): string {
        return this._content.val();
    }

    public onSubmit(callback: Function): void {
        this._button.on("click", <any>callback);
    }

    public validate(): boolean {
        this._dom.validate();
        return this._dom.valid();
    }

    private render(): string {
        return `<form id="addCardForm" autocomplete="off" novalidate>
                    <div class="card border-dark mx-auto mb-1" style="max-width: 16rem;">
                        <div class="card-header">
                            <input name="title" class="form-control" type="text" placeholder="Title" required/>
                        </div>
                        <div class="card-body text-dark">
                            <div class="card-text">
                                <textarea name="content" class="form-control" placeholder="Don't forget about ..." required></textarea>
                            </div>
                            <div class="text-right">
                                <button type="button" class="btn btn-link btn-sm">Add card</button>
                            </div>
                        </div>
                    </div>
                </form>`;
    }
}