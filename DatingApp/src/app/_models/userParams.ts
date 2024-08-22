import {Member} from "./Member";

export class UserParams {
  gender: string;
  minAge = 18;
  maxAge = 99;
  pageNumber = 1;
  pageSize = 3;
  orderBy = 'lastmodified';

  constructor(member: Member | null ) {
    this.gender = member?.basicInfo.gender === 'female' ? 'male' : 'female'
  }
}
