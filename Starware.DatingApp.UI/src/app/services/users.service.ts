import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { getPaginatedResult, getPaginationHttpParams } from '../common/PaginationHelper';
import { HttpStatusCode } from '../common/StatusCode';
import { MemberEditComponent } from '../components/members/member-edit/member-edit.component';
import { ApiResponse } from '../models/common/ApiResponse';
import { PaginatedResult, Pagination } from '../models/common/Pagination';
import { LikeDto } from '../models/users/LikeDto';
import { MemberDto } from '../models/users/MemberDto';
import { memberSearch } from '../models/users/memberSearch';
import { PhotoDto } from '../models/users/photoDto';
import { AccountService } from './account.service';

const apiUrl: string = environment.apiRoot + 'Users/';
const likesApiUrl: string = environment.apiRoot + 'Like/';


@Injectable({
  providedIn: 'any'
})

export class UsersService {
  resetMemberSearch() {
    this.memberSearchParams = new memberSearch();
  }

  memberCashe = new Map();
  usersResponse: ApiResponse<MemberDto[]>;
  memberSearchParams: memberSearch;

  constructor(private httpClient: HttpClient) {
    this.memberSearchParams = new memberSearch();
  }

  getMemberSearch(): memberSearch {
    return this.memberSearchParams;
  }

  setMemberSearch(memberSearch: memberSearch) {
    this.memberSearchParams = memberSearch;
  }

  getAllUsers(page?: number, pageSize?: number): Observable<PaginatedResult<ApiResponse<MemberDto[]>>> {

    if (this.memberSearchParams) {
      if (this.memberCashe.get(Object.values(this.memberSearchParams).join('-') + '-' + page)) {
        return of(this.memberCashe.get(Object.values(this.memberSearchParams).join('-') + '-' + page));
      }
    }

    var params = new HttpParams();

    if (pageSize !== 0 && page !== 0) {
      params = getPaginationHttpParams(page, pageSize);
    }

    if (this.memberSearchParams) {
      params = params.set('Name', this.memberSearchParams.name)
        .set('FromAge', this.memberSearchParams.fromAge.toString())
        .set('ToAge', this.memberSearchParams.toAge.toString())
        .set('OrderBy', this.memberSearchParams.OrderBy);
    }

    return getPaginatedResult<ApiResponse<MemberDto[]>>(apiUrl + 'GetAll', params, this.httpClient).pipe(
      map((response) => {
        this.memberCashe.set(Object.values(this.memberSearchParams).join('-') + '-' + page, response)
        return response;
      }));
  }


  getUserByUsername(username: string): Observable<ApiResponse<MemberDto>> {

    const member = [...this.memberCashe.values()]
      .reduce((arr, elem) => arr.concat(elem.result.data), []).find((member: MemberDto) =>
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

  addUserLike(id: number): Observable<ApiResponse<boolean>> {
    return this.httpClient.post<ApiResponse<boolean>>(likesApiUrl + 'add-user-like', id);
  }

  getikes(pagination: Pagination, predicate: string): Observable<PaginatedResult<ApiResponse<LikeDto[]>>> {
    var params = new HttpParams();

    params = params.set('PageNumber', pagination.PageNumber.toString())
      .set('PageSize', pagination.PageSize.toString())
      .set('Predicate', predicate);

    return this.httpClient.get<ApiResponse<LikeDto[]>>(likesApiUrl + "get-user-likes", { observe: 'response', params: params })
      .pipe(
        map((response) => {

          let likesPaginationResult = new PaginatedResult<ApiResponse<LikeDto[]>>();

          likesPaginationResult.pagination = JSON.parse(response.headers.get('Pagination'));

          likesPaginationResult.result = response.body;

          return likesPaginationResult;
        })
      );
  }

  deleteLike(likeUserId: number) {
    var params = new HttpParams();
    params = params.set('likeUserId', likeUserId.toString());
    return this.httpClient.delete(likesApiUrl + 'delete-like', { params: params });
  }

}
