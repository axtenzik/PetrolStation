using System;

namespace PetrolStationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Fuel.Create();
            bool valid = true;

            while (valid)
            {
                //gives the user a choice and asks for an input
                Console.Clear();
                Console.WriteLine("PLease Select either:");
                Console.WriteLine("1 - Run the program");
                Console.WriteLine("2 - Change configuration");
                //Load configuartion?
                Console.WriteLine("X - Exit the program");
                string userInput = Console.ReadLine();

                //Sees if user input was valid and then runs what was chosen
                if (userInput == "1")
                {
                    Pump.Create();
                    Vehicle.Create();
                    Log.Create();
                    Console.Clear();
                    Output.Draw();
                    Console.ReadLine();
                    Log.Close();
                    valid = false;
                }
                else if (userInput == "2")
                {
                    Config();
                }
                else if (userInput == "X" || userInput == "x")
                {
                    valid = false;
                }
                else
                {
                    Console.WriteLine("Invalid input, hit enter");
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Changes the configuration of the program. Can change the amount of pumps and the prices/ commission.
        /// </summary>
        private static void Config()
        {
            bool config = true;
            while (config)
            {
                //asks the user to choose and input
                Console.Clear();
                Console.WriteLine("What would you like to change?");
                Console.WriteLine("1 - How many pump lanes there are");
                Console.WriteLine("2 - How many pumps per lane");
                Console.WriteLine("3 - The price of the fuels");
                Console.WriteLine("4 - The commission charge");
                Console.WriteLine("X - Nothing more, return to program");
                string userInput = Console.ReadLine();
                
                bool successful = false;
                switch (userInput)
                {
                    case "1":
                        do
                        {
                            //Sets how many lanes of pumps there are. Also tests to see if what was inputted is valid
                            Console.Clear();
                            Console.WriteLine("How many lanes are there? (up to 10)");
                            successful = int.TryParse(Console.ReadLine(), out Pump.nol);
                            if (Pump.nol < 1 || Pump.nol > 10)
                            {
                                successful = false;
                                Console.WriteLine("Invalid input, hit enter");
                                Console.ReadLine();
                            }
                        } while (!successful);
                        break;
                    case "2":
                        do
                        {
                            //Sets how many pumps are in each lane. Also tests to see if what was inputted is valid
                            Console.Clear();
                            Console.WriteLine("How many pumps are there per lane? (up to 10)");
                            successful = int.TryParse(Console.ReadLine(), out Pump.ppl);
                            if (Pump.ppl < 1 || Pump.ppl > 10)
                            {
                                successful = false;
                                Console.WriteLine("Invalid input, hit enter");
                                Console.ReadLine();
                            }
                        } while (!successful);
                        break;
                    case "3":
                        do
                        {
                            //Sets the price of the fuels. Also tests to see if what was inputted is valid
                            for (int i = 0; i < Fuel.fuels.Count; i++)
                            {
                                Fuel f = Fuel.fuels[i];
                                Console.Clear();
                                Console.WriteLine("Fuel {0} of {1}", i, Fuel.fuels.Count);
                                Console.WriteLine("What is the price of {0} per litre?", f.type);
                                successful = double.TryParse(Console.ReadLine(), out f.price);
                            }

                            /*Console.Clear();
                            Console.WriteLine("What is the price of {0} per litre?");
                            successful = double.TryParse(Console.ReadLine(), out Output.priceUnleaded);*/
                        } while (!successful);
                        break;
                    case "4":
                        do
                        {
                            //Sets the commission the employees earn. Also tests to see if what was inputted is valid
                            Console.Clear();
                            Console.WriteLine("What is the commission that employees earn? (enter a number between 0.01 to 1)");
                            successful = double.TryParse(Console.ReadLine(), out Output.commission);
                            if (Output.commission < 0.01 || Output.commission > 1)
                            {
                                successful = false;
                                Console.WriteLine("Invalid input, hit enter");
                                Console.ReadLine();
                            }
                        } while (!successful);
                        break;
                    case "X":
                        config = false;
                        break;
                    case "x":
                        config = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input, hit enter");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
