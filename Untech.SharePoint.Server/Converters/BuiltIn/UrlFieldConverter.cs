using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Untech.SharePoint.Common.CodeAnnotations;
using Untech.SharePoint.Common.Converters;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Common.Models;

namespace Untech.SharePoint.Server.Converters.BuiltIn
{
	[SpFieldConverter("URL")]
	[UsedImplicitly]
	internal class UrlFieldConverter : MultiTypeFieldConverter
	{
		private static readonly IReadOnlyDictionary<Type, Func<IFieldConverter>> TypeConverters = new Dictionary<Type, Func<IFieldConverter>>
		{
			{typeof(string), () => new StringTypeConverter()},
			{typeof(UrlInfo), () => new UrlInfoTypeConverter()},
		};

		public override void Initialize(MetaField field)
		{
			base.Initialize(field);
			if (TypeConverters.ContainsKey(field.MemberType))
			{
				Internal = TypeConverters[field.MemberType]();
			}
			else
			{
				throw new ArgumentException("MemberType is invalid");
			}
		}

		private class StringTypeConverter : IFieldConverter
		{
			public void Initialize(MetaField field)
			{
				
			}

			public object FromSpValue(object value)
			{
				return value != null ? new SPFieldUrlValue(value.ToString()).Url : null;
			}

			public object ToSpValue(object value)
			{
				return value != null ? value.ToString() : null;
			}

			public string ToCamlValue(object value)
			{
				return (string) ToSpValue(value) ?? "";
			}
		}

		private class UrlInfoTypeConverter : IFieldConverter
		{
			public void Initialize(MetaField field)
			{

			}

			public object FromSpValue(object value)
			{
				if (value == null)
					return null;

				var spValue = new SPFieldUrlValue(value.ToString());

				return new UrlInfo
				{
					Url = spValue.Url,
					Description = spValue.Description
				};
			}

			public object ToSpValue(object value)
			{
				if (value == null)
					return null;

				var urlInfo = (UrlInfo)value;
                if (string.IsNullOrEmpty(urlInfo.Description))
                {
                    return urlInfo.Url;
                }
                return string.Format("{0}, {1}", urlInfo.Url.Replace(",", ",,"), urlInfo.Description);
            }

			public string ToCamlValue(object value)
			{
				return (string) ToSpValue(value) ?? "";
			}
		}
	}
}