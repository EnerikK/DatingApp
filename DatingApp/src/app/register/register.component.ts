import {Component, inject, input, Input, OnInit, output} from '@angular/core';
import {
  AbstractControl, FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidatorFn,
  Validators
} from "@angular/forms";
import { AccountService } from '../_services/account.service'
import {ToastrService} from 'ngx-toastr';
import {JsonPipe, NgIf} from "@angular/common";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, JsonPipe,NgIf],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{

  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);
  private fb = inject(FormBuilder);
  cancelRegister = output<boolean>();
  model: any = {}
  registerForm: FormGroup = new FormGroup({});
  maxDate = new Date();

  ngOnInit(): void {
   this.initializeForm();
   this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initializeForm(){
    this.registerForm = new FormGroup({
      gender: new FormControl(['']),
      username: new FormControl('',Validators.required),
      password: new FormControl('',Validators.required),
      confirmPassword: new FormControl('',[Validators.required , this.matchValues('password')]),
      firstname: new FormControl('',Validators.required),
      lastName: new FormControl('',Validators.required),
      dateofbirth: new FormControl('',Validators.required),
      phone: new FormControl('',Validators.required),
      currentCity: new FormControl('',Validators.required),
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {isMatching: true}
    }
  }

  register() {
    console.log(this.registerForm.value);
/*    this.accountService.register(this.model).subscribe({
      next: response => {
        console.log(response);
        this.cancel();
      },
      error: error => this.toastr.error(error.error)
    })*/
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
