import { Apartamento } from './apartamento';
import { BaseModel } from './basemodel';

export class Morador{
  nomeCompleto: string;
  dataNascimento: Date;
  fone: string;
  cpf: number;
  apartamento: Apartamento;
}
