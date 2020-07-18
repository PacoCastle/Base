import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MachineService } from '../common/machineService';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-computers-add',
  templateUrl: './computers-add.component.html',
  styleUrls: ['./computers-add.component.less']
})
export class ComputersAddComponent implements OnInit {
  machine: FormGroup;
  update: boolean;
  detailMachine;
  detailMachineOld;

  constructor(private machineService: MachineService) { }
  
  ngOnInit() {
    if(this.update){
      this.machine = new FormGroup({
        name: new FormControl(this.detailMachine.name,[Validators.required]),
        description: new FormControl(this.detailMachine.description),
      });
    } else {
      this.machine = new FormGroup({
        name: new FormControl(null,[Validators.required]),
        description: new FormControl(null),
        status: new FormControl(null, [Validators.required])
      });
    }
  }

  get formMachine(){
    return this.machine.controls;
  }

  addMachine(){
    let data = {
      Name: this.formMachine.name.value,
      Description: this.formMachine.description.value,
      Status: this.formMachine.status.value
    }
    this.machineService.addMachine(data).subscribe(res =>{
      Swal.fire("Success","Machine Successfully Added", "success")
    }, error =>{
      Swal.fire("Error", error, "error");
    });
  }

}
