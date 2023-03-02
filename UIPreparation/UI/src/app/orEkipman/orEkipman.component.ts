import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrEkipman } from './models/OrEkipman';
import { OrEkipmanService } from './services/orEkipman.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orEkipman',
	templateUrl: './orEkipman.component.html',
	styleUrls: ['./orEkipman.component.scss']
})
export class OrEkipmanComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orEkipmanId','photo','detay', 'update','delete','file'];

	orEkipmanList:OrEkipman[];
	orEkipman:OrEkipman=new OrEkipman();

	orEkipmanAddForm: FormGroup;

	photoForm: FormGroup;

	orEkipmanId:number;

	constructor(private orEkipmanService:OrEkipmanService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrEkipmanList();
    }

	ngOnInit() {

		this.createOrEkipmanAddForm();
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
		formData.append('orEkipmanId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orEkipmanService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrEkipmanList();
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


	getOrEkipmanList() {
		this.orEkipmanService.getOrEkipmanList().subscribe(data => {
			this.orEkipmanList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orEkipmanAddForm.valid) {
			this.orEkipman = Object.assign({}, this.orEkipmanAddForm.value)

			if (this.orEkipman.orEkipmanId == 0)
				this.addOrEkipman();
			else
				this.updateOrEkipman();
		}

	}

	addOrEkipman(){

		this.orEkipmanService.addOrEkipman(this.orEkipman).subscribe(data => {
			this.getOrEkipmanList();
			this.orEkipman = new OrEkipman();
			jQuery('#orekipman').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orEkipmanAddForm);

		})

	}

	updateOrEkipman(){

		this.orEkipmanService.updateOrEkipman(this.orEkipman).subscribe(data => {

			var index=this.orEkipmanList.findIndex(x=>x.orEkipmanId==this.orEkipman.orEkipmanId);
			this.orEkipmanList[index]=this.orEkipman;
			this.dataSource = new MatTableDataSource(this.orEkipmanList);
            this.configDataTable();
			this.orEkipman = new OrEkipman();
			jQuery('#orekipman').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orEkipmanAddForm);

		})

	}

	createOrEkipmanAddForm() {
		this.orEkipmanAddForm = this.formBuilder.group({		
			orEkipmanId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrEkipman(orEkipmanId:number){
		this.orEkipmanService.deleteOrEkipman(orEkipmanId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orEkipmanList=this.orEkipmanList.filter(x=> x.orEkipmanId!=orEkipmanId);
			this.dataSource = new MatTableDataSource(this.orEkipmanList);
			this.configDataTable();
		})
	}

	getOrEkipmanById(orEkipmanId:number){
		this.clearFormGroup(this.orEkipmanAddForm);
		this.orEkipmanService.getOrEkipmanById(orEkipmanId).subscribe(data=>{
			this.orEkipman=data;
			this.orEkipmanAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orEkipmanId')
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
