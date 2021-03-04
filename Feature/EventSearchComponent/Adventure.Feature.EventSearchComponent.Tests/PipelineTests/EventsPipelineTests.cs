using System;
using System.Collections.Generic;
using System.Linq;
using Adventure.Feature.EventSearchComponent.Filtering;
using Adventure.Feature.EventSearchComponent.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adventure.Feature.EventSearchComponent.Tests.PipelineTests
{
	[TestClass]
	public class EventsPipelineTests
	{
        private readonly Guid _templateId = new Guid("72B40E48-A2B6-4DDF-BE66-AD0911F53F75");

		[TestMethod]
		public void NameFilter_WhenItemsExist_ExpectNotEmptyList()
		{
			var searchSettings = new EventSearchSettings { Name = "Walking", PageSize = 3 };
			var pipeline = new EventPipeline().Register(new NameFilter());

			var result = pipeline.Process(searchSettings, CreateItems());

            Assert.IsTrue(result.All(x => x.Name == searchSettings.Name));
        }

		[TestMethod]
		public void DateFilter_WhenItemsExist_ExpectNotEmptyList()
		{
			var searchSettings = new EventSearchSettings { StartDate = DateTime.UtcNow.AddDays(4), PageSize = 3 };
			var pipeline = new EventPipeline().Register(new DateFilter());

			var result = pipeline.Process(searchSettings, CreateItems());

            Assert.IsTrue(result.All(x => x.StartDate.Date >= searchSettings.StartDate.Date));
        }

		[TestMethod]
		public void DifficultyFilter_WhenItemsExist_ExpectNotEmptyList()
		{
			var searchSettings = new EventSearchSettings { Difficulty = 5, PageSize = 3 };
			var pipeline = new EventPipeline().Register(new DifficultyFilter());

			var result = pipeline.Process(searchSettings, CreateItems());

            Assert.IsTrue(result.All(x => x.Difficulty == searchSettings.Difficulty));
        }

		[TestMethod]
		public void SortByFilter_WhenOrderByIsName_ExpectSortedListByName()
		{
			var searchSettings = new EventSearchSettings { OrderBy = "name", PageSize = 3 };
			var pipeline = new EventPipeline().Register(new SortByFilter());
			var expectedResult = CreateItems().OrderBy(i => i.Title);

			var result = pipeline.Process(searchSettings, CreateItems());

            CollectionAssert.AreEqual(
                expectedResult.Select(x => x.Name).ToList(),
                result.Select(x => x.Name).ToList());
        }

		[TestMethod]
		public void SortByFilter_WhenOrderByIsDifficulty_ExpectSortedListByName()
		{
			var searchSettings = new EventSearchSettings { OrderBy = "difficulty", PageSize = 3 };
			var pipeline = new EventPipeline().Register(new SortByFilter());
			var expectedResult = CreateItems().OrderBy(i => i.Difficulty);

			var result = pipeline.Process(searchSettings, CreateItems());

            CollectionAssert.AreEqual(
                expectedResult.Select(x => x.Difficulty).ToList(),
                result.Select(x => x.Difficulty).ToList());
		}

		[TestMethod]
		public void SortByFilter_WhenOrderByIsDate_ExpectSortedListByName()
		{
			var searchSettings = new EventSearchSettings { OrderBy = "date", PageSize = 3 };
			var pipeline = new EventPipeline().Register(new SortByFilter());
			var expectedResult = CreateItems().OrderBy(i => i.StartDate);

			var result = pipeline.Process(searchSettings, CreateItems());

            CollectionAssert.AreEqual(
                expectedResult.Select(x => x.StartDate.Date).ToList(),
                expectedResult.Select(x => x.StartDate.Date).ToList());
        }

		[TestMethod]
		[DataRow(2)]
		[DataRow(1)]
        public void Pagination_ExpectCorrectItemPage(int pageNumber)
		{
            var searchSettingsPage = new EventSearchSettings { PageNumber = pageNumber, PageSize = 2 };
            var expectedResult = new Dictionary<int,int> { { 1, 2 }, { 2, 1 } };
			var pipeline = new EventPipeline().Register(new SortByFilter());
			var allItems = CreateItems();

			var resultPage = pipeline.Process(searchSettingsPage, allItems);

            Assert.AreEqual(expectedResult[pageNumber], resultPage.Count());
		}

        [TestMethod]
		public void Search_WhenItemsNotExist_ExpectEmptyList()
		{
			var searchSettings = new EventSearchSettings { Name = "Some name", Difficulty = 3, StartDate = DateTime.UtcNow.AddDays(6) };
			var pipeline = new EventPipeline()
				.Register(new NameFilter())
				.Register(new DateFilter())
				.Register(new DifficultyFilter());

			var result = pipeline.Process(searchSettings, CreateItems());

			Assert.AreEqual(0, result.Count());
		}

        private IQueryable<EventDetailsSearchItem> CreateItems()
        {
            return new List<EventDetailsSearchItem>
			{
				new EventDetailsSearchItem
				{
                    Name = "Climbing",
					Difficulty = 5,
					StartDate = DateTime.UtcNow.AddDays(1),
					TemplateId = new Sitecore.Data.ID(_templateId)
				},
				new EventDetailsSearchItem
				{
                    Name = "Walking",
					Difficulty = 2,
					StartDate = DateTime.UtcNow.AddDays(3),
					TemplateId =  new Sitecore.Data.ID(_templateId)
				},
				new EventDetailsSearchItem
				{
					Name = "Other action",
					Difficulty = 4,
					StartDate = DateTime.UtcNow.AddDays(5),
					TemplateId =  new Sitecore.Data.ID(_templateId)
				}
			}.AsQueryable();
		}
	}
}
