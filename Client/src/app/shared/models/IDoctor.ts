import { IPatient } from './IPatients';

export interface IDoctor {
  id: string;
  name: string;
  crm: string;
  crmUf: string;
  patients: IPatient[];
}
