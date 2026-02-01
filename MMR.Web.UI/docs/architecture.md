# Architecture

The GUI design is split into two distinct parts:

  1. Web.Config: The settings converter which is responsible for translating the settings config output of the MMRandomizer CLI into a format the GUI can understand
  2. Web.UI: The actual GUI


# Prerequisites

Node.js and NPM needs to be installed (Node ships with NPM). Any Node version > 18.19.1 <= 24 should work fine.

# 1. Web.Config

## Overview

The settingsConverter.js found in Web.Config uses several input sources to produce the final settings_list.json that the GUI will use for rendering. Futhermore it handles adding extra arbitrary settings on top that are only needed on the web plus layouting. The script is written in and invoked using Node.js. 

### The settings_list.json structure

settings_list.json is not meant to be edited manually, it should be considered a compiled artifact. Nevertheless its structure is important to understand so it is explained in detail here. The file contains every single setting of the GUI both as a dictionary of objects and as an array of objects. The following main keys per setting entry are supported (a few more are omitted here for brevity):

* default &rarr; The default value in the GUI
* name &rarr; Internal name of the setting
* text &rarr; Name of the setting as shown to the user
* tooltip &rarr; The tooltip shown to the user when he hovers/clicks on the setting. Line breaks are inserted by adding a HTML `<br>` tag into the string
* hide-when-disabled &rarr; If this setting should be completely hidden when it gets disabled, not just greyed out.
* min &rarr; The minimum numeric value allowed. Used for Scales and Numberinputs
* max &rarr; The maximum numeric value allowed. Used for Scales and Numberinputs
* size &rarr; How big the input field should be. Used for Numberinputs, Textinputs and Scales. Allowed values are: small, medium, full (=as wide as the section is) (default: extra small)
* max-length &rarr; How many characters are maximum allowed in this input field. Used for Textinputs (default: 260)
* file-types &rarr; Used for Fileinput. An array of allowed file types the system dialog should allow the user to pick (name and extensions)
* no-line-break &rarr; If set, then there will be no line break between this setting and the next one allowing to put 2 settings onto the same line to conserve space
* type &rarr; Type of the GUI element to use to control the setting. Available types are:
    
  1. Checkbutton: A boolean on/off toggle
  2. Radiobutton: A radio in-line list of buttons you can select one option of
  3. Combobox: A dropdown box that offers multiple options you can select *one* of
  4. MultipleSelect: A dropdown box that offers multiple options you can select *multiple* of
  5. Scale: A numerical scale that goes from a minimum to a maximum value and is accompanied by a number input box for manual input
  6. IndexCombobox: A series of dropdown boxes with unique names that render their value into a common array
  7. Textinput: An input box designed for text
  8. Numberinput: An input box designed for numbers (no scale)
  9. Fileinput: An input box designed for file paths accompanied by a browse button that opens a system file dialog
  10. Directoryinput: An input box designed for directory paths accompanied by a browse button that opens a system directory dialog
  11. SearchBox: A dual list of options that the user can select multiple options of and move them between active and not active. Also allows to text search for an option or select all at once. Drag and drop is also supported. Has filter support
  12. SearchBoxMMR: A special variant of SearchBox that compiles the selected options to a bitmask string shown in a text box
  13. Dictionary: Supports multiple "inner types". Allows to present the same settings type multiple times next to each other with different key names and stores the values in a dictionary
  14. DictionaryLinked: A special container setting that wraps multiple other settings so the values are recorded together into a single set of keys
  15. Color: A color picker element that also offers a Randomize choice button
  16. Presetinput: Designed for the main tab only. Adds a combobox and 3 buttons for the preset handling
  17. Button: Renders a button. The "function" key can be used to invoke an arbitrary function in the TypeScript code. This can be used for custom settings that require individual design elements

* options &rarr; An array of options the setting can have. This is used by many settings types. Each option entry can have the following main keys:

  1. name: Internal name of the option value
  2. text: Name of the option as shown to the user
  3. tooltip: The tooltip shown to the user when he hovers over the option. This is only supported by SearchBox at the moment. Line breaks are again added by a `<br>` tag
  4. controls-visibility-tab: What tab(s) to disable when this option is selected. Multiple tabs can be separated by comma and are addressed by their internal name (see mapping.json structure)
  5. controls-visibility-section: What section(s) to disable when this option is selected
  6. controls-visibility-setting: What specific setting(s) to disable when this option is selected

### The settings_dump.json structure

This file can be found in Web.Config/inputFiles and first needs to be generated by the C# CLI. The structure is defined by the randomizer itself: ([Reference](https://github.com/ZoeyZolotova/mm-rando/blob/dev/MMR.CLI/Program.cs))

### The settings_extra.json structure

This file can be found in Web.Config/inputFiles. It defines extra settings and can be used to extend settings already defined in the settings_dump.json. The structure is identical with the settings_list.json structure. This is mainly used to add web related settings the C# randomizer doesn't have a need for and add additional information for settings that do exist to give the GUI additional context. This file should be considered arbitrary and the long term goal should be to remove it and transfer these settings and information into the C# base as best as possible.

### The settings_mapping.json structure

This file can be found in Web.Config/inputFiles. Used to extend the settings_dump.json and define the visual layout of the GUI for Electron and the website.

Starts with an array of tabs that are shown in the GUI. The following keys are supported:

* name &rarr; Internal name of the tab
* text &rarr; Name of the tab as shown in the GUI
* is-cosmetics &rarr; Whether the tab contains only cosmetic settings or not. Used to exclude cosmetics and sfx settings from presets and so on
* app-type &rarr; Restricts in which app the tab should be rendered in the GUI. Can be 'generator' or 'patcher '. Used on the website to differentiate between the seed generator and the standalone patcher that gets shown on the seed detail page. The latter only has the cosmetic tabs shown
* exclude-from-electron &rarr; If set then the settingsConverter will skip over these tabs when in Electron mode
* exclude-from-web &rarr; If set then the settingsConverter will skip over these tabs when in web mode
* sections &rarr; An array of sections to render in the tab, see next

The sections array follows that is used to group settings together in a tab. The following keys are supported:

* name &rarr; Internal name of the section
* text &rarr; Name of the section as shown in the GUI (can be left empty to not show a section header)
* subheader &rarr; Can be used to render a batch of text directly below the section header. `<br>` tags for line breaks are supported, as well as `<a>` tags for hyperlinks
* col-span &rarr; How many columns this section should try to occupy, affecting its width. If not set, the GUI will calculate a proper col-span automatically based on the number of columns available. On its biggest size the GUI can fit 4 columns per row, on its smallest 1. If this setting is used and not enough columns are available to satisfy this setting, it takes as many as possible. Thus setting a col-span of 4 will ensure that the section always takes up one full row regardless of GUI size
* row-span &rarr; Required and has to be set. How many rows this section should try to occupy, affecting its height. The GUI will distribute the available height evenly between each row, thus giving a section 2 rows will proportionally make it have a greater height than with 1
* settings &rarr; An array of settings to render in the section, see next

The settings array follows that defines the settings that should appear in this section.
The array consists of the internal names of the settings. Has to match with the settings defined in the settings_dump.json

### presets_default.json

The Web.Config/presets_default.json contains the settings presets that can be chosen from within the GUI. The tooltips for them can be found in the Web.Config/inputFiles/settings_extra.json.


# 2. Web.UI

The GUI is written in the Angular framework. This means TypeScript for code, Sass for styles and HTML for layout. The GUI can be rendered either in Electron (which is a special browser environment) or a traditional web browser. The Electron mode is currently only intended for development, but could be later adapted into running the entire randomizer locally. The needed hooks are in place already, but since the GUI is currently only intended for the website version, this document will focus on that.

## Electron

### Overview

The Electron source is contained within Web.UI/electron/src and written in TypeScript. Main.ts is managing the Electron process (the main side which has full access to the host system) and is used as the entry point. The preload.ts gets injected into any given browser window created by the main process and managed by the renderer process.

Currently this code is mostly just a thin wrapper into loading up the GUI as fast as possible for development.

## Angular

### Overview 

The Angular source is contained in the Web.UI/src folder and consists of components and modules. Each component has a HTML template with custom Angular specific directives, styles written in Sass (a CSS extension) and component code written in TypeScript. The src/index.html serves as the entry point and bootstraps Angular.

It is important to note that this project uses Nebular, which is an Angular UI toolkit and ships with pre-made UI elements and themes: ([Reference](https://akveo.github.io/nebular/))

Mostly relevant for GUI development is the src/app/pages/generator component, as well as the src/app/components/guiSettingsElement component. Those HTML files provide the template that renders the entire settings section and all its tabs. The scss files provide the Sass (CSS) styling immediately relevant to this. The ts files contain the code immediately used when the user interacts with the GUI elements.

The src/app/providers/GUIGlobal.ts is a global service and can be included with any component. It provides a lot of functionality that might be used from multiple components, as well as manages the GUI initialization. After Angular core this is the main entry point for our own logic. It will request the settings map from the preload.ts/the Electron main process via postMessage and store this and other information globally, so it can be accessed from anywhere in the app. It also handles a lot of differences between Electron and web mode transparently. The generator component uses GUIGlobal constantly.

The applications header and footer bar can be found and customized within src/app/@theme/components.

src/app/@theme/layouts/GUI assembles the header bar, generator component and footer bar together as the singular app that the user sees in the end.

### Styling and Theming

In addition to individual angular component styles, files for global styling exist under GUI/src/app/@theme/styles.

Two themes exist at the moment. The default theme and the dark mode theme. Dark mode can be activated by clicking on the moon symbol in the header bar.  

Colors and theme related properties are stored within theme.mmr-dark.scss and theme.mmr-light.scss. 

To support theming it is crucial to work with $nb-theme() variables whenever colorizing backgrounds, texts, borders, etc.

Whether dark mode is active or not is saved inside settings.sav.

### Website compatibility notes

View encapsulation has been deactivated since it is not supported by Nebular.
Excessive use of selector specificity is in place to ensure interoperability.