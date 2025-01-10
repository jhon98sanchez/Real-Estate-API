using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using RealEstate.Business.Implement;
using System.Net;
using System.Text;

namespace RealEstate.Test.UnitTest
{
    [TestFixture]
    public class FileManagerTests
    {
        private Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private FileManager _fileManager;

        [SetUp]
        public void Setup()
        {
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockWebHostEnvironment.Setup(x => x.ContentRootPath).Returns("C:\\TestUploads");
            _fileManager = new FileManager(_mockWebHostEnvironment.Object);
        }

        [Test]
        public async Task Upload_InvalidExtension_ReturnsBadRequest()
        {
            // Arrange
            var invalidFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, 20, "file", "test.txt")
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };

            // Act
            var response = await _fileManager.Upload(invalidFile);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.Code);
            Assert.AreEqual("Only .jpg,.jpeg,.png, files are allowed", response.Message);
        }

        [Test]
        public async Task Upload_ValidFile_ReturnsSuccess()
        {
            // Arrange
            var validFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, 20, "file", "test.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var uploadsFolder = Path.Combine(_mockWebHostEnvironment.Object.ContentRootPath, "Uploads");
            Directory.CreateDirectory(uploadsFolder);

            // Act
            var response = await _fileManager.Upload(validFile);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.AreEqual("File Upload", response.Message);
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(File.Exists(Path.Combine(uploadsFolder, response.Data)));

            // Cleanup
            File.Delete(Path.Combine(uploadsFolder, response.Data));
        }

        [Test]
        public async Task Upload_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var validFile = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Test file content")), 0, 20, "file", "test.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            _mockWebHostEnvironment.Setup(x => x.ContentRootPath).Throws(new Exception("Test exception"));

            // Act
            var response = await _fileManager.Upload(validFile);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.Code);
        }
    }
}
