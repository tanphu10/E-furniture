import { Injectable } from "@angular/core";


const TOKEN_KEY='auth-token';
const REFRESH_TOKEN='auth-refreshtoken';

@Injectable({
    providedIn:'root',
}) 
export class TokenStorageService{
    constructor(){}
    signOut():void{
        window.sessionStorage.clear();
    }
    public saveToken(token:string):void{
        window.sessionStorage.removeItem(TOKEN_KEY);
        window.sessionStorage.setItem(TOKEN_KEY,token);
    }
    public getToken():string|null{
        return window.sessionStorage.getItem(TOKEN_KEY);
    }

    public saveRefreshToken(refreshToken:string):void{
        window.sessionStorage.removeItem(REFRESH_TOKEN);
        window.sessionStorage.setItem(REFRESH_TOKEN,refreshToken);
    }
    public getRefreshToken():string|null{
        return window.sessionStorage.getItem(REFRESH_TOKEN);
    }
}