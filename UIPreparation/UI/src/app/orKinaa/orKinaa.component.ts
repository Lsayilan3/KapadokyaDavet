import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrKinaa } from './models/OrKinaa';
import { OrKinaaService } from './services/orKinaa.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orKinaa',
	templateUrl: './orKinaa.component.html',
	styleUrls: ['./orKinaa.component.scss']
})
export class OrKinaaComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orKinaaId','photo','detay', 'update','delete','file'];

	orKinaaList:OrKinaa[];
	orKinaa:OrKinaa=new OrKinaa();

	orKinaaAddForm: FormGroup;

	photoForm: FormGroup;

	orKinaaId:number;

	constructor(private orKinaaService:OrKinaaService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrKinaaList();
    }

	ngOnInit() {

		this.createOrKinaaAddForm();
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
		formData.append('orKinaaId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orKinaaService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrKinaaList();
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


	getOrKinaaList() {
		this.orKinaaService.getOrKinaaList().subscribe(data => {
			this.orKinaaList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orKinaaAddForm.valid) {
			this.orKinaa = Object.assign({}, this.orKinaaAddForm.value)

			if (this.orKinaa.orKinaaId == 0)
				this.addOrKinaa();
			else
				this.updateOrKinaa();
		}

	}

	addOrKinaa(){

		this.orKinaaService.addOrKinaa(this.orKinaa).subscribe(data => {
			this.getOrKinaaList();
			this.orKinaa = new OrKinaa();
			jQuery('#orkinaa').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orKinaaAddForm);

		})

	}

	updateOrKinaa(){

		this.orKinaaService.updateOrKinaa(this.orKinaa).subscribe(data => {

			var index=this.orKinaaList.findIndex(x=>x.orKinaaId==this.orKinaa.orKinaaId);
			this.orKinaaList[index]=this.orKinaa;
			this.dataSource = new MatTableDataSource(this.orKinaaList);
            this.configDataTable();
			this.orKinaa = new OrKinaa();
			jQuery('#orkinaa').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orKinaaAddForm);

		})

	}

	createOrKinaaAddForm() {
		this.orKinaaAddForm = this.formBuilder.group({		
			orKinaaId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrKinaa(orKinaaId:number){
		this.orKinaaService.deleteOrKinaa(orKinaaId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orKinaaList=this.orKinaaList.filter(x=> x.orKinaaId!=orKinaaId);
			this.dataSource = new MatTableDataSource(this.orKinaaList);
			this.configDataTable();
		})
	}

	getOrKinaaById(orKinaaId:number){
		this.clearFormGroup(this.orKinaaAddForm);
		this.orKinaaService.getOrKinaaById(orKinaaId).subscribe(data=>{
			this.orKinaa=data;
			this.orKinaaAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orKinaaId')
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
