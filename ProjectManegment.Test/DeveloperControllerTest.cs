using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectManagement.API.Controllers;
using ProjectManagement.Common.Models;
using ProjectManagement.Common.Models.DTOs;
using ProjectManagement.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManegment.Test
{
    public class DeveloperControllerTest
    {
        private readonly Mock<IUnitOfWork> _mockRepo;
        private readonly DevelopersController _controller;
        private readonly IMapper _mapper;

        public DeveloperControllerTest()
        {
            _mockRepo = new Mock<IUnitOfWork>();
            _controller = new DevelopersController(_mockRepo.Object, _mapper);
        }
        [Fact]
        public void GetDevelopers_ReturnsActionResult()
        {

            var result = _controller.GetDevelopers();
            Assert.IsType<Task<ActionResult<IEnumerable<DeveloperDTO>>>>(result);
        }

        [Fact]
        public async Task GetDevelopers_ReturnsListOfAllDevelopers()
        {
            var fakeDevs = A.CollectionOfDummy<DeveloperDTO>(10).AsEnumerable();
            var dataStore = A.Fake<IUnitOfWork>();
            //A.CallTo(() => dataStore.DeveloperRepository.All()).Returns(Task.FromResult(fakeDevs));
        }

        [Fact]
        public async Task GetById_ShouldReturnDeveloper_WhenDeveloperExists()
        {
            Random rand = new Random();
            //Arange
            var id = rand.Next(1, 100);
            var devName = "Percy";
            var dev = new Developer
            {
                Id= id,
                FirstName = devName
            };
            _mockRepo.Setup(x => x.DeveloperRepository.GetById(id)).ReturnsAsync(dev);

            //Act
            var developer = await _controller.GetDeveloper(1);

            //Assert
            Assert.Equal(id, dev.Id);
        }
    }
}
