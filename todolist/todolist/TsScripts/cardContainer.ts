class CardContainer {
    private _view: CardContainerView;

    public constructor() {
        let view = new CardContainerView();
        this._view = view;

        App.bus.on("cardAdd",
            (event: Event, card: Card) => {
                let item = new TodoItem(card);
                view.addCard(item.view);
            });
    }

    public get view(): CardContainerView {
        return this._view;
    }
}