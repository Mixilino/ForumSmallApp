import { defineMessages } from 'react-intl';

export const messages = defineMessages({
  secondsAgo: {
    id: 'time.secondsAgo',
    defaultMessage: ', {seconds} seconds ago',
  },
  minutesAgo: {
    id: 'time.minutesAgo',
    defaultMessage: ', {minutes} minutes ago',
  },
  hoursAgo: {
    id: 'time.hoursAgo',
    defaultMessage: ', {hours} hours ago',
  },
  daysAgo: {
    id: 'time.daysAgo',
    defaultMessage: ', {days} days ago',
  },
});

export const CalculateTime = (datePosted: string, formatMessage: any): string => {
  const diffSeconds = Math.floor(
    (new Date().getTime() - new Date(datePosted).getTime()) / 1000
  );

  if (diffSeconds < 60) {
    return formatMessage(messages.secondsAgo, { seconds: Math.floor(diffSeconds) });
  }
  if (diffSeconds < 60 * 60) {
    return formatMessage(messages.minutesAgo, { minutes: Math.floor(diffSeconds / 60) });
  }
  if (diffSeconds / 60 / 60 < 24) {
    return formatMessage(messages.hoursAgo, { hours: Math.floor(diffSeconds / 60 / 60) });
  }
  return formatMessage(messages.daysAgo, { days: Math.floor(diffSeconds / 86400) });
};
