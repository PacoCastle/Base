import { HttpClient, HttpHeaders, HttpParams, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

/**
 * Manejo de peticiones Http
 * La llamanda a servicios, se centraliza en esta clase
 * @export
 * @class BaseService
 */
@Injectable()
export class BaseService {
    configuration

    /**
     * Cabecera de la petición htttp
     * @private
     * @type HttpHeaders
     * @memberof BaseService
     */
    private headers: HttpHeaders;

    /**
     * Crea una instancia de la clase.
     * Inicializa la cabeceras de las peticiones, Json
     * Inicializa la url base
     * @param  {HttpClient} httpClient
     * @memberof BaseService
     */
    constructor(protected httpClient: HttpClient) {

      this.headers = new HttpHeaders();
      // this.headers = this.headers.set('Content-Type', 'application/json');
      // this.headers = this.headers.set('Accept', 'application/json');

      // Asignamos configuracion externalizada
      this.configuration = environment.apiUrl;
    }


    /**
     * Realiza una peticioón por el metodo GET
     * Regresa un blob
     * @template T Cast objeto a regresar
     * @param  {string} requestUrl Url del servicio
     * @param  {*} [httpQueryParams=null] Parametros por query
     * @param  {*} [httpParams={}] Pramtetros
     * @return Observable<T> Observable
     * @memberof BaseService
     */
    get<T>( requestUrl: string, httpQueryParams: any = null, httpParams: any = {}): Observable<T> {

      return this.httpClient.get<T>(this.configuration + requestUrl, { headers: this.headers});
    }

    /**
     * Realiza una peticioón por el metodo GET
     * Regresa un blob
     * @param  {string} requestUrl Url del servicio
     * @param  {*} [httpParams={}] Pramtetros
     * @return Observable<Blob> Regresa un Observable con un Blob
     * @memberof BaseService
     */
    getBlob( requestUrl: string, httpParams: any = {}): Observable<Blob> {
      let paramsIn = new HttpParams();

      Object.keys(httpParams).forEach(key => {
        if (httpParams[key]) {
          paramsIn = paramsIn.append(key, httpParams[key]);
        }
      });

      return this.httpClient.get(this.configuration + requestUrl, { headers: this.headers
                                                                                 , params: paramsIn
                                                                                 , withCredentials: true
                                                                                 , responseType: 'blob'
                                                                                 });
    }

    postBlob( requestUrl: string, bodyRequest: any = {}): Observable<Blob> {
      return this.httpClient.post(this.configuration + requestUrl, bodyRequest, { headers: this.headers
                                                                                               , responseType: 'blob'
                                                                                               , withCredentials: true
                                                                                               });
    }

    /**
     * Realiza una peticioón por el metodo POST
     * Regresa un json
     * @template T Cast objeto a regresar
     * @param  {string} requestUrl Url del servicio
     * @param  {*} bodyRequest Cuerpo de la petición
     * @return Observable<T> Observable
     * @memberof BaseService
     */
    post<T>( requestUrl: string, bodyRequest: any): Observable<T> {
        return this.httpClient.post<T>(this.configuration + requestUrl, bodyRequest, { headers: this.headers });
    }

    /**
     * Realiza una peticioón por el metodo PUT
     * Regresa un json
     * @template T Cast objeto a regresar
     * @param  {string} requestUrl Url del servicio
     * @param  {*} bodyRequest Cuerpo de la petición
     * @return Observable<T> Observable
     * @memberof BaseService
     */
    put<T>( requestUrl: string, bodyRequest: any): Observable<T> {
        return this.httpClient.put<T>(this.configuration + requestUrl, bodyRequest, { headers: this.headers});
    }

    /**
     * Realiza una peticioón por el metodo DELETE
     * Regresa un json
     * @template T Cast objeto a regresar
     * @param  {string} requestUrl Url del servicio
     * @return Observable<T> Observable
     * @memberof BaseService
     */
    delete<T>( requestUrl: string): Observable<T> {
      return this.httpClient.delete<T>(this.configuration + requestUrl, { headers: this.headers});
    }

    /**
     * Realiza una peticioón por el metodo PUT en Form Data
     * Regresa un json
     * @template T Cast objeto a regresar
     * @param  {string} requestUrl Url del servicio
     * @param  {*} bodyRequest Cuerpo de la petición
     * @return Observable<T> Observable
     * @memberof BaseService
     */
    putFormData<T>( requestUrl: string, bodyRequest: any): Observable<T> {
        const formData = new FormData();
        if (bodyRequest) {
          Object.keys(bodyRequest).forEach(key => {
            formData.append(key, bodyRequest[key]);
          });
        }

        return this.httpClient.put<T>(this.configuration + requestUrl, formData, { headers: this.headers, withCredentials: true } );
    }

    /**
     * Realiza una peticioón por el metodo PUT en Form Data, no agrega application/json
     * Regresa un json
     * @template T Cast objeto a regresar
     * @param  {string} requestUrl Url del servicio
     * @param  {*} bodyRequest Cuerpo de la petición
     * @return Observable<T> Observable
     * @memberof BaseService
     */
    putFormDataWithOutHeaders<T>( requestUrl: string, bodyRequest: any): Observable<T> {
      const formData = new FormData();
      if (bodyRequest) {
        Object.keys(bodyRequest).forEach(key => {
          formData.append(key, bodyRequest[key]);
        });
      }
      return this.httpClient.put<T>(this.configuration + requestUrl, formData, { withCredentials: true } );
    }

    /**
     * Realiza una peticioón por el metodo POST en Form Data
     * Regresa un json
     * @template T Cast objeto a regresar
     * @param  {string} requestUrl Url del servicio
     * @param  {*} bodyRequest Cuerpo de la petición
     * @return Observable<T> Observable
     * @memberof BaseService
     */
    postFormData<T>( requestUrl: string, bodyRequest: any): Observable<T> {
        const formData = new FormData();

        if (bodyRequest) {
          Object.keys(bodyRequest).forEach(key => {
            formData.append(key, bodyRequest[key]);
          });
        }

        return this.httpClient.post<T>(this.configuration + requestUrl, formData, { headers: this.headers, withCredentials: true });
    }

    /**
     * Realiza una peticioón por el metodo GET para descargar un archivo
     * Descarga el archivo
     * @param  {string} requestUrl Url del servicio
     * @param  {*} [httpParams={}] Pramtetros
     * @return Observable<Blob> Regresa un Observable con un Blob
     * @memberof BaseService
     */
    getDownload( url: string, http: any = {}) {
        let paramsIn = new HttpParams();

        Object.keys(http).forEach(key => {
            if (http[key]) {
              paramsIn = paramsIn.append(key, http[key]);
            }
        });

      return this.httpClient.get(this.configuration + url, { headers: this.headers, params: paramsIn, withCredentials: true, responseType: 'blob' });
    }

    upload( requestUrl: string, file: File): Observable<any> {
      const fb = new FormData();
      fb.append('file', file, file.name);

      return this.httpClient.post(this.configuration + requestUrl, fb, { reportProgress: true, observe: 'events', withCredentials: true });
    }
}
