using System;
using System.Linq.Expressions;
using Untech.SharePoint.Common.Data;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Common.MetaModels.Providers;

namespace Untech.SharePoint.Mappings.ClassLike
{
	public class ListPart<TContext> : IMetaListProvider
		where TContext : ISpContext
	{
		private readonly ContextMap<TContext> _contextMapMap;
		private readonly string _listTitle;

		internal ListPart(ContextMap<TContext> contextMap, string listTitle)
		{
			_contextMapMap = contextMap;
			_listTitle = listTitle;
		}

		public ListPart<TContext> ContentType<TEntity>(Expression<Func<TContext, ISpList<TEntity>>> property)
		{

			return this;
		}

		public MetaList GetMetaList(MetaContext parent)
		{
			throw new System.NotImplementedException();
		}
	}
}