import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { HttpStatusCode } from '../common/StatusCode';
import { MemberEditComponent } from '../components/members/member-edit/member-edit.component';
import { ApiResponse } from '../models/common/ApiResponse';
import { PaginatedResult } from '../models/common/Pagination';
import { MemberDto } from '../models/users/MemberDto';
import { memberSearch } from '../models/users/memberSearch';
import { PhotoDto } from '../models/users/photoDto';
import { AccountService } from './account.service';

const apiUrl: string = environment.apiRoot + 'Users/';

@Injectable({
  providedIn: 'any'
})

export class UsersService {

  memberCashe = new Map();
  usersResponse: ApiResponse<MemberDto[]>;
  memberSearchParams:memberSearch;

  constructor(private httpClient: HttpClient) {
  this.memberSearchParams = new memberSearch();
  }

  getMemberSearch() : memberSearch
  {
    return this.memberSearchParams;
  }

  setMemberSearch(memberSearch :memberSearch)
  {
    this.memberSearchParams = memberSearch;
  }

  getAllUsers(memberSearch: memberSearch, page?: number, pageSize?: number): Observable<PaginatedResult<ApiResponse<MemberDto[]>>> {



    if (memberSearch) {
      if (this.memberCashe.get(Object.values(memberSearch).join('-'))) {
        return of(this.memberCashe.get(Object.values(memberSearch).join('-')));
      }
    }

    var params = new HttpParams();

    if (pageSize !== 0 && page !== 0) {
      params = params.set('PageSize', pageSize.toString()).set('PageNumber', page.toString());
    }

    if (memberSearch) {
      params = params.set('Name', memberSearch.name)
        .set('FromAge', memberSearch.fromAge.toString())
        .set('ToAge', memberSearch.toAge.toString())
        .set('OrderBy', memberSearch.OrderBy);
    }

    return this.httpClient.get<ApiResponse<MemberDto[]>>(apiUrl + 'GetAll', { observe: 'response', params: params })
      .pipe(
        map((response) => {

          let paginatingResult: PaginatedResult<ApiResponse<MemberDto[]>> = new PaginatedResult<ApiResponse<MemberDto[]>>();
          paginatingResult.result = response.body;
          if (response.headers.get('Pagination')) {
            paginatingResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          this.memberCashe.set(Object.values(memberSearch).join('-'), paginatingResult)
          console.log(this.memberCashe);
          return paginatingResult;

        })
      );
  }


  getUserByUsername(username: string): Observable<ApiResponse<MemberDto>> {

    const member = [...this.memberCashe.values()]
      .reduce((arr, elem) => arr.concat(elem.result.data), []).find((member:MemberDto) =>
        member.userName.toLowerCase() === username.toLocaleLowerCase());
      
      console.log(member);
    let response = new ApiResponse<MemberDto>();
    if (member) {
      response.data = member;
      response.statusCode = HttpStatusCode.OK;
      return of(response);
    }
    return this.httpClient.get<ApiResponse<MemberDto>>(apiUrl + 'GetUserByUsername/' + username);
  }

  updateUser(member: MemberDto): Observable<boolean> {
    return this.httpClient.put<boolean>(apiUrl + 'updateUser', member).pipe(
      map((response) => {
        if (this.usersResponse?.data?.length > 0) {
          let memberIndex = this.usersResponse.data.indexOf(member);
          this.usersResponse.data[memberIndex] = member;
        }
        return response;
      })
    );
  }


  addPhoto(FormData: FormData): Observable<ApiResponse<PhotoDto>> {
    return this.httpClient.post<ApiResponse<PhotoDto>>(apiUrl + 'add-photo', FormData);
  }

  deletePhoto(publicId: string): Observable<ApiResponse<boolean>> {
    return this.httpClient.delete<ApiResponse<boolean>>(apiUrl + 'delete-photo/' + publicId);
  }

  setMainPhoto(photoId: number): Observable<ApiResponse<boolean>> {
    return this.httpClient.put<ApiResponse<boolean>>(apiUrl + 'set-main-photo/' + photoId, null);
  }
}
