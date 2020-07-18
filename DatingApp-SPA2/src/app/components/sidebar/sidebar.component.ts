import { Component, OnInit } from '@angular/core';

declare const $: any;
declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    submenus?: any
}
export const ROUTES: RouteInfo[] = [
    {path: "users", title: "Users", icon: "fa fa-id-card-o"} ,
    {path: "products", title: "Products", icon: "fa fa-cogs"} ,
    {path: "machines", title: "Machines", icon: "fa fa-desktop"},
    {path: "balancing-process", title: "Balancing Process", icon: "fa fa-bar-chart"} 
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor() { }

  ngOnInit() {
    this.menuItems = ROUTES.filter(pathItem => pathItem);
  }
  isMobileMenu() {
      if ($(window).width() > 991) {
          return false;
      }
      return true;
  };
}
