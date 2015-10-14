using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Untech.SharePoint.Mappings.ClassLike.Test
{
	public class SimpleEntity
	{
		public string String1 { get; set; }
		public string String2 { get; set; }

		public string String3 { get; set; }

		public int Int1 { get; set; }
		public int Int2 { get; set; }

		public int Int3 { get; set; }

		public bool Bool1 { get; set; }
		public bool Bool2 { get; set; }
		public bool Bool3 { get; set; }
	}

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

	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
		}
	}
}
