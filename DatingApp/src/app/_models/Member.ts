import { BasicInfo } from "./BasicInfo"
import {Photo} from "./Photo";

export interface Member {
  userProfileId: string
  basicInfo: BasicInfo
  dateCreated: Date
  lastModified: Date
  photos: Photo[]
}

