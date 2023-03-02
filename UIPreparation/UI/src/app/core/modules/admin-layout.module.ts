import { SliderTwoComponent } from './../../sliderTwo/sliderTwo.component';
import { OrDugunComponent } from './../../orDugun/orDugun.component';
import { CikolataComponent } from './../../cikolata/cikolata.component';



import { GalleryTwoComponent } from './../../galleryTwo/galleryTwo.component';
import { EnsonurunComponent } from './../../ensonurun/ensonurun.component';
import { EnsatanComponent } from './../../ensatan/ensatan.component';
import { SpotCategoryyComponent } from './../../spotCategoryy/spotCategoryy.component';

import { SpotComponent } from './../../spot/spot.component';
import { OrKinaaComponent } from './../../orKinaa/orKinaa.component';
import { OrTrioEkibiComponent } from './../../orTrioEkibi/orTrioEkibi.component';
import { OrSunnetComponent } from './../../orSunnet/orSunnet.component';
import { OrSokakLezzetiComponent } from './../../orSokakLezzeti/orSokakLezzeti.component';
import { OrSirketEglenceComponent } from './../../orSirketEglence/orSirketEglence.component';
import { OrPiknikComponent } from './../../orPiknik/orPiknik.component';
import { OrPersonelTeminiComponent } from './../../orPersonelTemini/orPersonelTemini.component';
import { OrPartiStoreComponent } from './../../orPartiStore/orPartiStore.component';
import { OrPartiEglenceComponent } from './../../orPartiEglence/orPartiEglence.component';
import { OrNisanComponent } from './../../orNisan/orNisan.component';
import { OrKokteylComponent } from './../../orKokteyl/orKokteyl.component';
import { OrEkipmanComponent } from './../../orEkipman/orEkipman.component';
import { OrCoffeComponent } from './../../orCoffe/orCoffe.component';
import { OrCateringComponent } from './../../orCatering/orCatering.component';
import { OrBabyComponent } from './../../orBaby/orBaby.component';
import { OrAnimasyoneComponent } from './../../orAnimasyone/orAnimasyone.component';
import { OrAcilisComponent } from './../../orAcilis/orAcilis.component';
import { MuzikComponent } from './../../muzik/muzik.component';
import { YiyecekComponent } from './../../yiyecek/yiyecek.component';
import { SliderComponent } from './../../slider/slider.component';
import { PartiComponent } from './../../parti/parti.component';
import { OrganizasyonComponent } from './../../organizasyon/organizasyon.component';
import { LazerComponent } from './../../lazer/lazer.component';
import { HediyelikComponent } from './../../hediyelik/hediyelik.component';
// import { ElmasComponent } from './../../elmas/elmas.component';
import { BlogComponent } from './../../blog/blog.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from '../components/app/layouts/admin-layout/admin-layout.routing';
import { DashboardComponent } from '../components/app/dashboard/dashboard.component';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';

import { TranslationService } from 'app/core/services/translation.service';
import { LanguageComponent } from '../components/admin/language/language.component';
import { TranslateComponent } from '../components/admin/translate/translate.component';
import { OperationClaimComponent } from '../components/admin/operationclaim/operationClaim.component';
import { LogDtoComponent } from '../components/admin/log/logDto.component';
import { MatSortModule } from '@angular/material/sort';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';


// export function layoutHttpLoaderFactory(http: HttpClient) {
// 
//   return new TranslateHttpLoader(http,'../../../../../../assets/i18n/','.json');
// }

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(AdminLayoutRoutes),
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatRippleModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatTooltipModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatCheckboxModule,
        NgbModule,
        NgMultiSelectDropDownModule,
        SweetAlert2Module,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                //useFactory:layoutHttpLoaderFactory,
                useClass: TranslationService,
                deps: [HttpClient]
            }
        })
    ],
    declarations: [
        DashboardComponent,
        UserComponent,
        LoginComponent,
        GroupComponent,
        LanguageComponent,
        TranslateComponent,
        OperationClaimComponent,
        LogDtoComponent,
        BlogComponent,
        // ElmasComponent,
        HediyelikComponent,
        LazerComponent,
        OrganizasyonComponent,
        PartiComponent,
        SliderComponent,
        YiyecekComponent,
        MuzikComponent,
        OrAcilisComponent,
        OrAnimasyoneComponent,
        OrBabyComponent,
        OrCateringComponent,
        OrCoffeComponent,
        OrEkipmanComponent,
        OrKokteylComponent,
        OrNisanComponent,
        OrPartiEglenceComponent,
        OrPartiStoreComponent,
        OrPersonelTeminiComponent,
        OrPiknikComponent,
        OrSirketEglenceComponent,
        OrSokakLezzetiComponent,
        OrSunnetComponent,
        OrTrioEkibiComponent,
        OrKinaaComponent,
        SpotComponent,
        SpotCategoryyComponent,
        EnsatanComponent,
        EnsonurunComponent,
        CikolataComponent,
        GalleryTwoComponent,
        OrDugunComponent,
        SliderTwoComponent,
    
        
        

    ]
})

export class AdminLayoutModule { }
