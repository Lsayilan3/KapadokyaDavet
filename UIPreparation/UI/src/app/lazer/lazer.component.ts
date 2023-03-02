import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Lazer } from './models/Lazer';
import { LazerService } from './services/lazer.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-lazer',
	templateUrl: './lazer.component.html',
	styleUrls: ['./lazer.component.scss']
})
export class LazerComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['lazerId','photo','title','tag','price','discountPrice', 'update','delete','file'];

	lazerList:Lazer[];
	lazer:Lazer=new Lazer();

	lazerAddForm: FormGroup;

	photoForm: FormGroup;

	lazerId:number;

	constructor(private lazerService:LazerService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getLazerList();
    }

	ngOnInit() {

		this.createLazerAddForm();
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
		formData.append('lazerId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.lazerService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getLazerList();
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


	getLazerList() {
		this.lazerService.getLazerList().subscribe(data => {
			this.lazerList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.lazerAddForm.valid) {
			this.lazer = Object.assign({}, this.lazerAddForm.value)

			if (this.lazer.lazerId == 0)
				this.addLazer();
			else
				this.updateLazer();
		}

	}

	addLazer(){

		this.lazerService.addLazer(this.lazer).subscribe(data => {
			this.getLazerList();
			this.lazer = new Lazer();
			jQuery('#lazer').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.lazerAddForm);

		})

	}

	updateLazer(){

		this.lazerService.updateLazer(this.lazer).subscribe(data => {

			var index=this.lazerList.findIndex(x=>x.lazerId==this.lazer.lazerId);
			this.lazerList[index]=this.lazer;
			this.dataSource = new MatTableDataSource(this.lazerList);
            this.configDataTable();
			this.lazer = new Lazer();
			jQuery('#lazer').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.lazerAddForm);

		})

	}

	createLazerAddForm() {
		this.lazerAddForm = this.formBuilder.group({		
			lazerId : [0],
photo : ["", Validators.required],
title : ["", Validators.required],
tag : ["", Validators.required],
price : ["", Validators.required],
discountPrice : ["", Validators.required]
		})
	}

	deleteLazer(lazerId:number){
		this.lazerService.deleteLazer(lazerId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.lazerList=this.lazerList.filter(x=> x.lazerId!=lazerId);
			this.dataSource = new MatTableDataSource(this.lazerList);
			this.configDataTable();
		})
	}

	getLazerById(lazerId:number){
		this.clearFormGroup(this.lazerAddForm);
		this.lazerService.getLazerById(lazerId).subscribe(data=>{
			this.lazer=data;
			this.lazerAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'lazerId')
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
