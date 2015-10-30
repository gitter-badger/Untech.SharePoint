﻿namespace Untech.SharePoint.Common.Data.QueryModels
{
	/// <summary>
	/// Describes different types of logical operation allowed in CAML.
	/// </summary>
	public enum LogicalJoinOperator
	{
		/// <summary>
		/// Not specified.
		/// </summary>
		None,
		/// <summary>
		/// Logical And: <example>Op1 and Op2.</example>
		/// </summary>
		And,
		/// <summary>
		/// Logical Or: <example>Op1 or Op2.</example>
		/// </summary>
		Or,
	}
}