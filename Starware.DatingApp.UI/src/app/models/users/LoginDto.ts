export class LoginDto{

    constructor(Username? :string , Password?:string) {
        this.userName = Username;
        this.password = Password;
    }
    userName : string ;
    password :string;
}