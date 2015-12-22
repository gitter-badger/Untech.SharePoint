﻿using System;
using Untech.SharePoint.Common.CodeAnnotations;
using Untech.SharePoint.Common.Extensions;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Common.Utils;

namespace Untech.SharePoint.Common.Converters.BuiltIn
{
	[SpFieldConverter("Number")]
	[SpFieldConverter("Currency")]
	[UsedImplicitly]
	internal class NumberFieldConverter : IFieldConverter
	{
		private MetaField Field { get; set; }
		private bool IsNullableMemberType { get; set; }

		public void Initialize(MetaField field)
		{
			Guard.CheckNotNull("field", field);

			Field = field;
			var memberType = field.MemberType;
			if (memberType.IsNullable())
			{
				IsNullableMemberType = true;
				memberType = memberType.GetGenericArguments()[0];
			}
			if (memberType != typeof(double))
			{
				throw new ArgumentException("Member type should be double or System.Nullable<double>.");
			}
		}

		public object FromSpValue(object value)
		{
			if (IsNullableMemberType)
				return (double?)value;

			return (double?) value ?? 0;
		}

		public object ToSpValue(object value)
		{
			return (double?)value;
		}

		public string ToCamlValue(object value)
		{
			return Convert.ToString(ToSpValue(value));
		}
	}
}
