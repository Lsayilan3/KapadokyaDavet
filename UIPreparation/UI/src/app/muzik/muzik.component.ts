import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Muzik } from './models/Muzik';
import { MuzikService } from './services/muzik.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-muzik',
	templateUrl: './muzik.component.html',
	styleUrls: ['./muzik.component.scss']
})
export class MuzikComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['muzikId','photo','title','tag','detay', 'update','delete','file'];

	muzikList:Muzik[];
	muzik:Muzik=new Muzik();

	muzikAddForm: FormGroup;

	photoForm: FormGroup;

	muzikId:number;

	constructor(private muzikService:MuzikService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getMuzikList();
    }

	ngOnInit() {

		this.createMuzikAddForm();
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
		formData.append('muzikId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.muzikService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getMuzikList();
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


	getMuzikList() {
		this.muzikService.getMuzikList().subscribe(data => {
			this.muzikList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.muzikAddForm.valid) {
			this.muzik = Object.assign({}, this.muzikAddForm.value)

			if (this.muzik.muzikId == 0)
				this.addMuzik();
			else
				this.updateMuzik();
		}

	}

	addMuzik(){

		this.muzikService.addMuzik(this.muzik).subscribe(data => {
			this.getMuzikList();
			this.muzik = new Muzik();
			jQuery('#muzik').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.muzikAddForm);

		})

	}

	updateMuzik(){

		this.muzikService.updateMuzik(this.muzik).subscribe(data => {

			var index=this.muzikList.findIndex(x=>x.muzikId==this.muzik.muzikId);
			this.muzikList[index]=this.muzik;
			this.dataSource = new MatTableDataSource(this.muzikList);
            this.configDataTable();
			this.muzik = new Muzik();
			jQuery('#muzik').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.muzikAddForm);

		})

	}

	createMuzikAddForm() {
		this.muzikAddForm = this.formBuilder.group({		
			muzikId : [0],
photo : ["", Validators.required],
title : ["", Validators.required],
tag : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteMuzik(muzikId:number){
		this.muzikService.deleteMuzik(muzikId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.muzikList=this.muzikList.filter(x=> x.muzikId!=muzikId);
			this.dataSource = new MatTableDataSource(this.muzikList);
			this.configDataTable();
		})
	}

	getMuzikById(muzikId:number){
		this.clearFormGroup(this.muzikAddForm);
		this.muzikService.getMuzikById(muzikId).subscribe(data=>{
			this.muzik=data;
			this.muzikAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'muzikId')
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
