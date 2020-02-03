/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />

class TodoItemView {
    // #region fields

    private readonly _dom: JQuery;
    private readonly _titleInput: JQuery;
    private readonly _titlePresentation: JQuery;
    private readonly _contentInput: JQuery;
    private readonly _contentPresentation: JQuery;
    private readonly _buttonContainer: JQuery;
    private _validator: any;

    // #endregion

    public constructor() {        
        let dom = $(this.render());
        this._dom = dom;

        this._titleInput = dom.find("input");
        this._titlePresentation = dom.find("span[name]");
        this._contentInput = dom.find("textarea");
        this._contentPresentation = dom.find("p[name]");
        this._buttonContainer = dom.find(".edit-buttons");

        this.bindInputsWithPresentation();
    }

    // #region properties

    public get dom(): JQuery {
        return this._dom;
    }

    public get title(): string {
        return this._titleInput.val();
    }

    public set title(title: string) {
        this._titlePresentation.text(title);
        this._titleInput.val(title);
    }

    public get content(): string {
        return this._contentInput.val();
    }

    public set content(content: string) {
        this._contentPresentation.text(content);
        this._contentInput.val(content);
    }

    public set editMode(enabled: boolean) {
        let editModeElements = this._buttonContainer.add(this._titleInput).add(this._contentInput);
        let normalModeElements = this._titlePresentation.add(this._contentPresentation);

        if (enabled) {
            editModeElements.show();
            normalModeElements.hide();
        } else {
            editModeElements.hide();
            normalModeElements.show();
        }
    }

    // #endregion

    // #region event subscription

    public onTitleClick(callback: Function): void {
        this._titlePresentation.on("click", <any>callback);
    }

    public onContentClick(callback: Function): void {
        this._contentPresentation.on("click", <any>callback);
    }

    public onCancel(callback: Function): void {
        this._dom.find(".cancel-btn").on("click", <any>callback);
    }

    public onSave(callback: Function): void {
        this._dom.find(".save-btn").on("click", <any>callback);
    }

    public onDelete(callback: Function): void {
        this._dom.find(".close").on("click", <any>callback);
    }

    // #endregion

    // #region public methods

    public focusOnTitle(): void {
        this._titleInput.focus();
    }

    public focusOnContent(): void {
        this._contentInput.focus();
    }

    public remove(): void {
        this._dom.remove();
    }

    public validate(): boolean {
        this._validator = this._dom.validate();
        return this._dom.valid();
    }

    public resetValidation(): void {
        let validator = this._validator;
        if (!validator) return;

        validator.resetForm();
        this._validator = undefined;
    }

    // #endregion

    // #region private methods

    private bindInputsWithPresentation() {
        this._titleInput.on("input",
            (event: Event) => {
                let value = $(event.target).val();
                this._titlePresentation.text(value);
            });

        this._contentInput.on("input",
            (event: Event) => {
                let value = $(event.target).val();
                this._contentPresentation.text(value);
            });
    }

    private render(): string {
        return `<form class="card border-dark mr-1 ml-1 mt-2 mb-auto" style="width: 16rem;" autocomplete="off" novalidate > 
                    <div class="card-header" >
                        <div class="row">
                            <div class="col-10">
                                <span class="h5" name="title" required></span>
                                <input type="text" name="title" class="form-control" style="display: none;" required />
                            </div>
                            <div class="col-2" >
                                <button type="button" class="close" aria-label="Close" >
                                    <span aria-hidden="true" >&times;</span>
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="card-body text-dark" >
                        <p name="content"></p>
                        <textarea name="content" class="form-control" style="display: none;" required></textarea>
                        <div class="text-right edit-buttons">
                            <button type="button" class="btn btn-sm btn-link cancel-btn">Cancel</button>
                            <button type="button" class="btn btn-sm btn-link save-btn">Save</button>
                        <div>
                    </div>
                </form>`;
    }

    // #endregion
}