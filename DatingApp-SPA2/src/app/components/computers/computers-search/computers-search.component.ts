import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource, MatDialog } from '@angular/material';
import { MachineService } from '../common/machineService';
import { ComputersAddComponent } from '../computers-add/computers-add.component';

@Component({
  selector: 'app-computers-search',
  templateUrl: './computers-search.component.html',
  styleUrls: ['./computers-search.component.less']
})
export class ComputersSearchComponent implements OnInit {
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  dataSourceMachines: MatTableDataSource<any>;
  columnsToDisplay: string[] = ['machineId', 'name', 'description', 'actions']
  constructor(private machineService: MachineService,
    private matDialog: MatDialog) { }

  ngOnInit() {
    this.searchMachines();
    
   
  }
  searchMachines() {
    this.machineService.getMachine().subscribe(res =>{
      this.dataSourceMachines = new MatTableDataSource<any>(res);
      this.dataSourceMachines.paginator = this.paginator;
    })
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

}
