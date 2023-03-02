import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Organizasyon } from './models/Organizasyon';
import { OrganizasyonService } from './services/organizasyon.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-organizasyon',
	templateUrl: './organizasyon.component.html',
	styleUrls: ['./organizasyon.component.scss']
})
export class OrganizasyonComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['organizasyonId','photo','title','tag','detay', 'update','delete','file'];

	organizasyonList:Organizasyon[];
	organizasyon:Organizasyon=new Organizasyon();

	organizasyonAddForm: FormGroup;

	photoForm: FormGroup;

	organizasyonId:number;

	constructor(private organizasyonService:OrganizasyonService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrganizasyonList();
    }

	ngOnInit() {

		this.createOrganizasyonAddForm();
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
		formData.append('organizasyonId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.organizasyonService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrganizasyonList();
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


	getOrganizasyonList() {
		this.organizasyonService.getOrganizasyonList().subscribe(data => {
			this.organizasyonList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.organizasyonAddForm.valid) {
			this.organizasyon = Object.assign({}, this.organizasyonAddForm.value)

			if (this.organizasyon.organizasyonId == 0)
				this.addOrganizasyon();
			else
				this.updateOrganizasyon();
		}

	}

	addOrganizasyon(){

		this.organizasyonService.addOrganizasyon(this.organizasyon).subscribe(data => {
			this.getOrganizasyonList();
			this.organizasyon = new Organizasyon();
			jQuery('#organizasyon').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.organizasyonAddForm);

		})

	}

	updateOrganizasyon(){

		this.organizasyonService.updateOrganizasyon(this.organizasyon).subscribe(data => {

			var index=this.organizasyonList.findIndex(x=>x.organizasyonId==this.organizasyon.organizasyonId);
			this.organizasyonList[index]=this.organizasyon;
			this.dataSource = new MatTableDataSource(this.organizasyonList);
            this.configDataTable();
			this.organizasyon = new Organizasyon();
			jQuery('#organizasyon').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.organizasyonAddForm);

		})

	}

	createOrganizasyonAddForm() {
		this.organizasyonAddForm = this.formBuilder.group({		
			organizasyonId : [0],
photo : ["", Validators.required],
title : ["", Validators.required],
tag : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrganizasyon(organizasyonId:number){
		this.organizasyonService.deleteOrganizasyon(organizasyonId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.organizasyonList=this.organizasyonList.filter(x=> x.organizasyonId!=organizasyonId);
			this.dataSource = new MatTableDataSource(this.organizasyonList);
			this.configDataTable();
		})
	}

	getOrganizasyonById(organizasyonId:number){
		this.clearFormGroup(this.organizasyonAddForm);
		this.organizasyonService.getOrganizasyonById(organizasyonId).subscribe(data=>{
			this.organizasyon=data;
			this.organizasyonAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'organizasyonId')
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
