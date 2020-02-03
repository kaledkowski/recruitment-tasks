class Index {
    private _view: IndexView;

    public constructor() {
        this._view = new IndexView();
    }

    public get view(): IndexView {
        return this._view;
    }
}