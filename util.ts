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

const isListAction = (name: string, value): boolean => {
    if (name == '/restapi') {
        return true;
    }
    const properties = value.get.responses.default.schema.properties;
    if (properties != undefined && properties.navigation != undefined) {
        return true;
    }
    return false;
}


export { isValidSegment, isListAction };