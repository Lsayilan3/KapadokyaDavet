import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrSunnet } from './models/OrSunnet';
import { OrSunnetService } from './services/orSunnet.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orSunnet',
	templateUrl: './orSunnet.component.html',
	styleUrls: ['./orSunnet.component.scss']
})
export class OrSunnetComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orSunnetId','photo','detay', 'update','delete','file'];

	orSunnetList:OrSunnet[];
	orSunnet:OrSunnet=new OrSunnet();

	orSunnetAddForm: FormGroup;

	photoForm: FormGroup;

	orSunnetId:number;

	constructor(private orSunnetService:OrSunnetService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrSunnetList();
    }

	ngOnInit() {

		this.createOrSunnetAddForm();
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
		formData.append('orSunnetId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orSunnetService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrSunnetList();
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

	getOrSunnetList() {
		this.orSunnetService.getOrSunnetList().subscribe(data => {
			this.orSunnetList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orSunnetAddForm.valid) {
			this.orSunnet = Object.assign({}, this.orSunnetAddForm.value)

			if (this.orSunnet.orSunnetId == 0)
				this.addOrSunnet();
			else
				this.updateOrSunnet();
		}

	}

	addOrSunnet(){

		this.orSunnetService.addOrSunnet(this.orSunnet).subscribe(data => {
			this.getOrSunnetList();
			this.orSunnet = new OrSunnet();
			jQuery('#orsunnet').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orSunnetAddForm);

		})

	}

	updateOrSunnet(){

		this.orSunnetService.updateOrSunnet(this.orSunnet).subscribe(data => {

			var index=this.orSunnetList.findIndex(x=>x.orSunnetId==this.orSunnet.orSunnetId);
			this.orSunnetList[index]=this.orSunnet;
			this.dataSource = new MatTableDataSource(this.orSunnetList);
            this.configDataTable();
			this.orSunnet = new OrSunnet();
			jQuery('#orsunnet').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orSunnetAddForm);

		})

	}

	createOrSunnetAddForm() {
		this.orSunnetAddForm = this.formBuilder.group({		
			orSunnetId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrSunnet(orSunnetId:number){
		this.orSunnetService.deleteOrSunnet(orSunnetId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orSunnetList=this.orSunnetList.filter(x=> x.orSunnetId!=orSunnetId);
			this.dataSource = new MatTableDataSource(this.orSunnetList);
			this.configDataTable();
		})
	}

	getOrSunnetById(orSunnetId:number){
		this.clearFormGroup(this.orSunnetAddForm);
		this.orSunnetService.getOrSunnetById(orSunnetId).subscribe(data=>{
			this.orSunnet=data;
			this.orSunnetAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orSunnetId')
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
