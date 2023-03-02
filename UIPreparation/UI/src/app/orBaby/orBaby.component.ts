import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrBaby } from './models/OrBaby';
import { OrBabyService } from './services/orBaby.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orBaby',
	templateUrl: './orBaby.component.html',
	styleUrls: ['./orBaby.component.scss']
})
export class OrBabyComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orBabyId','photo','detay', 'update','delete','file'];

	orBabyList:OrBaby[];
	orBaby:OrBaby=new OrBaby();

	orBabyAddForm: FormGroup;

	photoForm: FormGroup;

	orBabyId:number;

	constructor(private orBabyService:OrBabyService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrBabyList();
    }

	ngOnInit() {

		this.createOrBabyAddForm();
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
		formData.append('orBabyId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orBabyService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrBabyList();
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


	getOrBabyList() {
		this.orBabyService.getOrBabyList().subscribe(data => {
			this.orBabyList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orBabyAddForm.valid) {
			this.orBaby = Object.assign({}, this.orBabyAddForm.value)

			if (this.orBaby.orBabyId == 0)
				this.addOrBaby();
			else
				this.updateOrBaby();
		}

	}

	addOrBaby(){

		this.orBabyService.addOrBaby(this.orBaby).subscribe(data => {
			this.getOrBabyList();
			this.orBaby = new OrBaby();
			jQuery('#orbaby').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orBabyAddForm);

		})

	}

	updateOrBaby(){

		this.orBabyService.updateOrBaby(this.orBaby).subscribe(data => {

			var index=this.orBabyList.findIndex(x=>x.orBabyId==this.orBaby.orBabyId);
			this.orBabyList[index]=this.orBaby;
			this.dataSource = new MatTableDataSource(this.orBabyList);
            this.configDataTable();
			this.orBaby = new OrBaby();
			jQuery('#orbaby').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orBabyAddForm);

		})

	}

	createOrBabyAddForm() {
		this.orBabyAddForm = this.formBuilder.group({		
			orBabyId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrBaby(orBabyId:number){
		this.orBabyService.deleteOrBaby(orBabyId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orBabyList=this.orBabyList.filter(x=> x.orBabyId!=orBabyId);
			this.dataSource = new MatTableDataSource(this.orBabyList);
			this.configDataTable();
		})
	}

	getOrBabyById(orBabyId:number){
		this.clearFormGroup(this.orBabyAddForm);
		this.orBabyService.getOrBabyById(orBabyId).subscribe(data=>{
			this.orBaby=data;
			this.orBabyAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orBabyId')
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
