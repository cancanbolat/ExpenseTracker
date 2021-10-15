import { Component } from '@angular/core';
import * as Highcharts from "highcharts";
import { ApiService } from './Services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  private records: any[] = [];
  private categories: any[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.getRecords();
    this.getCategories();
  }

  //records
  getRecords() {
    this.apiService.get("/record")
      .subscribe((data: any) => {
        this.records = data["value"];
      }, (error: any) => {
        console.log(error);
      });
  }

  //categories
  getCategories() {
    this.apiService.get("/category")
      .subscribe((values: any) =>
        this.categories = values["value"],
        (error: any) => console.log(error)
      );
  }

  Highcharts: typeof Highcharts = Highcharts;
  chartOptions: Highcharts.Options = {
    title: { text: "Expense Tracker" },
    yAxis: { title: { text: "Records" } },
    xAxis: { accessibility: { rangeDescription: "2021-2021" } },
    legend: { layout: "vertical", align: "right", verticalAlign: "middle" },
    series: [
      {
        name: "",
        data: [], //todo
        type: 'line'
      }],
    plotOptions: {
      series: {
        label: { connectorAllowed: true },
        pointStart: 0
      }
    },
    responsive: {
      rules: [{
        condition: {
          maxWidth: 1000
        },
        chartOptions: {
          legend: {
            layout: 'horizontal',
            align: 'center',
            verticalAlign: 'bottom'
          }
        }
      }]
    }
  }
}
