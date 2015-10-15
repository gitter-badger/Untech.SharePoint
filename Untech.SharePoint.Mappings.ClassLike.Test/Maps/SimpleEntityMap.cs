using Untech.SharePoint.Mappings.ClassLike.Test.Models;

namespace Untech.SharePoint.Mappings.ClassLike.Test.Maps
{

	public class SimpleEntityMap : ContentTypeMap<SimpleEntity>
	{
		public SimpleEntityMap()
		{
			ContentTypeId("0x01");

			Field(n => n.String1).InternalName("STRING1").TypeAsString("Text");
			Field(n => n.String2).InternalName("Title").TypeAsString("Text");
			Field(n => n.String3).TypeAsString("Note");
		}
	}
}