import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { PageService } from '../common/pageServcie';
import { MatDialogRef } from '@angular/material';
import Swal from 'sweetalert2';
import {icons} from '../../commons/icons'


@Component({
  selector: 'app-page-add',
  templateUrl: './page-add.component.html',
  styleUrls: ['./page-add.component.scss']
})
export class PageAddComponent implements OnInit {
  
  /**
   * Page Form
   *
   * @type {FormGroup}
   * @memberof PageAddComponent
   */
  page: FormGroup;
  /**
   * Update flag
   *
   * @type {boolean}
   * @memberof PageAddComponent
   */
  update: boolean;
  /**
   * Page data
   *
   * @memberof PageAddComponent
   */
  detailPage;
  /**
   * Page data old
   *
   * @memberof PageAddComponent
   */
  detailPageOld;
  /**
   * Icon list
   *
   * @type {*}
   * @memberof PageAddComponent
   */
  iconList: any;
  constructor(private pageService: PageService,
    public dialogRef: MatDialogRef<PageAddComponent>) { }

  ngOnInit() {
    this.iconList = icons;
    if(this.update){
      this.page = new FormGroup({
        title: new FormControl(this.detailPage.title, [Validators.required]),
        path: new FormControl(this.detailPage.path, [Validators.required]),
        icon: new FormControl(this.detailPage.icon),
        status: new FormControl(this.detailPage.status)
      });
    } else {
      this.page = new FormGroup({ 
        title: new FormControl(null, [Validators.required]),
        path: new FormControl(null, [Validators.required]),
        icon: new FormControl(null),
        status: new FormControl(null)
      });
    }
  }

  /**
   * Method that retrives the form´s damages
   *
   * @readonly
   * @memberof RolesAddComponent
   */
  get formPage(){
    return this.page.controls;
  }

  addPage(){
    let data = {
      title: this.formPage.title.value,
      path: this.formPage.path.value,
      icon: this.formPage.icon.value,
      status: this.update? this.formPage.status.value:1
    };
    if(this.update){
      this.pageService.updatePage(this.detailPage.id, data).subscribe(res =>{
        Swal.fire("Success","Page Successfully Updated", "success");
        this.dialogRef.close(true);
      }, error =>{
        Swal.fire("Error Update", error.errors, "error");
      });
    } else{
      this.pageService.addPage(data).subscribe(res =>{
        Swal.fire("Success","Page Successfully Added", "success");
        this.dialogRef.close(true);
      }, error =>{
        Swal.fire("Error Add", error.errors, "error");
      });
    }
  }

}
