export class Pagination{
    TotalCount : number ;
    TotalPages : number ;
    PageSize : number = 10;
    PageNumber : number = 1;
}


export class PaginatedResult<T> 
{
    result : T;
    pagination:Pagination;
}