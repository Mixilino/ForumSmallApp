const fs = require('fs');
const path = require('path');
const chalk = require('chalk');

// Read the JSON files
const enPath = path.join(__dirname, '../src/lang/en.json');
const srPath = path.join(__dirname, '../src/lang/sr.json');
const frPath = path.join(__dirname, '../src/lang/fr.json');
const arPath = path.join(__dirname, '../src/lang/ar.json');

const enMessages = JSON.parse(fs.readFileSync(enPath, 'utf8'));
const srMessages = JSON.parse(fs.readFileSync(srPath, 'utf8'));
const frMessages = JSON.parse(fs.readFileSync(frPath, 'utf8'));
const arMessages = JSON.parse(fs.readFileSync(arPath, 'utf8'));

// Track untranslated and TODO messages
const untranslatedStats = {
    sr: { missing: [], todo: [] },
    fr: { missing: [], todo: [] },
    ar: { missing: [], todo: [] }
};

// Check for missing translations in each language
for (const [key, value] of Object.entries(enMessages)) {
    // Serbian
    if (!srMessages[key]) {
        untranslatedStats.sr.missing.push(key);
    } else if (srMessages[key].defaultMessage.startsWith('TODO:')) {
        untranslatedStats.sr.todo.push(key);
    }

    // French
    if (!frMessages[key]) {
        untranslatedStats.fr.missing.push(key);
    } else if (frMessages[key].defaultMessage.startsWith('TODO:')) {
        untranslatedStats.fr.todo.push(key);
    }

    // Arabic
    if (!arMessages[key]) {
        untranslatedStats.ar.missing.push(key);
    } else if (arMessages[key].defaultMessage.startsWith('TODO:')) {
        untranslatedStats.ar.todo.push(key);
    }
}

// Print translation status
console.log(chalk.blue('\n=== Translation Status ===\n'));

const printLanguageStatus = (lang, stats) => {
    if (stats.missing.length > 0 || stats.todo.length > 0) {
        console.log(chalk.yellow(`\n${lang.toUpperCase()} Translation Issues:`));
        
        if (stats.missing.length > 0) {
            console.log(chalk.red('\nMissing Translations:'));
            stats.missing.forEach(key => {
                console.log(chalk.red(`  - ${key}`));
            });
        }
        
        if (stats.todo.length > 0) {
            console.log(chalk.yellow('\nTODO Translations:'));
            stats.todo.forEach(key => {
                console.log(chalk.yellow(`  - ${key}`));
            });
        }
    }
};

printLanguageStatus('Serbian', untranslatedStats.sr);
printLanguageStatus('French', untranslatedStats.fr);
printLanguageStatus('Arabic', untranslatedStats.ar);

// If everything is translated
if (Object.values(untranslatedStats).every(lang => 
    lang.missing.length === 0 && lang.todo.length === 0)) {
    console.log(chalk.green('\n✓ All messages are translated in all languages!\n'));
} else {
    console.log(chalk.yellow('\n⚠️  Some translations are missing or marked as TODO\n'));
    
    // Summary
    console.log(chalk.blue('Summary:'));
    Object.entries(untranslatedStats).forEach(([lang, stats]) => {
        const total = stats.missing.length + stats.todo.length;
        if (total > 0) {
            console.log(chalk.yellow(
                `${lang.toUpperCase()}: ${stats.missing.length} missing, ${stats.todo.length} TODO`
            ));
        }
    });
    console.log('\n');
}

// Update files with missing translations
const languages = [
    { code: 'sr', path: srPath, messages: srMessages },
    { code: 'fr', path: frPath, messages: frMessages },
    { code: 'ar', path: arPath, messages: arMessages }
];

languages.forEach(lang => {
    if (untranslatedStats[lang.code].missing.length > 0) {
        const updatedMessages = { ...lang.messages };
        
        untranslatedStats[lang.code].missing.forEach(key => {
            updatedMessages[key] = {
                ...enMessages[key],
                defaultMessage: `TODO: ${enMessages[key].defaultMessage}`
            };
        });

        fs.writeFileSync(
            lang.path,
            JSON.stringify(updatedMessages, null, 2),
            'utf8'
        );
    }
}); 