export const CalculateTime = (datePosted: string): string => {
  var diffSeconds = Math.floor(
    (new Date().getTime() - new Date(datePosted).getTime()) / 1000
  );

  if (diffSeconds < 60) return `, ${Math.floor(diffSeconds)} seconds ago`;
  if (diffSeconds < 60 * 60)
    return `, ${Math.floor(diffSeconds / 60)} minutes ago`;
  if (diffSeconds / 60 / 60 < 24) {
    return `, ${Math.floor(diffSeconds / 60 / 60)} hours ago`;
  }
  return `, ${Math.floor(diffSeconds / 86400)} days ago`;
};
