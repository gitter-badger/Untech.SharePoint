using System;
using System.Linq.Expressions;
using System.Reflection;
using Untech.SharePoint.Common.Converters;
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

		public ListPart List<TEntity>(Expression<Func<TContext, ISpList<TEntity>>> property, ContentTypeMap<TEntity> contentType)
		{
			throw new NotImplementedException();
		}
	}

	public class ListPart : IMetaListProvider
	{
		public ListPart Title(string title)
		{
			throw new NotImplementedException();

			return this;
		}
	}


	public class ContentTypeMap<TEntity> : IMetaContentTypeProvider
	{

		public FieldPart Field<T>(Expression<Func<TEntity, T>> property)
		{
			throw new NotImplementedException();
		}

	}

	public class FieldPart : IMetaFieldProvider
	{
		public FieldPart InternalName(string internalName)
		{
			throw new NotImplementedException();
		}

		public FieldPart TypeAsString(string typeAsString)
		{
			throw new NotImplementedException();
		}

		public FieldPart CustomConverter<TConverter>()
			where TConverter : IFieldConverter, new() 
		{
			throw new NotImplementedException();
		}
	}
}