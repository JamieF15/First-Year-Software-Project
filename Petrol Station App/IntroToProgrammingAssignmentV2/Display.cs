using System;
using System.Collections.Generic;
using System.Text;

namespace IntroToProgrammingAssignmentV2
{
    class Display
    {
        // this object for allowing access to the station class
        static Station parent;
       
        /// <summary>
        /// This constructor is used to parse the station object in which it was created in.
        /// </summary>
        /// <param name="s"> expects a station object </param>
        public Display(Station s)
        {
            parent = s;
        }

        /// <summary>
        /// this method is responsible for printing the relevant transaction data associated with the fueling process
        /// </summary>
        public void PrintTranscationData()
        {
            float totalMoneyMade = 0;
            float totalUnleadedDispensed = 0;
            float UnleadedUsedThisTranaction = 0;
            float totalDieselDispensed = 0;
            float DieselUsedThisTransaction = 0;
            float totalLPGDispensed = 0;
            float LPGUsedThisTransaction = 0;
            float totalFuelDispensed = 0;
            float fuelCommision = 0;
            float totalVehiclesServiced = Transactions.VehiclesServiced;

            // this loop interates through each pump in the statoin and performs various calculations based on the data within each pump
            foreach (Pump pump in parent.Pumps)
            {
                totalMoneyMade += pump.TotalMoneyMade;
                totalUnleadedDispensed += pump.UnleadedUsed;
                totalDieselDispensed += pump.DieselUsed;
                totalLPGDispensed += pump.LPGUsed;
                UnleadedUsedThisTranaction = pump.UnleadedUsedThisTransaction;
                DieselUsedThisTransaction = pump.DieselUsedThisTransaction;
                LPGUsedThisTransaction = pump.LPGUsedThisTransaction;             
                totalFuelDispensed = totalUnleadedDispensed + totalDieselDispensed + totalLPGDispensed;
                fuelCommision += pump.FuelComission;
            }

            // this collection of WriteLines represents the graphical console representation of the data within the program
            Console.WriteLine();
            Console.WriteLine("Queue stats: ");
            Console.WriteLine("Number of vehicles in queue: " + parent.queue.Count);
            Console.WriteLine();
            Console.WriteLine("Total vehicles serviced: " + totalVehiclesServiced);
            Console.WriteLine("Total vehicles unserviced: " + Transactions.UnservicedVehicles);
            Console.WriteLine("Total money made after commission: £" + Math.Round(totalMoneyMade));
            Console.WriteLine("Total Unleaded dispensed: " + Math.Round(totalUnleadedDispensed, 2) + " litres");
            Console.WriteLine("Total Diesel dispensed: " + Math.Round(totalDieselDispensed, 2) + " litres");
            Console.WriteLine("Total LPG dispensed: " + Math.Round(totalLPGDispensed, 2) + " litres");
            Console.WriteLine("Total fuel dispensed: " + Math.Round(totalFuelDispensed, 2) + " litres");
            Console.WriteLine();
            Console.WriteLine("Transactions: ");

            // this list is here to act as a buffer that temporarily stores the data from the parent transaction list
            List<Transactions> tempTrans = new List<Transactions>(parent.transactions);

            // this 'if' statement is responsible for checking if the print of the list of transactions is greater than 5 in the interest of readabiliy
            int startIndex = 0;
            if (tempTrans.Count >= 5)
            {
                startIndex = tempTrans.Count -5;
            }

            // this loop iterates through the TempTrans list and performs WriteLines displaying data based on variables in various classes
            for (int i = startIndex; i < tempTrans.Count; i++)
            {          
                Console.Write("\nVehicle: " + tempTrans[i].vehicle.ID);
                Console.Write(" Vehicle type: " + tempTrans[i].vehicle.VehicleType);
                Console.Write(" Serviced at pump Number: " + tempTrans[i].PumpNumber);
                Console.Write(" Time taken to fuel: " + Math.Round(tempTrans[i].vehicle.TimeToFuel, 2));

                // checks the fuel type of the vehicle to display the correct fuel in the transaction
                if (tempTrans[i].vehicle.FuelType == "Unleaded")
                {
                    Console.WriteLine(" Litres dispensed this transaction: " + Math.Round(tempTrans[i].UnleadedDispensedThisTransaction, 2));
                }

                if (tempTrans[i].vehicle.FuelType == "Diesel")
                {
                    Console.WriteLine(" Litres dispensed this transaction: " + Math.Round(tempTrans[i].DieselDispensedThisTransaction, 2));
                }

                if (tempTrans[i].vehicle.FuelType == "LPG")
                {
                    Console.WriteLine(" Litres dispensed this transaction: " + Math.Round(tempTrans[i].LPGDispensedThisTransaction, 2));
                }

                Console.WriteLine();           
            }
        }

        /// <summary>
        /// This method is for representing the data within the station and pump classes.
        /// </summary>
        public void Show()
        {
            // clear the console everytime this method is called
            Console.Clear();

            // loops through each element of the pumps array and displays it as a string representation
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("| Pump: " + (i * 3 + j + 1) + " ");

                    if (parent.Pumps[i, j].CheckPumpAvailibility())
                    {
                        Console.Write($"{"FREE X",-10}");
                    }

                    // if there is a vehicle allocated to a pump, 'BUSY' will print instead of 'FREE'
                    else
                    {
                        Console.Write($"{"BUSY " + parent.Pumps[i, j].CurrentVehicle.ID,-10}");
                    }

                    // after each loop of the 'i' loop, the line will break, and this will make the pumps print in the correct order.
                    if (j == 2)
                    {
                        Console.WriteLine();
                    }  
                }
            }

            // this method prints the transaction data at each loop
            PrintTranscationData();
        }
    }
}
