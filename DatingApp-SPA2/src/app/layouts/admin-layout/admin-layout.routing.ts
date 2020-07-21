import { Routes } from '@angular/router';

import { DashboardComponent } from '../../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TableListComponent } from '../../table-list/table-list.component';
import { TypographyComponent } from '../../typography/typography.component';
import { IconsComponent } from '../../icons/icons.component';
import { MapsComponent } from '../../maps/maps.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { UpgradeComponent } from '../../upgrade/upgrade.component';
import { BalancingProcessComponent } from 'app/components/balancing-process/balancing-process.component';
import { HomeComponent } from 'app/components/home/home.component';
import { UsersSearchComponent } from 'app/components/users/users-search/users-search.component';
import { ProductsSearchComponent } from 'app/components/products/products-search/products-search.component';
import { ComputersSearchComponent } from 'app/components/computers/computers-search/computers-search.component';
import { BalancingAttemptsSearchComponent } from 'app/components/balancing-attempts/balancing-attempts-search/balancing-attempts-search.component';
import { PartsPerComputerSearchComponent } from 'app/components/parts-per-computer/parts-per-computer-search/parts-per-computer-search.component';
import { RolesComponent } from 'app/components/roles/roles/roles.component';

export const AdminLayoutRoutes: Routes = [
    // {
    //   path: '',
    //   children: [ {
    //     path: 'dashboard',
    //     component: DashboardComponent
    // }]}, {
    // path: '',
    // children: [ {
    //   path: 'userprofile',
    //   component: UserProfileComponent
    // }]
    // }, {
    //   path: '',
    //   children: [ {
    //     path: 'icons',
    //     component: IconsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'notifications',
    //         component: NotificationsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'maps',
    //         component: MapsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'typography',
    //         component: TypographyComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'upgrade',
    //         component: UpgradeComponent
    //     }]
    // }
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'table-list',     component: TableListComponent },
    { path: 'typography',     component: TypographyComponent },
    { path: 'icons',          component: IconsComponent },
    { path: 'maps',           component: MapsComponent },
    { path: 'notifications',  component: NotificationsComponent },
    { path: 'upgrade',        component: UpgradeComponent },
    { path: 'balancing-process',  component: BalancingProcessComponent},
    { path: 'home',           component: HomeComponent},
    { path: 'users',          component: UsersSearchComponent},
    { path: 'parts',       component: ProductsSearchComponent},
    { path: 'machines',      component: ComputersSearchComponent},
    { path: 'balancing-attempts', component: BalancingAttemptsSearchComponent},
    { path: 'parts-per-computer', component: PartsPerComputerSearchComponent},
    { path: 'pages-per-role', component: RolesComponent},

];
