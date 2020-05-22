import { BaseModel } from './basemodel';
import { Apartamento } from './apartamento';

export class Bloco extends BaseModel{
    descricao: string;
    apartamentos: Apartamento[];
}
