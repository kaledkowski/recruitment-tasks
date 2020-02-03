class TodoItem {
    private _view: TodoItemView;

    public constructor(card: Card) {
        let api = new Api();
        let view = new TodoItemView();

        this._view = view;

        view.title = card.title;
        view.content = card.content;

        view.onTitleClick(() => {
            view.editMode = true;
            view.focusOnTitle();
        });

        view.onContentClick(() => {
            view.editMode = true;
            view.focusOnContent();
        });

        view.onSave(async () => {
            if (!view.validate()) {
                return;
            }

            card.title = view.title;
            card.content = view.content;

            await api.update(card);

            view.editMode = false;
        });

        view.onDelete(async () => {
            await api.removeCard(card);
            view.remove();
        });

        view.onCancel(() => {
            view.resetValidation();

            view.title = card.title;
            view.content = card.content;

            view.editMode = false;
        });
    }

    public get view(): TodoItemView {
        return this._view;
    }
}