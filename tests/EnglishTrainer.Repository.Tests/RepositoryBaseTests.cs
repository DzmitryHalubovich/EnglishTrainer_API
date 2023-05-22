using AutoFixture;
using EnglishTrainer.API.Controllers;
using EnglishTrainer.Entities.DTO.Create;
using EnglishTrainer.Entities.Models;
using EnglishTrainer.Repositories.Implemintations;
using EnglishTrainer.Repositories.Interfaces;
using EnglishTrainer.Services;
using Moq;

namespace EnglishTrainer.Repository.Tests
{
    public class RepositoryBaseTests
    {
        private Mock<IRepositoryManager> _repositoryMock;
        private Mock<IWordRepository> _wordRepository;
        private readonly WordService _service;
        

        public RepositoryBaseTests()
        {
            _wordRepository = new Mock<IWordRepository>();
            _repositoryMock =new Mock<IRepositoryManager>();
            _service = new WordService(_repositoryMock.Object);
        }

        [Fact]
        public void Create_GetRequestToCreateNewWord_CreateNewWordSuccesfully()
        {
            //Arrange
            var fixture = new Fixture();

            var word = fixture.Build<Word>()
                .Without(x => x.Id)
                .Without(x=>x.Examples)
                .Create();

            _repositoryMock.SetupGet(x => x.Word).Returns(new Mock<IWordRepository>().Object);

            //Act
            var result = _service.Create(word);

            //Assert
            Assert.True(result);

            _repositoryMock.Verify(x => x.Word.CreateWord(word), Times.Once);
        }
    }
}
