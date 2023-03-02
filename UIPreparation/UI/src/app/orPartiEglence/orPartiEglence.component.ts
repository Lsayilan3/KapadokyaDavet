import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrPartiEglence } from './models/OrPartiEglence';
import { OrPartiEglenceService } from './services/orPartiEglence.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orPartiEglence',
	templateUrl: './orPartiEglence.component.html',
	styleUrls: ['./orPartiEglence.component.scss']
})
export class OrPartiEglenceComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orPartiEglenceId','photo','detay', 'update','delete','file'];

	orPartiEglenceList:OrPartiEglence[];
	orPartiEglence:OrPartiEglence=new OrPartiEglence();

	orPartiEglenceAddForm: FormGroup;

	photoForm: FormGroup;

	orPartiEglenceId:number;

	constructor(private orPartiEglenceService:OrPartiEglenceService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrPartiEglenceList();
    }

	ngOnInit() {

		this.createOrPartiEglenceAddForm();
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
		formData.append('orPartiEglenceId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orPartiEglenceService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrPartiEglenceList();
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


	getOrPartiEglenceList() {
		this.orPartiEglenceService.getOrPartiEglenceList().subscribe(data => {
			this.orPartiEglenceList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orPartiEglenceAddForm.valid) {
			this.orPartiEglence = Object.assign({}, this.orPartiEglenceAddForm.value)

			if (this.orPartiEglence.orPartiEglenceId == 0)
				this.addOrPartiEglence();
			else
				this.updateOrPartiEglence();
		}

	}

	addOrPartiEglence(){

		this.orPartiEglenceService.addOrPartiEglence(this.orPartiEglence).subscribe(data => {
			this.getOrPartiEglenceList();
			this.orPartiEglence = new OrPartiEglence();
			jQuery('#orpartieglence').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orPartiEglenceAddForm);

		})

	}

	updateOrPartiEglence(){

		this.orPartiEglenceService.updateOrPartiEglence(this.orPartiEglence).subscribe(data => {

			var index=this.orPartiEglenceList.findIndex(x=>x.orPartiEglenceId==this.orPartiEglence.orPartiEglenceId);
			this.orPartiEglenceList[index]=this.orPartiEglence;
			this.dataSource = new MatTableDataSource(this.orPartiEglenceList);
            this.configDataTable();
			this.orPartiEglence = new OrPartiEglence();
			jQuery('#orpartieglence').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orPartiEglenceAddForm);

		})

	}

	createOrPartiEglenceAddForm() {
		this.orPartiEglenceAddForm = this.formBuilder.group({		
			orPartiEglenceId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrPartiEglence(orPartiEglenceId:number){
		this.orPartiEglenceService.deleteOrPartiEglence(orPartiEglenceId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orPartiEglenceList=this.orPartiEglenceList.filter(x=> x.orPartiEglenceId!=orPartiEglenceId);
			this.dataSource = new MatTableDataSource(this.orPartiEglenceList);
			this.configDataTable();
		})
	}

	getOrPartiEglenceById(orPartiEglenceId:number){
		this.clearFormGroup(this.orPartiEglenceAddForm);
		this.orPartiEglenceService.getOrPartiEglenceById(orPartiEglenceId).subscribe(data=>{
			this.orPartiEglence=data;
			this.orPartiEglenceAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orPartiEglenceId')
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
