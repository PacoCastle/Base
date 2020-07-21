import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { MachineService } from 'app/components/computers/common/machineService';
import { RoleService } from '../common/rolesService';
import Swal from 'sweetalert2';
import { ProductService } from 'app/components/products/common/productService';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.less']
})
export class RolesComponent implements OnInit {
  rolList = [{id: 1, description:"Users"}];
  @ViewChild('paginator', {static: true}) paginator: MatPaginator;
  @ViewChild('paginatorNew', {static: true}) paginatorNew: MatPaginator;
  dataSourcePage = new MatTableDataSource<any>();
  dataSourcePageNew = new MatTableDataSource<any>();
  columnsToDisplay: string[] = ['page', 'actions'];
  validMachine = false;
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

  constructor(private machineService: MachineService,
    private roleService: RoleService,
    private partServcie: ProductService) { }

  ngOnInit() {
    this.dataSourcePage.data = this.dummyPages;
    this.dataSourcePage.paginator = this.paginator;
    this.dataSourcePageNew.paginator = this.paginatorNew;
  }

  searchPagesPerRole(event: any) {
    const machine = event.value;
    this.roleService.getPagesPerRole().subscribe(res => {
        this.partServcie.getProducts().subscribe(resp=> {
          this.filterLists(res, resp);
        });
    });
  }

  filterLists(res: any, resp: any) {
    let missingList=[];
    res.forEach(element => {
      let c = 0
      resp.forEach(elem => {
        if(elem.id  === element.id) {
          c++;
        }
      });
      if(c === 0){
        missingList.push(element);
      }
    });
    this.dataSourcePageNew.data = res;
    this.dataSourcePage.data = missingList;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourcePage.filter = filterValue.trim().toLowerCase();
  }
  applyFilterPM(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourcePageNew.filter = filterValue.trim().toLowerCase();
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
