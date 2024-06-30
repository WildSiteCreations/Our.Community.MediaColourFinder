import { LitElement, html, customElement, property, css } from "@umbraco-cms/backoffice/external/lit";
import { UmbPropertyEditorUiElement } from "@umbraco-cms/backoffice/extension-registry";

@customElement('wsc-colour-finder-property-editor-ui')
export default class ColourFinderPropertyEditor extends LitElement implements UmbPropertyEditorUiElement {
    @property({ type: String })
    public value = "";

    static styles = css`
    .patch{
        width: 100px;
        height: 100px;
        margin: 10px;
        -webkit-box-shadow: 4px 4px 5px 0 rgba(0, 0, 0, .32);
        -moz-box-shadow: 4px 4px 5px 0 rgba(0, 0, 0, .32);
        box-shadow: 4px 4px 5px 0 rgba(0, 0, 0, .32);
    }
    .colourContainer {
        display: flex;
        justify-content: space-between;
        gap: 35px
    }
  `;


    render() {
        return html` 
        <div class="colourContainer">
            <div class="colourSample">
                <p>Average</p>
                <div class="patch" style="background-color: #AC439C;">
                </div>
                <div>#FFFFFF</div>
            </div>
            <div class="colourSample">
                <p>Brightest</p>
                <div class="patch">
                </div>
                <div>#FFFFFF</div>
            </div>
            <div class="colourSample">
                <p>Opposite</p>
                <div class="patch">
                </div>
                <div>#FFFFFF</div>
            </div>
            <div class="colourSample">
                <p>Text Colour on average</p>
                <div class="patch">
                </div>
                <div>#FFFFFF</div>
            </div>
            
            </div>
            `
    }
}

declare global {
    interface HTMLElementTagNameMap {
        'wsc-colour-finder-property-editor-ui': ColourFinderPropertyEditor;
    }
}