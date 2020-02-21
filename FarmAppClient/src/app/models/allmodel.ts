export class User {
    public id: number;
    public login: string;
    public password: string;
    public userName: string;
    public roleId: number;
    public roleName: string;
    public isDisabled: boolean;
    public isDeleted: boolean;
}

export class Role {
    public id: number;
    public roleName: string;
    public isDisabled: boolean;
    public isDeleted: boolean;
}

export class RequestBody {
    public mapRoute: string;
    public requestTime: any = new Date(); // .getDate();
    public param: User;
}

export class ResponseSuccess {
    public id: number;
    public result: string;
}
