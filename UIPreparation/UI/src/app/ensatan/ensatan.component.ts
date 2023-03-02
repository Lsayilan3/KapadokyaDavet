import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Ensatan } from './models/Ensatan';
import { EnsatanService } from './services/ensatan.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-ensatan',
	templateUrl: './ensatan.component.html',
	styleUrls: ['./ensatan.component.scss']
})
export class EnsatanComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['ensatanId','photo','title','url', 'update','delete','file'];

	ensatanList:Ensatan[];
	ensatan:Ensatan=new Ensatan();

	ensatanAddForm: FormGroup;

	photoForm: FormGroup;

	ensatanId:number;

	constructor(private ensatanService:EnsatanService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getEnsatanList();
    }

	ngOnInit() {

		this.createEnsatanAddForm();
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
		formData.append('ensatanId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.ensatanService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getEnsatanList();
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

	getEnsatanList() {
		this.ensatanService.getEnsatanList().subscribe(data => {
			this.ensatanList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.ensatanAddForm.valid) {
			this.ensatan = Object.assign({}, this.ensatanAddForm.value)

			if (this.ensatan.ensatanId == 0)
				this.addEnsatan();
			else
				this.updateEnsatan();
		}

	}

	addEnsatan(){

		this.ensatanService.addEnsatan(this.ensatan).subscribe(data => {
			this.getEnsatanList();
			this.ensatan = new Ensatan();
			jQuery('#ensatan').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.ensatanAddForm);

		})

	}

	updateEnsatan(){

		this.ensatanService.updateEnsatan(this.ensatan).subscribe(data => {

			var index=this.ensatanList.findIndex(x=>x.ensatanId==this.ensatan.ensatanId);
			this.ensatanList[index]=this.ensatan;
			this.dataSource = new MatTableDataSource(this.ensatanList);
            this.configDataTable();
			this.ensatan = new Ensatan();
			jQuery('#ensatan').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.ensatanAddForm);

		})

	}

	createEnsatanAddForm() {
		this.ensatanAddForm = this.formBuilder.group({		
			ensatanId : [0],
photo : ["", Validators.required],
title : ["", Validators.required],
url : ["", Validators.required]
		})
	}

	deleteEnsatan(ensatanId:number){
		this.ensatanService.deleteEnsatan(ensatanId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.ensatanList=this.ensatanList.filter(x=> x.ensatanId!=ensatanId);
			this.dataSource = new MatTableDataSource(this.ensatanList);
			this.configDataTable();
		})
	}

	getEnsatanById(ensatanId:number){
		this.clearFormGroup(this.ensatanAddForm);
		this.ensatanService.getEnsatanById(ensatanId).subscribe(data=>{
			this.ensatan=data;
			this.ensatanAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'ensatanId')
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
