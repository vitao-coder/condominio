import { Bloco } from './bloco';
import { BaseModel } from './basemodel';

export class Apartamento extends BaseModel{
  bloco: Bloco;
  numero: number;
}

