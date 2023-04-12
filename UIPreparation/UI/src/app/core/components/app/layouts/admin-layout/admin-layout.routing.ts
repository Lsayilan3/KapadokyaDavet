import { SliderTwoComponent } from './../../../../../sliderTwo/sliderTwo.component';
import { OrDugunComponent } from './../../../../../orDugun/orDugun.component';
import { OrPartiEglenceComponent } from './../../../../../orPartiEglence/orPartiEglence.component';
import { CikolataComponent } from './../../../../../cikolata/cikolata.component';



import { GalleryTwoComponent } from './../../../../../galleryTwo/galleryTwo.component';
import { EnsonurunComponent } from './../../../../../ensonurun/ensonurun.component';
import { EnsatanComponent } from './../../../../../ensatan/ensatan.component';
import { SpotComponent } from './../../../../../spot/spot.component';
import { SpotCategoryyComponent } from './../../../../../spotCategoryy/spotCategoryy.component';
import { SpotCategoryy } from './../../../../../spotCategoryy/models/spotcategoryy';
import { OrTrioEkibiComponent } from './../../../../../orTrioEkibi/orTrioEkibi.component';
import { OrSunnetComponent } from './../../../../../orSunnet/orSunnet.component';
import { OrSokakLezzetiComponent } from './../../../../../orSokakLezzeti/orSokakLezzeti.component';
import { OrSirketEglenceComponent } from './../../../../../orSirketEglence/orSirketEglence.component';
import { OrPiknikComponent } from './../../../../../orPiknik/orPiknik.component';
import { OrPersonelTeminiComponent } from './../../../../../orPersonelTemini/orPersonelTemini.component';
import { OrPartiStoreComponent } from './../../../../../orPartiStore/orPartiStore.component';

import { OrNisanComponent } from './../../../../../orNisan/orNisan.component';
import { OrKokteylComponent } from './../../../../../orKokteyl/orKokteyl.component';
import { OrKinaaComponent } from './../../../../../orKinaa/orKinaa.component';
import { OrEkipmanComponent } from './../../../../../orEkipman/orEkipman.component';
import { OrCoffeComponent } from './../../../../../orCoffe/orCoffe.component';
import { OrCateringComponent } from './../../../../../orCatering/orCatering.component';
import { OrBabyComponent } from './../../../../../orBaby/orBaby.component';
import { OrAcilisComponent } from './../../../../../orAcilis/orAcilis.component';
import { OrAnimasyoneComponent } from './../../../../../orAnimasyone/orAnimasyone.component';
import { MuzikComponent } from './../../../../../muzik/muzik.component';
import { YiyecekComponent } from './../../../../../yiyecek/yiyecek.component';
import { SliderComponent } from './../../../../../slider/slider.component';
import { PartiComponent } from './../../../../../parti/parti.component';
import { OrganizasyonComponent } from './../../../../../organizasyon/organizasyon.component';
import { LazerComponent } from './../../../../../lazer/lazer.component';
import { HediyelikComponent } from './../../../../../hediyelik/hediyelik.component';
// import { ElmasComponent } from './../../../../../elmas/elmas.component';
import { BlogComponent } from './../../../../../blog/blog.component';
import { Routes } from '@angular/router';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { LanguageComponent } from 'app/core/components/admin/language/language.component';
import { LogDtoComponent } from 'app/core/components/admin/log/logDto.component';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { OperationClaimComponent } from 'app/core/components/admin/operationclaim/operationClaim.component';
import { TranslateComponent } from 'app/core/components/admin/translate/translate.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { LoginGuard } from 'app/core/guards/login-guard';
import { DashboardComponent } from '../../dashboard/dashboard.component';





export const AdminLayoutRoutes: Routes = [

    { path: 'dashboard',      component: DashboardComponent,canActivate:[LoginGuard] }, 
    { path: 'user',           component: UserComponent, canActivate:[LoginGuard] },
    { path: 'group',          component: GroupComponent, canActivate:[LoginGuard] },
    { path: 'login',          component: LoginComponent },
    { path: 'language',       component: LanguageComponent,canActivate:[LoginGuard]},
    { path: 'translate',      component: TranslateComponent,canActivate:[LoginGuard]},
    { path: 'operationclaim', component: OperationClaimComponent,canActivate:[LoginGuard]},
    { path: 'log',            component: LogDtoComponent,canActivate:[LoginGuard]},

    { path: 'blog',            component: BlogComponent,canActivate:[LoginGuard]},
    // { path: 'elmas',            component: ElmasComponent,canActivate:[LoginGuard]},
    { path: 'hediyelik',            component: HediyelikComponent,canActivate:[LoginGuard]},
    { path: 'lazer',            component: LazerComponent,canActivate:[LoginGuard]},
    { path: 'organizasyon',            component: OrganizasyonComponent,canActivate:[LoginGuard]},
    { path: 'parti',            component: PartiComponent,canActivate:[LoginGuard]},
    { path: 'slider',            component: SliderComponent,canActivate:[LoginGuard]},
    { path: 'slidertwo',            component: SliderTwoComponent,canActivate:[LoginGuard]},
    { path: 'yiyecek',            component: YiyecekComponent,canActivate:[LoginGuard]},
    { path: 'muzik',            component: MuzikComponent,canActivate:[LoginGuard]},


 
    { path: 'Animasyon',            component: OrAnimasyoneComponent,canActivate:[LoginGuard]},
    { path: 'Açılış',            component: OrAcilisComponent,canActivate:[LoginGuard]},
    { path: 'Baby',            component: OrBabyComponent,canActivate:[LoginGuard]},
    { path: 'Catering',            component: OrCateringComponent,canActivate:[LoginGuard]},
    { path: 'CoffeBreak',            component: OrCoffeComponent,canActivate:[LoginGuard]},
    { path: 'Ekipman',            component: OrEkipmanComponent,canActivate:[LoginGuard]},
    { path: 'Kına',            component: OrKinaaComponent,canActivate:[LoginGuard]},
    { path: 'Kokteyil',            component: OrKokteylComponent,canActivate:[LoginGuard]},
    { path: 'Nişan',            component: OrNisanComponent,canActivate:[LoginGuard]},
    { path: 'PartiEğlence',            component: OrPartiEglenceComponent,canActivate:[LoginGuard]},
    { path: 'PartiStore',            component: OrPartiStoreComponent,canActivate:[LoginGuard]},
    { path: 'PersonelTemini',            component: OrPersonelTeminiComponent,canActivate:[LoginGuard]},
    { path: 'Piknik',            component: OrPiknikComponent,canActivate:[LoginGuard]},
    { path: 'SirketEğlence',            component: OrSirketEglenceComponent,canActivate:[LoginGuard]},
    { path: 'SokakLezzeti',            component: OrSokakLezzetiComponent,canActivate:[LoginGuard]},
    { path: 'Sünnet',            component: OrSunnetComponent,canActivate:[LoginGuard]},
    { path: 'TrioEkibi',            component: OrTrioEkibiComponent,canActivate:[LoginGuard]},
    { path: 'Spot',            component: SpotComponent,canActivate:[LoginGuard]},
    { path: 'SpotCategory',            component: SpotCategoryyComponent,canActivate:[LoginGuard]},
    { path: 'EnCokSatan',            component: EnsatanComponent,canActivate:[LoginGuard]},
    { path: 'EnSonUrun',            component: EnsonurunComponent,canActivate:[LoginGuard]},
    { path: 'GaleriTwo',            component: GalleryTwoComponent,canActivate:[LoginGuard]},
    { path: 'Cikolata',            component: CikolataComponent,canActivate:[LoginGuard]},
    { path: 'Düğün',            component: OrDugunComponent,canActivate:[LoginGuard]},

   


    
];
