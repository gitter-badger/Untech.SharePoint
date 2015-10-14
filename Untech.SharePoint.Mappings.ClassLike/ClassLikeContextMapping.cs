using System;
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

	internal class ClassLikeContextMapping :  IMetaContextProvider
	{
		public MetaContext GetMetaContext()
		{
			throw new NotImplementedException();
		}
	}

	
}