import { Component, OnInit, ViewChild, Output, Input, EventEmitter } from '@angular/core';
import * as $ from 'jquery';


@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  constructor() { }
  @Input() listMenu 
  dummyListMenu = [
    {menu: "Admnistrator",
      icon: "fa fa-user-circle-o",
      subMenus: [
        {menu: "users",
        name: "Users",
          icon: "fa fa-id-card-o"} ,
          {menu: "products",
          name: "Products",
          icon: "fa fa-cogs"} ,
          {menu: "computers",
          name: "Computers",
          icon: "fa fa-desktop"}
       ]
    },
    {menu: "User",
      icon: "fa fa-user-o",
      subMenus: [
        {menu: "balancing-process",
          name: "Balancing Process",
          icon: "fa fa-bar-chart"} 
       ]
    }
  ];
  @Output() title = new EventEmitter<any>();
  ngOnInit() {
       //Toggle Click Function
       $("#menu-toggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
      });
  
  }
  sendTitle(title){
    this.title.emit(title);
  }
}
