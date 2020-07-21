import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { finalize } from "rxjs/operators";
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { NgxSpinnerService } from "ngx-spinner";

/**
 * ISBAN Mexico - (c) Banco Santander Central Hispano
 * Todos los derechos reservados
 *
 * loader-request-interceptor.ts
 *
 * Control de versiones:
 *
 * Version | Date/Hour           |  By               | Company     Description
 * -------   -------------------    ----------------   --------    -----------------------------------------------------------------
 * 1.0     | 23/01/2020          |  BCHAVEZ          | Vector ITC  Interceptor de Peticiones para automaticamente inciar y detener NgxLoader
 */
@Injectable()
export class LoaderRequestInterceptor implements HttpInterceptor {
    constructor(public loaderService: NgxSpinnerService) { }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.loaderService.show();
        return next.handle(req).pipe(
            finalize(() => this.loaderService.hide())
        );
    }
}