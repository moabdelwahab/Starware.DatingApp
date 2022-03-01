export class Pagination{
    TotalCount : number ;
    TotalPages : number ;
    PageSize : number ;
    PageNumber : number ;
}


export class PaginatedResult<T> 
{
    result : T;
    pagination:Pagination;
}