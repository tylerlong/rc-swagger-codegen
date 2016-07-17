import { Action, actions } from '../action';


describe("action", () => {
    describe("constructor", () => {
        it("should create /restapi", () => {
            const action = new Action('/restapi', 'get');
            expect(action.hasId).toBeFalsy();
            expect(action.method).toBe('get');
            expect(action.name).toBe('list');
            expect(action.path).toBe('/restapi');
            expect(action.segment).toBe('restapi');
            expect(action.queryParams()).toBeNull();
        });
        it("should create /restapi/v1.0", () => {
            const action = new Action('/restapi/v1.0', 'get');
            expect(action.hasId).toBeTruthy();
            expect(action.method).toBe('get');
            expect(action.name).toBe('get');
            expect(action.path).toBe('/restapi/v1.0');
            expect(action.segment).toBe('restapi');
            expect(action.queryParams()).toBeNull();
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
            expect(action.queryParams()).toBeNull();
        });
        it('should generate queryString', () => {
            let action = new Action('/restapi/v1.0/dictionary/state', 'get');
            expect(action.queryParams()).toEqual({ page: 1, perPage: 1, countryId: 1, withPhoneNumbers: true });
            action = new Action('/restapi/v1.0/account/{accountId}/phone-number', 'get');
            expect(action.queryParams()).toEqual({ page: 1, perPage: 1, usageType: 's' });
        });
    });
    describe("actions", () => {
        it("should contain no action", () => {
            expect(actions.has('dictionary')).toBeFalsy();
            expect(actions.has('oauth')).toBeFalsy();
        });
        it("should contain 1 action", () => {
            expect(actions.get('revoke').length).toBe(1);
            expect(actions.get('location').length).toBe(1);
        });
        it("should contain 2 actions", () => {
            expect(actions.get('timezone').length).toBe(2);
            expect(actions.get('state').length).toBe(2);
        });
        it("should contain 5 actions", () => {
            expect(actions.get('meeting').length).toBe(5);
        });
    });
});