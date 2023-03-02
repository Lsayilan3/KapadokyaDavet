import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Slider } from './models/Slider';
import { SliderService } from './services/slider.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-slider',
	templateUrl: './slider.component.html',
	styleUrls: ['./slider.component.scss']
})
export class SliderComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['sliderId','photo', 'update','delete','file'];

	sliderList:Slider[];
	slider:Slider=new Slider();

	sliderAddForm: FormGroup;

	photoForm: FormGroup;

	sliderId:number;

	constructor(private sliderService:SliderService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getSliderList();
    }

	ngOnInit() {

		this.createSliderAddForm();
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
		formData.append('sliderId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.sliderService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getSliderList();
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


	getSliderList() {
		this.sliderService.getSliderList().subscribe(data => {
			this.sliderList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sliderAddForm.valid) {
			this.slider = Object.assign({}, this.sliderAddForm.value)

			if (this.slider.sliderId == 0)
				this.addSlider();
			else
				this.updateSlider();
		}

	}

	addSlider(){

		this.sliderService.addSlider(this.slider).subscribe(data => {
			this.getSliderList();
			this.slider = new Slider();
			jQuery('#slider').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sliderAddForm);

		})

	}

	updateSlider(){

		this.sliderService.updateSlider(this.slider).subscribe(data => {

			var index=this.sliderList.findIndex(x=>x.sliderId==this.slider.sliderId);
			this.sliderList[index]=this.slider;
			this.dataSource = new MatTableDataSource(this.sliderList);
            this.configDataTable();
			this.slider = new Slider();
			jQuery('#slider').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sliderAddForm);

		})

	}

	createSliderAddForm() {
		this.sliderAddForm = this.formBuilder.group({		
			sliderId : [0],
photo : ["", Validators.required]
		})
	}

	deleteSlider(sliderId:number){
		this.sliderService.deleteSlider(sliderId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sliderList=this.sliderList.filter(x=> x.sliderId!=sliderId);
			this.dataSource = new MatTableDataSource(this.sliderList);
			this.configDataTable();
		})
	}

	getSliderById(sliderId:number){
		this.clearFormGroup(this.sliderAddForm);
		this.sliderService.getSliderById(sliderId).subscribe(data=>{
			this.slider=data;
			this.sliderAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'sliderId')
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
