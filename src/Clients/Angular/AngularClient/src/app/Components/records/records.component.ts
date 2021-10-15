import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Category } from '../../Models/Category';
import { Record } from '../../Models/Record';
import { ApiService } from '../../Services/api.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-records',
  templateUrl: './records.component.html',
  styleUrls: ['./records.component.css']
})
export class RecordsComponent implements OnInit {
  closeResult = '';
  baseUrl = 'http://localhost:5001/api/v1';
  page = 1;
  pageSize = 5;

  records: any[] = [];
  record: Record = {
    id: '',
    title: '',
    categoryId: '',
    amount: 0,
    CreatedAt: '',
    UpdatedAt: ''
  };

  categories: any = {};
  category: Category = {
    id: '',
    name: '',
    type: ''
  };

  constructor(private httpClient: HttpClient,
    private apiService: ApiService,
    private modalService: NgbModal) { }

  ngOnInit() {
    this.getRecords();
    this.getCategories();
  }

  //data table
  open(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  //categories
  getCategories() {
    this.apiService.get("/category")
      .subscribe(values => this.categories = values,
        error => console.log(error)
      );
  }

  getCategoryById(id: string) {
    this.httpClient.get(this.baseUrl + "/category" + id)
      .subscribe(value => this.category == value,
        error => console.log(error)
      );
  }

  //records
  getRecords() {
    this.apiService.get("/record")
      .subscribe((data: any) => {
        this.records = data["value"];
      }, error => {
        console.log(error);
      });
  }

  addRecord() {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(this.record);

    this.apiService.post("/record", body, { 'headers': headers }).subscribe(data => {
      this.getRecords();
      this.clear();
    });
  }

  deleteRecord(id: string) {
    this.apiService.delete("/record/" + id).subscribe(data => {
      this.getRecords()
    })
  }

  deleteAll() {
    this.apiService.delete("/record/deleteAll").subscribe(data => {
      this.getRecords()
    })
  }

  getRecordsByCategory(categoryName: string) {
    this.apiService.get("/record/getrecordbycategoryname/" + categoryName)
      .subscribe((data: any) => {
        this.records = data["value"]
      })
  }

  getRecordsByDate(date: string) {
    this.apiService.get("/record/getrecordbydate/" + date)
      .subscribe((data: any) => {
        this.records = data["value"]
      })
  }

  private clear() {
    this.record.title = '';
    this.record.categoryId = '';
    this.record.amount = 0;
  };

}
