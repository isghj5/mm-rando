# How to develop

### Run the Electron GUI in dev mode

1. Use MMR.CLI to obtain a settings_dump.json file and place it inside the MMR.Web.Config/inputFiles folder

2. Start a terminal, go to the MMR.Web.Config directory

3. Run `node settingsConverter.js`

4. Change current directory to the MMR.Web.UI folder 

5. Execute `npm ci` to delete your node_modules folder and install all dependencies as specified in package-lock.json. This can take a while and will use a considerable amount of file space (around 700 MB)

6. Execute `npm run ng-dev` to start Angular webpack dev server

7. In a secondary shell execute `npm run electron-compile` followed by `npm run electron-dev`

8. As soon as Electron has started, you may open DevTools as required (`Ctrl+Shift+I` or `F12`). Use `F5` to refresh the GUI if needed

Any saved changes to a file will then prompt the GUI to hot reload, making debugging very easy. You will also see console.log() and the likes in the javascript console.