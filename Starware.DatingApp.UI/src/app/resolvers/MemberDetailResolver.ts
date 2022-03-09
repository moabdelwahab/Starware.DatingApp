import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { ApiResponse } from "../models/common/ApiResponse";
import { MemberDto } from "../models/users/MemberDto";
import { UsersService } from "../services/users.service";

@Injectable({providedIn:'root'})

export class MemberDetailResolver implements Resolve<ApiResponse<MemberDto>>{

    constructor(private userservice:UsersService){

    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): ApiResponse<MemberDto> | Observable<ApiResponse<MemberDto>> | Promise<ApiResponse<MemberDto>> {
        return this.userservice.getUserByUsername(route.paramMap.get('username'));
    }

}