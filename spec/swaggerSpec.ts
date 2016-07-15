import { hasIds } from '../swagger';


describe("swagger", function () {
    describe("hasIds", function () {
        it("dictionary has no id", () => {
            expect(hasIds.get('dictionary')).toBe(false);
        });
        it("revoke has no id", () => {
            expect(hasIds.get('revoke')).toBe(false);
        });
        it("location has no id", () => {
            expect(hasIds.get('location')).toBe(false);
        });
        it("lookup has no id", () => {
            expect(hasIds.get('lookup')).toBe(false);
        });
        it("business-address has no id", () => {
            expect(hasIds.get('business-address')).toBe(false);
        });
        it("end has no id", () => {
            expect(hasIds.get('end')).toBe(false);
        });
        it("account has id", () => {
            expect(hasIds.get('account')).toBe(true);
        });
        it("extension has id", () => {
            expect(hasIds.get('extension')).toBe(true);
        });
        it("state has id", () => {
            expect(hasIds.get('state')).toBe(true);
        });
        it("meeting has id", () => {
            expect(hasIds.get('meeting')).toBe(true);
        });
        it("call-log has id", () => {
            expect(hasIds.get('call-log')).toBe(true);
        });
        it("restapi has id", () => {
            expect(hasIds.get('restapi')).toBe(true);
        });
    });
});