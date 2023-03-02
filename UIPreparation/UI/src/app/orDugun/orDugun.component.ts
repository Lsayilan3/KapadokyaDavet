import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrDugun } from './models/OrDugun';
import { OrDugunService } from './services/orDugun.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orDugun',
	templateUrl: './orDugun.component.html',
	styleUrls: ['./orDugun.component.scss']
})
export class OrDugunComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orDugunId','photo','detay', 'update','delete','file'];

	orDugunList:OrDugun[];
	orDugun:OrDugun=new OrDugun();

	orDugunAddForm: FormGroup;

	photoForm: FormGroup;

	orDugunId:number;

	constructor(private orDugunService:OrDugunService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrDugunList();
    }

	ngOnInit() {

		this.createOrDugunAddForm();
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
		formData.append('orDugunId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orDugunService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrDugunList();
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


	getOrDugunList() {
		this.orDugunService.getOrDugunList().subscribe(data => {
			this.orDugunList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orDugunAddForm.valid) {
			this.orDugun = Object.assign({}, this.orDugunAddForm.value)

			if (this.orDugun.orDugunId == 0)
				this.addOrDugun();
			else
				this.updateOrDugun();
		}

	}

	addOrDugun(){

		this.orDugunService.addOrDugun(this.orDugun).subscribe(data => {
			this.getOrDugunList();
			this.orDugun = new OrDugun();
			jQuery('#ordugun').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orDugunAddForm);

		})

	}

	updateOrDugun(){

		this.orDugunService.updateOrDugun(this.orDugun).subscribe(data => {

			var index=this.orDugunList.findIndex(x=>x.orDugunId==this.orDugun.orDugunId);
			this.orDugunList[index]=this.orDugun;
			this.dataSource = new MatTableDataSource(this.orDugunList);
            this.configDataTable();
			this.orDugun = new OrDugun();
			jQuery('#ordugun').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orDugunAddForm);

		})

	}

	createOrDugunAddForm() {
		this.orDugunAddForm = this.formBuilder.group({		
			orDugunId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrDugun(orDugunId:number){
		this.orDugunService.deleteOrDugun(orDugunId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orDugunList=this.orDugunList.filter(x=> x.orDugunId!=orDugunId);
			this.dataSource = new MatTableDataSource(this.orDugunList);
			this.configDataTable();
		})
	}

	getOrDugunById(orDugunId:number){
		this.clearFormGroup(this.orDugunAddForm);
		this.orDugunService.getOrDugunById(orDugunId).subscribe(data=>{
			this.orDugun=data;
			this.orDugunAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orDugunId')
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
