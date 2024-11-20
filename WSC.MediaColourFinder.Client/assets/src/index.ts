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
        gap: 35px;
        overflow: hidden; /* Clearfix to wrap floated elements */

    }
    .colourSample {
        float: left;
        margin-right: 10px; /* Add some spacing between samples */
    }
     #wrapper {
      margin-top: 10px;
      display: flex;
      gap: 10px;
          clear: both; /* Clear the float to start a new line */
    }
    .element {
      width: 100%;
    }
  `;


    render() {
        return html` 
<uui-box>
    <uui-color-swatch label="Average" show-label="true" value="#d0021b"></uui-color-swatch>
    <uui-color-swatch label="Brightest" show-label="true" value="#d0021b"></uui-color-swatch>
    <uui-color-swatch label="Opposite" show-label="true" value="#d0021b"></uui-color-swatch>
    <uui-color-swatch label="Text" show-label="true" value="#d0021b"></uui-color-swatch>
    <div id="wrapper">
       <uui-button label="Update Colours" look="primary">Update colours</uui-button>
    </div>
   </uui-box>

            `
    }
}

declare global {
    interface HTMLElementTagNameMap {
        'wsc-colour-finder-property-editor-ui': ColourFinderPropertyEditor;
    }
}