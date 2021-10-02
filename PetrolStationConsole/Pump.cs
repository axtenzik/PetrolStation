using System;
using System.Collections.Generic;
using System.Timers;

namespace PetrolStationConsole
{
    /// <summary>
    /// Includes everything to do with the pumps
    /// </summary>
    class Pump
    {
        public static int nol = 3; //short for number of lanes, default set to 3
        public static int ppl = 3; //short for pumps per lane, default set to 3
        /// <summary>
        /// The list that contains all the pumps
        /// </summary>
        public static List<Pump> pumps;
        public Vehicle servicing;
        private static int newPumpNumber;
        public int pumpNumber;
        public int lane;
        public bool availability;
        private readonly double dispense;


        /// <summary>
        /// The constructor for each pump
        /// </summary>
        /// <param name="column">column is the number of lanes</param>
        public Pump(int column)
        {
            pumpNumber = newPumpNumber++;
            lane = column;
            availability = true;
            dispense = 1.5; //litres per second
        }

        /// <summary>
        /// Creates the pumps
        /// </summary>
        public static void Create()
        {
            pumps = new List<Pump>();
            Pump p;

            for (int i = 0; i < nol; i++) //how many lanes of pumps
            {
                for (int j = 0; j < ppl; j++) //how many pumps per lane
                {
                    p = new Pump(i);
                    pumps.Add(p);
                }
            }
        }

        /// <summary>
        /// The method that selects the pump the vehicle will be assigned to
        /// </summary>
        public static void Assign()
        {
            Vehicle v;
            Pump p;

            //checks to see if any vehicles in list
            //will always be at least one here but doesn't hurt to have it
            if (Vehicle.vehicles.Count == 0)
            {
                return;
            }

            // check first pump in lane, if it is not available move to next lane
            // check next pump in lane, if it is not available add vehicle to previous pump
            // check if last pump in lane, if available add vehicle
            for (int i = 0; i < (nol * ppl); i++)
            {
                p = pumps[i];

                if (p.availability == false && p.pumpNumber % ppl == 0)
                {
                    i = i + (ppl - 1); // skip to the next lane
                    // add the number of pumps per lane here - 1
                    // then the loop adds 1 as it iterates
                }
                else if (p.availability == false)
                {
                    i = i - 1; //go to the previous pump
                    v = Vehicle.vehicles[0];
                    v.wTimer.Enabled = false;
                    Vehicle.vehicles.RemoveAt(0);
                    pumps[i].Occupy(v);
                    pumps[i].availability = false;
                    i = -1; // loop will add one to make it 0
                }
                else if (p.pumpNumber % ppl == ppl - 1)
                {
                    v = Vehicle.vehicles[0];
                    v.wTimer.Enabled = false;
                    Vehicle.vehicles.RemoveAt(0);
                    p.Occupy(v);
                    p.availability = false;
                    i = -1;
                }

                //Checks to see if any vehicles are left queuing
                if (Vehicle.vehicles.Count == 0)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Changes the pump so it is occupied by a vehicle
        /// </summary>
        /// <param name="v">The vehicle being added</param>
        public void Occupy(Vehicle v)
        {
            servicing = v;

            Timer pTimer = new Timer();
            pTimer.Interval = (v.fuelTime / dispense) * 1000; //Total fuel needed / the dispense rate * change to milliseconds
            pTimer.AutoReset = false;
            pTimer.Elapsed += Unoccupy;
            pTimer.Enabled = true;
            pTimer.Start();
        }

        /// <summary>
        /// Removes the vehicle once it has finished fuelling up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Unoccupy(object sender, ElapsedEventArgs e)
        {
            /*Output.totalLitres += servicing.fuelTime;

            if (servicing.fuelType == "Unleaded")
            {
                Output.totalUnleaded += servicing.fuelTime;
            }
            else if (servicing.fuelType == "LPG")
            {
                Output.totalLPG += servicing.fuelTime;
            }
            else if (servicing.fuelType == "Diesel")
            {
                Output.totalDiesel += servicing.fuelTime;
            }
            else
            {
                Output.unknownFuel += servicing.fuelTime;
            }*/

            for (int i = 0; i < Fuel.fuels.Count; i++)
            {
                Fuel f = Fuel.fuels[i];
                if (servicing.fuelType == f.type)
                {
                    f.total += servicing.fuelTime;
                    Output.totalLitres += servicing.fuelTime;
                    f.earnings = f.total * f.price;
                }
            }

            Output.servicedVehicles++;
            //Log.Add(servicing.genTime, servicing.vehicleID, servicing.vehicleType, servicing.fuelType, servicing.fuelTime, pumpNumber); //Send object!!!
            Log.Add(servicing, pumpNumber);
            Vehicle.leaving.Add(servicing);
            servicing = null;
            availability = true;
        }
    }
}
