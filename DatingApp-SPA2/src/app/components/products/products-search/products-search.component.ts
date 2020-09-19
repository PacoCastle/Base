import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource, MatDialog } from '@angular/material';
import { ProductService } from '../common/productService';
import { ProductsAddComponent } from '../products-add/products-add.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-products-search',
  templateUrl: './products-search.component.html',
  styleUrls: ['./products-search.component.less']
})
export class ProductsSearchComponent implements OnInit {
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  dataSourceProducts = new MatTableDataSource<any>();
  columnsToDisplay: string[] = ['name', 'description', 'tubeDiameter', 'rpm', 'leftPlane', 'centerPlane', 'rightPlane',
                                'attempts', 'actions'];
  constructor(private matDialog: MatDialog,
    private productService: ProductService) { }

  ngOnInit() {
    this.searchProducts();
  }

  searchProducts(){
    this.productService.getProducts().subscribe(res =>{
      this.dataSourceProducts.data = res.dataResponse;
    });
    this.dataSourceProducts.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSourceProducts.filter = filterValue.trim().toLowerCase();
  }
  openModalAdd() {
    const dialogRef = this.matDialog.open(ProductsAddComponent, {
      disableClose: true,
      width: '60%'
    })
    dialogRef.afterClosed().subscribe((option) => {
      if(option){
        this.searchProducts();
      }
    });
  }

  openModalUpdate(product: any){
    const dialogRef = this.matDialog.open(ProductsAddComponent, {
      disableClose: true,
      width: '60%',
      maxHeight: '80vh'
    });
    dialogRef.componentInstance.detailProduct = product;
    dialogRef.componentInstance.detailProductOld = product;
    dialogRef.componentInstance.update = true;
    dialogRef.afterClosed().subscribe((option) => {
      if(option){
        this.searchProducts();
      }
    });
  }

  deleteProduct(product: any){
    let data = {
      Name: product.name,
      Description: product.description,
      Status: 0
    };
    this.productService.updateProduct(product.id, data).subscribe(res =>{
      Swal.fire("Success","Product Successfully Deleted", "success").then(succes =>{
        this.searchProducts();
      });
    }, error =>{
      Swal.fire("Error Delete", error.error, "error");
    });
  }

}
