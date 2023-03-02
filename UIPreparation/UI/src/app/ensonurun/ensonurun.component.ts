import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Ensonurun } from './models/ensonurun';
import { EnsonurunService } from './services/ensonurun.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-ensonurun',
	templateUrl: './ensonurun.component.html',
	styleUrls: ['./ensonurun.component.scss']
})
export class EnsonurunComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['ensonurunId','photo','title','url', 'update','delete','file'];

	ensonurunList:Ensonurun[];
	ensonurun:Ensonurun=new Ensonurun();

	ensonurunAddForm: FormGroup;

	photoForm: FormGroup;

	ensonurunId:number;

	constructor(private ensonurunService:EnsonurunService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getEnsonurunList();
    }

	ngOnInit() {

		this.createEnsonurunAddForm();
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
		formData.append('ensonurunId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

			this.ensonurunService.addFile(formData).subscribe(data=>{
				jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getEnsonurunList();
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

	getEnsonurunList() {
		this.ensonurunService.getEnsonurunList().subscribe(data => {
			this.ensonurunList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.ensonurunAddForm.valid) {
			this.ensonurun = Object.assign({}, this.ensonurunAddForm.value)

			if (this.ensonurun.ensonurunId == 0)
				this.addEnsonurun();
			else
				this.updateEnsonurun();
		}

	}

	addEnsonurun(){

		this.ensonurunService.addEnsonurun(this.ensonurun).subscribe(data => {
			this.getEnsonurunList();
			this.ensonurun = new Ensonurun();
			jQuery('#ensonurun').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.ensonurunAddForm);

		})

	}

	updateEnsonurun(){

		this.ensonurunService.updateEnsonurun(this.ensonurun).subscribe(data => {

			var index=this.ensonurunList.findIndex(x=>x.ensonurunId==this.ensonurun.ensonurunId);
			this.ensonurunList[index]=this.ensonurun;
			this.dataSource = new MatTableDataSource(this.ensonurunList);
            this.configDataTable();
			this.ensonurun = new Ensonurun();
			jQuery('#ensonurun').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.ensonurunAddForm);

		})

	}

	createEnsonurunAddForm() {
		this.ensonurunAddForm = this.formBuilder.group({		
			ensonurunId : [0],
photo : ["", Validators.required],
title : ["", Validators.required],
url : ["", Validators.required]
		})
	}

	deleteEnsonurun(ensonurunId:number){
		this.ensonurunService.deleteEnsonurun(ensonurunId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.ensonurunList=this.ensonurunList.filter(x=> x.ensonurunId!=ensonurunId);
			this.dataSource = new MatTableDataSource(this.ensonurunList);
			this.configDataTable();
		})
	}

	getEnsonurunById(ensonurunId:number){
		this.clearFormGroup(this.ensonurunAddForm);
		this.ensonurunService.getEnsonurunById(ensonurunId).subscribe(data=>{
			this.ensonurun=data;
			this.ensonurunAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'ensonurunId')
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
