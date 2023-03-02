import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrAnimasyone } from './models/OrAnimasyone';
import { OrAnimasyoneService } from './services/orAnimasyone.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orAnimasyone',
	templateUrl: './orAnimasyone.component.html',
	styleUrls: ['./orAnimasyone.component.scss']
})
export class OrAnimasyoneComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orAnimasyoneId','photo','detay', 'update','delete','file'];

	orAnimasyoneList:OrAnimasyone[];
	orAnimasyone:OrAnimasyone=new OrAnimasyone();

	orAnimasyoneAddForm: FormGroup;

	photoForm: FormGroup;

	orAnimasyoneId:number;

	constructor(private orAnimasyoneService:OrAnimasyoneService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrAnimasyoneList();
    }

	ngOnInit() {

		this.createOrAnimasyoneAddForm();
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
		formData.append('orAnimasyoneId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orAnimasyoneService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrAnimasyoneList();
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


	getOrAnimasyoneList() {
		this.orAnimasyoneService.getOrAnimasyoneList().subscribe(data => {
			this.orAnimasyoneList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orAnimasyoneAddForm.valid) {
			this.orAnimasyone = Object.assign({}, this.orAnimasyoneAddForm.value)

			if (this.orAnimasyone.orAnimasyoneId == 0)
				this.addOrAnimasyone();
			else
				this.updateOrAnimasyone();
		}

	}

	addOrAnimasyone(){

		this.orAnimasyoneService.addOrAnimasyone(this.orAnimasyone).subscribe(data => {
			this.getOrAnimasyoneList();
			this.orAnimasyone = new OrAnimasyone();
			jQuery('#oranimasyone').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orAnimasyoneAddForm);

		})

	}

	updateOrAnimasyone(){

		this.orAnimasyoneService.updateOrAnimasyone(this.orAnimasyone).subscribe(data => {

			var index=this.orAnimasyoneList.findIndex(x=>x.orAnimasyoneId==this.orAnimasyone.orAnimasyoneId);
			this.orAnimasyoneList[index]=this.orAnimasyone;
			this.dataSource = new MatTableDataSource(this.orAnimasyoneList);
            this.configDataTable();
			this.orAnimasyone = new OrAnimasyone();
			jQuery('#oranimasyone').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orAnimasyoneAddForm);

		})

	}

	createOrAnimasyoneAddForm() {
		this.orAnimasyoneAddForm = this.formBuilder.group({		
			orAnimasyoneId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrAnimasyone(orAnimasyoneId:number){
		this.orAnimasyoneService.deleteOrAnimasyone(orAnimasyoneId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orAnimasyoneList=this.orAnimasyoneList.filter(x=> x.orAnimasyoneId!=orAnimasyoneId);
			this.dataSource = new MatTableDataSource(this.orAnimasyoneList);
			this.configDataTable();
		})
	}

	getOrAnimasyoneById(orAnimasyoneId:number){
		this.clearFormGroup(this.orAnimasyoneAddForm);
		this.orAnimasyoneService.getOrAnimasyoneById(orAnimasyoneId).subscribe(data=>{
			this.orAnimasyone=data;
			this.orAnimasyoneAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orAnimasyoneId')
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
