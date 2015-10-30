﻿using Untech.SharePoint.Common.Configuration;
using Untech.SharePoint.Common.Data;
using Untech.SharePoint.Common.Mappings;
using Untech.SharePoint.Common.Mappings.Annotation;
using Untech.SharePoint.Common.MetaModels;

namespace Untech.SharePoint.Common.Test.Mappings.Annotation.Models
{
	public class AnnotatedContext : ISpContext
	{
		[SpList(Title = "List1")]
		public ISpList<AnnotatedEntity> Entities { get; set; }

		[SpList(Title = "List1")]
		public ISpList<DerivedAnnotatedEntityWithIheritedAnnotation> DerivedEntities { get; set; }

		[SpList(Title = "List2")]
		public ISpList<DerivedAnnotatedEntityWithOverwrittenAnnotation> OtherEntities { get; set; }

		public ISpList<AnnotatedEntity> MissingAttribute { get; set; }

		public string NotAList { get; set; }
		public Config Config { get; private set; }
		public IMappingSource MappingSource { get; private set; }
		public MetaContext Model { get; private set; }
	}
}