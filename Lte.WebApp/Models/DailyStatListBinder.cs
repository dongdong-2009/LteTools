using System.Linq;
using System.Web.Mvc;
using Lte.Parameters.Kpi.Entities;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Concrete;

namespace Lte.WebApp.Models
{
    public class CdmaDailyStatListBinder : IModelBinder
    {
        private const string sessionKey = "CdmaDailyStatList";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            AllCdmaDailyStatList list
                = (AllCdmaDailyStatList)controllerContext.HttpContext.Session[sessionKey];

            if (list == null)
            {
                IRegionRepository regionRepository = new EFRegionRepository();
                list = new AllCdmaDailyStatList(regionRepository.OptimizeRegions.ToList());
                controllerContext.HttpContext.Session[sessionKey] = list;
            }
            // return the cart
            return list;
        }
    }
}