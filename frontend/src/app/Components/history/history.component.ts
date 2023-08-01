import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { HistoryService } from '../../services/history-search.service';
import { EngineResultDto } from '../../Dtos/engine-result.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  searchResults: EngineResultDto[] = [];
  dataSource: MatTableDataSource<EngineResultDto> = new MatTableDataSource<EngineResultDto>();
  displayedColumns: string[] = ['keyWords', 'url', 'engineName', 'ranking', 'timeStamp'];
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageSize: number = 10;
  pageIndex: number = 0;
  totalItems: number = 0;

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;

  constructor(private historyService: HistoryService, private datePipe: DatePipe) {}

  ngOnInit(): void {
    this.dataSource.paginator = this.paginator;
    this.fetchSearchResults();
  }

  fetchSearchResults(): void {
    this.historyService.getSearchResults().subscribe((results: EngineResultDto[]) => {
      results.forEach(e => (e.timeStamp = this.formatTimestamp(e.timeStamp)));
      this.searchResults = results;
      this.totalItems = results.length;
      this.updateDataSource();
    });
  }

  formatTimestamp(timestamp: string): string {
    const date = new Date(timestamp);
    return this.datePipe.transform(date, 'yyyy-MM-dd HH:mm:ss')!;
  }

  onPageChange(event: PageEvent): void {
    this.pageSize = event.pageSize;
    this.pageIndex = event.pageIndex;
    this.updateDataSource();
  }

  private updateDataSource(): void {
    const startIndex = this.pageIndex * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.dataSource.data = this.searchResults;
  }
}
