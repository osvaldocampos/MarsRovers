using MarsRovers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarsRovers.Interfaces
{
    public interface INavigationService
    {
        Task<NavigationCommandModel> Navigate(NavigationCommandModel navigationCommand);
    }
}
