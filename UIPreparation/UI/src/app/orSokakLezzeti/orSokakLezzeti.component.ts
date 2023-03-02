import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrSokakLezzeti } from './models/OrSokakLezzeti';
import { OrSokakLezzetiService } from './services/orSokakLezzeti.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orSokakLezzeti',
	templateUrl: './orSokakLezzeti.component.html',
	styleUrls: ['./orSokakLezzeti.component.scss']
})
export class OrSokakLezzetiComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orSokakLezzetiId','photo','detay', 'update','delete','file'];

	orSokakLezzetiList:OrSokakLezzeti[];
	orSokakLezzeti:OrSokakLezzeti=new OrSokakLezzeti();

	orSokakLezzetiAddForm: FormGroup;

	photoForm: FormGroup;

	orSokakLezzetiId:number;

	constructor(private orSokakLezzetiService:OrSokakLezzetiService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrSokakLezzetiList();
    }

	ngOnInit() {

		this.createOrSokakLezzetiAddForm();
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
		formData.append('orSokakLezzetiId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orSokakLezzetiService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrSokakLezzetiList();
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


	getOrSokakLezzetiList() {
		this.orSokakLezzetiService.getOrSokakLezzetiList().subscribe(data => {
			this.orSokakLezzetiList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orSokakLezzetiAddForm.valid) {
			this.orSokakLezzeti = Object.assign({}, this.orSokakLezzetiAddForm.value)

			if (this.orSokakLezzeti.orSokakLezzetiId == 0)
				this.addOrSokakLezzeti();
			else
				this.updateOrSokakLezzeti();
		}

	}

	addOrSokakLezzeti(){

		this.orSokakLezzetiService.addOrSokakLezzeti(this.orSokakLezzeti).subscribe(data => {
			this.getOrSokakLezzetiList();
			this.orSokakLezzeti = new OrSokakLezzeti();
			jQuery('#orsokaklezzeti').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orSokakLezzetiAddForm);

		})

	}

	updateOrSokakLezzeti(){

		this.orSokakLezzetiService.updateOrSokakLezzeti(this.orSokakLezzeti).subscribe(data => {

			var index=this.orSokakLezzetiList.findIndex(x=>x.orSokakLezzetiId==this.orSokakLezzeti.orSokakLezzetiId);
			this.orSokakLezzetiList[index]=this.orSokakLezzeti;
			this.dataSource = new MatTableDataSource(this.orSokakLezzetiList);
            this.configDataTable();
			this.orSokakLezzeti = new OrSokakLezzeti();
			jQuery('#orsokaklezzeti').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orSokakLezzetiAddForm);

		})

	}

	createOrSokakLezzetiAddForm() {
		this.orSokakLezzetiAddForm = this.formBuilder.group({		
			orSokakLezzetiId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrSokakLezzeti(orSokakLezzetiId:number){
		this.orSokakLezzetiService.deleteOrSokakLezzeti(orSokakLezzetiId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orSokakLezzetiList=this.orSokakLezzetiList.filter(x=> x.orSokakLezzetiId!=orSokakLezzetiId);
			this.dataSource = new MatTableDataSource(this.orSokakLezzetiList);
			this.configDataTable();
		})
	}

	getOrSokakLezzetiById(orSokakLezzetiId:number){
		this.clearFormGroup(this.orSokakLezzetiAddForm);
		this.orSokakLezzetiService.getOrSokakLezzetiById(orSokakLezzetiId).subscribe(data=>{
			this.orSokakLezzeti=data;
			this.orSokakLezzetiAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orSokakLezzetiId')
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
