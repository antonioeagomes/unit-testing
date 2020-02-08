using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _service;
        private Mock<IFileReader> _mockFileReader;
        private Mock<IVideoRepository> _mockRepository;

        [SetUp]
        public void Init()
        {
            _mockFileReader = new Mock<IFileReader>();
            _mockRepository = new Mock<IVideoRepository>();
            _service = new VideoService(_mockFileReader.Object, _mockRepository.Object);
        }

        [Test]
        public void ReadVideoTitle_MethodParameter_EmptyFile_ReturnError()
        {
            /* DI - via Method parameter*/
            var result = _service.ReadVideoTitle(new FakeFileReader());

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void ReadVideoTitle_ViaProperty_EmptyFile_ReturnError()
        {
            /* DI - via property*/
            VideoService service = new VideoService() { FileReader = new FakeFileReader() };

            var result = service.ReadVideoTitleDIViaProperty();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void ReadVideoTitle_Constructor_EmptyFile_ReturnError()
        {
            /* DI - via constructor */
            var service = new VideoService(new FakeFileReader(), null);
            var result = service.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void ReadVideoTitle_Moq_EmptyFile_ReturnError()
        {
            /* Using Moq Framework */
            _mockFileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _service.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideoProcessed_ReturnEmptyString()
        {
            _mockRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_FewVideosUnprocessed_ReturnStringWithIds()
        {
            _mockRepository.Setup(r => r.GetUnprocessedVideos())
                    .Returns(new List<Video>()
                    {
                        new Video { Id = 1 },
                        new Video { Id = 2 },
                        new Video { Id = 3 }
                    });

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_OnlyOneVideoUnprocessed_ReturnStringWithOneId()
        {
            _mockRepository.Setup(r => r.GetUnprocessedVideos())
                    .Returns(new List<Video>()
                    {
                        new Video { Id = 1 },
                    });

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1"));
        }

    }
}
