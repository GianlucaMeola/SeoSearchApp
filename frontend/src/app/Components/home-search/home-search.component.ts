import { Component, ElementRef, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EngineResultDto } from '../../Dtos/engine-result.model';
import { EngineSearchService } from '../../services/engine-serch.service';

@Component({
  selector: 'app-home-search',
  templateUrl: './home-search.component.html',
  styleUrls: ['./home-search.component.css']
})
export class HomeSearchComponent implements OnInit {
  keyWordsFormControl = new FormControl();
  urlFormControl = new FormControl();
  googleCheckbox = new FormControl();
  bingCheckbox = new FormControl();
  allEngineSelected: boolean = false;
  searchResults: EngineResultDto[] = [];
  isLoading: boolean = false;

  @Input() searchEngines: Array<string> = ['Google', 'Bing'];

  constructor(
    private engineSearchService: EngineSearchService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.urlFormControl.setValue('www.infotrack.co.uk');
    this.googleCheckbox.setValue(true);
  }

  clearInputField(inputFieldName: string) {
    if (inputFieldName === 'keyWordsFormControl') {
      this.keyWordsFormControl.setValue('');
    } else if (inputFieldName === 'urlFormControl') {
      this.urlFormControl.setValue('');
    }
  }

  toggleAllCheck() {
    this.allEngineSelected = !this.allEngineSelected;
    this.googleCheckbox.setValue(this.allEngineSelected);
    this.bingCheckbox.setValue(this.allEngineSelected);
  }

  onSubmit() {
    const keywords = this.keyWordsFormControl.value;
    const url = this.urlFormControl.value;
    const selectedEngines = [];
    if (this.googleCheckbox.value) {
      selectedEngines.push('google');
    }
    if (this.bingCheckbox.value) {
      selectedEngines.push('bing');
    }

    if (keywords && url && selectedEngines.length > 0) {
      this.isLoading = true;

      this.engineSearchService.getSearchResults(keywords, url, selectedEngines).subscribe(
        (results: EngineResultDto[]) => {
          this.searchResults = results;
          this.isLoading = false;
        },
        (error) => {
          console.error('Error fetching search results:', error);
          this.showSnackBar('Error fetching search results.');
          this.isLoading = false;
        }
      );
    } else {
      this.showSnackBar('Please fill in all the fields and select at least one search engine.');
    }
  }

  private showSnackBar(message: string) {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
    });
  }
}
