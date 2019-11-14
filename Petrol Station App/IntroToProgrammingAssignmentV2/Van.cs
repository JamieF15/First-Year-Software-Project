using System;
using System.Collections.Generic;
using System.Text;

namespace IntroToProgrammingAssignmentV2
{
    class Van : Vehicle
    {
        /// <summary>
        /// this contructor represents van objects, which inherits from the vehicle class
        /// </summary>
        /// <param name="_ID"> inherits from the vechile class ID property</param>
        public Van(int _ID) : base(_ID)
        {
            VehicleType = "Van";
            TankCapacity = 80;
            SelectVanFuelType();
            GetStartingFuel(0, TankCapacity / 4);
        }

        /// <summary>
        /// this method is responsible for randomly selecting the fuel type for a van
        /// </summary>
        public void SelectVanFuelType()
        {
            int randomFueltype = rng.Next(1, 3);

            switch (randomFueltype)
            {           
                case 1:
                    FuelType = "Diesel";
                break;

                case 2:
                    FuelType = "LPG";
                break;
            }
        }
    }
}
