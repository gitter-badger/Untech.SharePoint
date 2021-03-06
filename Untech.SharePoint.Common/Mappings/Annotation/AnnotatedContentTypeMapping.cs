using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Common.MetaModels.Providers;
using Untech.SharePoint.Common.Utils;

namespace Untech.SharePoint.Common.Mappings.Annotation
{
	internal class AnnotatedContentTypeMapping : IMetaContentTypeProvider
	{
		private readonly Type _entityType;
		private readonly List<AnnotatedFieldPart> _fieldParts;
		private readonly SpContentTypeAttribute _contentTypeAttrbiute;

		public AnnotatedContentTypeMapping(Type entityType)
		{
			Guard.CheckNotNull("entityType", entityType);

			_entityType = entityType;
			_contentTypeAttrbiute = _entityType.GetCustomAttribute<SpContentTypeAttribute>() ?? new SpContentTypeAttribute();
			
			_fieldParts = CreateFieldParts();
		}

		private string ContentTypeId
		{
			get { return _contentTypeAttrbiute.Id; }
		}

		private string ContentTypeName
		{
			get { return _contentTypeAttrbiute.Name; }
		}

		public MetaContentType GetMetaContentType(MetaList parent)
		{
			return new MetaContentType(parent, _entityType, _fieldParts)
			{
				Id = ContentTypeId,
				Name = ContentTypeName
			};
		}

		#region [Private Methods]

		private List<AnnotatedFieldPart> CreateFieldParts()
		{
			var props = _entityType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
				.Where(AnnotatedFieldPart.IsAnnotated)
				.Select(AnnotatedFieldPart.Create);

			var fields = _entityType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
				.Where(AnnotatedFieldPart.IsAnnotated)
				.Select(AnnotatedFieldPart.Create);

			return props.Concat(fields).ToList();
		}

		#endregion

	}
}