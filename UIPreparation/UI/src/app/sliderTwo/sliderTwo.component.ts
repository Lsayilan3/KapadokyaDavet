import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { SliderTwo } from './models/SliderTwo';
import { SliderTwoService } from './services/sliderTwo.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-sliderTwo',
	templateUrl: './sliderTwo.component.html',
	styleUrls: ['./sliderTwo.component.scss']
})
export class SliderTwoComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['sliderTwoId','title','detay','price','discountPrice','photo', 'update','delete','file'];

	sliderTwoList:SliderTwo[];
	sliderTwo:SliderTwo=new SliderTwo();

	sliderTwoAddForm: FormGroup;

	photoForm: FormGroup;

	sliderTwoId:number;

	constructor(private sliderTwoService:SliderTwoService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getSliderTwoList();
    }

	ngOnInit() {

		this.createSliderTwoAddForm();
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
		formData.append('sliderTwoId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.sliderTwoService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getSliderTwoList();
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


	getSliderTwoList() {
		this.sliderTwoService.getSliderTwoList().subscribe(data => {
			this.sliderTwoList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sliderTwoAddForm.valid) {
			this.sliderTwo = Object.assign({}, this.sliderTwoAddForm.value)

			if (this.sliderTwo.sliderTwoId == 0)
				this.addSliderTwo();
			else
				this.updateSliderTwo();
		}

	}

	addSliderTwo(){

		this.sliderTwoService.addSliderTwo(this.sliderTwo).subscribe(data => {
			this.getSliderTwoList();
			this.sliderTwo = new SliderTwo();
			jQuery('#slidertwo').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sliderTwoAddForm);

		})

	}

	updateSliderTwo(){

		this.sliderTwoService.updateSliderTwo(this.sliderTwo).subscribe(data => {

			var index=this.sliderTwoList.findIndex(x=>x.sliderTwoId==this.sliderTwo.sliderTwoId);
			this.sliderTwoList[index]=this.sliderTwo;
			this.dataSource = new MatTableDataSource(this.sliderTwoList);
            this.configDataTable();
			this.sliderTwo = new SliderTwo();
			jQuery('#slidertwo').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sliderTwoAddForm);

		})

	}

	createSliderTwoAddForm() {
		this.sliderTwoAddForm = this.formBuilder.group({		
			sliderTwoId : [0],
title : ["", Validators.required],
detay : ["", Validators.required],
price : [0, Validators.required],
discountPrice : [0, Validators.required],
photo : ["", Validators.required]
		})
	}

	deleteSliderTwo(sliderTwoId:number){
		this.sliderTwoService.deleteSliderTwo(sliderTwoId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sliderTwoList=this.sliderTwoList.filter(x=> x.sliderTwoId!=sliderTwoId);
			this.dataSource = new MatTableDataSource(this.sliderTwoList);
			this.configDataTable();
		})
	}

	getSliderTwoById(sliderTwoId:number){
		this.clearFormGroup(this.sliderTwoAddForm);
		this.sliderTwoService.getSliderTwoById(sliderTwoId).subscribe(data=>{
			this.sliderTwo=data;
			this.sliderTwoAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'sliderTwoId')
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
