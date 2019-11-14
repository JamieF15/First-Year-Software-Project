using System;


namespace IntroToProgrammingAssignmentV2
{
    class Vehicle
    {
        // attributes
        public int ID { get; set; }
        public string VehicleType { get; set; }
        public int TankCapacity { get; set; } 
        public int StartingFuel { get; set; }
        public float TimeToFuel { get; set; }
        public string FuelType { get; set; }
        protected Random rng = new Random();
      
        /// <summary>
        /// vehicle constructor 
        /// </summary>
        /// <param name="_ID">the ID of the vehicle is stored here</param>
        public Vehicle(int _ID)
        {
            ID = _ID;
        }

        /// <summary>
        /// this method represents the time it will take a vechile to be fueled, which will range from the number that are entered into the arugment of the method
        /// </summary>
        public void GenerateTimeToFuel(int min, int max)
        {
            int randomFuelTime = rng.Next(min, max);
            TimeToFuel = randomFuelTime;
        }

        /// <summary>
        /// generates a random number between 0 and 25% of the vehicles tank capacity to represent the starting fuel of a vehicle
        /// </summary>
        /// <returns></returns>
        public void GetStartingFuel(int min, int max)
        {
            int randomTimeToFuel = rng.Next(min, max); 
            StartingFuel = randomTimeToFuel;
        }
    }
}
