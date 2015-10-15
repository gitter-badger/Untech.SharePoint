using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Untech.SharePoint.Common.Collections;
using Untech.SharePoint.Common.Data;
using Untech.SharePoint.Common.Mappings;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Common.MetaModels.Providers;
using Untech.SharePoint.Common.Utils;

namespace Untech.SharePoint.Mappings.ClassLike
{
	public class ContextMap<TContext> : IMetaContextProvider, IListTitleResolver
		where TContext : ISpContext
	{
		private readonly Container<string, ListPart> _listParts;
		private readonly Container<MemberInfo, ListPart> _memberToListPartMap;

		public ContextMap()
		{
			_listParts = new Container<string, ListPart>();
			_memberToListPartMap = new Container<MemberInfo, ListPart>();
		}

		public ListPart List(string title)
		{
			_listParts.Register(title, new ListPart(this, title));

			return _listParts.Resolve(title);
		}

		public MetaContext GetMetaContext()
		{
			return new MetaContext(_listParts.Select(n => n.Value).ToList());
		}

		public string GetListTitleFromContextMember(MemberInfo member)
		{
			return _memberToListPartMap.Resolve(member).ListTitle;
		}

		internal void Validate()
		{
			foreach (var pair in _memberToListPartMap)
			{
				var entityType = TypeSystem.GetMemberType(pair.Key).GetGenericArguments()[0];

				pair.Value.Validate(entityType);
			}
		}

		#region [Nested Class]

		public class ListPart : IMetaListProvider
		{
			private readonly ContextMap<TContext> _contextMap;
			private readonly Container<Type, IMetaContentTypeProvider> _contentTypes;

			internal ListPart(ContextMap<TContext> contextMap, string listTitle)
			{
				_contextMap = contextMap;
				ListTitle = listTitle;

				_contentTypes = new Container<Type, IMetaContentTypeProvider>();
			}

			internal string ListTitle { get; private set; }

			public ListPart For<TEntity>(Expression<Func<TContext, ISpList<TEntity>>> property)
			{
				var member = ((MemberExpression)property.Body).Member;

				_contextMap._memberToListPartMap.Register(member, this);

				return this;
			}

			public ListPart ContentType<TEntity>(ContentTypeMap<TEntity> contentType)
			{
				_contentTypes.Register(typeof(TEntity), contentType);

				return this;
			}

			internal void Validate(Type entityType)
			{
				if (_contentTypes.IsRegistered(entityType))
				{
					return;
				}

				throw new ArgumentException(string.Format("You should register content type map for '{0}' for list {1}", entityType, ListTitle));
			}

			MetaList IMetaListProvider.GetMetaList(MetaContext parent)
			{
				return new MetaList(parent, ListTitle, _contentTypes.Select(n=>n.Value).ToList());
			}
		}

		#endregion
	}
}