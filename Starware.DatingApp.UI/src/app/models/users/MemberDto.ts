import { PhotoDto } from "./photoDto";

export class MemberDto {
    id: number;
    userName: string;
    firstName: string;
    middleName: string;
    lastName: string;
    gender: string;
    age: number;
    knownAs: string;
    interests: string;
    introduction: string;
    lookingFor: string;
    lastActive:Date;
    city: string;
    country: string;
    photoUrl: string;
    photos: PhotoDto[];
}