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
    {path: "parts", title: "Parts", icon: "fa fa-cog"} ,
    {path: "pages", title: "Pages", icon: "fa fa-cog"} ,
    {path: "machines", title: "Machines", icon: "fa fa-desktop"},
    {path: "parts-per-computer", title: "Parts Per Machine", icon: "fa fa-cogs"},
    {path: "pages-per-role", title: "Pages Per Role", icon: "fa fa-address-card-o"},
    {path: "balancing-process", title: "Balancing Process", icon: "fa fa-bar-chart"},
    {path: "balancing-attempts", title: "Balanced Parts", icon: "fa fa-file-text-o"} 
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
