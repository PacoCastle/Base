import { Component, OnInit, ViewChild } from '@angular/core';
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated"
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { MatPaginator, MatTableDataSource } from '@angular/material';
@Component({
  selector: 'app-balancing-process',
  templateUrl: './balancing-process.component.html',
  styleUrls: ['./balancing-process.component.css']
})
export class BalancingProcessComponent implements OnInit {
  balancing: FormGroup;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  dataSourceAttempts: MatTableDataSource<any>;
  columnsToDisplay: string[] = ['attempt']

  constructor(private authService: AuthService,) { }
  dummyAttempt={
    part: "254C1846",
    description: "Pieza1",
    attemps:[
      {
        part: "254C1846",
        attempt: "LH 22 CH 0.0 RH 1.3 17/07/2020 11:12:05",
        date: "17/07/2020",
        hour: "11:12:05",
        data:[
          {
            angle: "LH",
            quantity: 22
          },
          {
            angle: "CH",
            quantity: 0.0
          },
           {
            angle: "RH",
            quantity: 21.32
          },
        ]
      },
      {
        part: "254C1846",
        attempt: "LH 22 CH 0.0 RH 1.3 17/07/2020 11:12:05",
        date: "17/07/2020",
        hour: "11:12:05",
        data:[
          {
            angle: "LH",
            quantity: 22
          },
          {
            angle: "CH",
            quantity: 0.0
          },
           {
            angle: "RH",
            quantity: 21.32
          },
        ]
      },
      {
        part: "254C1846",
        attempt: "LH 22 CH 0.0 RH 1.3 17/07/2020 11:12:05",
        date: "17/07/2020",
        hour: "11:12:05",
        data:[
          {
            angle: "LH",
            quantity: 22
          },
          {
            angle: "CH",
            quantity: 0.0
          },
           {
            angle: "RH",
            quantity: 21.32
          },
        ]
      }
    ]

  };
  ngOnInit() {
    this.dataSourceAttempts = new MatTableDataSource<any>(this.dummyAttempt.attemps);
    this.dataSourceAttempts.paginator = this.paginator;
    this.graph();
    this.balancing = new FormGroup({
      user: new FormControl(this.authService.currentUser.username,[Validators.required]),
      partNumber: new FormControl(13215446,[Validators.required]),
      consecutive: new FormControl(123544125778,[Validators.required]),
      date: new FormControl("14/07/2020",[Validators.required]),
      hour: new FormControl("03:00",[Validators.required]),
      planeNumber: new FormControl(25,[Validators.required]),
      tubeDiameter: new FormControl(21,[Validators.required]),
      rpm: new FormControl(24,[Validators.required]),
      angleLH1: new FormControl(3,[Validators.required]),
      massLH1: new FormControl(12,[Validators.required]),
      angleLH2: new FormControl(3,[Validators.required]),
      massLH2: new FormControl(12,[Validators.required]),
      angleLH3: new FormControl(3,[Validators.required]),
      massLH3: new FormControl(12,[Validators.required])
    });
  }
  graph() {
    am4core.ready(function() {

      // Themes begin
      am4core.useTheme(am4themes_animated);
      // Themes end
      
      /* Create chart instance */
      let chart = am4core.create("chartdiv", am4charts.RadarChart);
      
      /* Add data */
      chart.data = [ {
        "direction": "N",
        "value": 8
      }, {
        "direction": "NE",
        "value": 9
      }, {
        "direction": "E",
        "value": 4.5
      }, {
        "direction": "SE",
        "value": 3.5
      }, {
        "direction": "S",
        "value": 9.2
      }, {
        "direction": "SW",
        "value": 8.4
      }, {
        "direction": "W",
        "value": 11.1
      }, {
        "direction": "NW",
        "value": 10
      } ];
      
      /* Create axes */
      let categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis() as any);
      categoryAxis.dataFields.category = "direction";
      let valueAxis = chart.yAxes.push(new am4charts.ValueAxis() as any);
      //valueAxis.renderer.gridType = "polygons";
      
      let range = categoryAxis.axisRanges.create();
    
      range.axisFill.fill = am4core.color("#0066CC");
      range.axisFill.fillOpacity = 0.3;
      
      let range2 = categoryAxis.axisRanges.create();
      range2.axisFill.fill = am4core.color("#0066CC");
      range2.axisFill.fillOpacity = 0.3;
      
      let range3 = categoryAxis.axisRanges.create();
      range3.axisFill.fill = am4core.color("#CC3333");
      range3.axisFill.fillOpacity = 0.3;
      range3.locations.endCategory = 0;
      
      /* Create and configure series */
      let series = chart.series.push(new am4charts.RadarSeries());
      series.dataFields.valueY = "value";
      series.dataFields.categoryX = "direction";
      series.name = "Wind direction";
      series.strokeWidth = 3;
      series.fillOpacity = 0.2;
      
      });
  }

}
