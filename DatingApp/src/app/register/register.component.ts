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
import {Router} from "@angular/router";

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
  private router = inject(Router)
  cancelRegister = output<boolean>();
  model: any = {}
  registerForm: FormGroup = new FormGroup({});
  maxDate = new Date();
  validationErrors: string[] | undefined;

  ngOnInit(): void {
   this.initializeForm();
  }

/*  initializeForm(){
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
  }*/

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : {isMatching: true}
    }
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      firstname: ['', Validators.required],
      lastName: ['', Validators.required],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      introduction: ['', Validators.required],
      interests: ['', Validators.required],
      photoUrl: ['', Validators.required],
      lookingFor: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required],
      dateofbirth: ['', Validators.required],
      phone: ['', Validators.required],
      currentCity: ['', Validators.required]
    });
  }

  register() {
    if (this.registerForm.valid) {
      const model = this.prepareModel();
      this.accountService.register(model).subscribe({
        next: response => this.router.navigateByUrl('/members'),
        error: error => console.log(error)
      });
    }
  }

  prepareModel() {
    return {
      FirstName: this.registerForm.get('firstname')?.value,
      LastName: this.registerForm.get('lastName')?.value,
      Username: this.registerForm.get('username')?.value,
      knownAs: this.registerForm.get('knownAs')?.value,
      introduction: this.registerForm.get('introduction')?.value,
      lookingFor: this.registerForm.get('lookingFor')?.value,
      photoUrl: this.registerForm.get('photoUrl')?.value,
      interests: this.registerForm.get('interests')?.value,
      Password: this.registerForm.get('password')?.value,
      DateOfBirth: this.registerForm.get('dateofbirth')?.value,
      Phone: this.registerForm.get('phone')?.value,
      CurrentCity: this.registerForm.get('currentCity')?.value
    };
  }

 /* register() {
    this.accountService.register(this.model).subscribe({
      next: response => this.router.navigateByUrl('/members'),

    })
  }
  private getDateOnly(dob: string | undefined) {
    if (!dob) return;
    return new Date(dob).toISOString().slice(0,10);
  }*/
  cancel() {
    this.cancelRegister.emit(false);
  }
}
