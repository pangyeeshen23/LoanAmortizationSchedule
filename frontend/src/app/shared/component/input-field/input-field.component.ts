import { NgIf } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'input-field',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf],
  templateUrl: './input-field.component.html',
  styleUrl: './input-field.component.css'
})
export class InputFieldComponent {
  @Input() label: String = ""; 
  @Input() inputType: String = "";
  @Input() control!: FormControl;

  getInputType(): String{
    let inputType : String = this.inputType;
    if(this.inputType == "year") inputType = "number";
    return inputType;
  }

  checkCharacter(event : any){
    let allowed : Boolean = true;
    let value = this.control.value || null;
    switch(this.inputType){
      case "number":
        allowed = this.preventCharForNumInput(event, value)
      break;
      case "year":
        allowed = this.preventCharForYearInput(event, value)
      break;
    }
    if(allowed){
      return allowed;
    }else{
      event.stopPropagation();
      return false;
    }
  }

  preventCharForNumInput(event : any, value : String): Boolean{
    if(!this.preventInvalidCharacterForNum(event)) return false;
    return true;
  }

  preventCharForYearInput(event : any, value : String): Boolean{
    if(!this.preventInvalidCharacterForNum(event)) return false;
    return true;
  }

  preventInvalidCharacterForNum(event : any): Boolean{
    var invalidChars = ["-", "e", "+", "E"];
    if(invalidChars.includes(event.key)){
      return false;
    }
    return true;
  }
}
