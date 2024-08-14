export interface DogMeeting {
    // Define las propiedades de DogMeeting según lo que tengas en el modelo C#
    meetingId: number;
    // Añade otras propiedades necesarias
  }

export interface Meeting {
    id: number;
    meetingTitle?: string;
    maxParticpants: number;
    description?: string;
    location?: string;
    date: Date;
    dogMeetings?: DogMeeting[];
  }