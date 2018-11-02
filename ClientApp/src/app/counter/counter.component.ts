import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;
   resp: string;

  public incrementCounter() {
    this.currentCount++;
  }

  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //  http.get<string>(baseUrl + 'api/SampleData/GetValues').subscribe(result => {
  //    this.resp = result;
  //  }, error => console.error(error));
  //}
}
