using System;
using System.Reflection;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Common.MetaModels.Providers;

namespace Untech.SharePoint.Common.AnnotationMapping
{
	internal sealed class AnnotatedFieldProvider : IMetaFieldProvider
	{
		public AnnotatedFieldProvider(MemberInfo member)
		{
			Member = member;

			FieldAttribute = member.GetCustomAttribute<SpFieldAttribute>();
			if (FieldAttribute == null)
			{
				throw new ArgumentException(string.Format("Member {0} has no attribute SpFieldAttribute", Member.Name), "memberInfo");
			}
		}

		public MemberInfo Member { get; private set; }

		public SpFieldAttribute FieldAttribute { get; private set; }

		public MetaField GetMetaField(MetaContentType parent)
		{
			var metaField = new MetaField(parent, Member, FieldAttribute.InternalName ?? Member.Name);

			if (FieldAttribute.CustomConverterType != null)
			{
				metaField.CustomConverterType = FieldAttribute.CustomConverterType;
			}

			return metaField;
		}
	}
}