import { Injectable, Inject } from '@angular/core';
import { IFutbolista } from '../interfaces/ifutbolista';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { inject } from '@angular/core/testing';

@Injectable({
  providedIn: 'root'
})
export class FutbolistasService {

  public listaFutbolistas: IFutbolista[];
  private apiUrl : string = "https://localhost:5001/" + "api/futbolistas";

  constructor(@Inject('BASE_URL') private baseUrl: string, private http: HttpClient) { }

  MostrarTodos(): Observable<IFutbolista[]>{
    return this.http.get<IFutbolista[]>(this.apiUrl);
  }

  MostrarPorId(id:number){
    return this.http.get<IFutbolista>(this.apiUrl + "/" + id);
  }
    
}
