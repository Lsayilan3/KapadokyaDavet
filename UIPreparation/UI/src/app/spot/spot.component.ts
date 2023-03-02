import { SpotCategoryy } from './../spotCategoryy/models/spotcategoryy';
import { SpotCategoryyService } from './../spotCategoryy/services/spotcategoryy.service';
import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Spot } from './models/Spot';
import { SpotService } from './services/spot.service';

declare var jQuery: any;

@Component({
	selector: 'app-spot',
	templateUrl: './spot.component.html',
	styleUrls: ['./spot.component.scss']
})
export class SpotComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['spotId','categoryId','photo','title','tag','price','discountPrice', 'update','delete','file'];

	spotList:Spot[];
	spot:Spot=new Spot();

	spotAddForm: FormGroup;

	photoForm: FormGroup;

	spotCategoryyList: SpotCategoryy [];

	spotId:number;



	constructor(private spotCategoryyService : SpotCategoryyService, private spotService:SpotService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getSpotList();
		this.spotCategoryyService.getSpotCategoryyList().subscribe(data=>this.spotCategoryyList=data);
    }

	ngOnInit() {
		this.spotCategoryyService.getSpotCategoryyList().subscribe(data=>this.spotCategoryyList=data);
		this.createSpotAddForm();
	}

	uploadFile(event) {
		const file = (event.target as HTMLInputElement).files[0];
		this.photoForm.patchValue({
		  file: file,
		});
		this.photoForm.get('file').updateValueAndValidity();
		
	  }

	upFile( id : number){
		this.photoForm = this.formBuilder.group({		
			id : [id],
file : ["", Validators.required]
		})
	}

	addPhotoSave(){
		var formData: any = new FormData();
		formData.append('spotId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.spotService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getSpotList();
				console.log(data);
				this.alertifyService.success(data);
})
		// this.http
		//   .post('https://localhost:44375/WebAPI/api/cities/addPhoto', formData)
		//   .subscribe({
    
		// 	next: (response) => {
		// 		jQuery('#photoModal').modal('hide');
		// 		this.clearFormGroup(this.photoForm);
		// 		this.getCityList();
		// 		console.log(response);
		// 		this.alertifyService.success(response.toString());
		// 	},
		// 	error: (error) => console.log(error),
			
		//   });
	}


	getSpotList() {
		this.spotService.getSpotList().subscribe(data => {
			this.spotList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.spotAddForm.valid) {
			this.spot = Object.assign({}, this.spotAddForm.value)

			if (this.spot.spotId == 0)
				this.addSpot();
			else
				this.updateSpot();
		}

	}

	addSpot(){

		this.spotService.addSpot(this.spot).subscribe(data => {
			this.getSpotList();
			this.spot = new Spot();
			jQuery('#spot').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.spotAddForm);

		})

	}

	updateSpot(){

		this.spotService.updateSpot(this.spot).subscribe(data => {

			var index=this.spotList.findIndex(x=>x.spotId==this.spot.spotId);
			this.spotList[index]=this.spot;
			this.dataSource = new MatTableDataSource(this.spotList);
            this.configDataTable();
			this.spot = new Spot();
			jQuery('#spot').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.spotAddForm);

		})

	}

	createSpotAddForm() {
		this.spotAddForm = this.formBuilder.group({		
			spotId : [0],
categoryId : [0, Validators.required],
photo : ["", Validators.required],
title : ["", Validators.required],
tag : ["", Validators.required],
price : ["", Validators.required],
discountPrice : ["", Validators.required]
		})
	}

	deleteSpot(spotId:number){
		this.spotService.deleteSpot(spotId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.spotList=this.spotList.filter(x=> x.spotId!=spotId);
			this.dataSource = new MatTableDataSource(this.spotList);
			this.configDataTable();
		})
	}

	getSpotById(spotId:number){
		this.clearFormGroup(this.spotAddForm);
		this.spotService.getSpotById(spotId).subscribe(data=>{
			this.spot=data;
			this.spotAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'spotId')
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
