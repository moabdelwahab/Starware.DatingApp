import { HttpStatusCode } from "src/app/common/StatusCode";

export class ApiResponse<T> {
    statusCode : HttpStatusCode;
    message: string;
    data: T;
}