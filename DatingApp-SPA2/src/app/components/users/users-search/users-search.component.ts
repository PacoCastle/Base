import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator } from '@angular/material';

@Component({
  selector: 'app-users-search',
  templateUrl: './users-search.component.html',
  styleUrls: ['./users-search.component.less']
})
export class UsersSearchComponent implements OnInit {
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  dataSourceUsers: MatTableDataSource<any>;
  columnsToDisplay: string[] = ['employeeId', 'name', 'motherLastName', 'actions']
  constructor() { }
 
  ngOnInit() {
    this.dataSourceUsers = new MatTableDataSource<any>();
    this.dataSourceUsers.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourceUsers.filter = filterValue.trim().toLowerCase();
  }

}
