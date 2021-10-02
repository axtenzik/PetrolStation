using System;
using System.Collections.Generic;
using System.Timers;

namespace PetrolStationConsole
{
    /// <summary>
    /// Includes everything that is related to the vehicles
    /// </summary>
    class Vehicle
    {
        /// <summary>
        /// The queue for vehicles waiting for a pump
        /// </summary>
        public static List<Vehicle> vehicles;
        /// <summary>
        /// A queue for merging with the motorway again
        /// </summary>
        public static List<Vehicle> leaving;
        private static int newVehicleID;
        public int vehicleID;
        public string genTime;
        public string vehicleType;
        public string fuelType;
        public string icon;
        public int tankSize; //In litres
        private readonly int currentFuel; //In litres
        public int fuelTime; //The amount of fuel needed to fill the tank.
        private readonly int patience;
        private static readonly Random rnd = new Random();
        private static readonly Random rand = new Random();
        public Timer wTimer;
        private static Timer vTimer;

        /// <summary>
        /// The constructor for each vehicle
        /// </summary>
        public Vehicle()
        {
            Fuel f;
            
            vehicleID = newVehicleID++;
            genTime = DateTime.Now.ToString("ss.fff");
            string[] typeVehicle = { "Car", "Van", "HGV", "Bike" }; // if adding a vehicle, keep name <= 5 characters
            vehicleType = typeVehicle[rand.Next(0, typeVehicle.Length)];
            switch (vehicleType)
            {
                case "HGV":
                    f = Fuel.fuels[0];
                    fuelType = f.type;
                    icon = "▄▐███";
                    tankSize = 150;
                    currentFuel = rand.Next(0, tankSize / 4);
                    patience = rand.Next(1000, 2001); //In milliseconds
                    break;
                case "Van":
                    //fuelType = typeFuel[rand.Next(0, 2)];
                    f = Fuel.fuels[rand.Next(0, 2)];
                    fuelType = f.type;
                    icon = "▄███ ";
                    tankSize = 80;
                    currentFuel = rand.Next(0, tankSize / 4);
                    patience = rand.Next(1000, 2001);
                    break;
                case "Car":
                    //fuelType = typeFuel[rand.Next(0, 3)];
                    f = Fuel.fuels[rand.Next(0, 3)];
                    fuelType = f.type;
                    icon = "▄██▄ ";
                    tankSize = 40;
                    currentFuel = rand.Next(0, tankSize / 4);
                    patience = rand.Next(1000, 2001);
                    break;
                case "Bike":
                    //fuelType = typeFuel[2];
                    f = Fuel.fuels[2];
                    fuelType = f.type;
                    icon = " ▄▄  ";
                    tankSize = 15;
                    currentFuel = rand.Next(0, tankSize / 4);
                    patience = rand.Next(1000, 2001);
                    break;
                default://added in case typeVehicle is extended but a case for it is not added
                    //fuelType = typeFuel[0];
                    f = Fuel.fuels[0];
                    fuelType = f.type;
                    icon = "▄▐███";
                    tankSize = 40;
                    currentFuel = rand.Next(0, tankSize / 4);
                    patience = rand.Next(1000, 2001);
                    break;
            }
            fuelTime = tankSize - currentFuel;
            wTimer = new Timer(patience); //Timer for how long the vehicle will wait
            wTimer.AutoReset = false;
            wTimer.Elapsed += Check;
            wTimer.Enabled = false;

        }

        /// <summary>
        /// Creates the timer for creating the vehicles
        /// </summary>
        public static void Create()
        {
            vehicles = new List<Vehicle>();
            leaving = new List<Vehicle>();

            //Random rnd = new Random();
            vTimer = new Timer(rnd.Next(1500, 2201));//creates number used in time interval for generating cars
            vTimer.AutoReset = true;
            vTimer.Elapsed += NewVehicle;
            vTimer.Enabled = true;
            vTimer.Start();
        }

        /// <summary>
        /// Creates a vehicle and adds it to the queue if the queue is less than 5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void NewVehicle(object sender, ElapsedEventArgs e)
        {
            vTimer.Interval = rnd.Next(1500, 2201);
            if (vehicles.Count < 5)
            {
                Vehicle v = new Vehicle();
                vehicles.Add(v);
                v.wTimer.Enabled = true; //Starts the vehicle wait timer
                v.wTimer.Start();
                Output.Draw();
            }
        }

        /// <summary>
        /// Removed the vehicle from the queue if it has waited too long
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check(object sender, ElapsedEventArgs e)
        {
            //int number = vehicleID;
            Vehicle v = this;
            Pump.Assign();
            //vehicles.RemoveAll(x => vehicleID == number); //This is a different way to remove the vehicle
            bool removed = vehicles.Remove(this);
            if (removed)
            {
                Output.impatient++;
                Log.Impatient(v);
                leaving.Add(v);
            }
        }
    }
}
