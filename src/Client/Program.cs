using MarsRovers.DependencyResolution;
using MarsRovers.Interfaces;
using MarsRovers.Models;
using MarsRovers.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarsRovers
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            var serviceProvider = ServicesRegistry
                .Register(serviceCollection)
                .BuildServiceProvider();

            Console.WriteLine("Enter the grid bounds eg.(5 5):");
            string gridBounds = Console.ReadLine();

            bool isCommandComplete = false;            
            List<NavigationCommandModel> commandModels = new List<NavigationCommandModel>();

            while (!isCommandComplete)
            {                
                Console.WriteLine("Enter the rover's coordinates eg.(1 1 N):");
                string  coordinates = Console.ReadLine();
                Console.WriteLine("Enter the rover's instructions eg.(LMLMLMMM):");
                string instructions = Console.ReadLine();
                var result = NavigationCommandBuilder.Get()
                    .WithGridBounds(gridBounds)
                    .WithCurrentPosition(coordinates)
                    .WithCommands(instructions)
                    .Build();

                if (!result.IsSuccess)
                {
                    Console.WriteLine($"There was an error trying to build the command: { result.Message }");
                    continue;
                }

                commandModels.Add(result.Value);

                Console.WriteLine("Press (Esc) to finish or <Enter> to add an other command.");
                bool waitingForUser = true;
                while (waitingForUser)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        waitingForUser = false;
                        isCommandComplete = true;
                        continue;
                    }

                    if (key.Key == ConsoleKey.Enter)
                    {
                        waitingForUser = false;
                        continue;
                    }
                }
            }

            List<Task> commandTasks = new List<Task>();

            foreach (var command in commandModels)
            {
                var rover = serviceProvider.GetService<IRoverService>();
                commandTasks.Add(rover.ExecuteCommand(command));
            }

            var task = Task.WhenAll(commandTasks);

            task.Wait();
        }
    }
}
