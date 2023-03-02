import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Parti } from './models/Parti';
import { PartiService } from './services/parti.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-parti',
	templateUrl: './parti.component.html',
	styleUrls: ['./parti.component.scss']
})
export class PartiComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['partiId','photo','title','tag','price','discountPrice', 'update','delete','file'];

	partiList:Parti[];
	parti:Parti=new Parti();

	partiAddForm: FormGroup;

	photoForm: FormGroup;


	partiId:number;

	constructor(private partiService:PartiService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getPartiList();
    }

	ngOnInit() {

		this.createPartiAddForm();
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
		formData.append('partiId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.partiService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getPartiList();
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


	getPartiList() {
		this.partiService.getPartiList().subscribe(data => {
			this.partiList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.partiAddForm.valid) {
			this.parti = Object.assign({}, this.partiAddForm.value)

			if (this.parti.partiId == 0)
				this.addParti();
			else
				this.updateParti();
		}

	}

	addParti(){

		this.partiService.addParti(this.parti).subscribe(data => {
			this.getPartiList();
			this.parti = new Parti();
			jQuery('#parti').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.partiAddForm);

		})

	}

	updateParti(){

		this.partiService.updateParti(this.parti).subscribe(data => {

			var index=this.partiList.findIndex(x=>x.partiId==this.parti.partiId);
			this.partiList[index]=this.parti;
			this.dataSource = new MatTableDataSource(this.partiList);
            this.configDataTable();
			this.parti = new Parti();
			jQuery('#parti').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.partiAddForm);

		})

	}

	createPartiAddForm() {
		this.partiAddForm = this.formBuilder.group({		
			partiId : [0],
photo : ["", Validators.required],
title : ["", Validators.required],
tag : ["", Validators.required],
price : ["", Validators.required],
discountPrice : ["", Validators.required]
		})
	}

	deleteParti(partiId:number){
		this.partiService.deleteParti(partiId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.partiList=this.partiList.filter(x=> x.partiId!=partiId);
			this.dataSource = new MatTableDataSource(this.partiList);
			this.configDataTable();
		})
	}

	getPartiById(partiId:number){
		this.clearFormGroup(this.partiAddForm);
		this.partiService.getPartiById(partiId).subscribe(data=>{
			this.parti=data;
			this.partiAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'partiId')
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
