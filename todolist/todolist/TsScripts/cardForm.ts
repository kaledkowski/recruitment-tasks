class CardForm {
    private _view: CardFormView;

    public constructor() {
        let api = new Api();
        let view = new CardFormView();

        view.onSubmit(async (event: Event) => {
            if (!view.validate()) {
                return;
            }
            let card = new Card("", view.title, view.content);
            await api.addCard(card);

            App.bus.trigger("cardAdd", card);
        });

        this._view = view;
    }

    public get view(): CardFormView {
        return this._view;
    }
}