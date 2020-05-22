import { BaseModel } from './basemodel';
export class User extends BaseModel{
  firstName: string;
  lastName: string;
  username: string;
  password: string;
  token?: string;
}
