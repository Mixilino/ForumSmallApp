import { Spinner } from 'flowbite-react';
import React from 'react';
import { IntlProvider } from 'react-intl';

const LANGUAGE_STORAGE_NAME = 'forum-ui-language' as const;

export type SupportedLanguages = 'en' | 'sr';

interface LanguageContextType {
  language: SupportedLanguages;
  changeLanguage: (lang: SupportedLanguages) => void;
}

const LanguageContext = React.createContext<LanguageContextType>({
  language: 'en',
  changeLanguage: () => undefined,
});

const LOCALE_MAP = new Map<string, SupportedLanguages>([
  ['sr', 'sr'],
  ['sh', 'sr'],
  ['en', 'en']
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

interface LanguageProviderProps {
  children: React.ReactNode;
}

const LanguageProvider: React.FC<LanguageProviderProps> = ({ children }) => {
  const [language, setLanguage] = React.useState<SupportedLanguages>(detectInitialLanguage());
  const [messages, setMessages] = React.useState<Record<string, string> | null>(null);

  const changeLanguage = React.useCallback((lang: SupportedLanguages) => {
    setLanguage(lang);
    storage.write(lang);
  }, []);

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
    <LanguageContext.Provider value={{ language, changeLanguage }}>
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
