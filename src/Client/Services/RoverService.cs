using MarsRovers.Interfaces;
using MarsRovers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarsRovers.Services
{
    public class RoverService : IRoverService
    {
        private INavigationService _navigationService;
        public RoverService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task ExecuteCommand(NavigationCommandModel navigationCommand)
        {
            await _navigationService.Navigate(navigationCommand);            
        }
    }
}
