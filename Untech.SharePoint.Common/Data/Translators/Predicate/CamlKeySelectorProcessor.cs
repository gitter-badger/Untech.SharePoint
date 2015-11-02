﻿using System.Linq.Expressions;
using Untech.SharePoint.Common.Data.QueryModels;
using Untech.SharePoint.Common.Extensions;

namespace Untech.SharePoint.Common.Data.Translators.Predicate
{
	internal class CamlKeySelectorProcessor : IExpressionProcessor<MemberRefModel>
	{
		public MemberRefModel Process(Expression predicate)
		{
			predicate = predicate.StripQuotes();
			if (predicate.NodeType == ExpressionType.Lambda)
			{
				predicate = ((LambdaExpression)predicate).Body;
			}

			return CamlProcessorUtils.GetFieldRef(predicate);
		}
	}
}