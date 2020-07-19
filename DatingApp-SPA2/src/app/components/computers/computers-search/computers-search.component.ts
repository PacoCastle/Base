import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource, MatDialog } from '@angular/material';
import { MachineService } from '../common/machineService';
import { ComputersAddComponent } from '../computers-add/computers-add.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-computers-search',
  templateUrl: './computers-search.component.html',
  styleUrls: ['./computers-search.component.less']
})
export class ComputersSearchComponent implements OnInit {
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  dataSourceMachines = new MatTableDataSource<any>();
  columnsToDisplay: string[] = ['machineId', 'name', 'description', 'actions']
  constructor(private machineService: MachineService,
    private matDialog: MatDialog) { }

  ngOnInit() {
    this.searchMachines();
    
   
  }
  searchMachines() {
    this.machineService.getMachine().subscribe(res =>{
      this.dataSourceMachines.data = res;
    });
    this.dataSourceMachines.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourceMachines.filter = filterValue.trim().toLowerCase();
  }

  openModalAdd() {
    const dialogRef = this.matDialog.open(ComputersAddComponent, {
      disableClose: true,
      width: '60%'
    })
    dialogRef.afterClosed().subscribe((option) => {
      if(option){
        this.searchMachines();
      }
    });
  }

  openModalUpdate(machine: any){
    const dialogRef = this.matDialog.open(ComputersAddComponent, {
      disableClose: true,
      width: '60%'
    });
    dialogRef.componentInstance.detailMachine = machine;
    dialogRef.componentInstance.detailMachineOld = machine;
    dialogRef.componentInstance.update = true;
    dialogRef.afterClosed().subscribe((option) => {
      if(option){
        this.searchMachines();
      }
    });
  }

  deleteMachine(machine: any){
    let data = {
      Name: machine.name,
      Description: machine.description,
      Status: 0
    };
    this.machineService.updateMachine(machine.id, data).subscribe(res =>{
      Swal.fire("Success","Machine Successfully Deleted", "success")
    }, error =>{
      Swal.fire("Error Delete", error.error, "error");
    });
  }

}
