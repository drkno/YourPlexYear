export const preloadImage = src => {
    const img = new Image();
    img.src = src;
};

export const pluralise = (count, string) => {
    return count !== 1 ? string + 's' : string;
};
