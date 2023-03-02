import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { GalleryTwo } from './models/GalleryTwo';
import { GalleryTwoService } from './services/galleryTwo.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-galleryTwo',
	templateUrl: './galleryTwo.component.html',
	styleUrls: ['./galleryTwo.component.scss']
})
export class GalleryTwoComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['galleryTwoId','photo', 'update','delete','file'];

	galleryTwoList:GalleryTwo[];
	galleryTwo:GalleryTwo=new GalleryTwo();

	galleryTwoAddForm: FormGroup;

	photoForm: FormGroup;

	galleryTwoId:number;

	constructor(private galleryTwoService:GalleryTwoService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getGalleryTwoList();
    }

	ngOnInit() {

		this.createGalleryTwoAddForm();
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
		formData.append('galleryTwoId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.galleryTwoService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getGalleryTwoList();
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

	getGalleryTwoList() {
		this.galleryTwoService.getGalleryTwoList().subscribe(data => {
			this.galleryTwoList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.galleryTwoAddForm.valid) {
			this.galleryTwo = Object.assign({}, this.galleryTwoAddForm.value)

			if (this.galleryTwo.galleryTwoId == 0)
				this.addGalleryTwo();
			else
				this.updateGalleryTwo();
		}

	}

	addGalleryTwo(){

		this.galleryTwoService.addGalleryTwo(this.galleryTwo).subscribe(data => {
			this.getGalleryTwoList();
			this.galleryTwo = new GalleryTwo();
			jQuery('#gallerytwo').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.galleryTwoAddForm);

		})

	}

	updateGalleryTwo(){

		this.galleryTwoService.updateGalleryTwo(this.galleryTwo).subscribe(data => {

			var index=this.galleryTwoList.findIndex(x=>x.galleryTwoId==this.galleryTwo.galleryTwoId);
			this.galleryTwoList[index]=this.galleryTwo;
			this.dataSource = new MatTableDataSource(this.galleryTwoList);
            this.configDataTable();
			this.galleryTwo = new GalleryTwo();
			jQuery('#gallerytwo').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.galleryTwoAddForm);

		})

	}

	createGalleryTwoAddForm() {
		this.galleryTwoAddForm = this.formBuilder.group({		
			galleryTwoId : [0],
photo : ["", Validators.required]
		})
	}

	deleteGalleryTwo(galleryTwoId:number){
		this.galleryTwoService.deleteGalleryTwo(galleryTwoId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.galleryTwoList=this.galleryTwoList.filter(x=> x.galleryTwoId!=galleryTwoId);
			this.dataSource = new MatTableDataSource(this.galleryTwoList);
			this.configDataTable();
		})
	}

	getGalleryTwoById(galleryTwoId:number){
		this.clearFormGroup(this.galleryTwoAddForm);
		this.galleryTwoService.getGalleryTwoById(galleryTwoId).subscribe(data=>{
			this.galleryTwo=data;
			this.galleryTwoAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'galleryTwoId')
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
