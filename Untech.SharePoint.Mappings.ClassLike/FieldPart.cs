using System;
using System.Reflection;
using Untech.SharePoint.Common.Converters;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Common.MetaModels.Providers;

namespace Untech.SharePoint.Mappings.ClassLike
{
	public class FieldPart : IMetaFieldProvider
	{
		private readonly MemberInfo _member;
		private string _internalName;
		private string _typeAsString;
		private Type _customConverterType;

		internal FieldPart(MemberInfo member)
		{
			_member = member;
		}


		public FieldPart InternalName(string internalName)
		{
			_internalName = internalName;
			return this;
		}

		public FieldPart TypeAsString(string typeAsString)
		{
			_typeAsString = typeAsString;
			return this;
		}

		public FieldPart CustomConverter<TConverter>()
			where TConverter : IFieldConverter, new()
		{
			_customConverterType = typeof (TConverter);
			return this;
		}

		MetaField IMetaFieldProvider.GetMetaField(MetaContentType parent)
		{
			var internalName = string.IsNullOrEmpty(_internalName) ? _member.Name : _internalName;
			return new MetaField(parent, _member, internalName)
			{
				TypeAsString = _typeAsString,
				CustomConverterType = _customConverterType
			};
		}
	}
}