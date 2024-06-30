import { LitElement, html, customElement, property } from "@umbraco-cms/backoffice/external/lit";
import { UmbPropertyEditorUiElement } from "@umbraco-cms/backoffice/extension-registry";

@customElement('wsc-colour-finder-property-editor-ui')
export default class MySuggestionsPropertyEditorUIElement extends LitElement implements UmbPropertyEditorUiElement {
    @property({ type: String })
    public value = "TEST";

    render() {
        return html`I'm a property editor!`;
    }
}

declare global {
    interface HTMLElementTagNameMap {
        'wsc-colour-finder-property-editor-ui': MySuggestionsPropertyEditorUIElement;
    }
}