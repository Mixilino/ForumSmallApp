const fs = require('fs');
const path = require('path');

// Read the JSON files
const enPath = path.join(__dirname, '../src/lang/en.json');
const srPath = path.join(__dirname, '../src/lang/sr.json');

const enMessages = JSON.parse(fs.readFileSync(enPath, 'utf8'));
const srMessages = JSON.parse(fs.readFileSync(srPath, 'utf8'));

// Find new messages
const newMessages = {};
let hasNewMessages = false;

for (const [key, value] of Object.entries(enMessages)) {
    if (!srMessages[key]) {
        hasNewMessages = true;
        newMessages[key] = {
            ...value,
            defaultMessage: `TODO: ${value.defaultMessage}` // Mark new messages for translation
        };
    }
}

if (hasNewMessages) {
    // Merge existing translations with new messages
    const updatedSrMessages = {
        ...srMessages,
        ...newMessages
    };

    // Write back to sr.json
    fs.writeFileSync(
        srPath,
        JSON.stringify(updatedSrMessages, null, 2),
        'utf8'
    );

    console.log('New messages added to sr.json:');
    console.log(newMessages);
} else {
    console.log('No new messages to add.');
} 