using System;
using System.Collections.Generic;

namespace PetrolStationConsole
{
    /// <summary>
    /// The class for dealing with the fuels
    /// </summary>
    class Fuel
    {
        /// <summary>
        /// The list of the different fuels
        /// </summary>
        public static List<Fuel> fuels;
        public string type;
        public double price;
        public int total;
        public double earnings;

        /// <summary>
        /// Creates all the different fuels
        /// </summary>
        public static void Create()
        {
            fuels = new List<Fuel>();

            string[] types = { "Diesel", "LPG", "Unleaded" };
            double[] prices = { 1.40, 1.30, 1.20 };
            for (int i =0; i < types.Length; i++)
            {
                Fuel f = new Fuel(types[i], prices[i]);
                fuels.Add(f);
            }
        }

        /// <summary>
        /// The constuctor for each individual fuel type
        /// </summary>
        /// <param name="f">fuel type</param>
        /// <param name="p">fuel price</param>
        public Fuel(string f, double p)
        {
            type = f;
            price = p;
            total = 0;
            earnings = 0;
        }
    }
}
