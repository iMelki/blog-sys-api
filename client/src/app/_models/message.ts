export interface Message {
    id: number;
    senderUserId: number;
    senderUsername: string;
    recipientUserId: number;
    recipientUsername: string;
    content: string;
    dateRead?: Date;
    timePosted: Date;
  }