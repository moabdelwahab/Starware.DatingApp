export interface Message {
    id: number;
    creationDate: Date;
    senderId: number;
    senderUsername: string;
    senderPhotoUrl: string;
    recipientId: number;
    recipientUsername: string;
    recipientPhotoUrl: string;
    content: string;
    dateReaded?: Date;
    recipientDeleted: boolean;
    senderDeleted: boolean;
}