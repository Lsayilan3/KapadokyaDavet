import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrAcilis } from './models/OrAcilis';
import { OrAcilisService } from './services/orAcilis.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orAcilis',
	templateUrl: './orAcilis.component.html',
	styleUrls: ['./orAcilis.component.scss']
})
export class OrAcilisComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orAcilisId','photo','detay', 'update','delete','file'];

	orAcilisList:OrAcilis[];
	orAcilis:OrAcilis=new OrAcilis();

	orAcilisAddForm: FormGroup;

	photoForm: FormGroup;

	orAcilisId:number;

	constructor(private orAcilisService:OrAcilisService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrAcilisList();
    }

	ngOnInit() {

		this.createOrAcilisAddForm();
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
		formData.append('orAcilisId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orAcilisService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrAcilisList();
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



	getOrAcilisList() {
		this.orAcilisService.getOrAcilisList().subscribe(data => {
			this.orAcilisList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orAcilisAddForm.valid) {
			this.orAcilis = Object.assign({}, this.orAcilisAddForm.value)

			if (this.orAcilis.orAcilisId == 0)
				this.addOrAcilis();
			else
				this.updateOrAcilis();
		}

	}

	addOrAcilis(){

		this.orAcilisService.addOrAcilis(this.orAcilis).subscribe(data => {
			this.getOrAcilisList();
			this.orAcilis = new OrAcilis();
			jQuery('#oracilis').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orAcilisAddForm);

		})

	}

	updateOrAcilis(){

		this.orAcilisService.updateOrAcilis(this.orAcilis).subscribe(data => {

			var index=this.orAcilisList.findIndex(x=>x.orAcilisId==this.orAcilis.orAcilisId);
			this.orAcilisList[index]=this.orAcilis;
			this.dataSource = new MatTableDataSource(this.orAcilisList);
            this.configDataTable();
			this.orAcilis = new OrAcilis();
			jQuery('#oracilis').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orAcilisAddForm);

		})

	}

	createOrAcilisAddForm() {
		this.orAcilisAddForm = this.formBuilder.group({		
			orAcilisId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrAcilis(orAcilisId:number){
		this.orAcilisService.deleteOrAcilis(orAcilisId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orAcilisList=this.orAcilisList.filter(x=> x.orAcilisId!=orAcilisId);
			this.dataSource = new MatTableDataSource(this.orAcilisList);
			this.configDataTable();
		})
	}

	getOrAcilisById(orAcilisId:number){
		this.clearFormGroup(this.orAcilisAddForm);
		this.orAcilisService.getOrAcilisById(orAcilisId).subscribe(data=>{
			this.orAcilis=data;
			this.orAcilisAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orAcilisId')
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
