using Untech.SharePoint.Common.Data;
using Untech.SharePoint.Common.Mappings;

namespace Untech.SharePoint.Mappings.ClassLike
{
	public static class MappingsExtension
	{
		public static IMappingSource<TContext> ClassLike<TContext>(this Common.Mappings.Mappings mappings, ContextMap contextMap)
		   where TContext : ISpContext
		{
			return new ClassLikeMappingSource<TContext>();
		}
	}
}