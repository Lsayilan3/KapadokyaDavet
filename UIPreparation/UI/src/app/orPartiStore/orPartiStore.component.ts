import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrPartiStore } from './models/OrPartiStore';
import { OrPartiStoreService } from './services/orPartiStore.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orPartiStore',
	templateUrl: './orPartiStore.component.html',
	styleUrls: ['./orPartiStore.component.scss']
})
export class OrPartiStoreComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orPartiStoreId','photo','detay', 'update','delete','file'];

	orPartiStoreList:OrPartiStore[];
	orPartiStore:OrPartiStore=new OrPartiStore();

	orPartiStoreAddForm: FormGroup;

	photoForm: FormGroup;

	orPartiStoreId:number;

	constructor(private orPartiStoreService:OrPartiStoreService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrPartiStoreList();
    }

	ngOnInit() {

		this.createOrPartiStoreAddForm();
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
		formData.append('orPartiStoreId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orPartiStoreService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrPartiStoreList();
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


	getOrPartiStoreList() {
		this.orPartiStoreService.getOrPartiStoreList().subscribe(data => {
			this.orPartiStoreList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orPartiStoreAddForm.valid) {
			this.orPartiStore = Object.assign({}, this.orPartiStoreAddForm.value)

			if (this.orPartiStore.orPartiStoreId == 0)
				this.addOrPartiStore();
			else
				this.updateOrPartiStore();
		}

	}

	addOrPartiStore(){

		this.orPartiStoreService.addOrPartiStore(this.orPartiStore).subscribe(data => {
			this.getOrPartiStoreList();
			this.orPartiStore = new OrPartiStore();
			jQuery('#orpartistore').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orPartiStoreAddForm);

		})

	}

	updateOrPartiStore(){

		this.orPartiStoreService.updateOrPartiStore(this.orPartiStore).subscribe(data => {

			var index=this.orPartiStoreList.findIndex(x=>x.orPartiStoreId==this.orPartiStore.orPartiStoreId);
			this.orPartiStoreList[index]=this.orPartiStore;
			this.dataSource = new MatTableDataSource(this.orPartiStoreList);
            this.configDataTable();
			this.orPartiStore = new OrPartiStore();
			jQuery('#orpartistore').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orPartiStoreAddForm);

		})

	}

	createOrPartiStoreAddForm() {
		this.orPartiStoreAddForm = this.formBuilder.group({		
			orPartiStoreId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrPartiStore(orPartiStoreId:number){
		this.orPartiStoreService.deleteOrPartiStore(orPartiStoreId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orPartiStoreList=this.orPartiStoreList.filter(x=> x.orPartiStoreId!=orPartiStoreId);
			this.dataSource = new MatTableDataSource(this.orPartiStoreList);
			this.configDataTable();
		})
	}

	getOrPartiStoreById(orPartiStoreId:number){
		this.clearFormGroup(this.orPartiStoreAddForm);
		this.orPartiStoreService.getOrPartiStoreById(orPartiStoreId).subscribe(data=>{
			this.orPartiStore=data;
			this.orPartiStoreAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orPartiStoreId')
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
