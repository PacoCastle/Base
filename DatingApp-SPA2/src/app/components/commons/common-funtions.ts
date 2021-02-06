import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

/**
 * Clase que contiene las funciones comunes
 *
 * @export
 * @class CommonFuntions
 */
@Injectable({
    providedIn: 'root'
  })
export class CommonFuntions {

    /**
     * Metodo que valida que el dato no sea null en caso de que si lo remplasa por ""
     *
     * @param {*} dato Atributo que representa el valor a validar
     * @memberof CommonFuntions
     */
    parseNull( dato:any){
        if (dato === null || dato === undefined){
            dato= "";
        } 
        return dato;
    }

    static patternValidator(regex: RegExp, error: ValidationErrors): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } => {
          if (!control.value) {
            // if control is empty return no error
            return null;
          }
      
          // test the value of the control against the regexp supplied
          const valid = regex.test(control.value);
      
          // if true, return no error (no error), else return error passed in the second parameter
          return valid ? null : error;
        };
    }

    static passwordMatchValidator(control: AbstractControl) {
        const password: string = control.get('password').value; // get password from our password form control
        const confirmPassword: string = control.get('cpassword').value; // get password from our confirmPassword form control
        // compare is the password math
        if (password !== confirmPassword) {
          // if they don't match, set an error in our confirmPassword form control
          control.get('cpassword').setErrors({ NoPassswordMatch: true });
        }
      }
}