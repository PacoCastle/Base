import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BalancingAttemptsService } from '../common/balancingAttemptsService';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-balancing-attempts-add',
  templateUrl: './balancing-attempts-add.component.html',
  styleUrls: ['./balancing-attempts-add.component.less']
})
export class BalancingAttemptsAddComponent implements OnInit {
  part: FormGroup;
  detailPart;
  detailPartOld;
  constructor(private balancingAttemptService: BalancingAttemptsService,) { }

  ngOnInit() {
    this.part = new FormGroup({
      machineName: new FormControl(this.detailPart.machineModelId),
      partName: new FormControl(this.detailPart.partModelId),
      consecutiveAttempt: new FormControl(this.detailPart.internalSequence),
      defaultAttempts: new FormControl(this.detailPart.defaultAttempts),
      extraAttempts: new FormControl(this.detailPart.extraAttempts? this.detailPart.extraAttempts:null, [Validators.required])
    });
  }

  get formPart(){
    return this.part.controls;
  }

  addMachine(){
    let data = {
      extraAttempts: this.formPart.extraAttempts.value
    };
    
    this.balancingAttemptService.updateBalancigAttempt(this.detailPart.id, data).subscribe(res =>{
      Swal.fire("Success","Machine Successfully Updated", "success")
    }, error =>{
      Swal.fire("Error Update", error.error, "error");
    });

    
  }

}
