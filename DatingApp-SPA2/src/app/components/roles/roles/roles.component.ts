import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource, MatDialog } from '@angular/material';
import { MachineService } from 'app/components/computers/common/machineService';
import { RoleService } from '../common/rolesService';
import Swal from 'sweetalert2';
import { ProductService } from 'app/components/products/common/productService';
import { RolesAddComponent } from '../roles-add/roles-add.component';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.less']
})
export class RolesComponent implements OnInit {
  /**
   * Role list
   *
   * @memberof RolesComponent
   */
  rolList = [];

  /**
   *Viewchild role pages table paginator
   *
   * @type {MatPaginator}
   * @memberof RolesComponent
   */
  @ViewChild('paginatorNew', {static: true}) paginatorNew: MatPaginator;

  /**
   * Datasource role pages table
   *
   * @memberof RolesComponent
   */
  dataSourcePageNew = new MatTableDataSource<any>();
  /**
   * Columns name of the table
   *
   * @type {string[]}
   * @memberof RolesComponent
   */
  columnsToDisplay: string[] = ['page'];
  /**
   * Valid Machine flag
   *
   * @memberof RolesComponent
   */
  validMachine = false;
  /**
   * Data role selected
   *
   * @memberof RolesComponent
   */
  dataRole;


  /**
   * Creates an instance of RolesComponent.
   * @param {MachineService} machineService
   * @param {RoleService} roleService
   * @param {ProductService} partServcie
   * @memberof RolesComponent
   */
  constructor(private machineService: MachineService,
    private roleService: RoleService,
    private partServcie: ProductService,
    private matDialog: MatDialog,) { }

  ngOnInit() {
    this.getRoles();
    this.dataSourcePageNew.paginator = this.paginatorNew;
  }
  /**
   * method that consults the list of roles
   *
   * @memberof RolesComponent
   */
  getRoles() {
    this.roleService.getRoles().subscribe(res=>{
      this.rolList = res.dataResponse;
    });
  }

  /**
   * Method that consults pages by role
   *
   * @param {*} event variable containing the machine information
   * @memberof RolesComponent
   */
  searchPagesPerRole(event: any) {
    this.dataRole = event;
    this.roleService.getPagesPerRole().subscribe(res => {
        this.partServcie.getProducts().subscribe(resp=> {
          
        });
    });
  }

  
  openModalAdd() {
    const dialogRef = this.matDialog.open(RolesAddComponent, {
      disableClose: true,
      width: '60%'
    })
    dialogRef.afterClosed().subscribe((option) => {
      if(option){
        this.getRoles();
      }
    });
  }
  openModalUpdate(role: any){
    const dialogRef = this.matDialog.open(RolesAddComponent, {
      disableClose: true,
      width: '60%',
      maxHeight: '80vh'
    });
    let data = {
      name: this.dataRole.value,
      list: this.dataSourcePageNew.data
    }
    dialogRef.componentInstance.detailRole = data;
    dialogRef.componentInstance.detailRoleOld = data;
    dialogRef.componentInstance.update = true;
    dialogRef.afterClosed().subscribe((option) => {
      if(option){
        this.searchPagesPerRole(this.dataRole);
      }
    });
  }
}
