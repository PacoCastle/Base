import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatPaginator, MatTableDataSource, MatDialogRef } from '@angular/material';
import { RoleService } from '../common/rolesService';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-roles-add',
  templateUrl: './roles-add.component.html',
  styleUrls: ['./roles-add.component.scss']
})
export class RolesAddComponent implements OnInit {
  /**
   * User Form
   *
   * @type {FormGroup}
   * @memberof RolesAddComponent
   */
  role: FormGroup;

  /**
   * Update flag
   *
   * @type {boolean}
   * @memberof RolesAddComponent
   */
  update: boolean;

  /**
   * New role information
   *
   * @memberof RolesAddComponent
   */
  detailRole;

  /**
   * Old role information
   *
   * @memberof RolesAddComponent
   */
  detailRoleOld;

  /**
   *Viewchild of the user roles table paginator
   *
   * @type {MatPaginator}
   * @memberof RolesAddComponent
   */
  @ViewChild('paginator', {static: true}) paginator: MatPaginator;
  /**
   *Viewchild of the roles table paginator
   *
   * @type {MatPaginator}
   * @memberof RolesAddComponent
   */
  @ViewChild('paginatorNew', {static: true}) paginatorNew: MatPaginator;
  /**
   *Datasource of the user roles table
   *
   * @type {MatPaginator}
   * @memberof RolesAddComponent
   */
  dataSourcePage = new MatTableDataSource<any>();
  /**
   *Datasource of the roles table
   *
   * @type {MatPaginator}
   * @memberof RolesAddComponent
   */
  dataSourcePageNew = new MatTableDataSource<any>();
  /**
   * Columns name of the tables
   *
   * @type {string[]}
   * @memberof RolesAddComponent
   */
  columnsToDisplay: string[] = ['page', 'actions'];
  constructor(private roleService: RoleService,
    public dialogRef: MatDialogRef<RolesAddComponent>) { }

  ngOnInit() {
    if(this.update){
      this.role = new FormGroup({
        name: new FormControl(this.detailRole.userName, [Validators.required]),
        status: new FormControl(this.detailRole.status)
      });
    } else {
      this.role = new FormGroup({ 
        name: new FormControl(null, [Validators.required]),
        status: new FormControl(null)
      });
    }
    this.roleService.getPages().subscribe(pages => {
      this.dataSourcePage.data = pages.dataResponse;
    })
    this.dataSourcePage.paginator = this.paginator;
    this.dataSourcePageNew.paginator = this.paginatorNew;
  }

  /**
   * Method that retrives the form´s damages
   *
   * @readonly
   * @memberof RolesAddComponent
   */
  get formRole(){
    return this.role.controls;
  }

    /**
   *  page table filter
   *
   * @param {Event} event vairiable containing the page
   * @memberof RolesAddComponent
   */
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourcePage.filter = filterValue.trim().toLowerCase();
  }
  /**
   * Role pages table filter
   *
   * @param {Event} event vairiable containing the page
   * @memberof RolesAddComponent
   */
  applyFilterPM(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourcePageNew.filter = filterValue.trim().toLowerCase();
  }

  /**
     *Method that takes care of the page add
   *
   * @param {*} indexvariable containing the position of the page
   * @param {*} page variable containing the page information
   * @memberof RolesAddComponent
   */
  addPage(index, page){
    index = index  + (this.paginator.pageIndex * this.paginator.pageSize);
    let list = this.dataSourcePageNew.data;
    let list2 = this.dataSourcePage.data;
    list.push(page);
    list2.splice(index, 1);
    this.dataSourcePageNew.data = list;
    this.dataSourcePage.data = list2;
  }

  /**
   *Method that takes care of the pafe remove
   *
   * @param {*} indexvariable containing the position of the page
   * @param {*} page variable containing the page information
   * @memberof RolesAddComponent
   */
  removePage(index, page){
    index = index  + (this.paginatorNew.pageIndex * this.paginatorNew.pageSize);
    let list = this.dataSourcePage.data;
    let list2 = this.dataSourcePageNew.data;
    list.push(page);
    list2.splice(index, 1);
    this.dataSourcePage.data = list;
    this.dataSourcePageNew.data = list2;
  }

  
   /**
   *Method that takes care of the registration or update of users
   *
   * @memberof UsersAddComponent
   */
  createRole(){
    let listpage= [];
    
    this.dataSourcePageNew.data.forEach(pag => {
      let pages = {id: pag.id};
      listpage.push(pages);
      
    })
    let data = {
      name: this.formRole.name.value,
      menus: listpage,
      status: this.update? this.formRole.status.value:1
    };
    if(this.update){
      this.roleService.updateRole(this.detailRole.name, data).subscribe(res =>{
        Swal.fire("Success","Role Successfully Updated", "success");
        this.dialogRef.close(true);
      }, error =>{
        Swal.fire("Error Update", error.error, "error");
      });
    } else{
      this.roleService.addRole(data).subscribe(res =>{
        Swal.fire("Success","Role Successfully Added", "success");
        this.dialogRef.close(true);
      }, error =>{
        Swal.fire("Error Add", error.error, "error");
      });
    }
  }
}
