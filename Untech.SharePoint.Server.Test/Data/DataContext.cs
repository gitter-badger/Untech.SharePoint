﻿using Microsoft.SharePoint;
using Untech.SharePoint.Common.Configuration;
using Untech.SharePoint.Common.Data;
using Untech.SharePoint.Common.Mappings.Annotation;
using Untech.SharePoint.Common.Test.Spec.Models;
using Untech.SharePoint.Server.Data;

namespace Untech.SharePoint.Server.Test.Data
{
	public class DataContext : SpServerContext<DataContext>, IDataContext
	{
		public DataContext(SPWeb web, Config config) 
			: base(web, config)
		{

		}

		[SpList]
		public ISpList<NewsModel> News
		{
			get { return GetList(x => x.News); }
		}

		[SpList]
		public ISpList<EventModel> Events
		{
			get { return GetList(x => x.Events); }
		}

		[SpList]
		public ISpList<TeamModel> Teams
		{
			get { return GetList(x => x.Teams); }
		}

		[SpList]
		public ISpList<ProjectModel> Projects
		{
			get { return GetList(x => x.Projects); }
		}

	}
}