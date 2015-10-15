using Untech.SharePoint.Mappings.ClassLike.Test.Models;

namespace Untech.SharePoint.Mappings.ClassLike.Test.Maps
{
	public class MySpContextMap : ContextMap<MySpContext>
	{
		public MySpContextMap()
		{
			List("List1")
				.For(n => n.List1)
				.ContentType(new SimpleEntityMap());

			List("List2")
				.For(n => n.List2ContentType1)
				.For(n => n.List2ContentType2)
				.ContentType(new SecondEntityMap());
		}
	}
}