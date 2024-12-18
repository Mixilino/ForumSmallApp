import { Spinner } from 'flowbite-react';
import React from 'react';
import { IntlProvider } from 'react-intl';

const LANGUAGE_STORAGE_NAME = 'forum-ui-language' as const;

export type SupportedLanguages = 'en' | 'sr' | 'fr' | 'ar';

interface LanguageContextType {
  language: SupportedLanguages;
  changeLanguage: (lang: SupportedLanguages) => void;
  isRTL: boolean;
}

const RTL_LANGUAGES = ['ar'];

const LanguageContext = React.createContext<LanguageContextType>({
  language: 'en',
  changeLanguage: () => undefined,
  isRTL: false,
});

const LOCALE_MAP = new Map<string, SupportedLanguages>([
  ['sr', 'sr'],
  ['sh', 'sr'],
  ['en', 'en'],
  ['fr', 'fr'],
  ['fr-FR', 'fr'],
  ['fr-CA', 'fr'],
  ['ar', 'ar'],
  ['ar-SA', 'ar'],
  ['ar-EG', 'ar']
]);

const storage = {
  write: (lang: SupportedLanguages): void => {
    localStorage.setItem(LANGUAGE_STORAGE_NAME, lang);
  },
  read: (): SupportedLanguages | null => 
    localStorage.getItem(LANGUAGE_STORAGE_NAME) as SupportedLanguages | null
};

const getBrowserLocale = (): SupportedLanguages => {
  const browserLanguages = navigator.languages || [navigator.language];
  
  const mappedLang = browserLanguages
    .map(lang => LOCALE_MAP.get(lang))
    .find(lang => lang !== undefined);
    
  return mappedLang ?? 'en';
};

const detectInitialLanguage = (): SupportedLanguages => 
  storage.read() ?? getBrowserLocale();

export async function loadMessages(language: SupportedLanguages) {
  try {
    const messages = await import(`../../lang/${language}.json`);
    return Object.entries(messages.default).reduce<Record<string, string>>(
      (acc, [key, value]) => {
        acc[key] = (value as { defaultMessage: string }).defaultMessage;
        return acc;
      }, 
      {}
    );
  } catch (error) {
    console.error(`Failed to load messages for ${language}`, error);
    return {};
  }
}

const LanguageProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [language, setLanguage] = React.useState<SupportedLanguages>(() => {
    const stored = storage.read();
    if (stored) return stored;

    const browserLang = navigator.language.toLowerCase();
    return LOCALE_MAP.get(browserLang) || 'en';
  });

  const [messages, setMessages] = React.useState<Record<string, string> | null>(null);
  const isRTL = RTL_LANGUAGES.includes(language);

  const changeLanguage = React.useCallback((newLang: SupportedLanguages) => {
    setLanguage(newLang);
    storage.write(newLang);
    // Update document direction
    document.dir = RTL_LANGUAGES.includes(newLang) ? 'rtl' : 'ltr';
  }, []);

  // Set initial direction
  React.useEffect(() => {
    document.dir = isRTL ? 'rtl' : 'ltr';
  }, [isRTL]);

  React.useEffect(() => {
    const fetchMessages = async () => {
      const loadedMessages = await loadMessages(language);
      setMessages(loadedMessages);
    };

    fetchMessages();
  }, [language]);

  if (!messages) {
    return <Spinner />;
  }

  return (
    <LanguageContext.Provider value={{ language, changeLanguage, isRTL }}>
      <IntlProvider messages={messages} locale={language} defaultLocale="en">
        {children}
      </IntlProvider>
    </LanguageContext.Provider>
  );
};

// Custom hook for using the language context
export const useLanguage = (): LanguageContextType => {
  const context = React.useContext(LanguageContext);
  if (!context) {
    throw new Error('useLanguage must be used within a LanguageProvider');
  }
  return context;
};

export { LanguageProvider, LanguageContext };
