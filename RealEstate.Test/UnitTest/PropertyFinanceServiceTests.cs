using Moq;
using RealEstate.Business.Implement;
using RealEstate.Domain.DbSets;
using RealEstate.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Test.UnitTest
{
    [TestFixture]
    public class PropertyFinanceServiceTests
    {
        private Mock<IPropertyFinanceRepository> _mockPropertyFinanceRepository;
        private Mock<IPropertyRepository> _mockPropertyRepository;
        private PropertyFinanceService _service;

        [SetUp]
        public void Setup()
        {
            _mockPropertyFinanceRepository = new Mock<IPropertyFinanceRepository>();
            _mockPropertyRepository = new Mock<IPropertyRepository>();
            _service = new PropertyFinanceService(_mockPropertyFinanceRepository.Object, _mockPropertyRepository.Object);
        }

        [Test]
        public async Task Create_PropertyNotFound_ReturnsBadRequest()
        {
            // Arrange
            var propertyFinance = new PropertyFinance { PropertyId = 1, Name = "Test", Tax = 10, Value = 100 };

            // Act
            var response = await _service.Create(propertyFinance);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.Code);
        }

        [Test]
        public async Task Create_PropertyAlreadySold_ReturnsBadRequest()
        {
            // Arrange
            var property = new Property { Id = 1, Status = Domain.Utils.StatusProperty.Sold, Name = "Test", Address = "TV", CodeInternal = "AA", Price = 10, Year = 2012 };
            var propertyFinance = new PropertyFinance { PropertyId = 1, Name = "Test" };

            // Act
            var response = await _service.Create(propertyFinance);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.Code);
        }
    }

}
