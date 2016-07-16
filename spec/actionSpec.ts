import { Action } from '../action';


describe("action", () => {
    describe("constructor", () => {
        it("should create /restapi", () => {
            const action = new Action('/restapi', 'get');
            expect(action.hasId).toBeFalsy();
            expect(action.method).toBe('get');
            expect(action.name).toBe('list');
            expect(action.path).toBe('/restapi');
            expect(action.segment).toBe('restapi');
            expect(action.queryString()).toBeNull();
        });
        it("should create /restapi/v1.0", () => {
            const action = new Action('/restapi/v1.0', 'get');
            expect(action.hasId).toBeTruthy();
            expect(action.method).toBe('get');
            expect(action.name).toBe('get');
            expect(action.path).toBe('/restapi/v1.0');
            expect(action.segment).toBe('restapi');
            expect(action.queryString()).toBeNull();
        });
        it("should create /restapi/v1.0/dictionary/state", () => {
            const action = new Action('/restapi/v1.0/dictionary/state', 'get');
            expect(action.hasId).toBeFalsy();
            expect(action.method).toBe('get');
            expect(action.name).toBe('list');
            expect(action.path).toBe('/restapi/v1.0/dictionary/state');
            expect(action.segment).toBe('state');
        });
        it("should create /restapi/v1.0/dictionary/country/{countryId}", () => {
            const action = new Action('/restapi/v1.0/dictionary/country/{countryId}', 'get');
            expect(action.hasId).toBeTruthy();
            expect(action.method).toBe('get');
            expect(action.name).toBe('get');
            expect(action.path).toBe('/restapi/v1.0/dictionary/country/{countryId}');
            expect(action.segment).toBe('country');
            expect(action.queryString()).toBeNull();
        });
        it('should generate queryString', () => {
            let action = new Action('/restapi/v1.0/dictionary/state', 'get');
            expect(action.queryString()).toEqual({ page: 1, perPage: 1, countryId: 1, withPhoneNumbers: true });
            action = new Action('/restapi/v1.0/account/{accountId}/phone-number', 'get');
            expect(action.queryString()).toEqual({ page: 1, perPage: 1, usageType: 's' });
        });
    });
});