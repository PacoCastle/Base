import { Component, OnInit, ViewChild } from '@angular/core';
import { BalancingAttemptsService } from '../common/balancingAttemptsService';
import { MatPaginator, MatTableDataSource, MatDialog } from '@angular/material';
import { MachineService } from 'app/components/computers/common/machineService';
import Swal from 'sweetalert2';
import { BalancingAttemptsAddComponent } from '../balancing-attempts-add/balancing-attempts-add.component';

@Component({
  selector: 'app-balancing-attempts-search',
  templateUrl: './balancing-attempts-search.component.html',
  styleUrls: ['./balancing-attempts-search.component.less']
})
export class BalancingAttemptsSearchComponent implements OnInit {
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  dataSourceBalancingParts = new MatTableDataSource<any>();

  columnsToDisplay: string[] = ['machineName', 'partName', 'attemptConsecutive', 'defaultAttempts',
                                  'extraAttempts', 'availableAttempts', 'actions'];
  machineList;
  machine: any;
  constructor(private balancingAttemptService: BalancingAttemptsService,
    private machineService: MachineService,
    private matDialog: MatDialog) { }

  ngOnInit() {
    this.machineService.getMachine().subscribe(list => {
      this.machineList = list;
    }, error => {
      Swal.fire("Error", error.error, "error");
    });
  }

  searchAttemptsPerMachine(event?: any) {
    if(event){
      this.machine = event.value;
    }

    this.balancingAttemptService.searchPartsPerMachine(this.machine).subscribe(res => {
        this.dataSourceBalancingParts = res;
    });
    this.dataSourceBalancingParts.paginator = this.paginator;
  }

  openModalUpdate(machine: any){
    const dialogRef = this.matDialog.open(BalancingAttemptsAddComponent, {
      disableClose: true,
      width: '60%'
    });
    dialogRef.componentInstance.detailPart = machine;
    dialogRef.componentInstance.detailPartOld = machine;
    dialogRef.afterClosed().subscribe((option) => {
      if(option){
        this.searchAttemptsPerMachine();
      }
    });
  }
}
