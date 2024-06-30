import type { ManifestDashboard } from "@umbraco-cms/backoffice/extension-registry";

const dashboards: Array<ManifestDashboard> = [
    {
        type: 'dashboard',
        name: 'WSC.MediaColourFinder',
        alias: 'WSC.MediaColourFinder.dashboard',
        elementName: 'wsc-colour-finder-property-editor-ui',
        js: ()=> import('./colour-finder-property-editor-ui.js'),
        weight: -10,
        meta: {
            label: 'WSC.MediaColourFinder',
            pathname: 'WSC.MediaColourFinder'
        },
        conditions: [
            {
                alias: 'Umb.Condition.SectionAlias',
                match: 'Umb.Section.Content'
            }
        ]
    }
]

export const manifests = [...dashboards];