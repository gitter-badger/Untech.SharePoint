﻿using System.Linq;
using Untech.SharePoint.Common.Models;
using Untech.SharePoint.Common.Test.Spec.Models;
using Untech.SharePoint.Common.Test.Tools.Generators;
using Untech.SharePoint.Common.Test.Tools.Generators.Basic;

namespace Untech.SharePoint.Common.Test.Tools.DataGenerators
{
	public class TestDataGenerator
	{
		private readonly ListTestDataGenerator<NewsModel> _newsData;
		private readonly ListTestDataGenerator<EventModel> _eventsData;
		private readonly ListTestDataGenerator<TeamModel> _teamsData;
		private readonly ListTestDataGenerator<ProjectModel> _projectsData;

		public TestDataGenerator(IDataContext dataContext)
		{
			_newsData = new ListTestDataGenerator<NewsModel>(dataContext.News);
			_eventsData = new ListTestDataGenerator<EventModel>(dataContext.Events);
			_teamsData = new ListTestDataGenerator<TeamModel>(dataContext.Teams);
			_projectsData = new ListTestDataGenerator<ProjectModel>(dataContext.Projects);
		}

		public void Generate()
		{
			GenerateNews();
			GenerateEvents();
			GenerateTeams();
			GenerateProjects();
		}

		private void GenerateNews()
		{
			_newsData
				.WithArray(500, Generators.GetNewsGenerator())
				.WithArray(20, Generators.GetNewsGenerator()
					.WithRange(x => x.Description, new[] { "DESCRIPTION 1", "DESCRIPTION 2", "DESCRIPTION 3", "DESCRIPTION 4" }))
				.WithArray(20, Generators.GetNewsGenerator()
					.WithStatic(x => x.Description, "STATIC"))
				.WithArray(1, Generators.GetNewsGenerator()
					.WithStatic(x => x.Description, "SINGLETON"))
				.Generate();
		}

		private void GenerateEvents()
		{
			_eventsData
				.WithArray(100, Generators.GetCompletedEventGenerator())
				.WithArray(100, Generators.GetGoingEventGenerator())
				.WithArray(100, Generators.GetFutureEventGenerator())
				.Generate();
		}

		private void GenerateTeams()
		{
			_teamsData
				.WithArray(20, Generators.GetTeamGenerator())
				.Generate();
		}

		private void GenerateProjects()
		{
			GenerateProjects1();
			GenerateProjects2();
		}

		private void GenerateProjects1()
		{
			var teamsRefs = _teamsData.GeneratedItems
				.Select(n => new ObjectReference { Id = n.Id })
				.ToList();

			_projectsData
				.WithArray(100, Generators.GetProjectGenerator())
				.WithArray(100, Generators.GetProjectGenerator().WithRange(x => x.Team, teamsRefs))
				.Generate();
		}

		private void GenerateProjects2()
		{
			var parentRefs = _projectsData.GeneratedItems
				.Select(n => new ObjectReference { Id = n.Id })
				.ToList();

			var subprojectGenerator = new ArrayGenerator<ObjectReference>(new RangeGenerator<ObjectReference>(parentRefs))
			{
				Size = 4,
				Options = ArrayGenerationOptions.RandomSize
			};

			_projectsData
				.WithArray(100, Generators.GetProjectGenerator().With(x => x.SubProjects, subprojectGenerator))
				.WithArray(50, Generators.GetProjectGenerator()
					.With(x => x.SubProjects, subprojectGenerator)
					.WithStatic(x => x.Status, "Approved"))
				.WithArray(50, Generators.GetProjectGenerator()
					.With(x => x.SubProjects, subprojectGenerator)
					.WithStatic(x => x.Status, "Cancelled"))
				.Generate();
		}
	}
}