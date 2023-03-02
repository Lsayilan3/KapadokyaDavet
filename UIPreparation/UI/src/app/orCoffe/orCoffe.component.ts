import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrCoffe } from './models/OrCoffe';
import { OrCoffeService } from './services/orCoffe.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orCoffe',
	templateUrl: './orCoffe.component.html',
	styleUrls: ['./orCoffe.component.scss']
})
export class OrCoffeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orCoffeId','photo','detay', 'update','delete','file'];

	orCoffeList:OrCoffe[];
	orCoffe:OrCoffe=new OrCoffe();

	orCoffeAddForm: FormGroup;

	photoForm: FormGroup;

	orCoffeId:number;

	constructor(private orCoffeService:OrCoffeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrCoffeList();
    }

	ngOnInit() {

		this.createOrCoffeAddForm();
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
		formData.append('orCoffeId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orCoffeService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrCoffeList();
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


	getOrCoffeList() {
		this.orCoffeService.getOrCoffeList().subscribe(data => {
			this.orCoffeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orCoffeAddForm.valid) {
			this.orCoffe = Object.assign({}, this.orCoffeAddForm.value)

			if (this.orCoffe.orCoffeId == 0)
				this.addOrCoffe();
			else
				this.updateOrCoffe();
		}

	}

	addOrCoffe(){

		this.orCoffeService.addOrCoffe(this.orCoffe).subscribe(data => {
			this.getOrCoffeList();
			this.orCoffe = new OrCoffe();
			jQuery('#orcoffe').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orCoffeAddForm);

		})

	}

	updateOrCoffe(){

		this.orCoffeService.updateOrCoffe(this.orCoffe).subscribe(data => {

			var index=this.orCoffeList.findIndex(x=>x.orCoffeId==this.orCoffe.orCoffeId);
			this.orCoffeList[index]=this.orCoffe;
			this.dataSource = new MatTableDataSource(this.orCoffeList);
            this.configDataTable();
			this.orCoffe = new OrCoffe();
			jQuery('#orcoffe').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orCoffeAddForm);

		})

	}

	createOrCoffeAddForm() {
		this.orCoffeAddForm = this.formBuilder.group({		
			orCoffeId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrCoffe(orCoffeId:number){
		this.orCoffeService.deleteOrCoffe(orCoffeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orCoffeList=this.orCoffeList.filter(x=> x.orCoffeId!=orCoffeId);
			this.dataSource = new MatTableDataSource(this.orCoffeList);
			this.configDataTable();
		})
	}

	getOrCoffeById(orCoffeId:number){
		this.clearFormGroup(this.orCoffeAddForm);
		this.orCoffeService.getOrCoffeById(orCoffeId).subscribe(data=>{
			this.orCoffe=data;
			this.orCoffeAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orCoffeId')
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
