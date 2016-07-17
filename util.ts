const isValidSegment = (s: string): boolean => {
    if (s == '') {
        return false;
    }
    if (s == 'v1.0') {
        return false;
    }
    if (s[0] == '{') {
        return false;
    }
    return true;
};


export { isValidSegment };