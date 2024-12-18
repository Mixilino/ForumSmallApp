const fs = require('fs');
const path = require('path');

// Read the JSON files
const enPath = path.join(__dirname, '../src/lang/en.json');
const srPath = path.join(__dirname, '../src/lang/sr.json');
const frPath = path.join(__dirname, '../src/lang/fr.json');
const arPath = path.join(__dirname, '../src/lang/ar.json');

const enMessages = JSON.parse(fs.readFileSync(enPath, 'utf8'));
const srMessages = JSON.parse(fs.readFileSync(srPath, 'utf8'));
const frMessages = JSON.parse(fs.readFileSync(frPath, 'utf8'));
const arMessages = JSON.parse(fs.readFileSync(arPath, 'utf8'));

// Find new messages for each language
const newMessages = {
    sr: {},
    fr: {},
    ar: {}
};
const hasNewMessages = {
    sr: false,
    fr: false,
    ar: false
};

// Check for missing translations in each language
for (const [key, value] of Object.entries(enMessages)) {
    if (!srMessages[key]) {
        hasNewMessages.sr = true;
        newMessages.sr[key] = {
            ...value,
            defaultMessage: `TODO: ${value.defaultMessage}`
        };
    }
    if (!frMessages[key]) {
        hasNewMessages.fr = true;
        newMessages.fr[key] = {
            ...value,
            defaultMessage: `TODO: ${value.defaultMessage}`
        };
    }
    if (!arMessages[key]) {
        hasNewMessages.ar = true;
        newMessages.ar[key] = {
            ...value,
            defaultMessage: `TODO: ${value.defaultMessage}`
        };
    }
}

// Update each language file if needed
const languages = [
    { code: 'sr', path: srPath, messages: srMessages },
    { code: 'fr', path: frPath, messages: frMessages },
    { code: 'ar', path: arPath, messages: arMessages }
];

languages.forEach(lang => {
    if (hasNewMessages[lang.code]) {
        const updatedMessages = {
            ...lang.messages,
            ...newMessages[lang.code]
        };

        fs.writeFileSync(
            lang.path,
            JSON.stringify(updatedMessages, null, 2),
            'utf8'
        );

        console.log(`New messages added to ${lang.code}.json:`);
        console.log(newMessages[lang.code]);
    }
});

if (!Object.values(hasNewMessages).some(Boolean)) {
    console.log('No new messages to add.');
} 