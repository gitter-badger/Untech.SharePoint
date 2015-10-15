using Untech.SharePoint.Mappings.ClassLike.Test.Models;

namespace Untech.SharePoint.Mappings.ClassLike.Test.Maps
{
	public class SecondEntityMap : ContentTypeMap<SimpleEntity>
	{
		public SecondEntityMap()
		{
			ContentTypeId("0x01");

			Field(n => n.String1).InternalName("LALA").TypeAsString("Text");
			Field(n => n.String2).InternalName("HEHE").TypeAsString("Text");
			Field(n => n.String3).TypeAsString("WOW");
		}
	}

}