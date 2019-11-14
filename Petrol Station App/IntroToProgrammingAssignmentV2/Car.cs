using System;
using System.Collections.Generic;
using System.Text;

namespace IntroToProgrammingAssignmentV2
{
    class Car : Vehicle
    {
        /// <summary>
        /// this contructor represents car objects, which inherits from the vehicle class
        /// </summary>
        /// <param name="_ID"> inherits from the vechile class ID property</param>
        public Car(int _ID) : base(_ID)
        {
            VehicleType = "Car";
            TankCapacity = 40;
            GetStartingFuel(0, TankCapacity / 4);
            SelectCarFuelType();
        }
        
        /// <summary>
        /// this method is responsible for randomly selecting the fuel type for a car
        /// </summary>
        public void SelectCarFuelType()
        {
            int randomFuelType = rng.Next(1, 4);

            switch (randomFuelType)
            {
                case 1:
                    FuelType = "Unleaded";
                break;

                case 2:
                    FuelType = "Diesel";
                break;

                case 3:
                    FuelType = "LPG";
                break;
            }
        }
    }
}
