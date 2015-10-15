using Microsoft.VisualStudio.TestTools.UnitTesting;
using Untech.SharePoint.Common.Configuration;
using Untech.SharePoint.Mappings.ClassLike.Test.Maps;

namespace Untech.SharePoint.Mappings.ClassLike.Test
{

	[TestClass]
	public class ClassLikeMappingTest
	{
		[TestMethod]
		public void CanRun()
		{
			new ConfigBuilder()
				.RegisterMappings(n => n.ClassLike(new MySpContextMap()))
				.BuildConfig();
		}
	}
}
