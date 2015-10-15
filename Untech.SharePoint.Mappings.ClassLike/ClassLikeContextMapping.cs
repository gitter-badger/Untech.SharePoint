using System;
using System.Linq.Expressions;
using System.Reflection;
using Untech.SharePoint.Common.Data;
using Untech.SharePoint.Common.Mappings;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Common.MetaModels.Providers;

namespace Untech.SharePoint.Mappings.ClassLike
{
	internal class ClassLikeMappingSource<TContext> : IMappingSource<TContext>
		where TContext : ISpContext
	{
		private readonly ContextMap<TContext> _contextMap;

		public ClassLikeMappingSource(ContextMap<TContext> contextMap)
		{
			_contextMap = contextMap;
		}

		public MetaContext GetMetaContext()
		{
			return ((IMetaContextProvider) _contextMap).GetMetaContext();
		}

		public string GetListTitleFromContextMember(MemberInfo member)
		{
			return _contextMap.MemberListPartMap.Resolve(member).ListTitle;
		}

		public Type ContextType { get { return typeof (TContext); } }
	}
}