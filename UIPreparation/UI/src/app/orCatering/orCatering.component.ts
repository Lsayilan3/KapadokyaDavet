import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrCatering } from './models/OrCatering';
import { OrCateringService } from './services/orCatering.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orCatering',
	templateUrl: './orCatering.component.html',
	styleUrls: ['./orCatering.component.scss']
})
export class OrCateringComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orCateringId','photo','detay', 'update','delete','file'];

	orCateringList:OrCatering[];
	orCatering:OrCatering=new OrCatering();

	orCateringAddForm: FormGroup;

	photoForm: FormGroup;

	orCateringId:number;

	constructor(private orCateringService:OrCateringService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrCateringList();
    }

	ngOnInit() {

		this.createOrCateringAddForm();
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
		formData.append('orCateringId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orCateringService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrCateringList();
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


	getOrCateringList() {
		this.orCateringService.getOrCateringList().subscribe(data => {
			this.orCateringList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orCateringAddForm.valid) {
			this.orCatering = Object.assign({}, this.orCateringAddForm.value)

			if (this.orCatering.orCateringId == 0)
				this.addOrCatering();
			else
				this.updateOrCatering();
		}

	}

	addOrCatering(){

		this.orCateringService.addOrCatering(this.orCatering).subscribe(data => {
			this.getOrCateringList();
			this.orCatering = new OrCatering();
			jQuery('#orcatering').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orCateringAddForm);

		})

	}

	updateOrCatering(){

		this.orCateringService.updateOrCatering(this.orCatering).subscribe(data => {

			var index=this.orCateringList.findIndex(x=>x.orCateringId==this.orCatering.orCateringId);
			this.orCateringList[index]=this.orCatering;
			this.dataSource = new MatTableDataSource(this.orCateringList);
            this.configDataTable();
			this.orCatering = new OrCatering();
			jQuery('#orcatering').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orCateringAddForm);

		})

	}

	createOrCateringAddForm() {
		this.orCateringAddForm = this.formBuilder.group({		
			orCateringId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrCatering(orCateringId:number){
		this.orCateringService.deleteOrCatering(orCateringId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orCateringList=this.orCateringList.filter(x=> x.orCateringId!=orCateringId);
			this.dataSource = new MatTableDataSource(this.orCateringList);
			this.configDataTable();
		})
	}

	getOrCateringById(orCateringId:number){
		this.clearFormGroup(this.orCateringAddForm);
		this.orCateringService.getOrCateringById(orCateringId).subscribe(data=>{
			this.orCatering=data;
			this.orCateringAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orCateringId')
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
