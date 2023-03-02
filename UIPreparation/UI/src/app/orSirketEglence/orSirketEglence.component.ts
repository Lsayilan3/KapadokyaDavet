import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrSirketEglence } from './models/OrSirketEglence';
import { OrSirketEglenceService } from './services/orSirketEglence.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orSirketEglence',
	templateUrl: './orSirketEglence.component.html',
	styleUrls: ['./orSirketEglence.component.scss']
})
export class OrSirketEglenceComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orSirketEglenceId','photo','detay', 'update','delete','file'];

	orSirketEglenceList:OrSirketEglence[];
	orSirketEglence:OrSirketEglence=new OrSirketEglence();

	orSirketEglenceAddForm: FormGroup;

	photoForm: FormGroup;

	orSirketEglenceId:number;

	constructor(private orSirketEglenceService:OrSirketEglenceService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrSirketEglenceList();
    }

	ngOnInit() {

		this.createOrSirketEglenceAddForm();
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
		formData.append('orSirketEglenceId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orSirketEglenceService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrSirketEglenceList();
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

	getOrSirketEglenceList() {
		this.orSirketEglenceService.getOrSirketEglenceList().subscribe(data => {
			this.orSirketEglenceList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orSirketEglenceAddForm.valid) {
			this.orSirketEglence = Object.assign({}, this.orSirketEglenceAddForm.value)

			if (this.orSirketEglence.orSirketEglenceId == 0)
				this.addOrSirketEglence();
			else
				this.updateOrSirketEglence();
		}

	}

	addOrSirketEglence(){

		this.orSirketEglenceService.addOrSirketEglence(this.orSirketEglence).subscribe(data => {
			this.getOrSirketEglenceList();
			this.orSirketEglence = new OrSirketEglence();
			jQuery('#orsirketeglence').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orSirketEglenceAddForm);

		})

	}

	updateOrSirketEglence(){

		this.orSirketEglenceService.updateOrSirketEglence(this.orSirketEglence).subscribe(data => {

			var index=this.orSirketEglenceList.findIndex(x=>x.orSirketEglenceId==this.orSirketEglence.orSirketEglenceId);
			this.orSirketEglenceList[index]=this.orSirketEglence;
			this.dataSource = new MatTableDataSource(this.orSirketEglenceList);
            this.configDataTable();
			this.orSirketEglence = new OrSirketEglence();
			jQuery('#orsirketeglence').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orSirketEglenceAddForm);

		})

	}

	createOrSirketEglenceAddForm() {
		this.orSirketEglenceAddForm = this.formBuilder.group({		
			orSirketEglenceId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrSirketEglence(orSirketEglenceId:number){
		this.orSirketEglenceService.deleteOrSirketEglence(orSirketEglenceId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orSirketEglenceList=this.orSirketEglenceList.filter(x=> x.orSirketEglenceId!=orSirketEglenceId);
			this.dataSource = new MatTableDataSource(this.orSirketEglenceList);
			this.configDataTable();
		})
	}

	getOrSirketEglenceById(orSirketEglenceId:number){
		this.clearFormGroup(this.orSirketEglenceAddForm);
		this.orSirketEglenceService.getOrSirketEglenceById(orSirketEglenceId).subscribe(data=>{
			this.orSirketEglence=data;
			this.orSirketEglenceAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orSirketEglenceId')
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
