using System.Linq;
using System.Reflection;
using Untech.SharePoint.Common.Collections;
using Untech.SharePoint.Common.Data;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Common.MetaModels.Providers;

namespace Untech.SharePoint.Mappings.ClassLike
{
	public class ContextMap<TContext> : IMetaContextProvider
		where TContext : ISpContext
	{
		public ContextMap()
		{
			ListParts = new Container<string, ListPart<TContext>>();
			MemberContentTypeMap = new Container<MemberInfo, IMetaContentTypeProvider>();
			MemberListPartMap = new Container<MemberInfo, ListPart<TContext>>();
		}

		internal Container<string, ListPart<TContext>> ListParts { get; private set; }

		internal Container<MemberInfo, IMetaContentTypeProvider> MemberContentTypeMap { get; private set; }

		internal Container<MemberInfo, ListPart<TContext>> MemberListPartMap { get; private set; }

		public ListPart<TContext> List(string title)
		{
			ListParts.Register(title, new ListPart<TContext>(this, title));

			return ListParts.Resolve(title);
		}

		MetaContext IMetaContextProvider.GetMetaContext()
		{
			return new MetaContext(ListParts.Select(n => n.Value).ToList());
		}
	}
}