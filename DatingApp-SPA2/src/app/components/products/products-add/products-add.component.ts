import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ProductService } from '../common/productService';
import Swal from 'sweetalert2';
import { nullSafeIsEquivalent } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-products-add',
  templateUrl: './products-add.component.html',
  styleUrls: ['./products-add.component.less']
})
export class ProductsAddComponent implements OnInit {
  product: FormGroup;
  update: boolean;
  detailProduct;
  detailProductOld;
  pLeft = false;
  pCenter = false;
  pRight = false;

  constructor(private productService: ProductService) { }

  ngOnInit() {
    if(this.update){
      this.product = new FormGroup({
        name: new FormControl(this.detailProduct.name, [Validators.required]),
        description: new FormControl(this.detailProduct.description, [Validators.required]),
        tubeDiameter: new FormControl(null, [Validators.required]),
        rpm: new FormControl(null, [Validators.required]),
        planeLeft: new FormControl(null),
        planeCenter: new FormControl(null),
        planeRight: new FormControl(null),
        attempts: new FormControl(null, [Validators.required]),
        extraAttempts: new FormControl(null)
      });
    } else {
      this.product = new FormGroup({
        name: new FormControl(null, [Validators.required]),
        description: new FormControl(null, [Validators.required]),
        tubeDiameter: new FormControl(null, [Validators.required]),
        rpm: new FormControl(null, [Validators.required]),
        planeLeft: new FormControl(null),
        planeCenter: new FormControl(null),
        planeRight: new FormControl(null),
        attempts: new FormControl(null, [Validators.required]),
        extraAttempts: new FormControl(null)
      });
    }
  }

  get formProduct(){
    return this.product.controls;
  }

  addProduct(){
    let data = {
      Name: this.formProduct.name.value,
      Description: this.formProduct.description.value,
      Status: 1
    };
    if(this.update){
      this.productService.updateProduct(this.detailProduct.id, data).subscribe(res =>{
        Swal.fire("Success","Machine Successfully Updated", "success")
      }, error =>{
        Swal.fire("Error Update", error.error, "error");
      });
    } else{
      this.productService.addProduct(data).subscribe(res =>{
        Swal.fire("Success","Machine Successfully Added", "success")
      }, error =>{
        Swal.fire("Error Add", error.error, "error");
      });
    }
  }

}
