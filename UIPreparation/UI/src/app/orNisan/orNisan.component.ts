import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrNisan } from './models/OrNisan';
import { OrNisanService } from './services/orNisan.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orNisan',
	templateUrl: './orNisan.component.html',
	styleUrls: ['./orNisan.component.scss']
})
export class OrNisanComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orNisanId','photo','detay', 'update','delete','file'];

	orNisanList:OrNisan[];
	orNisan:OrNisan=new OrNisan();

	orNisanAddForm: FormGroup;

	photoForm: FormGroup;

	orNisanId:number;

	constructor(private orNisanService:OrNisanService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrNisanList();
    }

	ngOnInit() {

		this.createOrNisanAddForm();
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
		formData.append('orNisanId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orNisanService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrNisanList();
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


	getOrNisanList() {
		this.orNisanService.getOrNisanList().subscribe(data => {
			this.orNisanList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orNisanAddForm.valid) {
			this.orNisan = Object.assign({}, this.orNisanAddForm.value)

			if (this.orNisan.orNisanId == 0)
				this.addOrNisan();
			else
				this.updateOrNisan();
		}

	}

	addOrNisan(){

		this.orNisanService.addOrNisan(this.orNisan).subscribe(data => {
			this.getOrNisanList();
			this.orNisan = new OrNisan();
			jQuery('#ornisan').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orNisanAddForm);

		})

	}

	updateOrNisan(){

		this.orNisanService.updateOrNisan(this.orNisan).subscribe(data => {

			var index=this.orNisanList.findIndex(x=>x.orNisanId==this.orNisan.orNisanId);
			this.orNisanList[index]=this.orNisan;
			this.dataSource = new MatTableDataSource(this.orNisanList);
            this.configDataTable();
			this.orNisan = new OrNisan();
			jQuery('#ornisan').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orNisanAddForm);

		})

	}

	createOrNisanAddForm() {
		this.orNisanAddForm = this.formBuilder.group({		
			orNisanId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrNisan(orNisanId:number){
		this.orNisanService.deleteOrNisan(orNisanId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orNisanList=this.orNisanList.filter(x=> x.orNisanId!=orNisanId);
			this.dataSource = new MatTableDataSource(this.orNisanList);
			this.configDataTable();
		})
	}

	getOrNisanById(orNisanId:number){
		this.clearFormGroup(this.orNisanAddForm);
		this.orNisanService.getOrNisanById(orNisanId).subscribe(data=>{
			this.orNisan=data;
			this.orNisanAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orNisanId')
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
