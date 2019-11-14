using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Timers;


using System.Drawing;
namespace IntroToProgrammingAssignmentV2
{
    class Pump
    {
        // instantion of a station for access to its data
        public Station s;

        // timer to represent the time it will fuel a vehicle
        Timer FuelTimer;

        // attributes
        public string FuelType { get; set; }
        public float TotalFuelUsed { get; set; }
        public float UnleadedUsed { get; set; }
        public float DieselUsed { get; set; }
        public float LPGUsed { get; set; }
        public float UnleadedUsedThisTransaction { get; set; }
        public float DieselUsedThisTransaction { get; set; }
        public float LPGUsedThisTransaction { get; set; }
        public float FuelUsedThisTransaction { get; set; }
        public float TotalMoneyMade { get; set; }
        public float FuelComission { get; set; }
        public float FuelSpeed { get; set; } = 1.50f;
        public float CostPerLitre { get; set; } = 2f;
        public Vehicle CurrentVehicle { get; set; }

        /// <summary>
        /// this method is responsible for calcualting the transaction data 
        /// </summary>
        void Calcualtions()
        {
            // these if statements perform the appropriate calculations based on the fuel type of the vehicle.
            if (CurrentVehicle.FuelType == "Unleaded")
            {
                UnleadedUsedThisTransaction = CurrentVehicle.TankCapacity - CurrentVehicle.StartingFuel;
                UnleadedUsed += CurrentVehicle.TankCapacity - CurrentVehicle.StartingFuel;
            }

            if (CurrentVehicle.FuelType == "Diesel")
            {         
                DieselUsedThisTransaction = CurrentVehicle.TankCapacity - CurrentVehicle.StartingFuel;
                DieselUsed += CurrentVehicle.TankCapacity - CurrentVehicle.StartingFuel;
            }

            if (CurrentVehicle.FuelType == "LPG")
            { 
                LPGUsedThisTransaction = CurrentVehicle.TankCapacity - CurrentVehicle.StartingFuel;
                LPGUsed += CurrentVehicle.TankCapacity - CurrentVehicle.StartingFuel;
            }

            TotalFuelUsed = UnleadedUsed + DieselUsed + LPGUsed;
            FuelComission = TotalFuelUsed * CostPerLitre / 100f;
            TotalMoneyMade = (TotalFuelUsed * CostPerLitre) - FuelComission;
        }

        // used to check if a pump if availbe for a vehicle to accomadate
        public bool CheckPumpAvailibility()
        {
            return CurrentVehicle == null;        
        }

        /// <summary>
        /// this method is used to calculate the time to fuel for each vehicle, based on its starting fuel  
        /// </summary>
        public void GetTimeToFuel()
        {
            CurrentVehicle.TimeToFuel = (CurrentVehicle.TankCapacity - CurrentVehicle.StartingFuel) * 1.5f * 100;
        }

        /// <summary>
        /// this method is used to assign a vehicle to a pump using a timer
        /// </summary>
        /// <param name="v"> a vehicle object is used for referenece </param>
        public void AssignVehicleToPump(Vehicle v)
        {
            CurrentVehicle = v;
            GetTimeToFuel();
            FuelTimer = new Timer();
            FuelTimer.Interval = CurrentVehicle.TimeToFuel;
            FuelTimer.AutoReset = false;
            FuelTimer.Elapsed += RemoveVehicle;
            FuelTimer.Enabled = true;
            FuelTimer.Start();
        }

        /// <summary>
        /// when the v.TimeToFuel variable to elapsed, this method will run, which will remove the vehicle that is assigned to a pump and create a transaction
        /// </summary>
        /// <param name="source"> used for system.timers </param>
        /// <param name="e"> used for system.timers </param>
        public void RemoveVehicle(object source, ElapsedEventArgs e)
        {
            Calcualtions();
            s.transactions.Add(new Transactions(TotalFuelUsed, s.FindPumpNumber(this), CurrentVehicle, UnleadedUsed, DieselUsed, LPGUsed, UnleadedUsedThisTransaction, DieselUsedThisTransaction, LPGUsedThisTransaction, TotalMoneyMade));
            CurrentVehicle = null;
        }
    }
}

