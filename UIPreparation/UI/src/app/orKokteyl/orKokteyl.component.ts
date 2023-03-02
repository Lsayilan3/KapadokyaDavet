import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrKokteyl } from './models/OrKokteyl';
import { OrKokteylService } from './services/orKokteyl.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orKokteyl',
	templateUrl: './orKokteyl.component.html',
	styleUrls: ['./orKokteyl.component.scss']
})
export class OrKokteylComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orKokteylId','photo','detay', 'update','delete','file'];

	orKokteylList:OrKokteyl[];
	orKokteyl:OrKokteyl=new OrKokteyl();

	orKokteylAddForm: FormGroup;

	photoForm: FormGroup;

	orKokteylId:number;

	constructor(private orKokteylService:OrKokteylService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrKokteylList();
    }

	ngOnInit() {

		this.createOrKokteylAddForm();
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
		formData.append('orKokteylId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orKokteylService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrKokteylList();
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

	getOrKokteylList() {
		this.orKokteylService.getOrKokteylList().subscribe(data => {
			this.orKokteylList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orKokteylAddForm.valid) {
			this.orKokteyl = Object.assign({}, this.orKokteylAddForm.value)

			if (this.orKokteyl.orKokteylId == 0)
				this.addOrKokteyl();
			else
				this.updateOrKokteyl();
		}

	}

	addOrKokteyl(){

		this.orKokteylService.addOrKokteyl(this.orKokteyl).subscribe(data => {
			this.getOrKokteylList();
			this.orKokteyl = new OrKokteyl();
			jQuery('#orkokteyl').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orKokteylAddForm);

		})

	}

	updateOrKokteyl(){

		this.orKokteylService.updateOrKokteyl(this.orKokteyl).subscribe(data => {

			var index=this.orKokteylList.findIndex(x=>x.orKokteylId==this.orKokteyl.orKokteylId);
			this.orKokteylList[index]=this.orKokteyl;
			this.dataSource = new MatTableDataSource(this.orKokteylList);
            this.configDataTable();
			this.orKokteyl = new OrKokteyl();
			jQuery('#orkokteyl').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orKokteylAddForm);

		})

	}

	createOrKokteylAddForm() {
		this.orKokteylAddForm = this.formBuilder.group({		
			orKokteylId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrKokteyl(orKokteylId:number){
		this.orKokteylService.deleteOrKokteyl(orKokteylId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orKokteylList=this.orKokteylList.filter(x=> x.orKokteylId!=orKokteylId);
			this.dataSource = new MatTableDataSource(this.orKokteylList);
			this.configDataTable();
		})
	}

	getOrKokteylById(orKokteylId:number){
		this.clearFormGroup(this.orKokteylAddForm);
		this.orKokteylService.getOrKokteylById(orKokteylId).subscribe(data=>{
			this.orKokteyl=data;
			this.orKokteylAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orKokteylId')
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
