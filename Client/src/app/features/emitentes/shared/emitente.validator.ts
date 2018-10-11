import { AbstractControl, ValidationErrors } from '@angular/forms';

export class EmitenteValidators {
  public static checkCPNJ(control: AbstractControl): ValidationErrors | null {
    const cnpj: any = control.get('numerodocumento');
     const cnpjmodelo: string = '00.000.000/0000-00';
    if (!(cnpj)) return null;

    return cnpj.value !== cnpjmodelo ? null : { invalid: true };
  }
}
