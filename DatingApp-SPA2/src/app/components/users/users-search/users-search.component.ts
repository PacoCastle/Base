import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatDialog } from '@angular/material';
import { UsersAddComponent } from '../users-add/users-add.component';
import Swal from 'sweetalert2';
import { UserService } from '../common/userService';
import { CommonFuntions} from '../../commons/common-funtions'

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
    private userService: UserService,private commonFuntion: CommonFuntions ) { }
 
  ngOnInit() {
   this.searchUsers();
  }
  
  searchUsers(){
    this.userService.getUsers().subscribe(res =>{
      this.dataSourceUsers.data = res.dataResponse;
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
      password: this.commonFuntion.parseNull(user.password),
      email: this.commonFuntion.parseNull(user.email),
      name: this.commonFuntion.parseNull(user.name),
      lastName: this.commonFuntion.parseNull(user.lastName),
      secondLastName: this.commonFuntion.parseNull(user.secondLastName),
      roleNames: this.getRoleList(user.roleNames),
      status: 0,
      sexo: this.commonFuntion.parseNull(user.sexo),
    };
    this.userService.updateUser(user.id, data).subscribe(res =>{
      Swal.fire("Success","User Successfully Deleted", "success")
    }, error =>{
      let e:string = "";
      error.error.errors.forEach(element => {
        e += `${element}\n`; 
      });
      Swal.fire("Error Delete", e, "error");
    });
  }

    /**
   * Metodo que se encarga de obtener la lista de roles
   *
   * @param {MatTableDataSource<any>} dataSourceRoleNew
   * @memberof UsersAddComponent
   */
  getRoleList(dataSourceRoleNew:any) {
    let roleList= [];
    if(dataSourceRoleNew !== null){
      dataSourceRoleNew.forEach(element => {
        roleList.push(element.name)
      });
    } else {
      roleList.push("");
    }
    return roleList;
  }

}
