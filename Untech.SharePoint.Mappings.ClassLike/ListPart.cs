using System;
using System.Linq;
using System.Linq.Expressions;
using Untech.SharePoint.Common.Data;
using Untech.SharePoint.Common.Extensions;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Common.MetaModels.Providers;

namespace Untech.SharePoint.Mappings.ClassLike
{
	public class ListPart<TContext> : IMetaListProvider
		where TContext : ISpContext
	{
		private readonly ContextMap<TContext> _contextMap;

		internal ListPart(ContextMap<TContext> contextMap, string listTitle)
		{
			_contextMap = contextMap;
			ListTitle = listTitle;
		}

		internal string ListTitle { get; private set; }

		public ListPart<TContext> ContentType<TEntity>(Expression<Func<TContext, ISpList<TEntity>>> property, ContentTypeMap<TEntity> contentType)
		{
			var member = ((MemberExpression)property.Body).Member;

			_contextMap.MemberListPartMap.Register(member, this);
			_contextMap.MemberContentTypeMap.Register(member, contentType);

			return this;
		}

		MetaList IMetaListProvider.GetMetaList(MetaContext parent)
		{
			var listMembers = _contextMap.MemberListPartMap
				.Where(n => n.Value == this)
				.Select(n => n.Key)
				.ToList();

			var contentTypes = _contextMap.MemberContentTypeMap
				.Where(n => n.Key.In(listMembers))
				.Select(n => n.Value)
				.ToList();

			return new MetaList(parent, ListTitle, contentTypes);
		}
	}
}