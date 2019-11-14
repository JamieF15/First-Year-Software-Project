using System;
using System.Collections.Generic;
using System.Text;

namespace IntroToProgrammingAssignmentV2
{
    class Transactions
    {
        // attributes
        public static int VehiclesServiced { get; set; }
        public static int UnservicedVehicles { get; set; }
        public float LitresTaken { get; set; }
        public int PumpNumber { get; set; }
        public float UnleadedDispensed { get; set; }
        public float DieselDispensed { get; set; }
        public float LPGDispensed { get; set; }
        public float UnleadedDispensedThisTransaction { get; set; }
        public float DieselDispensedThisTransaction { get; set; }
        public float LPGDispensedThisTransaction { get; set; }
        public static float TotalMoneyMade { get; set; }
        public Vehicle vehicle { get; set; }

        /// <summary>
        /// this contructor represents the structure of a transaction
        /// </summary>
        /// <param name="_litresTaken"> represents the litres taken </param>
        /// <param name="_pumpNumber"> represents the pump number </param>
        /// <param name="_vehicle"> represents the particular vehicle that has been processed </param>
        /// <param name="_unleadedDispensed"> represents the amount of unleaded fuel dispensed </param>
        /// <param name="_dieselDispensed"> represents the amount of diesel fuel dispensed </param>
        /// <param name="_LPGDispensed"> represents the amount of LPG fuel dispensed </param>
        /// <param name="_unleadedUsedThisTransaction"> represents the amount of unleaded fuel dispnesed in a particular transaction </param>
        /// <param name="_dieselUsedThisTransaction"> represents the amount of diesel fuel dispnesed in a particular transaction </param>
        /// <param name="_LPGUsedThisTransaction"> represents the amount of LPG fuel dispnesed in a particular transaction </param>
        /// <param name="_totalMoneyMade"> represents the amount of money made in total </param>
        public Transactions(float _litresTaken, int _pumpNumber, Vehicle _vehicle, float _unleadedDispensed, float _dieselDispensed, float _LPGDispensed, float _unleadedUsedThisTransaction, float _dieselUsedThisTransaction, float _LPGUsedThisTransaction, float _totalMoneyMade)
        {
            VehiclesServiced++;
            LitresTaken += _litresTaken;
            PumpNumber = _pumpNumber;
            vehicle = _vehicle;
            UnleadedDispensed = _unleadedDispensed;
            DieselDispensed = _dieselDispensed;
            LPGDispensed = _LPGDispensed;
            UnleadedDispensedThisTransaction = _unleadedUsedThisTransaction;
            DieselDispensedThisTransaction = _dieselUsedThisTransaction;
            LPGDispensedThisTransaction = _LPGUsedThisTransaction;
            TotalMoneyMade += _totalMoneyMade;
        }
    }
}
