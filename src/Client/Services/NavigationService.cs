using MarsRovers.Interfaces;
using MarsRovers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarsRovers.Services
{
    public class NavigationService : INavigationService
    {
        private NavigationCommandModel _navigationCommandModel;
        private List<char> _cardinalPositions => new List<char> { 'N', 'E', 'S', 'W' };
        public NavigationService()
        {
        }

        public async Task<NavigationCommandModel> Navigate(NavigationCommandModel navigationCommand)
        {
            _navigationCommandModel = navigationCommand;
            var instructions = navigationCommand.Instructions.ToCharArray();
            await Task.Run(() =>
            {
                Array.ForEach(instructions, x =>
                {
                    if (x == 'L') TurnLeft();
                    if (x == 'R') TurnRight();
                    if (x == 'M') Move();
                });
            });

            Console.WriteLine($"Output position...");
            Console.WriteLine($"{ _navigationCommandModel.X_Position } { _navigationCommandModel.Y_Position } { _navigationCommandModel.Z_Position }");

            return _navigationCommandModel;
        }

        private void TurnLeft()
        {
            _navigationCommandModel.Z_Position = getNextCardinalPosition(_navigationCommandModel.Z_Position, 'L');
        }

        private void TurnRight()
        {
            _navigationCommandModel.Z_Position = getNextCardinalPosition(_navigationCommandModel.Z_Position, 'R');
        }

        private void Move()
        {
            switch (_navigationCommandModel.Z_Position)
            {
                case 'N':
                    _navigationCommandModel.Y_Position++;
                    break;
                case 'E':
                    _navigationCommandModel.X_Position++;
                    break;
                case 'S':
                    _navigationCommandModel.Y_Position--;
                    break;
                case 'W':
                    _navigationCommandModel.X_Position--;
                    break;
            }            
        }

        private char getNextCardinalPosition(char currentCardinalPos, char command)
        {
            int maxBound = _cardinalPositions.Count -1;
            var currentCardinalIndex = _cardinalPositions.IndexOf(currentCardinalPos);

            int nextCardinalIndex = 0;

            if (command == 'L')
            {
                nextCardinalIndex = (currentCardinalIndex - 1) < 0 ? maxBound : (currentCardinalIndex - 1);
            }

            if (command == 'R')
            {
                nextCardinalIndex = (currentCardinalIndex + 1) > maxBound ? 0 : (currentCardinalIndex + 1);
            }

            return _cardinalPositions[nextCardinalIndex];
        }
    }
}
