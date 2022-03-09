import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs/operators";
import { ApiResponse } from "../models/common/ApiResponse";
import { PaginatedResult } from "../models/common/Pagination";

export function getPaginatedResult<T>(url:string , pararms:HttpParams ,http:HttpClient)
{
    const paginatedResult :PaginatedResult<T> = new PaginatedResult<T>();
     
    return http.get<T>(url, { observe: 'response', params : pararms})
    .pipe(
      map((response) => {

        paginatedResult.result = response.body;

        if (response.headers.get('Pagination')) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
}

export function getPaginationHttpParams(pagenumber:number , pageSize:number)
{
    let params = new HttpParams();
    params = params.set('PageSize', pageSize.toString()).set('PageNumber', pagenumber.toString());
    return params
}