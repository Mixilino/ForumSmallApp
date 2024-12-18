import React, { useContext } from 'react';
import { LanguageContext, SupportedLanguages } from '../../store/LanguageProvider/LanguageProvider';
import { useIntl } from 'react-intl';
import { messages } from './messages';

export const Footer = () => {
    const { changeLanguage, language, isRTL } = useContext(LanguageContext);
    const { formatMessage } = useIntl();

    return (
        <div className='min-h-10 flex justify-between items-center bg-gray-800 text-white p-4' dir={isRTL ? 'rtl' : 'ltr'}>
            <select
                value={language}
                onChange={(e) => changeLanguage(e.target.value as SupportedLanguages)}
                className="bg-gray-700 text-white rounded pl-2 py-1 pr-8 min-w-[7rem]"
            >
                <option value="en">{formatMessage(messages.languageEnglish)}</option>
                <option value="sr">{formatMessage(messages.languageSerbian)}</option>
                <option value="fr">{formatMessage(messages.languageFrench)}</option>
                <option value="ar">{formatMessage(messages.languageArabic)}</option>
            </select>
            <div>{formatMessage(messages.copyright)}</div>
        </div>
    )
}