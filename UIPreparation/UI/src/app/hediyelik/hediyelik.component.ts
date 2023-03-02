import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Hediyelik } from './models/Hediyelik';
import { HediyelikService } from './services/hediyelik.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-hediyelik',
	templateUrl: './hediyelik.component.html',
	styleUrls: ['./hediyelik.component.scss']
})
export class HediyelikComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['hediyelikId','photo','title','tag','price','discountPrice', 'update','delete','file'];

	hediyelikList:Hediyelik[];
	hediyelik:Hediyelik=new Hediyelik();

	hediyelikAddForm: FormGroup;

	photoForm: FormGroup;

	hediyelikId:number;

	constructor(private hediyelikService:HediyelikService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getHediyelikList();
    }

	ngOnInit() {

		this.createHediyelikAddForm();
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
		formData.append('hediyelikId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.hediyelikService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getHediyelikList();
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


	getHediyelikList() {
		this.hediyelikService.getHediyelikList().subscribe(data => {
			this.hediyelikList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.hediyelikAddForm.valid) {
			this.hediyelik = Object.assign({}, this.hediyelikAddForm.value)

			if (this.hediyelik.hediyelikId == 0)
				this.addHediyelik();
			else
				this.updateHediyelik();
		}

	}

	addHediyelik(){

		this.hediyelikService.addHediyelik(this.hediyelik).subscribe(data => {
			this.getHediyelikList();
			this.hediyelik = new Hediyelik();
			jQuery('#hediyelik').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hediyelikAddForm);

		})

	}

	updateHediyelik(){

		this.hediyelikService.updateHediyelik(this.hediyelik).subscribe(data => {

			var index=this.hediyelikList.findIndex(x=>x.hediyelikId==this.hediyelik.hediyelikId);
			this.hediyelikList[index]=this.hediyelik;
			this.dataSource = new MatTableDataSource(this.hediyelikList);
            this.configDataTable();
			this.hediyelik = new Hediyelik();
			jQuery('#hediyelik').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.hediyelikAddForm);

		})

	}

	createHediyelikAddForm() {
		this.hediyelikAddForm = this.formBuilder.group({		
			hediyelikId : [0],
photo : ["", Validators.required],
title : ["", Validators.required],
tag : ["", Validators.required],
price : ["", Validators.required],
discountPrice : ["", Validators.required]
		})
	}

	deleteHediyelik(hediyelikId:number){
		this.hediyelikService.deleteHediyelik(hediyelikId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.hediyelikList=this.hediyelikList.filter(x=> x.hediyelikId!=hediyelikId);
			this.dataSource = new MatTableDataSource(this.hediyelikList);
			this.configDataTable();
		})
	}

	getHediyelikById(hediyelikId:number){
		this.clearFormGroup(this.hediyelikAddForm);
		this.hediyelikService.getHediyelikById(hediyelikId).subscribe(data=>{
			this.hediyelik=data;
			this.hediyelikAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'hediyelikId')
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
