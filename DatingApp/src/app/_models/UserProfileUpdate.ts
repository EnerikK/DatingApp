import {Photo} from "./Photo";

export interface UserProfileUpdate {
  firstName: string;
  lastName: string;
  emailAddress: string;
  phone: string;
  dateOfBirth: string;  // This can be a string or Date depending on how you manage dates in your application
  currentCity: string;
  knownAs: string;
  introduction: string;
  interests: string;
  lookingFor: string;
  photoUrl: string
  photos: Photo
}
