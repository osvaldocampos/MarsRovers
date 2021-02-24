using MarsRovers.Models;
using MarsRovers.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MarsRoversTest
{
    public class NavigationServiceTest
    {
        [Fact]
        public async Task ExpectedResult()
        {
            //Arrange
            var service = new NavigationService();
            var request = new NavigationCommandModel()
            {
                X_Position = 1,
                Y_Position = 2,
                Z_Position = 'N',
                Instructions = "LMLMLMLMM"
            };


            //Act
            var result = await service.Navigate(request);

            //Assert
            Assert.Equal(1, result.X_Position);
            Assert.Equal(3, result.Y_Position);
            Assert.Equal('N', result.Z_Position);
        }
    }
}
