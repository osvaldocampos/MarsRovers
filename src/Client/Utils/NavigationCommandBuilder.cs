using System;
using System.Collections.Generic;
using System.Text;
using MarsRovers.Models;

namespace MarsRovers.Utils
{
    public class NavigationCommandBuilder
    {
        private string _gridBounds;
        private string _currentPosition;
        private List<char> _validZPositions => new List<char>() { 'N', 'E', 'S', 'W' };
        private List<char> _validInstructions => new List<char>() { 'L', 'R', 'M'};
        protected NavigationCommandModel NavigationCommand { set; get; }

        private NavigationCommandBuilder() 
        {
            NavigationCommand = new NavigationCommandModel();
        }

        public static NavigationCommandBuilder Get() => new NavigationCommandBuilder();

        public NavigationCommandBuilder WithGridBounds(string gridBounds)
        {            
            _gridBounds = gridBounds;
            return this;
        }

        public NavigationCommandBuilder WithCurrentPosition(string currentPosition)
        {
            _currentPosition = currentPosition;
            return this;
        }

        public NavigationCommandBuilder WithCommands(string command)
        {
            NavigationCommand.Instructions = command;
            return this;
        }

        public Result<NavigationCommandModel> Build()
        {
            try
            {
                var gridBoundsValues = _gridBounds.Split(' ');
                var currentPositionValues = _currentPosition.Split(' ');

                int x_Bounds = int.Parse(gridBoundsValues[0]);
                int y_Bounds = int.Parse(gridBoundsValues[1]);

                NavigationCommand.X_Position = int.Parse(currentPositionValues[0]);
                NavigationCommand.Y_Position = int.Parse(currentPositionValues[1]);
                NavigationCommand.Z_Position = char.Parse(currentPositionValues[2]);

                if (this.NavigationCommand.X_Position > x_Bounds)
                    return Result<NavigationCommandModel>.Fail($"Current X ({ this.NavigationCommand.Z_Position }) position is greater than X Bounds ({ x_Bounds })");

                if (this.NavigationCommand.Y_Position > y_Bounds)
                    return Result<NavigationCommandModel>.Fail($"Current Y ({ this.NavigationCommand.Y_Position }) position is greater than Y Bounds ({ y_Bounds })");

                if (!_validZPositions.Contains(this.NavigationCommand.Z_Position))
                    return Result<NavigationCommandModel>.Fail($"Invalid value for current Z ({ this.NavigationCommand.Z_Position }) position");

                bool isValidInstruction = true;

                Array.ForEach(NavigationCommand.Instructions.ToCharArray(), x => {
                    if (!_validInstructions.Contains(x))
                    {
                        isValidInstruction = false;
                    }
                });

                if (!isValidInstruction)
                {
                    return Result<NavigationCommandModel>.Fail($"Invalid command");
                }

                return Result<NavigationCommandModel>.Ok(this.NavigationCommand);
            }
            catch
            {
                return Result<NavigationCommandModel>.Fail($"Invalid arguments");
            }


        }
    }
}
