import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Cikolata } from './models/Cikolata';
import { CikolataService } from './services/cikolata.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-cikolata',
	templateUrl: './cikolata.component.html',
	styleUrls: ['./cikolata.component.scss']
})
export class CikolataComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['cikolataId','photo','title','tag','detay', 'update','delete','file'];

	cikolataList:Cikolata[];
	cikolata:Cikolata=new Cikolata();

	cikolataAddForm: FormGroup;

	photoForm: FormGroup;

	cikolataId:number;

	constructor(private cikolataService:CikolataService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getCikolataList();
    }

	ngOnInit() {

		this.createCikolataAddForm();
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
		formData.append('cikolataId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.cikolataService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getCikolataList();
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


	getCikolataList() {
		this.cikolataService.getCikolataList().subscribe(data => {
			this.cikolataList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.cikolataAddForm.valid) {
			this.cikolata = Object.assign({}, this.cikolataAddForm.value)

			if (this.cikolata.cikolataId == 0)
				this.addCikolata();
			else
				this.updateCikolata();
		}

	}

	addCikolata(){

		this.cikolataService.addCikolata(this.cikolata).subscribe(data => {
			this.getCikolataList();
			this.cikolata = new Cikolata();
			jQuery('#cikolata').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.cikolataAddForm);

		})

	}

	updateCikolata(){

		this.cikolataService.updateCikolata(this.cikolata).subscribe(data => {

			var index=this.cikolataList.findIndex(x=>x.cikolataId==this.cikolata.cikolataId);
			this.cikolataList[index]=this.cikolata;
			this.dataSource = new MatTableDataSource(this.cikolataList);
            this.configDataTable();
			this.cikolata = new Cikolata();
			jQuery('#cikolata').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.cikolataAddForm);

		})

	}

	createCikolataAddForm() {
		this.cikolataAddForm = this.formBuilder.group({		
			cikolataId : [0],
photo : ["", Validators.required],
title : ["", Validators.required],
tag : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteCikolata(cikolataId:number){
		this.cikolataService.deleteCikolata(cikolataId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.cikolataList=this.cikolataList.filter(x=> x.cikolataId!=cikolataId);
			this.dataSource = new MatTableDataSource(this.cikolataList);
			this.configDataTable();
		})
	}

	getCikolataById(cikolataId:number){
		this.clearFormGroup(this.cikolataAddForm);
		this.cikolataService.getCikolataById(cikolataId).subscribe(data=>{
			this.cikolata=data;
			this.cikolataAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'cikolataId')
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
