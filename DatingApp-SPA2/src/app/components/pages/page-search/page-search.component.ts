import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource, MatDialog } from '@angular/material';
import { PageService } from '../common/pageServcie';
import { ProductsAddComponent } from 'app/components/products/products-add/products-add.component';
import Swal from 'sweetalert2';
import { PageAddComponent } from '../page-add/page-add.component';

@Component({
  selector: 'app-page-search',
  templateUrl: './page-search.component.html',
  styleUrls: ['./page-search.component.scss']
})
export class PageSearchComponent implements OnInit {
  /**
   *Viewchild page table paginator
   *
   * @type {MatPaginator}
   * @memberof PageSearchComponent
   */
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  /**
   * Datasource page table
   *
   * @memberof PageSearchComponent
   */
  dataSourcePages = new MatTableDataSource<any>();
  /**
   * Page table columns name
   *
   * @type {string[]}
   * @memberof PageSearchComponent
   */
  columnsToDisplay: string[] = ['title', 'path', 'icon', 'actions'];
  constructor(private pageService: PageService,
    private matDialog: MatDialog,) { }

  ngOnInit() {
    this.searchPages();0
  }

  /**
   * Method that consult the list of pages
   *
   * @memberof PageSearchComponent
   */
  searchPages(){
    this.pageService.getPage().subscribe(res =>{
      this.dataSourcePages.data = res.dataResponse;
    });
    this.dataSourcePages.paginator = this.paginator;
  }

  /**
   * Page table filter
   *
   * @param {Event} event Variable containig the information to be searched
   * @memberof PageSearchComponent
   */
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourcePages.filter = filterValue.trim().toLowerCase();
  }
  /**
   * method that opens the modal for discharge
   *
   * @memberof PageSearchComponent
   */
  openModalAdd() {
    const dialogRef = this.matDialog.open(PageAddComponent, {
      disableClose: true,
      width: '60%'
    })
    dialogRef.afterClosed().subscribe((option) => {
      if(option){
        this.searchPages();
      }
    });
  }

  /**
   *method that opens the modal for update
   *
   * @param {*} product
   * @memberof PageSearchComponent
   */
  openModalUpdate(product: any){
    const dialogRef = this.matDialog.open(PageAddComponent, {
      disableClose: true,
      width: '60%',
      maxHeight: '80vh'
    });
    dialogRef.componentInstance.detailPage = product;
    dialogRef.componentInstance.detailPageOld = product;
    dialogRef.componentInstance.update = true;
    dialogRef.afterClosed().subscribe((option) => {
      if(option){
        this.searchPages();
      }
    });
  }

  /**
   * method that removes the page
   *
   * @param {*} product
   * @memberof PageSearchComponent
   */
  deleteProduct(product: any){
    let data = {
      Name: product.name,
      Description: product.description,
      Status: 0
    };
    this.pageService.updatePage(product.id, data).subscribe(res =>{
      Swal.fire("Success","Product Successfully Deleted", "success").then(succes =>{
        this.searchPages();
      });
    }, error =>{
      Swal.fire("Error Delete", error.error, "error");
    });
  }


}
