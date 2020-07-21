import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatDialog } from '@angular/material';
import { UsersAddComponent } from '../users-add/users-add.component';
import Swal from 'sweetalert2';
import { UserService } from '../common/userService';

@Component({
  selector: 'app-users-search',
  templateUrl: './users-search.component.html',
  styleUrls: ['./users-search.component.less']
})
export class UsersSearchComponent implements OnInit {
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  dataSourceUsers = new MatTableDataSource<any>();
  columnsToDisplay: string[] = ['employeeId', 'name', 'motherLastName', 'actions']

  constructor(private matDialog: MatDialog,
    private userService: UserService) { }
 
  ngOnInit() {
   this.searchUsers();
  }
  
  searchUsers(){
    this.userService.getUsers().subscribe(res =>{
      this.dataSourceUsers.data = res;
    });
    this.dataSourceUsers.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourceUsers.filter = filterValue.trim().toLowerCase();
  }
  openModalAdd() {
    const dialogRef = this.matDialog.open(UsersAddComponent, {
      disableClose: true,
      width: '60%'
    })
    dialogRef.afterClosed().subscribe((option) => {
      if(option){
        this.searchUsers();
      }
    });
  }

  openModalUpdate(user: any){
    const dialogRef = this.matDialog.open(UsersAddComponent, {
      disableClose: true,
      width: '60%',
      maxHeight: '80vh'
    });
    dialogRef.componentInstance.detailUser = user;
    dialogRef.componentInstance.detailUserOld = user;
    dialogRef.componentInstance.update = true;
    dialogRef.afterClosed().subscribe((option) => {
      if(option){
        this.searchUsers();
      }
    });
  }

  deleteUser(user: any){
    let data = {
      Name: user.name,
      Description: user.description,
      Status: 0
    };
    this.userService.updateUser(user.id, data).subscribe(res =>{
      Swal.fire("Success","User Successfully Deleted", "success")
    }, error =>{
      Swal.fire("Error Delete", error.error, "error");
    });
  }

}
