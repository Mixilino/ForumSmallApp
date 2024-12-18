const shell = require('shelljs');
const path = require('path');

// Define the directories for source and destination
const srcDir = path.join(__dirname, '..', 'src', 'lang');  // Source directory for extracted files
const destDir = path.join(__dirname, '..', 'src', 'compiled-lang');  // Destination directory for compiled files

// Loop through each JSON file in src/lang
shell.ls(path.join(srcDir, '*.json')).forEach((file) => {
  const fileName = path.basename(file);
  const outFile = path.join(destDir, fileName);

  // Run formatjs compile on each file and output to the compiled-lang folder
  shell.exec(`formatjs compile "${file}" --ast --out-file "${outFile}"`);
});
