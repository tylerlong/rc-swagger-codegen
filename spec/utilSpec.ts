import { isValidSegment } from '../util';


describe("util", () => {
    describe("isValidSegment", () => {
        it("should identify valid segments", () => {
            expect(isValidSegment('account')).toBeTruthy();
            expect(isValidSegment('addess-book')).toBeTruthy();
        });
        it("should identify invalid segments", () => {
            expect(isValidSegment('{accountId}')).toBeFalsy();
            expect(isValidSegment('v1.0')).toBeFalsy();
        });
    });
});