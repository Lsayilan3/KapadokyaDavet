import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Yiyecek } from './models/Yiyecek';
import { YiyecekService } from './services/yiyecek.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-yiyecek',
	templateUrl: './yiyecek.component.html',
	styleUrls: ['./yiyecek.component.scss']
})
export class YiyecekComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['yiyecekId','photo','title','tag','price','discountPrice', 'update','delete','file'];

	yiyecekList:Yiyecek[];
	yiyecek:Yiyecek=new Yiyecek();

	yiyecekAddForm: FormGroup;

	photoForm: FormGroup;

	yiyecekId:number;

	constructor(private yiyecekService:YiyecekService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getYiyecekList();
    }

	ngOnInit() {

		this.createYiyecekAddForm();
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
		formData.append('yiyecekId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.yiyecekService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getYiyecekList();
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


	getYiyecekList() {
		this.yiyecekService.getYiyecekList().subscribe(data => {
			this.yiyecekList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.yiyecekAddForm.valid) {
			this.yiyecek = Object.assign({}, this.yiyecekAddForm.value)

			if (this.yiyecek.yiyecekId == 0)
				this.addYiyecek();
			else
				this.updateYiyecek();
		}

	}

	addYiyecek(){

		this.yiyecekService.addYiyecek(this.yiyecek).subscribe(data => {
			this.getYiyecekList();
			this.yiyecek = new Yiyecek();
			jQuery('#yiyecek').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.yiyecekAddForm);

		})

	}

	updateYiyecek(){

		this.yiyecekService.updateYiyecek(this.yiyecek).subscribe(data => {

			var index=this.yiyecekList.findIndex(x=>x.yiyecekId==this.yiyecek.yiyecekId);
			this.yiyecekList[index]=this.yiyecek;
			this.dataSource = new MatTableDataSource(this.yiyecekList);
            this.configDataTable();
			this.yiyecek = new Yiyecek();
			jQuery('#yiyecek').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.yiyecekAddForm);

		})

	}

	createYiyecekAddForm() {
		this.yiyecekAddForm = this.formBuilder.group({		
			yiyecekId : [0],
photo : ["", Validators.required],
title : ["", Validators.required],
tag : ["", Validators.required],
price : ["", Validators.required],
discountPrice : ["", Validators.required]
		})
	}

	deleteYiyecek(yiyecekId:number){
		this.yiyecekService.deleteYiyecek(yiyecekId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.yiyecekList=this.yiyecekList.filter(x=> x.yiyecekId!=yiyecekId);
			this.dataSource = new MatTableDataSource(this.yiyecekList);
			this.configDataTable();
		})
	}

	getYiyecekById(yiyecekId:number){
		this.clearFormGroup(this.yiyecekAddForm);
		this.yiyecekService.getYiyecekById(yiyecekId).subscribe(data=>{
			this.yiyecek=data;
			this.yiyecekAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'yiyecekId')
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
