using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    // Thid makes it so we can read Double Value input
    static double ReadDouble(string prompt)
    {
        double value;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (double.TryParse(input, out value))
            {
                return value;
            }
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    // This makes it so descimals can be used in input
    static decimal ReadDecimal(string prompt)
    {
        decimal value;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (decimal.TryParse(input, out value))
            {
                return value;
            }
            Console.WriteLine("Invalid input. Please enter a valid decimal number.");
        }
    }

    static void Main()
    {
        // Calculator greeting
        Console.WriteLine("Welcome to the Console Calculator!");

        const decimal taxRate = 0.05m; // Tax rate of 5%

        bool continueRunning = true; // tells main loop keep running
        int totalCalculations = 0;  // total calculations done in session
        string lastOperation = "N/A"; // last operation performed can be reviewed

        var OperationCounts = new Dictionary<string, int>()
        {
            {"+", 0},
            {"-", 0},
            {"*", 0},
            {"/", 0},
            {"avg", 0},
            {"tax", 0}
        };

        // Start of do...while loop 
        do
        {
            // Menu display
            Console.WriteLine("\nMenus for Operation");
            Console.WriteLine("Choose an option by typing:(ex. +, -, *, /, avg, tax, exit");

            Console.WriteLine("1. Addition (+)");
            Console.WriteLine("2. Subtraction (-)");
            Console.WriteLine("3. Multiplication (*)");
            Console.WriteLine("4. Division (/)");
            Console.WriteLine("5. Average (avg)");
            Console.WriteLine("6. Calculate Tax (tax)");
            Console.WriteLine("7. Exit");

            string input = Console.ReadLine();

            if (input != null)
            {
                input = input.Trim(); // Removes any odd spaces
                input = input.ToLower(); // Makes it not case sensitive
            }

            string choice = input;

            switch (choice)
            {
                //lets user choose operation by selecting a number
                case "+":
                case "-":
                case "*":
                case "/":
                case "avg":
                    {
                        // Asks for Input from user
                        double a = ReadDouble("Enter first number: ");
                        double b = ReadDouble("Enter second number: ");
                        double result = 0;
                        bool valid = true;

                        switch (choice)
                        {
                            case "+":
                                result = a + b;
                                lastOperation = $"{a} + {b} = {result:F3}";
                                OperationCounts["+"]++;
                                break;

                            case "-":
                                result = a - b;
                                lastOperation = $"{a} - {b} = {result:F3}";
                                OperationCounts["-"]++;
                                break;

                            case "*":
                                result = a * b;
                                lastOperation = $"{a} * {b} = {result:F3}";
                                OperationCounts["*"]++;
                                break;

                            case "/":
                                if (b != 0)
                                {
                                    result = a / b;
                                    lastOperation = $"{a} / {b} = {result:F3}";
                                    OperationCounts["/"]++;
                                }
                                else
                                {
                                    Console.WriteLine("Error: Division by zero is not allowed.");
                                    lastOperation = "N/A";
                                    valid = false;
                                }
                                break;

                            case "avg":
                                result = (a + b) / 2;
                                lastOperation = $"Average of {a} and {b} = {result:F3}";
                                OperationCounts["avg"]++;
                                break;
                        }

                        if (valid)
                        {
                            Console.WriteLine($"Result: {result:F3}");
                            totalCalculations++;
                        }
                        break;
                    }

                case "tax":
                    {
                        decimal amount = ReadDecimal("Enter the amount: ");
                        decimal tax = amount * taxRate;
                        decimal totalWithTax = amount + tax;

                        Console.WriteLine($"Tax: {tax:C}");
                        Console.WriteLine($"Total with Tax: {totalWithTax:C}");

                        lastOperation = $"Amount: {amount:C}, Tax: {tax:C}, Total: {totalWithTax:C}";
                        OperationCounts["tax"]++;
                        totalCalculations++;
                        break;
                    }

                case "exit":
                    continueRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid operation.");
                    break;
            }

        } while (continueRunning);

        // Thid prints the summary
        Console.WriteLine("\n--- Summary ---");
        Console.WriteLine($"Total calculations: {totalCalculations}");
        foreach (var kvp in OperationCounts)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
        Console.WriteLine($"Last result was: {(lastOperation != "N/A" ? lastOperation : "N/A")}");
    }
}
