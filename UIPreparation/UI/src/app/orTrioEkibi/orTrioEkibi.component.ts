import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OrTrioEkibi } from './models/OrTrioEkibi';
import { OrTrioEkibiService } from './services/orTrioEkibi.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-orTrioEkibi',
	templateUrl: './orTrioEkibi.component.html',
	styleUrls: ['./orTrioEkibi.component.scss']
})
export class OrTrioEkibiComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['orTrioEkibiId','photo','detay', 'update','delete','file'];

	orTrioEkibiList:OrTrioEkibi[];
	orTrioEkibi:OrTrioEkibi=new OrTrioEkibi();

	orTrioEkibiAddForm: FormGroup;

	photoForm: FormGroup;


	orTrioEkibiId:number;

	constructor(private orTrioEkibiService:OrTrioEkibiService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrTrioEkibiList();
    }

	ngOnInit() {

		this.createOrTrioEkibiAddForm();
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
		formData.append('orTrioEkibiId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.orTrioEkibiService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getOrTrioEkibiList();
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


	getOrTrioEkibiList() {
		this.orTrioEkibiService.getOrTrioEkibiList().subscribe(data => {
			this.orTrioEkibiList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orTrioEkibiAddForm.valid) {
			this.orTrioEkibi = Object.assign({}, this.orTrioEkibiAddForm.value)

			if (this.orTrioEkibi.orTrioEkibiId == 0)
				this.addOrTrioEkibi();
			else
				this.updateOrTrioEkibi();
		}

	}

	addOrTrioEkibi(){

		this.orTrioEkibiService.addOrTrioEkibi(this.orTrioEkibi).subscribe(data => {
			this.getOrTrioEkibiList();
			this.orTrioEkibi = new OrTrioEkibi();
			jQuery('#ortrioekibi').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orTrioEkibiAddForm);

		})

	}

	updateOrTrioEkibi(){

		this.orTrioEkibiService.updateOrTrioEkibi(this.orTrioEkibi).subscribe(data => {

			var index=this.orTrioEkibiList.findIndex(x=>x.orTrioEkibiId==this.orTrioEkibi.orTrioEkibiId);
			this.orTrioEkibiList[index]=this.orTrioEkibi;
			this.dataSource = new MatTableDataSource(this.orTrioEkibiList);
            this.configDataTable();
			this.orTrioEkibi = new OrTrioEkibi();
			jQuery('#ortrioekibi').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orTrioEkibiAddForm);

		})

	}

	createOrTrioEkibiAddForm() {
		this.orTrioEkibiAddForm = this.formBuilder.group({		
			orTrioEkibiId : [0],
photo : ["", Validators.required],
detay : ["", Validators.required]
		})
	}

	deleteOrTrioEkibi(orTrioEkibiId:number){
		this.orTrioEkibiService.deleteOrTrioEkibi(orTrioEkibiId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orTrioEkibiList=this.orTrioEkibiList.filter(x=> x.orTrioEkibiId!=orTrioEkibiId);
			this.dataSource = new MatTableDataSource(this.orTrioEkibiList);
			this.configDataTable();
		})
	}

	getOrTrioEkibiById(orTrioEkibiId:number){
		this.clearFormGroup(this.orTrioEkibiAddForm);
		this.orTrioEkibiService.getOrTrioEkibiById(orTrioEkibiId).subscribe(data=>{
			this.orTrioEkibi=data;
			this.orTrioEkibiAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'orTrioEkibiId')
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
