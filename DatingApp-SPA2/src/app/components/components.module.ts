import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { FooterComponent } from './footer/footer.component';
import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule, MatToolbarModule, MatListModule, MatSidenavModule, MatExpansionModule, MatMenu, MatAutocompleteModule, MatButtonToggleModule, MatChipsModule, MatStepperModule, MatDatepickerModule, MatDialogModule, MatGridListModule, MatNativeDateModule, MatPaginatorModule, MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule, MatSelectModule, MatSliderModule, MatSlideToggleModule, MatSnackBarModule, MatSortModule, MatTableModule, MatTabsModule, MatTooltipModule} from '@angular/material';
import {MatMenuModule} from '@angular/material/menu';
import { BalancingProcessComponent } from './balancing-process/balancing-process.component';
import { HomeComponent } from './home/home.component';
import { ResetPasswordComponent } from './users/reset-password/reset-password.component';
import { UsersAddComponent } from './users/users-add/users-add.component';
import { ConfigPasswordComponent } from './users/config-password/config-password.component';
import { UsersSearchComponent } from './users/users-search/users-search.component';
import { ProductsAddComponent } from './products/products-add/products-add.component';
import { ProductsSearchComponent } from './products/products-search/products-search.component';
import { ComputersAddComponent } from './computers/computers-add/computers-add.component';
import { ComputersSearchComponent } from './computers/computers-search/computers-search.component';
import { BalancingAttemptsAddComponent } from './balancing-attempts/balancing-attempts-add/balancing-attempts-add.component';
import { BalancingAttemptsSearchComponent } from './balancing-attempts/balancing-attempts-search/balancing-attempts-search.component';
import { PartsPerComputerAddComponent } from './parts-per-computer/parts-per-computer-add/parts-per-computer-add.component';
import { PartsPerComputerSearchComponent } from './parts-per-computer/parts-per-computer-search/parts-per-computer-search.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatListModule,
    MatCheckboxModule, 
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatExpansionModule,
    MatMenuModule,
    MatAutocompleteModule,
    MatButtonToggleModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatGridListModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatTooltipModule
  ],
  declarations: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    LoginComponent,
    BalancingProcessComponent,
    HomeComponent,
    ConfigPasswordComponent,
    ResetPasswordComponent,
    UsersAddComponent,
    UsersSearchComponent,
    ProductsAddComponent,
    ProductsSearchComponent,
    ComputersAddComponent,
    ComputersSearchComponent,
    BalancingAttemptsAddComponent,
    BalancingAttemptsSearchComponent,
    PartsPerComputerAddComponent,
    PartsPerComputerSearchComponent
  ],
  exports: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent
  ],
  entryComponents: [
    ComputersAddComponent
  ]
})
export class ComponentsModule { }
