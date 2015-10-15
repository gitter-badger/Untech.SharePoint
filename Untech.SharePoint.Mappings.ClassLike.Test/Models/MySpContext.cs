using Untech.SharePoint.Common.Data;

namespace Untech.SharePoint.Mappings.ClassLike.Test.Models
{
	public class MySpContext : ISpContext
	{
		public ISpList<SimpleEntity> List1 { get; set; }
		public ISpList<SimpleEntity> List2ContentType1 { get; set; }
		public ISpList<SimpleEntity> List2ContentType2 { get; set; }
	}

}