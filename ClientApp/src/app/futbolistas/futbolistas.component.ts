import { Component, OnInit } from '@angular/core';
import { FutbolistasService} from '../servicios/futbolistas.service';
import {IFutbolista} from '../interfaces/ifutbolista';

@Component({
  selector: 'app-futbolistas',
  templateUrl: './futbolistas.component.html',
  styleUrls: ['./futbolistas.component.css']
})
export class FutbolistasComponent implements OnInit {

  public listadoFutbolistas: IFutbolista[];
  constructor(private servicio:FutbolistasService) { }

  ngOnInit() {
    this.mostrarListado();
  }

  //llama al servicio
  mostrarListado(){
    this.servicio.MostrarTodos()
    .subscribe(
      resultado=> {
        console.log(resultado);
        this.listadoFutbolistas=resultado;
      },
      ()=>console.log("Terminó de listar")
    );
  }
}
