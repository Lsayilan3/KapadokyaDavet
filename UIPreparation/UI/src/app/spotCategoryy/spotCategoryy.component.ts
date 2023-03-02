import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { SpotCategoryy } from './models/SpotCategoryy';
import { SpotCategoryyService } from './services/SpotCategoryy.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-spotCategoryy',
	templateUrl: './spotCategoryy.component.html',
	styleUrls: ['./spotCategoryy.component.scss']
})
export class SpotCategoryyComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['spotCategoryyId','categoryId','categoryName', 'update','delete'];

	spotCategoryyList:SpotCategoryy[];
	spotCategoryy:SpotCategoryy=new SpotCategoryy();

	spotCategoryyAddForm: FormGroup;


	spotCategoryyId:number;

	constructor(private spotCategoryyService:SpotCategoryyService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getSpotCategoryyList();
    }

	ngOnInit() {

		this.createSpotCategoryyAddForm();
	}


	getSpotCategoryyList() {
		this.spotCategoryyService.getSpotCategoryyList().subscribe(data => {
			this.spotCategoryyList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.spotCategoryyAddForm.valid) {
			this.spotCategoryy = Object.assign({}, this.spotCategoryyAddForm.value)

			if (this.spotCategoryy.spotCategoryyId == 0)
				this.addSpotCategoryy();
			else
				this.updateSpotCategoryy();
		}

	}

	addSpotCategoryy(){

		this.spotCategoryyService.addSpotCategoryy(this.spotCategoryy).subscribe(data => {
			this.getSpotCategoryyList();
			this.spotCategoryy = new SpotCategoryy();
			jQuery('#spotcategoryy').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.spotCategoryyAddForm);

		})

	}

	updateSpotCategoryy(){

		this.spotCategoryyService.updateSpotCategoryy(this.spotCategoryy).subscribe(data => {

			var index=this.spotCategoryyList.findIndex(x=>x.spotCategoryyId==this.spotCategoryy.spotCategoryyId);
			this.spotCategoryyList[index]=this.spotCategoryy;
			this.dataSource = new MatTableDataSource(this.spotCategoryyList);
            this.configDataTable();
			this.spotCategoryy = new SpotCategoryy();
			jQuery('#spotcategoryy').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.spotCategoryyAddForm);

		})

	}

	createSpotCategoryyAddForm() {
		this.spotCategoryyAddForm = this.formBuilder.group({		
			spotCategoryyId : [0],
categoryId : [0, Validators.required],
categoryName : ["", Validators.required]
		})
	}

	deleteSpotCategoryy(spotCategoryyId:number){
		this.spotCategoryyService.deleteSpotCategoryy(spotCategoryyId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.spotCategoryyList=this.spotCategoryyList.filter(x=> x.spotCategoryyId!=spotCategoryyId);
			this.dataSource = new MatTableDataSource(this.spotCategoryyList);
			this.configDataTable();
		})
	}

	getSpotCategoryyById(spotCategoryyId:number){
		this.clearFormGroup(this.spotCategoryyAddForm);
		this.spotCategoryyService.getSpotCategoryyById(spotCategoryyId).subscribe(data=>{
			this.spotCategoryy=data;
			this.spotCategoryyAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'spotCategoryyId')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
