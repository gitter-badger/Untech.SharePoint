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

		public MetaContext GetMetaContext()
		{
			throw new NotImplementedException();
		}

		public string GetListTitleFromContextMember(MemberInfo member)
		{
			throw new NotImplementedException();
		}

		public Type ContextType { get; private set; }
	}

	public class ContextMap<TContext> : IMetaContextProvider
		where TContext : ISpContext
	{
		public MetaContext GetMetaContext()
		{
			throw new NotImplementedException();
		}

		public void List<TEntity>(Expression<Func<TContext, ISpList<TEntity>>> property, ContentTypeMap<TEntity> contentType)
		{
			throw new NotImplementedException();
		}
	}

}