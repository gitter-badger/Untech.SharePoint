using Microsoft.VisualStudio.TestTools.UnitTesting;
using Untech.SharePoint.Common.Data;

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

	public class MySpContext : ISpContext
	{
		public ISpList<SimpleEntity> List1 { get; set; }
		public ISpList<SimpleEntity> List2ContentType1 { get; set; }
		public ISpList<SimpleEntity> List2ContentType2 { get; set; }
	}

	public class MySpContextMap : ContextMap<MySpContext>
	{
		public MySpContextMap()
		{
			List("List1").ContentType(n => n.List1, new SimpleEntityMap());

			List("List2")
				.ContentType(n => n.List2ContentType1, new SimpleEntityMap())
				.ContentType(n => n.List2ContentType2, new SecondEntityMap());
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
