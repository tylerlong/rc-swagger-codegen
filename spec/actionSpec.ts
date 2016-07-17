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
    });

    describe("queryParams", () => {
        it('should generate queryParams', () => {
            let action = new Action('/restapi/v1.0/dictionary/state', 'get');
            expect(action.queryParams()).toEqual({ page: 1, perPage: 1, countryId: 1, withPhoneNumbers: true });
            action = new Action('/restapi/v1.0/account/{accountId}/phone-number', 'get');
            expect(action.queryParams()).toEqual({ page: 1, perPage: 1, usageType: 's' });
        });
    });

    describe("requestBody", () => {
        it('should generate requestBody', () => {
            let action = new Action('/restapi/v1.0/dictionary/state', 'get');
            expect(action.requestBody()).toBeNull();
            action = new Action('/restapi/v1.0/account/{accountId}/phone-number', 'get');
            expect(action.requestBody()).toBeNull();
            action = new Action('/restapi/v1.0/account/{accountId}/extension/{extensionId}/meeting', 'post');
            expect(Array.isArray(action.requestBody())).toBeFalsy();
            action = new Action('/restapi/v1.0/account/{accountId}/extension/{extensionId}', 'put');
            expect(Array.isArray(action.requestBody())).toBeTruthy();
        });
    });
});


describe("actions", () => {
    describe("number of actions", () => {
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
    describe("requestBody", () => {
        it("should generate for create meeting", () => {
            const action = actions.get('meeting').find(item => item.method == 'post');
            expect(action).not.toBe(null);
            const requestBody = action.requestBody();
            expect(requestBody).not.toEqual({});
            expect(requestBody).toEqual({ topic: 's', meetingType: 's',
                password: 's', schedule: { startTime: 's', durationInMinutes: 1, timeZone: {
                    id: 's'
                } }, allowJoinBeforeHost: true, startHostVideo: true,
                startParticipantsVideo: true, audioOptions: ['s'] });
        });
        it("should generate for update contact", () => {
            const action = actions.get('contact').find(item => item.method == 'put');
            expect(action).not.toBe(null);
            const requestBody = action.requestBody();
            expect(requestBody).not.toEqual({});
            expect(requestBody).toEqual({ id: 1, url: 's', availability: 's', firstName: 's',
                lastName: 's', middleName: 's', nickName: 's', company: 's', jobTitle: 's',
                homePhone: 's', homePhone2: 's', businessPhone: 's', businessPhone2: 's', mobilePhone: 's',
                businessFax: 's', companyPhone: 's', assistantPhone: 's', carPhone: 's', otherPhone: 's',
                otherFax: 's', callbackPhone: 's', email: 's', email2: 's', email3: 's',
                homeAddress: { country: 's', state: 's', city: 's', street: 's', zip: 's' },
                businessAddress: { country: 's', state: 's', city: 's', street: 's', zip: 's' },
                otherAddress: { country: 's', state: 's', city: 's', street: 's', zip: 's' },
                birthday: 's', webPage: 's', notes: 's' });
        });
    });
    describe("responseBody", () => {
        it("should generate for create meeting", () => {
            const action = actions.get('meeting').find(item => item.method == 'post');
            expect(action).not.toBe(null);
            const responseBody = action.responseBody();
            expect(responseBody).not.toEqual({});
            expect(responseBody).toEqual({ uri: 's', id: 's', topic: 's', meetingType: 's', password: 's',
                status: 's', links: { startUri: 's', joinUri: 's' }, schedule: { startTime: 's', durationInMinutes: 1,
                timezone: { id: 's' }}, allowJoinBeforeHost: true, startHostVideo: true,
                startParticipantsVideo: true, audioOptions: ['s']});
        });
    });
});