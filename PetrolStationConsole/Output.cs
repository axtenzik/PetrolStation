using System;

namespace PetrolStationConsole
{
    /// <summary>
    /// Used for displaying onto the console and has all the counters
    /// </summary>
    class Output
    {
        public static int totalLitres;
        public static double totalEarn;
        public static double commission = 0.01;
        public static double totalCommission;
        public static int impatient;
        public static int servicedVehicles;
        private static int width = Console.WindowWidth;

        /// <summary>
        /// The method that draws everything
        /// </summary>
        public static void Draw()
        {
            //I recently rewrote this whole method so i can output a road and little car symbols
            //as I've done this close to submission time I haven't been able to clean it up or comment it
            //still not 100% implemented how i want but it does display everything needed.
            //Final version TBD
            if (width == Console.WindowWidth)
            {
                Console.SetCursorPosition(0, 0);
            }
            else
            {
                Console.Clear();
                width = Console.WindowWidth;
            }

            Pump p;

            totalEarn = 0;
            for (int i = 0; i < Fuel.fuels.Count; i++)
            {
                Fuel f = Fuel.fuels[i];
                totalEarn += f.earnings;
                totalEarn = Math.Floor(totalEarn * 100) / 100;
            }
            totalCommission = Math.Floor(totalEarn * commission * 100) / 100;

            string[] counters = new string[7];
            counters[0] = string.Format("Total dispensed : {0, -7}", totalLitres);
            counters[1] = string.Format("Total earnings  : {0, -7}", totalEarn);
            counters[2] = string.Format("Total commission: {0, -7}", totalCommission);
            counters[3] = string.Format("Served vehicles : {0, -7}", servicedVehicles);
            counters[4] = string.Format("Total unserved  : {0, -7}", impatient);
            counters[5] = string.Format("Transaction list found in");
            counters[6] = string.Format("  My documents           ");

            Console.WriteLine("Car: ▄██▄ | Van: ▄███ | HGV: ▄▐███ | Bike: ▄▄");

            bool tooLarge = true;
            int displayPump = Pump.ppl;
            while (tooLarge && displayPump > 1)
            {
                if (Console.WindowWidth < 6 * (2 * displayPump - 1) + 18)
                {
                    displayPump--;
                }
                else
                {
                    tooLarge = false;
                }
            }

            //Motorway
            string s = "";
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                s += string.Format("═");
            }
            s += string.Format("\n");
            for (int i = 0; i < Console.WindowWidth - 3; i = i + 3)
            {
                if ((i / 3) % 2 == 0)
                {
                    s += string.Format("───");
                }
                else
                {
                    s += string.Format("   ");
                }
            }
            s += string.Format("\n\n══╗     ╔");
            for (int i = 0; i < 6 * (2 * displayPump - 1); i++)
            {
                s += string.Format("═");
            }
            s += string.Format("╗     ╔");
            for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 16; j++)
            {
                s += string.Format("═");
            }
            Console.Write(s);

            //queues
            int position = 0;
            for (int i = 0; i < 9; i++)
            {
                if (i % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("██");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("║");
                    if (Vehicle.leaving.Count <= position)
                    {
                        Console.Write("     ");
                    }
                    else
                    {
                        Console.Write("{0}", Vehicle.leaving[position].icon);
                    }
                    Console.Write("║");
                    for (int j = 0; j < 6 * (2 * displayPump - 1); j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("█");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write("║");
                    if ((4- position) >= Vehicle.vehicles.Count)
                    {
                        Console.Write("     ");
                    }
                    else
                    {
                        Console.Write("{0}", Vehicle.vehicles[4 - position].icon);
                    }
                    Console.Write("║");
                    for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 16; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("█");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    position++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("██");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("║     ║");
                    for (int j = 0; j < 6 * (2 * displayPump - 1); j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("█");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write("║     ║");
                    for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 16; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("█");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }

            //station
            int counter = 0;
            string u = "";
            u += string.Format(" ╔╝     ╚");
            for (int i = 0; i < 6 * (2 * displayPump - 1); i++)
            {
                u += string.Format("═");
            }
            u += string.Format("╝     ╚");
            if (displayPump == Pump.ppl)
            {
                u += string.Format("╗");
            }
            else
            {
                u += string.Format("═");
            }
            if (Console.WindowWidth - 6 * (2 * displayPump - 1) - 17 > 25 && counter < counters.Length)
            {
                u += counters[counter];
                counter++;
                for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 42; j++)
                {
                    u += string.Format(" ");
                }
            }
            else
            {
                for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 17; j++)
                {
                    u += string.Format(" ");
                }
            }

            u += string.Format(" ║");
            for (int i = 0; i < 6 * (2 * displayPump - 1) + 14; i++)
            {
                u += string.Format(" ");
            }
            if (displayPump == Pump.ppl)
            {
                u += string.Format("║");
            }
            else
            {
                u += string.Format(" ");
            }
            if (Console.WindowWidth - 6 * (2 * displayPump - 1) - 17 > 25 && counter < counters.Length)
            {
                u += counters[counter];
                counter++;
                for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 42; j++)
                {
                    u += string.Format(" ");
                }
            }
            else
            {
                for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 17; j++)
                {
                    u += string.Format(" ");
                }
            }

            int up;
            int down = Pump.pumps.Count - 1;
            for (int i = 0; i < Pump.nol; i++)
            {
                up = 0;
                u += string.Format(" ║");
                u += string.Format("       ");
                for (int j = 0; j < Pump.ppl; j++)
                {
                    if (up < displayPump)
                    {
                        u += string.Format("┌────┐");
                        u += string.Format("      ");
                        up++;
                    }
                }
                if (displayPump == Pump.ppl)
                {
                    u += string.Format(" ║");
                }
                else
                {
                    u += string.Format("  ");
                }
                if (Console.WindowWidth - 6 * (2 * displayPump - 1) - 17 > 25 && counter < counters.Length)
                {
                    u += counters[counter];
                    counter++;
                    for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 42; j++)
                    {
                        u += string.Format(" ");
                    }
                }
                else
                {
                    for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 17; j++)
                    {
                        u += string.Format(" ");
                    }
                }

                up = 0;
                u += string.Format(" ║");
                u += string.Format("       ");
                for (int j = 0; j < Pump.ppl; j++)
                {
                    if (up < displayPump)
                    {
                        u += string.Format("└────┘");
                        u += string.Format("      ");
                        up++;
                    }
                }
                if (displayPump == Pump.ppl)
                {
                    u += string.Format(" ║");
                }
                else
                {
                    u += string.Format("  ");
                }
                if (Console.WindowWidth - 6 * (2 * displayPump - 1) - 17 > 25 && counter < counters.Length)
                {
                    u += counters[counter];
                    counter++;
                    for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 42; j++)
                    {
                        u += string.Format(" ");
                    }
                }
                else
                {
                    for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 17; j++)
                    {
                        u += string.Format(" ");
                    }
                }

                up = 0;
                u += string.Format(" ║");
                u += string.Format("       ");
                for (int j = 0; j < Pump.ppl; j++)
                {
                    if (up < displayPump)
                    {
                        p = Pump.pumps[down];
                        if (p.servicing == null)
                        {
                            u += string.Format("      ");
                        }
                        else
                        {
                            p = Pump.pumps[down];
                            u += string.Format(" {0}", p.servicing.icon);
                        }
                        u += string.Format("      ");
                        down--;
                        up++;
                    }
                    else
                    {
                        down--;
                    }
                }
                if (displayPump == Pump.ppl)
                {
                    u += string.Format(" ║");
                }
                else
                {
                    u += string.Format("  ");
                }
                if (Console.WindowWidth - 6 * (2 * displayPump - 1) - 17 > 25 && counter < counters.Length)
                {
                    u += counters[counter];
                    counter++;
                    for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 42; j++)
                    {
                        u += string.Format(" ");
                    }
                }
                else
                {
                    for (int j = 0; j < Console.WindowWidth - 6 * (2 * displayPump - 1) - 17; j++)
                    {
                        u += string.Format(" ");
                    }
                }
            }
            u += string.Format(" ╚");
            for (int i = 0; i < 6 * (2 * displayPump - 1) + 14; i++)
            {
                u += string.Format("═");
            }
            if (displayPump == Pump.ppl)
            {
                u += string.Format("╝ ");
            }
            else
            {
                u += string.Format("═");
            }
            Console.Write(u);

            if (Vehicle.leaving.Count > 0)
            {
                Vehicle.leaving.RemoveAt(0);
            }
            if (Vehicle.leaving.Count > 5)
            {
                while (Vehicle.leaving.Count > 5)
                {
                    Vehicle.leaving.RemoveAt(0);
                }
            }
        }
    }
}
