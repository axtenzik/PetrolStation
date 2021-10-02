using System;
using System.IO;

namespace PetrolStationConsole
{
    /// <summary>
    /// Class for writing files that the data will be outputted to
    /// </summary>
    class Log
    {
        /// <summary>
        /// Creates a folder in documents if it doesn't already exist and then creates the files in the folder 
        /// </summary>
        public static void Create()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            docPath = Path.Combine(docPath, "Petrol_Station");

            Directory.CreateDirectory(docPath);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Log.csv")))
            {
                //outputFile.WriteLine("Price Unleaded,Price LPG,Price Diesel,Commission");
                //outputFile.WriteLine("{0},{1},{2},{3}", Output.priceUnleaded, Output.priceLPG, Output.priceDiesel, Output.commission);
                for (int i = 0; i < Fuel.fuels.Count; i++)
                {
                    Fuel f = Fuel.fuels[i];
                    outputFile.Write("Price {0},", f.type);
                }
                outputFile.WriteLine("Commission");

                for (int i = 0; i < Fuel.fuels.Count; i++)
                {
                    Fuel f = Fuel.fuels[i];
                    outputFile.Write("{0},", f.price);
                }
                outputFile.WriteLine("{0}", Output.commission);
                outputFile.WriteLine("Pump Number,Vehicle ID,Vehicle Type,Fuel type,Total dispensed Fuel,Vehicle Generation Timestamp");
            }

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "OverflowLog.csv")))
            {
                //outputFile.WriteLine("Price Unleaded,Price LPG,Price Diesel,Commission");
                //outputFile.WriteLine("{0},{1},{2},{3}", Output.priceUnleaded, Output.priceLPG, Output.priceDiesel, Output.commission);
                //outputFile.WriteLine("Vehicle ID,VehicleType,Fuel type,Total dispensed Fuel,Pump Number");
                for (int i = 0; i < Fuel.fuels.Count; i++)
                {
                    Fuel f = Fuel.fuels[i];
                    outputFile.Write("Price {0},", f.type);
                }
                outputFile.WriteLine("Commission");

                for (int i = 0; i < Fuel.fuels.Count; i++)
                {
                    Fuel f = Fuel.fuels[i];
                    outputFile.Write("{0},", f.price);
                }
                outputFile.WriteLine("{0}", Output.commission);
                outputFile.WriteLine("Pump Number,Vehicle ID,Vehicle Type,Fuel type,Total dispensed Fuel,Vehicle Generation Timestamp");
            }

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Impatient_Vehicles.csv")))
            {
                outputFile.WriteLine("Pump Number,Vehicle ID,Vehicle Type,Fuel type,Total dispensed Fuel,Vehicle Generation Timestamp");
            }
        }
        
        /// <summary>
        /// Adds the serviced vehicle to the log
        /// </summary>
        /// <param name="v">The serviced vehicle</param>
        /// <param name="pN">Pump number</param>
        public static void Add(Vehicle v, int pN)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            docPath = Path.Combine(docPath, "Petrol_Station");

            //Sometimes 2 vehicles try logging at the same time, so the program trys opening the log file to write to it
            //while the program is still writing to it previously.
            //Its possible that 3 could leave at the same time and this fix wont catch that but I've decided that would
            //be too unlikely to happen.
            try
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Log.csv"), true))
                {
                    outputFile.WriteLine("{0},{1},{2},{3},{4},{5}", pN, v.vehicleID, v.vehicleType, v.fuelType, v.fuelTime, v.genTime);
                }
            }
            catch
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "OverflowLog.csv"), true))
                {
                    outputFile.WriteLine("{0},{1},{2},{3},{4},{5}", pN, v.vehicleID, v.vehicleType, v.fuelType, v.fuelTime, v.genTime);
                    //If I did change it for 3 vehicles I'd just have a variable called "unlogged vehicles that it would add to
                    //I chose this instead because of the unlikelyhood of it and this way still logs the vehicle
                }
            }
        }

        /// <summary>
        /// Creates log for the impatient vehicles that leave
        /// </summary>
        /// <param name="v">The impatient vehicle</param>
        public static void Impatient(Vehicle v)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            docPath = Path.Combine(docPath, "Petrol_Station");

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Impatient_Vehicles.csv"), true))
            {
                outputFile.WriteLine("n/a,{0},{1},{2},n/a,{3}", v.vehicleID, v.vehicleType, v.fuelType, v.genTime);
            }
        }

        /// <summary>
        /// Adds the totals from all the counters
        /// </summary>
        public static void Close()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            docPath = Path.Combine(docPath, "Petrol_Station");

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Total_Earnings.csv")))
            {
                for (int i = 0; i < Fuel.fuels.Count; i++)
                {
                    Fuel f = Fuel.fuels[i];
                    outputFile.WriteLine("Price of {0}:,{1}", f.type, f.price);
                }

                //outputFile.WriteLine("Total litres dispensed,Total Unleaded Earnings,Total LPG Earnings,Total Diesel Earnings,Total earnings,Total commission,Serviced vehicles,Impatient vehicles");
                //outputFile.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8}", Output.totalLitres, Output.totalUnleaded, Output.totalLPG, Output.totalDiesel, Output.totalEarn, Output.totalCommission, Output.servicedVehicles, Output.impatient);
                /*outputFile.WriteLine("Price of Unleaded:,{0}", Output.priceUnleaded);
                outputFile.WriteLine("Price of LPG:,{0}", Output.priceLPG);
                outputFile.WriteLine("Price of Diesel:,{0}", Output.priceDiesel);
                outputFile.WriteLine("");*/
                outputFile.WriteLine("");
                outputFile.WriteLine("Total litres dispensed:,{0}", Output.totalLitres);
                outputFile.WriteLine("Total earnings:,{0}", Output.totalEarn);
                outputFile.WriteLine("Total commission earned:,{0}", Output.totalCommission);
                outputFile.WriteLine("Total serviced vehicles:,{0}", Output.servicedVehicles);
                outputFile.WriteLine("Total unserviced vehicles:,{0}", Output.impatient);
                outputFile.WriteLine("");

                for (int i = 0; i < Fuel.fuels.Count; i++)
                {
                    Fuel f = Fuel.fuels[i];
                    outputFile.WriteLine("Total {0} dispensed:,{1}", f.type, f.total);
                    outputFile.WriteLine("Total earnings from {0}:,{1}", f.type, f.earnings);
                }

                /*outputFile.WriteLine("Total unleaded dispensed:,{0}", Output.totalUnleaded);
                outputFile.WriteLine("Total earnings from unleaded:,{0}", Output.earnUnleaded);
                outputFile.WriteLine("Total LPG dispensed:,{0}", Output.totalLPG);
                outputFile.WriteLine("Total earnings from LPG:,{0}", Output.earnLPG);
                outputFile.WriteLine("Total diesel dispensed:,{0}", Output.totalDiesel);
                outputFile.WriteLine("Total earnings from diesel:,{0}", Output.earnDiesel);*/
            }
        }
    }
}
