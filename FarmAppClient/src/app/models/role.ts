export class Role{
    constructor(
        public id: number,
        public roleName: string,
        public isDisabled: boolean,
        public isDeleted: boolean) { }
}