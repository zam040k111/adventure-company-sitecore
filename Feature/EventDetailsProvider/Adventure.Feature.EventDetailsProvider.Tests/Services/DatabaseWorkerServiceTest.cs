using Adventure.Feature.EventDetailsProvider.Models;
using Adventure.Feature.EventDetailsProvider.Repositories.Interfaces;
using Adventure.Feature.EventDetailsProvider.Services;
using Foundation.ORM.Sitecore.templates.Project.Adventure.Constants;
using log4net;
using Moq;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using Xunit;

namespace Adventure.Feature.EventDetailsProvider.Tests.Services
{
    public class DatabaseWorkerServiceTest
    {
        private static readonly TemplateID _eventTemplateId = new TemplateID(EventDetailsPage.TemplateId);
        private readonly DatabaseWorkerService _databaseWorkerService;
        private readonly Mock<IEventListRepository> _eventListRepository;

        public DatabaseWorkerServiceTest()
        {
            _eventListRepository = new Mock<IEventListRepository>();

            _databaseWorkerService = new DatabaseWorkerService(
                Mock.Of<ILog>(),
                _eventListRepository.Object);
        }

        [Fact]
        public void AddEvent_InputNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _databaseWorkerService.AddEvent(null));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void AddEvent_SomethingGoWrong_ShouldEndEditing(
            IEnumerable<Item> repositoryCollection,
            NewEvent inputParameter,
            Mock<Item> mockedNewItem,
            Mock<FieldCollection> mockedNewItemCollection)
        {
            mockedNewItemCollection.Setup(x => x[EventDetailsPage.Fields.ButtonText]).Throws<ArgumentException>();
            _eventListRepository
                .Setup(x => x.GetAll())
                .Returns(repositoryCollection);


            _databaseWorkerService.AddEvent(inputParameter);

            mockedNewItem.Verify(x => x.Editing.CancelEdit(), Times.Once);
            mockedNewItem.Verify(x => x.Editing.EndEdit(), Times.Never);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void AddEvent_ShouldAddCorrectDataIntoSitecore(
            IEnumerable<Item> repositoryCollection,
            NewEvent inputParameter,
            Mock<Item> mockedNewItem,
            Mock<FieldCollection> mockedNewItemCollection)
        {
            _eventListRepository
                .Setup(x => x.GetAll())
                .Returns(repositoryCollection);

            _databaseWorkerService.AddEvent(inputParameter);

            mockedNewItem.Verify(x => x.Editing.BeginEdit(), Times.Once);
            mockedNewItemCollection.Verify(x => x[EventDetailsPage.Fields.ButtonText], Times.Once);
            mockedNewItemCollection.Verify(x => x[EventDetailsPage.Fields.Description], Times.Once);
            mockedNewItemCollection.Verify(x => x[EventDetailsPage.Fields.ShortDescription], Times.Once);
            mockedNewItemCollection.Verify(x => x[EventDetailsPage.Fields.Difficulty], Times.Once);
            mockedNewItemCollection.Verify(x => x[EventDetailsPage.Fields.EndDate], Times.Once);
            mockedNewItemCollection.Verify(x => x[EventDetailsPage.Fields.StartDate], Times.Once);
            mockedNewItemCollection.Verify(x => x[EventDetailsPage.Fields.Title], Times.Once);
            mockedNewItem.Verify(x => x.Editing.EndEdit(), Times.Once);
        }

        public static IEnumerable<object[]> TestData()
        {
            var newEvent = new NewEvent
            {
                ButtonText = "bt",
                Description = "d",
                Difficulty = 1,
                EndDate = DateTime.Now,
                ShortDescription = "sd",
                StartDate = DateTime.Now.AddDays(-1),
                Title = "t"
            };

            var newItem = CreateItem();
            var newItemEditing = new Mock<ItemEditing>(newItem.Object);
            var newItemFieldColletion = new Mock<FieldCollection>(newItem.Object);
            newItem.Setup(x => x.Editing).Returns(newItemEditing.Object);
            newItem.Setup(x => x.Fields).Returns(newItemFieldColletion.Object);
            newItemFieldColletion.Setup(x => x[EventDetailsPage.Fields.ButtonText]).Returns(CreateField().Object);
            newItemFieldColletion.Setup(x => x[EventDetailsPage.Fields.Description]).Returns(CreateField().Object);
            newItemFieldColletion.Setup(x => x[EventDetailsPage.Fields.Difficulty]).Returns(CreateField().Object);
            newItemFieldColletion.Setup(x => x[EventDetailsPage.Fields.EndDate]).Returns(CreateField().Object);
            newItemFieldColletion.Setup(x => x[EventDetailsPage.Fields.ShortDescription]).Returns(CreateField().Object);
            newItemFieldColletion.Setup(x => x[EventDetailsPage.Fields.StartDate]).Returns(CreateField().Object);
            newItemFieldColletion.Setup(x => x[EventDetailsPage.Fields.Title]).Returns(CreateField().Object);

            var parentItem = CreateItem();
            parentItem.Setup(x => x.Add(It.IsAny<string>(), _eventTemplateId)).Returns(newItem.Object);

            var repositoryCollection = new List<Item> { parentItem.Object };

            return new List<object[]>
            {
                new object[] { repositoryCollection, newEvent, newItem, newItemFieldColletion }
            };
        }

        private static Mock<Item> CreateItem()
        {
            return new Mock<Item>(ID.NewID, ItemData.Empty, new Mock<Database>().Object);
        }

        private static Mock<Field> CreateField()
        {
            var field =  new Mock<Field>(ID.NewID, CreateItem().Object);
            field.Setup(x => x.Value);

            return field;
        }
    }
}
