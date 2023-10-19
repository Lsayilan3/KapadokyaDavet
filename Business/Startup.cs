using Autofac;
using Business.Constants;
using Business.DependencyResolvers;
using Business.Fakes.DArch;
using Business.Services.Authentication;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.ElasticSearch;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.MongoDb.Context;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace Business
{
    public partial class BusinessStartup
    {
        public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }

        protected IHostEnvironment HostEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            Func<IServiceProvider, ClaimsPrincipal> getPrincipal = (sp) =>
                sp.GetService<IHttpContextAccessor>().HttpContext?.User ??
                new ClaimsPrincipal(new ClaimsIdentity(Messages.Unknown));

            services.AddScoped<IPrincipal>(getPrincipal);
            services.AddMemoryCache();

           

            var coreModule = new CoreModule();

            services.AddDependencyResolvers(Configuration, new ICoreModule[] { coreModule });

            services.AddTransient<IAuthenticationCoordinator, AuthenticationCoordinator>();

            services.AddSingleton<ConfigurationManager>();


            services.AddTransient<ITokenHelper, JwtHelper>();
            services.AddTransient<IElasticSearch, ElasticSearchManager>();

            services.AddTransient<IMessageBrokerHelper, MqQueueHelper>();
            services.AddTransient<IMessageConsumer, MqConsumerHelper>();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();

            services.AddAutoMapper(typeof(ConfigurationManager));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly);

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>()
                    ?.GetName();
            };
        }

        /// <summary>
        /// This method gets called by the Development
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<ILazerRepository, LazerRepository>();
            services.AddTransient<IEnsonurunRepository, EnsonurunRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IYiyecekRepository, YiyecekRepository>();
            services.AddTransient<ISpotCategoryyRepository, SpotCategoryyRepository>();
            services.AddTransient<ISpotRepository, SpotRepository>();
            services.AddTransient<ISliderTwoRepository, SliderTwoRepository>();
            services.AddTransient<IPartiRepository, PartiRepository>();
            services.AddTransient<IOrTrioEkibiRepository, OrTrioEkibiRepository>();
            services.AddTransient<IOrSunnetRepository, OrSunnetRepository>();
            services.AddTransient<IOrSokakLezzetiRepository, OrSokakLezzetiRepository>();
            services.AddTransient<IOrSirketEglenceRepository, OrSirketEglenceRepository>();
            services.AddTransient<IOrPiknikRepository, OrPiknikRepository>();
            services.AddTransient<IOrPersonelTeminiRepository, OrPersonelTeminiRepository>();
            services.AddTransient<IOrPartiStoreRepository, OrPartiStoreRepository>();
            services.AddTransient<IOrPartiEglenceRepository, OrPartiEglenceRepository>();
            services.AddTransient<IOrNisanRepository, OrNisanRepository>();
            services.AddTransient<IOrKokteylRepository, OrKokteylRepository>();
            services.AddTransient<IOrKinaaRepository, OrKinaaRepository>();
            services.AddTransient<IOrganizasyonRepository, OrganizasyonRepository>();
            services.AddTransient<IOrEkipmanRepository, OrEkipmanRepository>();
            services.AddTransient<IOrDugunRepository, OrDugunRepository>();
            services.AddTransient<IOrCoffeRepository, OrCoffeRepository>();
            services.AddTransient<IOrCikolataRepository, OrCikolataRepository>();
            services.AddTransient<IOrCateringRepository, OrCateringRepository>();
            services.AddTransient<IOrBabyRepository, OrBabyRepository>();
            services.AddTransient<IOrAnimasyoneRepository, OrAnimasyoneRepository>();
            services.AddTransient<IOrAcilisRepository, OrAcilisRepository>();
            services.AddTransient<IMuzikRepository, MuzikRepository>();
            services.AddTransient<IHediyelikRepository, HediyelikRepository>();
            services.AddTransient<IGalleryTwoRepository, GalleryTwoRepository>();
            services.AddTransient<IEnsatanRepository, EnsatanRepository>();
            services.AddTransient<ICikolataRepository, CikolataRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();

            services.AddDbContext<ProjectDbContext, DArchInMemory>(ServiceLifetime.Transient);
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        /// <summary>
        /// This method gets called by the Staging
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<ILazerRepository, LazerRepository>();
            services.AddTransient<IEnsonurunRepository, EnsonurunRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IYiyecekRepository, YiyecekRepository>();
            services.AddTransient<ISpotCategoryyRepository, SpotCategoryyRepository>();
            services.AddTransient<ISpotRepository, SpotRepository>();
            services.AddTransient<ISliderTwoRepository, SliderTwoRepository>();
            services.AddTransient<IPartiRepository, PartiRepository>();
            services.AddTransient<IOrTrioEkibiRepository, OrTrioEkibiRepository>();
            services.AddTransient<IOrSunnetRepository, OrSunnetRepository>();
            services.AddTransient<IOrSokakLezzetiRepository, OrSokakLezzetiRepository>();
            services.AddTransient<IOrSirketEglenceRepository, OrSirketEglenceRepository>();
            services.AddTransient<IOrPiknikRepository, OrPiknikRepository>();
            services.AddTransient<IOrPersonelTeminiRepository, OrPersonelTeminiRepository>();
            services.AddTransient<IOrPartiStoreRepository, OrPartiStoreRepository>();
            services.AddTransient<IOrPartiEglenceRepository, OrPartiEglenceRepository>();
            services.AddTransient<IOrNisanRepository, OrNisanRepository>();
            services.AddTransient<IOrKokteylRepository, OrKokteylRepository>();
            services.AddTransient<IOrKinaaRepository, OrKinaaRepository>();
            services.AddTransient<IOrganizasyonRepository, OrganizasyonRepository>();
            services.AddTransient<IOrEkipmanRepository, OrEkipmanRepository>();
            services.AddTransient<IOrDugunRepository, OrDugunRepository>();
            services.AddTransient<IOrCoffeRepository, OrCoffeRepository>();
            services.AddTransient<IOrCikolataRepository, OrCikolataRepository>();
            services.AddTransient<IOrCateringRepository, OrCateringRepository>();
            services.AddTransient<IOrBabyRepository, OrBabyRepository>();
            services.AddTransient<IOrAnimasyoneRepository, OrAnimasyoneRepository>();
            services.AddTransient<IOrAcilisRepository, OrAcilisRepository>();
            services.AddTransient<IMuzikRepository, MuzikRepository>();
            services.AddTransient<IHediyelikRepository, HediyelikRepository>();
            services.AddTransient<IGalleryTwoRepository, GalleryTwoRepository>();
            services.AddTransient<IEnsatanRepository, EnsatanRepository>();
            services.AddTransient<ICikolataRepository, CikolataRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();
            services.AddDbContext<ProjectDbContext,MsDbContext>();

            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        /// <summary>
        /// This method gets called by the Production
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<ILazerRepository, LazerRepository>();
            services.AddTransient<IEnsonurunRepository, EnsonurunRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IYiyecekRepository, YiyecekRepository>();
            services.AddTransient<ISpotCategoryyRepository, SpotCategoryyRepository>();
            services.AddTransient<ISpotRepository, SpotRepository>();
            services.AddTransient<ISliderTwoRepository, SliderTwoRepository>();
            services.AddTransient<IPartiRepository, PartiRepository>();
            services.AddTransient<IOrTrioEkibiRepository, OrTrioEkibiRepository>();
            services.AddTransient<IOrSunnetRepository, OrSunnetRepository>();
            services.AddTransient<IOrSokakLezzetiRepository, OrSokakLezzetiRepository>();
            services.AddTransient<IOrSirketEglenceRepository, OrSirketEglenceRepository>();
            services.AddTransient<IOrPiknikRepository, OrPiknikRepository>();
            services.AddTransient<IOrPersonelTeminiRepository, OrPersonelTeminiRepository>();
            services.AddTransient<IOrPartiStoreRepository, OrPartiStoreRepository>();
            services.AddTransient<IOrPartiEglenceRepository, OrPartiEglenceRepository>();
            services.AddTransient<IOrNisanRepository, OrNisanRepository>();
            services.AddTransient<IOrKokteylRepository, OrKokteylRepository>();
            services.AddTransient<IOrKinaaRepository, OrKinaaRepository>();
            services.AddTransient<IOrganizasyonRepository, OrganizasyonRepository>();
            services.AddTransient<IOrEkipmanRepository, OrEkipmanRepository>();
            services.AddTransient<IOrDugunRepository, OrDugunRepository>();
            services.AddTransient<IOrCoffeRepository, OrCoffeRepository>();
            services.AddTransient<IOrCikolataRepository, OrCikolataRepository>();
            services.AddTransient<IOrCateringRepository, OrCateringRepository>();
            services.AddTransient<IOrBabyRepository, OrBabyRepository>();
            services.AddTransient<IOrAnimasyoneRepository, OrAnimasyoneRepository>();
            services.AddTransient<IOrAcilisRepository, OrAcilisRepository>();
            services.AddTransient<IMuzikRepository, MuzikRepository>();
            services.AddTransient<IHediyelikRepository, HediyelikRepository>();
            services.AddTransient<IGalleryTwoRepository, GalleryTwoRepository>();
            services.AddTransient<IEnsatanRepository, EnsatanRepository>();
            services.AddTransient<ICikolataRepository, CikolataRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();


            services.AddDbContext<ProjectDbContext>();

            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacBusinessModule(new ConfigurationManager(Configuration, HostEnvironment)));
        }
    }
}
