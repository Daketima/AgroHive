using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmMartUI.Factory
{
    public class AreaFactory
    {
        public AreaFactory() { }

        public FactoryBase GetArea(string area)
        {
            FactoryBase getArea = null;
            switch (area)
            {
                case "Farmer":
                    getArea = new FarmerArea(area);
                    break;

                default:
                    throw new NotImplementedException();
                     
            }
            return getArea;
        }
    }
}