using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;


namespace IntroToProgrammingAssignmentV2
{
    class Station
    {
        // instantiation of the display class
        Display d;

        // attributes
        public int NextVehicleID { get; set; } = 1;

        /// <summary>
        /// this list contains the transactions that are generated when a vehicle is serviced at a pump
        /// </summary>
        public List<Transactions> transactions = new List<Transactions>();

        /// <summary>
        /// this list represents the queue of vehicles 
        /// </summary>
        public List<Vehicle> queue = new List<Vehicle>();

        /// <summary>
        /// this 2D array represents the 9 pumps within the station
        /// </summary>
        public Pump[,] Pumps = new Pump[3, 3];

        /// <summary>
        /// timer to represent how long it will take to spawn a car
        /// </summary>
        Timer StationTimer;

        /// <summary>
        /// this timer is used to represent the time it will take to remove a vehicle that is waiting in a queue
        /// </summary>
        Timer RemoveWaitingVehicle;

        /// <summary>
        /// this method is used to setup the timer that is responsible for remooving cars from the queue
        /// </summary>
        void SetupWaitingVehicleTimer()
        {
            RemoveWaitingVehicle = new Timer();
            RemoveWaitingVehicle.Interval = RandomTimeToRemoveCar(1000, 2001);
            RemoveWaitingVehicle.AutoReset = true;
            RemoveWaitingVehicle.Elapsed += RemoveVehicleFromQueue;
            RemoveWaitingVehicle.Enabled = true;        
            RemoveWaitingVehicle.Start();         
        }

        /// <summary>
        /// this method is used to determine if a car is to be removed from the queue based on if there are more than 5 vehicles in the quue
        /// </summary>
        /// <param name="source"> used for system.timers </param>
        /// <param name="e"> used for system.tiemrs </param>
        void RemoveVehicleFromQueue(object source, ElapsedEventArgs e)
        {
            if (queue.Count > 1)
            {
                queue.RemoveAt(0);
                Transactions.UnservicedVehicles++;
                RemoveWaitingVehicle.Stop();
            }
        }

        /// <summary>
        /// this method is used to randomly generate a number that represents the time to remove a car
        /// </summary>
        /// <param name="min"> lower boundry of the random number </param>
        /// <param name="max"> upper boundry of the random number </param>
        /// <returns></returns>
        int RandomTimeToRemoveCar(int min, int max)
        {
            int randomRemovalTime;
            Random rng = new Random();
            randomRemovalTime = rng.Next(min, max);
            return randomRemovalTime;
        }

        /// <summary>
        /// this methid us used to randomly spawn a car
        /// </summary>
        /// <param name="min"> lower boundry of the random number </param>
        /// <param name="max"> upper boundry of the random number </param>
        /// <returns></returns>
        int RandomTimeToSpawnCar(int min, int max)
        {
            int randomSpawnTime;
            Random rng = new Random();
            randomSpawnTime = rng.Next(min, max);
            return randomSpawnTime;
        }

        /// <summary>
        /// this station constructor called within main to start the program and instantiate the pumps 
        /// </summary>
        public Station()
        {
            CreatePumps();
            d = new Display(this);

            StationTimer = new Timer();
            StationTimer.Interval = RandomTimeToSpawnCar(1500, 2201);
            StationTimer.AutoReset = true;
            StationTimer.Elapsed += GenerateVehicle;
            StationTimer.Enabled = true;
            StationTimer.Start();
        }

        /// <summary>
        /// this method is responsible for determining the number of a pump
        /// </summary>
        /// <param name="p"> a pump object for reference </param>
        /// <returns>this method returns the pump number using a calculation</returns>
        public int FindPumpNumber(Pump p)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Pumps[i, j] == p)
                    {
                        return i * 3 + j + 1;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// this method is used to assign a 'pump' object to each position of the 'Pumps' array
        /// </summary>
        public void CreatePumps()
        {         
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Pumps[i, j] = new Pump();
                    Pumps[i, j].s = this;
                }
            }
        }

        /// <summary>
        /// This method is responsible for generate a vehicle and adding it to a queue.
        /// </summary>
        public void GenerateVehicle(object source, ElapsedEventArgs e)
        {        
            // check is the queue is greater than or equal to 4 to limit the amount of cars that can wait to be serviced
            if (queue.Count <= 4) 
            {
                Random rng = new Random();
                int VehicleSelector = rng.Next(0, 3);
                Vehicle v = new Vehicle(0);
                
                switch (VehicleSelector)
                {
                    case 0:
                        v = new Car(NextVehicleID);
                    break;

                    case 1:
                        v = new Van(NextVehicleID);
                    break;

                    case 2:
                        v = new HGV(NextVehicleID);
                    break;
                }

                queue.Add(v);
                SetupWaitingVehicleTimer();
                NextVehicleID++;
                SearchForPump();
            }

            d.Show();
        }

        /// <summary>
        /// This method is responsible for searching the availiblity for a pump. Queue blocking is also incorporated within this method 
        /// </summary>
        void SearchForPump()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 2; j >= 0; j--)
                {
                    if (Pumps[i, j].CheckPumpAvailibility())
                    {
                        bool pumpBlocked = false;

                        for (int k = j; k >= 0; k--)
                        {
                            if (!Pumps[i, k].CheckPumpAvailibility())
                            {
                                pumpBlocked = true;
                            }
                        }

                        if (!pumpBlocked)
                        {                        
                            Pumps[i, j].AssignVehicleToPump(queue[0]);
                            queue.RemoveAt(0);
                            RemoveWaitingVehicle.Stop();
                            return;
                        }

                    } 
                }
            }
        }     
    }
}
