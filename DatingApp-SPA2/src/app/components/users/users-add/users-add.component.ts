import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from '../common/userService';
import Swal from 'sweetalert2';
import { MatPaginator, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-users-add',
  templateUrl: './users-add.component.html',
  styleUrls: ['./users-add.component.less']
})
export class UsersAddComponent implements OnInit {
  user: FormGroup;
  update: boolean;
  detailUser;
  detailUserOld;
  rolList = [{id: 1, description:"Users"}];
  @ViewChild('paginator', {static: true}) paginator: MatPaginator;
  @ViewChild('paginatorNew', {static: true}) paginatorNew: MatPaginator;
  dataSourcePage = new MatTableDataSource<any>();
  dataSourcePageNew = new MatTableDataSource<any>();
  columnsToDisplay: string[] = ['page', 'actions'];
  dummyPages = [
    {
      id: 1,
      description: "Page1"
    },
    {
      id: 2,
      description: "Page2"
    },
    {
      id: 3,
      description: "Page3"
    }
  ];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.dataSourcePage.data = this.dummyPages;
    this.dataSourcePage.paginator = this.paginator;
    this.dataSourcePageNew.paginator = this.paginatorNew;
    if(this.update){
      this.user = new FormGroup({
        employeeiId: new FormControl(this.detailUser.name, [Validators.required]),
        name: new FormControl(this.detailUser.description, [Validators.required]),
        lastName: new FormControl(null, [Validators.required]),
        motherLastName: new FormControl(null, [Validators.required]),
        age: new FormControl(null, [Validators.required]),
        heigth: new FormControl(null, [Validators.required]),
        bodyMass: new FormControl(null, [Validators.required]),
        imss: new FormControl(null, [Validators.required]),
        role: new FormControl(null, [Validators.required])
      });
    } else {
      this.user = new FormGroup({
        employeeiId: new FormControl(null, [Validators.required]),
        name: new FormControl(null, [Validators.required]),
        lastName: new FormControl(null, [Validators.required]),
        motherLastName: new FormControl(null, [Validators.required]),
        age: new FormControl(null, [Validators.required]),
        heigth: new FormControl(null, [Validators.required]),
        bodyMass: new FormControl(null, [Validators.required]),
        imss: new FormControl(null, [Validators.required]),
        role: new FormControl(null, [Validators.required])
      });
    }
  }

  get formUser(){
    return this.user.controls;
  }

  addUser(){
    let data = {
      Name: this.formUser.name.value,
      Description: this.formUser.description.value,
      Status: 1
    };
    if(this.update){
      this.userService.updateUser(this.detailUser.id, data).subscribe(res =>{
        Swal.fire("Success","Machine Successfully Updated", "success")
      }, error =>{
        Swal.fire("Error Update", error.error, "error");
      });
    } else{
      this.userService.addUser(data).subscribe(res =>{
        Swal.fire("Success","Machine Successfully Added", "success")
      }, error =>{
        Swal.fire("Error Add", error.error, "error");
      });
    }
  }

  addPage(index, page){
    index = index  + (this.paginator.pageIndex * this.paginator.pageSize);
    let list = this.dataSourcePageNew.data;
    let list2 = this.dataSourcePage.data;
    list.push(page);
    list2.splice(index, 1);
    this.dataSourcePageNew.data = list;
    this.dataSourcePage.data = list2;
  }

  removePage(index, page){
    index = index  + (this.paginatorNew.pageIndex * this.paginatorNew.pageSize);
    let list = this.dataSourcePage.data;
    let list2 = this.dataSourcePageNew.data;
    list.push(page);
    list2.splice(index, 1);
    this.dataSourcePage.data = list;
    this.dataSourcePageNew.data = list2;
  }

}
