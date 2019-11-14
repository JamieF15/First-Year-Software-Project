using System;
using System.Collections.Generic;
using System.Text;

namespace IntroToProgrammingAssignmentV2
{
    class HGV : Vehicle
    {
        /// <summary>
        /// this contructor represents HGV objects, which inherits from the vehicle class
        /// </summary>
        /// <param name="_ID"> inherits from the vechile class ID property</param>
        public HGV(int _ID) : base(_ID)
        {
            VehicleType = "HGV";
            TankCapacity = 140;
            FuelType = "LPG"; 
            GetStartingFuel(0, TankCapacity / 4);
        }
    }
}
