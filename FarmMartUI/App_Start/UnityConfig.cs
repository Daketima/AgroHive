using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using FarmMartBLL.Core;
using FarmMartDAL.Model;
using FarmMartBLL.ServiceAPI;
using FarmMartUI.Controllers;
using FarmMartUI.helper;

namespace FarmMartUI.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            var superController = typeof(SuperBaseController);
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterInstance(superController);
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IRepositoryService<Planting>, PlantingService>();
            container.RegisterType<IRepositoryService<Crop>, CropService>();
            container.RegisterType<IRepositoryService<Address>, AddressService>();
            container.RegisterType<IRepositoryService<FarmCrop>, FarmCropService>();
            container.RegisterType<IRepositoryService<FarmLivestock>, FarmLivestockService>();
            container.RegisterType<IRepositoryService<Farm>, FarmService>();
            container.RegisterType<IRepositoryService<HarvestPeriod>, PlantingPeriodService>();
            container.RegisterType<IRepositoryService<LivestockPrice>, LivestockPriceService>();
            container.RegisterType<IRepositoryService<Livestock>, LivestockService>();
            container.RegisterType<IRepositoryService<LivestockType>, LivestockTypeService>();
            container.RegisterType<IRepositoryService<LocalGovernment>, LocalGovermentService>();
            container.RegisterType<IRepositoryService<Measurement>, MeasurementService>();
            container.RegisterType<IRepositoryService<CropPrice>, CropPriceService>();
            container.RegisterType<IRepositoryService<State>, StateService>();
            container.RegisterType<IRepositoryService<Messaging>, MessagingService>();
            container.RegisterType<IRepositoryService<MessageReply>, MessageReplyService>();
            container.RegisterType<IRepositoryService<CropVariety>, CropVarietyService>();
            container.RegisterType<IRepositoryService<AnimalGender>, AnimalGenderService>();
            container.RegisterType<IRepositoryService<CropType>, CropTypeService>();
            container.RegisterType<IRepositoryService<FarmSizeUnit>, FarmSizeUnitService>();
        }
    }
}
