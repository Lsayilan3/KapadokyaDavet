import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrPiknik } from './models/OrPiknik';
import { OrPiknikService } from './services/orPiknik.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orPiknik',
	templateUrl: './orPiknik.component.html',
	styleUrls: ['./orPiknik.component.scss']
})
export class OrPiknikComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orPiknikId','photo','detay', 'update','delete','file'];

	orPiknikList:OrPiknik[];
	orPiknik:OrPiknik=new OrPiknik();

	orPiknikAddForm: FormGroup;

	photoForm: FormGroup;

	orPiknikId:number;

	constructor(private orPiknikService:OrPiknikService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrPiknikList();
    }

	ngOnInit() {

		this.createOrPiknikAddForm();
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
		formData.append('orPiknikId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orPiknikService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrPiknikList();
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


	getOrPiknikList() {
		this.orPiknikService.getOrPiknikList().subscribe(data => {
			this.orPiknikList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orPiknikAddForm.valid) {
			this.orPiknik = Object.assign({}, this.orPiknikAddForm.value)

			if (this.orPiknik.orPiknikId == 0)
				this.addOrPiknik();
			else
				this.updateOrPiknik();
		}

	}

	addOrPiknik(){

		this.orPiknikService.addOrPiknik(this.orPiknik).subscribe(data => {
			this.getOrPiknikList();
			this.orPiknik = new OrPiknik();
			jQuery('#orpiknik').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orPiknikAddForm);

		})

	}

	updateOrPiknik(){

		this.orPiknikService.updateOrPiknik(this.orPiknik).subscribe(data => {

			var index=this.orPiknikList.findIndex(x=>x.orPiknikId==this.orPiknik.orPiknikId);
			this.orPiknikList[index]=this.orPiknik;
			this.dataSource = new MatTableDataSource(this.orPiknikList);
            this.configDataTable();
			this.orPiknik = new OrPiknik();
			jQuery('#orpiknik').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orPiknikAddForm);

		})

	}

	createOrPiknikAddForm() {
		this.orPiknikAddForm = this.formBuilder.group({		
			orPiknikId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrPiknik(orPiknikId:number){
		this.orPiknikService.deleteOrPiknik(orPiknikId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orPiknikList=this.orPiknikList.filter(x=> x.orPiknikId!=orPiknikId);
			this.dataSource = new MatTableDataSource(this.orPiknikList);
			this.configDataTable();
		})
	}

	getOrPiknikById(orPiknikId:number){
		this.clearFormGroup(this.orPiknikAddForm);
		this.orPiknikService.getOrPiknikById(orPiknikId).subscribe(data=>{
			this.orPiknik=data;
			this.orPiknikAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orPiknikId')
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
