import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrPersonelTemini } from './models/OrPersonelTemini';
import { OrPersonelTeminiService } from './services/orPersonelTemini.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orPersonelTemini',
	templateUrl: './orPersonelTemini.component.html',
	styleUrls: ['./orPersonelTemini.component.scss']
})
export class OrPersonelTeminiComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orPersonelTeminiId','photo','detay', 'update','delete','file'];

	orPersonelTeminiList:OrPersonelTemini[];
	orPersonelTemini:OrPersonelTemini=new OrPersonelTemini();

	orPersonelTeminiAddForm: FormGroup;

	photoForm: FormGroup;

	orPersonelTeminiId:number;

	constructor(private orPersonelTeminiService:OrPersonelTeminiService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrPersonelTeminiList();
    }

	ngOnInit() {

		this.createOrPersonelTeminiAddForm();
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
		formData.append('orPersonelTeminiId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orPersonelTeminiService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrPersonelTeminiList();
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

	getOrPersonelTeminiList() {
		this.orPersonelTeminiService.getOrPersonelTeminiList().subscribe(data => {
			this.orPersonelTeminiList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orPersonelTeminiAddForm.valid) {
			this.orPersonelTemini = Object.assign({}, this.orPersonelTeminiAddForm.value)

			if (this.orPersonelTemini.orPersonelTeminiId == 0)
				this.addOrPersonelTemini();
			else
				this.updateOrPersonelTemini();
		}

	}

	addOrPersonelTemini(){

		this.orPersonelTeminiService.addOrPersonelTemini(this.orPersonelTemini).subscribe(data => {
			this.getOrPersonelTeminiList();
			this.orPersonelTemini = new OrPersonelTemini();
			jQuery('#orpersoneltemini').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orPersonelTeminiAddForm);

		})

	}

	updateOrPersonelTemini(){

		this.orPersonelTeminiService.updateOrPersonelTemini(this.orPersonelTemini).subscribe(data => {

			var index=this.orPersonelTeminiList.findIndex(x=>x.orPersonelTeminiId==this.orPersonelTemini.orPersonelTeminiId);
			this.orPersonelTeminiList[index]=this.orPersonelTemini;
			this.dataSource = new MatTableDataSource(this.orPersonelTeminiList);
            this.configDataTable();
			this.orPersonelTemini = new OrPersonelTemini();
			jQuery('#orpersoneltemini').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orPersonelTeminiAddForm);

		})

	}

	createOrPersonelTeminiAddForm() {
		this.orPersonelTeminiAddForm = this.formBuilder.group({		
			orPersonelTeminiId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrPersonelTemini(orPersonelTeminiId:number){
		this.orPersonelTeminiService.deleteOrPersonelTemini(orPersonelTeminiId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orPersonelTeminiList=this.orPersonelTeminiList.filter(x=> x.orPersonelTeminiId!=orPersonelTeminiId);
			this.dataSource = new MatTableDataSource(this.orPersonelTeminiList);
			this.configDataTable();
		})
	}

	getOrPersonelTeminiById(orPersonelTeminiId:number){
		this.clearFormGroup(this.orPersonelTeminiAddForm);
		this.orPersonelTeminiService.getOrPersonelTeminiById(orPersonelTeminiId).subscribe(data=>{
			this.orPersonelTemini=data;
			this.orPersonelTeminiAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orPersonelTeminiId')
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
