using ExpressVoiture.API.Domain.Models;
using ExpressVoiture.Services;
using ExpressVoiture.Shared.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ExpressVoiture.Tests.UnitsTests
{
    public class FileServiceTests
    {
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private readonly FileService _fileService;

        public FileServiceTests()
        {
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _fileService = new FileService(_mockWebHostEnvironment.Object);
        }

        [Fact]
        public void CreateFile_ShouldReturnVehicle_WhenFileIsNull()
        {
            // Arrange
            var vehicle = new AddOrUpdateVehicleViewModel();

            // Act
            var result = _fileService.CreateFile(vehicle, null);

            // Assert
            Assert.Equal(vehicle, result);
        }

        [Fact]
        public void CreateFile_ShouldUpdateVehicleImagePath_WhenFileIsProvided()
        {
            // Arrange
            var vehicle = new AddOrUpdateVehicleViewModel();
            var fileMock = new Mock<IFormFile>();
            string wwwRootPath = "wwwroot";
            _mockWebHostEnvironment.Setup(x => x.WebRootPath).Returns(wwwRootPath);
            var fileName = "test.jpg";
            var vehiclePath = Path.Combine(wwwRootPath, "images", "vehicles");

            if (!Directory.Exists(vehiclePath))
            {
                Directory.CreateDirectory(vehiclePath);
            }

            var filePath = Path.Combine(vehiclePath, fileName);
            fileMock.Setup(f => f.FileName).Returns(fileName);

            using (var stream = new MemoryStream())
            {
                fileMock.Setup(f => f.OpenReadStream()).Returns(stream);

                // Act
                var result = _fileService.CreateFile(vehicle, fileMock.Object);

                // Assert
                Assert.StartsWith(@"images\vehicles\", result.ImagePath);
                Assert.EndsWith(".jpg", result.ImagePath);
            }
        }

        //[Fact]
        //public void DeleteFile_ShouldRemoveFile_WhenImagePathIsProvided()
        //{
        //    // Arrange
        //    var vehicle = new VoitureAVendre
        //    {
        //        ImagePath = @"images\vehicles\test.jpg"
        //    };
        //    string wwwRootPath = "wwwroot";
        //    _mockWebHostEnvironment.Setup(x => x.WebRootPath).Returns(wwwRootPath);
        //    var fullImagePath = Path.Combine(wwwRootPath, vehicle.ImagePath.TrimStart('\\'));

        //    // Act
        //    var result =  _fileService.DeleteFileByVehiculeId(vehicle.VoitureId);

        //    // Assert
        //    Assert.Equal(vehicle, result);
        //}

        //[Fact]
        //public void DeleteFile_ShouldNotThrowException_WhenImagePathIsNull()
        //{
        //    // Arrange
        //    var vehicle = new VoitureAVendre
        //    {
        //        ImagePath = null
        //    };

        //    // Act & Assert
        //    var exception = Record.Exception(() => _fileService.DeleteFile(vehicle));
        //    Assert.Null(exception);
        //}
    }
}
