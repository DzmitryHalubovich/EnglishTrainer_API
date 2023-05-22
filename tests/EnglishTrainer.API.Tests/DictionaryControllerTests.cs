using AutoFixture;
using EnglishTrainer.API.Controllers;
using EnglishTrainer.API.Extensions;
using EnglishTrainer.Entities.DTO.Read;
using EnglishTrainer.Entities.RequestFeatures;
using EnglishTrainer.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Moq;

namespace EnglishTrainer.API.Tests
{
    public class DictionaryControllerTests
    {
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly DictionaryController _controller;

        public DictionaryControllerTests()
        {
            _repositoryMock = new Mock<IRepositoryManager>();
            //_controller = new DictionaryController(_repositoryMock.Object);
        }

        [Fact]
        public void GetWords_CallRepositoryManagerMethodGetWordsAsync_CallRepositoryManagerMethodSuccessfully()
        {
            //Arrange
            var fixture = new Fixture();
            var wordParameters = fixture.Build<WordParameters>().Create();
            _repositoryMock.SetupGet(x => x.Word).Returns(new Mock<IWordRepository>().Object);

            //Act
            var result = _controller.GetWords(wordParameters);

            //Assert
            _repositoryMock.Verify(x => x.Word.GetWordsAsync(wordParameters,false), Times.Once);
        }


        [Fact]
        public async Task StatusMiddlewareReturnsPong()
        {
            //var hostBuilder = new HostBuilder().ConfigureWebHost(webHost =>
            //{
            //    webHost.Configure(app => 
            //        app.UseMiddleware<ExceptionMiddlewareExtensions>());
            //    webHost.UseTestServer();
            //});
        }
        //[Fact]
        //public async void GetWords_GetWordsFromDictionary_Get4WordsFromCollection()
        //{
        //    //Arrange
        //    var fixture = new Fixture();
        //    var wordParameters = fixture.Build<WordParameters>().Create();
        //    _repositoryMock.Setup(rep => rep.Word.GetWordsAsync(wordParameters, false)).Returns(GetTestWords());

        //    //Act 
        //    var result = await _controller.GetWords(wordParameters);

        //    //Assert
        //    //Assert.IsAssignableFrom<IActionResult>(result);
        //    //Assert.IsAssignableFrom<IEnumerable<Word>>(collectionResult.Value);
        //    Assert.NotNull(result);
        //}

        private async Task<IEnumerable<WordReadDTO>> GetTestWords()
        {
            var words = new List<WordReadDTO>
            {
                new WordReadDTO { Name = "Test1", Translations = "Translation1", Description = "Description1"},
                new WordReadDTO { Name = "Test2", Translations = "Translation2", Description = "Description2"},
                new WordReadDTO { Name = "Test3", Translations = "Translation3", Description = "Description3"},
                new WordReadDTO { Name = "Test4", Translations = "Translation4", Description = "Description4"}
            };

            return words;
        } 
    }
}
