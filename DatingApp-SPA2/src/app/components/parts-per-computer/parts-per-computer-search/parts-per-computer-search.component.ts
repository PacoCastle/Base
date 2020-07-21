import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { MachineService } from 'app/components/computers/common/machineService';
import { PartsPerComputerService } from '../common/partsPerComputerService';
import Swal from 'sweetalert2';
import { ProductService } from 'app/components/products/common/productService';

@Component({
  selector: 'app-parts-per-computer-search',
  templateUrl: './parts-per-computer-search.component.html',
  styleUrls: ['./parts-per-computer-search.component.less']
})
export class PartsPerComputerSearchComponent implements OnInit {
  @ViewChild('paginator', {static: true}) paginator: MatPaginator;
  @ViewChild('paginatorPm', {static: true}) paginatorPm: MatPaginator;
  dataSourceProducts = new MatTableDataSource<any>();
  dataSourceProductsMachine = new MatTableDataSource<any>();
  columnsToDisplay: string[] = ['name', 'description', 'actions'];
  machineList;
  validMachine = false;
  constructor(private machineService: MachineService,
    private partsPerComputerService: PartsPerComputerService,
    private partServcie: ProductService) { }

  ngOnInit() {
    this.dataSourceProducts.paginator = this.paginator;
    this.dataSourceProductsMachine.paginator = this.paginatorPm;
    this.machineService.getMachine().subscribe(list => {
      this.machineList = list;
    }, error => {
      Swal.fire("Error", error.error, "error");
    });
  }

  searchPartsPerMachine(event: any) {
    const machine = event.value;
    this.partsPerComputerService.getProductsPerMachine().subscribe(res => {
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
    this.dataSourceProductsMachine.data = res;
    this.dataSourceProducts.data = missingList;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourceProducts.filter = filterValue.trim().toLowerCase();
  }
  applyFilterPM(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourceProductsMachine.filter = filterValue.trim().toLowerCase();
  }

  addPart(index, page){
    index = index  + (this.paginator.pageIndex * this.paginator.pageSize);
    let list = this.dataSourceProductsMachine.data;
    let list2 = this.dataSourceProducts.data;
    list.push(page);
    list2.splice(index, 1);
    this.dataSourceProductsMachine.data = list;
    this.dataSourceProducts.data = list2;
  }

  removePart(index, page){
    index = index  + (this.paginatorPm.pageIndex * this.paginatorPm.pageSize);
    let list = this.dataSourceProducts.data;
    let list2 = this.dataSourceProductsMachine.data;
    list.push(page);
    list2.splice(index, 1);
    this.dataSourceProducts.data = list;
    this.dataSourceProductsMachine.data = list2;
  }

}
