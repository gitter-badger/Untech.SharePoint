using System;
using System.Linq;
using System.Reflection;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Common.MetaModels.Providers;
using Untech.SharePoint.Common.Utils;

namespace Untech.SharePoint.Common.Mappings.Annotation
{
	internal class AnnotatedFieldPart : IMetaFieldProvider
	{
		private readonly MemberInfo _member;
		private readonly SpFieldAttribute _fieldAttribute;

		private AnnotatedFieldPart(MemberInfo member)
		{
			Guard.CheckNotNull("member", member);

			_member = member;
			_fieldAttribute = member.GetCustomAttribute<SpFieldAttribute>(true);
		}

		#region [Public Static]

		public static bool IsAnnotated(MemberInfo member)
		{
			return member.IsDefined(typeof(SpFieldAttribute)) && !member.IsDefined(typeof(SpFieldRemovedAttribute));
		}

		public static AnnotatedFieldPart Create(PropertyInfo property)
		{
			if (!property.CanRead || !property.CanWrite)
			{
				throw new InvalidAnnotationException(string.Format("Property {1}.{0} should be readable and writable", property.Name, property.DeclaringType));
			}
			if (property.GetIndexParameters().Any())
			{
				throw new InvalidAnnotationException(string.Format("Indexer in {0} cannot be annotated",
					property.DeclaringType));
			}

			return new AnnotatedFieldPart(property);
		}

		public static AnnotatedFieldPart Create(FieldInfo field)
		{
			if (field.IsInitOnly || field.IsLiteral)
			{
				throw new InvalidAnnotationException(string.Format("Field {1}.{0} cannot be readonly or const", field.Name, field.DeclaringType));
			}

			return new AnnotatedFieldPart(field);
		}

		#endregion

		private string InternalName
		{
			get { return string.IsNullOrEmpty(_fieldAttribute.Name) ? _member.Name : _fieldAttribute.Name; }
		}

		private string TypeAsString
		{
			get { return _fieldAttribute.FieldType; }
		}

		private Type CustomConverterType
		{
			get { return _fieldAttribute.CustomConverterType; }
		}

		public MetaField GetMetaField(MetaContentType parent)
		{
			return new MetaField(parent, _member, InternalName)
			{
				CustomConverterType = CustomConverterType,
				TypeAsString = TypeAsString
			};
		}
	}
}