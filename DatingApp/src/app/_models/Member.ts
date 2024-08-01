import { BasicInfo } from "./BasicInfo"

export interface Member {
  userProfileId: string
  basicInfo: BasicInfo
  dateCreated: Date
  lastModified: Date
}

