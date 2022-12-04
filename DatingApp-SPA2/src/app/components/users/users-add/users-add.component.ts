import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, ValidatorFn } from '@angular/forms';
import { UserService } from '../common/userService';
import Swal from 'sweetalert2';
import { MatPaginator, MatTableDataSource, MatDialogRef } from '@angular/material';
import { CommonFuntions } from 'app/components/commons/common-funtions';

@Component({
  selector: 'app-users-add',
  templateUrl: './users-add.component.html',
  styleUrls: ['./users-add.component.less']
})
export class UsersAddComponent implements OnInit {
  /**
   * User Form
   *
   * @type {FormGroup}
   * @memberof UsersAddComponent
   */
  user: FormGroup;

  /**
   * Update flag
   *
   * @type {boolean}
   * @memberof UsersAddComponent
   */
  update: boolean;

  /**
   * New User information
   *
   * @memberof UsersAddComponent
   */
  detailUser;

  /**
   * Old user information
   *
   * @memberof UsersAddComponent
   */
  detailUserOld;

  /**
   *Viewchild of the user roles table paginator
   *
   * @type {MatPaginator}
   * @memberof UsersAddComponent
   */
  @ViewChild('paginator', {static: true}) paginator: MatPaginator;
  /**
   *Viewchild of the roles table paginator
   *
   * @type {MatPaginator}
   * @memberof UsersAddComponent
   */
  @ViewChild('paginatorNew', {static: true}) paginatorNew: MatPaginator;
  /**
   *Datasource of the user roles table
   *
   * @type {MatPaginator}
   * @memberof UsersAddComponent
   */
  dataSourceRole = new MatTableDataSource<any>();
  /**
   *Datasource of the roles table
   *
   * @type {MatPaginator}
   * @memberof UsersAddComponent
   */
  dataSourceRoleNew = new MatTableDataSource<any>();
  /**
   * Columns name of the tables
   *
   * @type {string[]}
   * @memberof UsersAddComponent
   */
  columnsToDisplay: string[] = ['role', 'actions'];

  

  /**
   * Creates an instance of UsersAddComponent.
   * @param {UserService} userService
   * @memberof UsersAddComponent
   */
  constructor(private userService: UserService,
    public dialogRef: MatDialogRef<UsersAddComponent>,private commonFuntions: CommonFuntions) { }

  ngOnInit() {
    if(this.update){
      this.user = new FormGroup({
        userName: new FormControl(this.detailUser.userName, [Validators.required]),
        password: new FormControl(null),
        cpassword: new FormControl(null),
        email: new FormControl(this.detailUser.email, [Validators.required]),
        name: new FormControl(this.detailUser.name),
        lastName: new FormControl(this.detailUser.lastName),
        motherLastName: new FormControl(this.detailUser.secondLastName),
        status: new FormControl(this.detailUser.status.toString()),
        sexo: new FormControl(this.detailUser.sexo, [Validators.required])
        // age: new FormControl(null, [Validators.required]),
        // heigth: new FormControl(null, [Validators.required]),
        // bodyMass: new FormControl(null, [Validators.required]),
        // imss: new FormControl(null, [Validators.required]),
        // role: new FormControl(null, [Validators.required])
      },{
        // check whether our password and confirm password match
        validators: CommonFuntions.passwordMatchValidator as ValidatorFn 
     });
     
      this.formUser.userName.disable();
      this.formUser.password.valueChanges.subscribe(pass => {
        this.formUser.password.setValidators([
          Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{10,}')
        ]);
        this.formUser.password.updateValueAndValidity({emitEvent: true});
        if(this.commonFuntions.validaDato(pass) ){
          this.formUser.password.setValidators([]);
          this.formUser.cpassword.setValidators([]); 
        }
      });
        this.userService.getRolesById(this.detailUser.id).subscribe(rep => {
        this.dataSourceRoleNew.data = rep.dataResponse.roles?rep.dataResponse.roles:[];
        this.dataSourceRole.data = rep.dataResponse.unAssignedRoles?rep.dataResponse.unAssignedRoles:[]; 
        this.dataSourceRole.paginator = this.paginator;
        this.dataSourceRoleNew.paginator = this.paginatorNew;
      });
    } else {
      this.user = new FormGroup({
        userName: new FormControl(null, [Validators.required]),
        password: new FormControl(null, [Validators.compose([
          Validators.required,
          Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{10,}')
        ])]),
        cpassword: new FormControl(null, [Validators.required]),
        email: new FormControl(null, [Validators.required]),
        name: new FormControl(null),
        lastName: new FormControl(null),
        motherLastName: new FormControl(null),
        status: new FormControl(null),
        sexo: new FormControl(null,  [Validators.required])
        // age: new FormControl(null, [Validators.required]),
        // heigth: new FormControl(null, [Validators.required]),
        // bodyMass: new FormControl(null, [Validators.required]),
        // imss: new FormControl(null, [Validators.required]),
        // role: new FormControl(null, [Validators.required])
      },{
          // check whether our password and confirm password match
          validators: CommonFuntions.passwordMatchValidator as ValidatorFn 
       });
       this.userService.getRoles().subscribe(rep => {
        this.dataSourceRole.data = rep.dataResponse.filter(roles => roles.status ==1);
        this.dataSourceRole.paginator = this.paginator;
      });
    }
  }

  /**
   * Method that retrives the form´s damages
   *
   * @readonly
   * @memberof UsersAddComponent
   */
  get formUser(){
    return this.user.controls;
  }

  /**
   *Method that takes care of the registration or update of users
   *
   * @memberof UsersAddComponent
   */
  addUser(){
    let data = {
      userName: this.formUser.userName.value,
      password: this.commonFuntions.parseNull(this.formUser.password.value),
      email: this.formUser.email.value,
      name: this.formUser.name.value,
      lastName: this.formUser.lastName.value,
      secondLastName: this.formUser.motherLastName.value,      
      // dateOfBirth: this.formUser.description.value,
      created: this.update?this.detailUser.created: new Date(),
      roleNames: this.getRoleList(this.dataSourceRoleNew),
      status: this.update?this.formUser.status.value: 1,
      sexo: this.formUser.sexo.value
    };
    if(this.update){
      this.userService.updateUser(this.detailUser.id, data).subscribe(res =>{
        Swal.fire("Success","User Successfully Updated", "success");
        this.dialogRef.close(true);
      }, error =>{
        Swal.fire("Error Update", error.error, "error");
      });
    } else{
      this.userService.addUser(data).subscribe(res =>{
        Swal.fire("Success","User Successfully Added", "success");
        this.dialogRef.close(true);
      }, error =>{
        Swal.fire("Error Add", error.error.errors[0], "error");
      });
    }
  }
  /**
   * Metodo que se encarga de obtener la lista de roles
   *
   * @param {MatTableDataSource<any>} dataSourceRoleNew
   * @memberof UsersAddComponent
   */
  getRoleList(dataSourceRoleNew: MatTableDataSource<any>) {
    let roleList= [];
    dataSourceRoleNew.data.forEach(element => {
      roleList.push(element.name)
    });
    return roleList;
  }
  /**
   *Method that takes care of the Role add
   *
   * @param {*} indexvariable containing the position of the role
   * @param {*} role variable containing the role information
   * @memberof UsersAddComponent
   */
  addRole(index, role){
    index = index  + (this.paginator.pageIndex * this.paginator.pageSize);
    let list = this.dataSourceRoleNew.data;
    let list2 = this.dataSourceRole.data;
    list.push(role);
    list2.splice(index, 1);
    this.dataSourceRoleNew.data = list;
    this.dataSourceRole.data = list2;
    this.dataSourceRole.paginator = this.paginator;
      this.dataSourceRoleNew.paginator = this.paginatorNew;
  }

  /**
   *Method that takes care of the Role Remove
   *
   * @param {*} indexvariable containing the position of the role
   * @param {*} role variable containing the role information
   * @memberof UsersAddComponent
   */
  removeRole(index, role){
    index = index  + (this.paginatorNew.pageIndex * this.paginatorNew.pageSize);
    let list = this.dataSourceRole.data;
    let list2 = this.dataSourceRoleNew.data;
    list.push(role);
    list2.splice(index, 1);
    this.dataSourceRole.data = list;
    this.dataSourceRoleNew.data = list2;
    this.dataSourceRole.paginator = this.paginator;
      this.dataSourceRoleNew.paginator = this.paginatorNew;
  }

  /**
   * Role table filter
   *
   * @param {Event} event vairiable containing the role
   * @memberof UsersAddComponent
   */
  applyFilter(event) {
    this.dataSourceRole.filter = event.trim().toLocaleLowerCase();
  }


  /**
   * User roles table filter
   *
   * @param {Event} event vairiable containing the role
   * @memberof UsersAddComponent
   */
  applyFilterPM(event) {
    this.dataSourceRoleNew.filter = event.trim().toLocaleLowerCase();
  }
}
