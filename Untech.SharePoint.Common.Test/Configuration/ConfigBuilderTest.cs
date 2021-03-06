﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Untech.SharePoint.Common.Configuration;
using Untech.SharePoint.Common.Test.Mappings.Annotation;

namespace Untech.SharePoint.Common.Test.Configuration
{
	[TestClass]
	public class ConfigBuilderTest
	{
		[TestMethod]
		public void CanBuildConfig()
		{
			Assert.IsNotNull(new ConfigBuilder().BuildConfig());
		}

		[TestMethod]
		public void CanRegisterMappings()
		{
			var config = new ConfigBuilder()
				.RegisterMappings(n => n.Annotated<AnnotatedContextMappingTest.Ctx>())
				.BuildConfig();

			Assert.IsNotNull(config.Mappings.Resolve(typeof(AnnotatedContextMappingTest.Ctx)));
		}

		[TestMethod]
		public void CanRegisterBuiltInConverters()
		{
			var config = new ConfigBuilder()
				.RegisterConverters(n => n.AddFromAssembly(typeof(ConfigBuilder).Assembly))
				.RegisterConverters(n => n.AddFromAssembly(typeof(ConfigBuilderTest).Assembly))
				.BuildConfig();

			Assert.IsNotNull(config.FieldConverters.Resolve("BUILT_IN_TEST_CONVERTER"));
		}

		[TestMethod]
		public void CanRegisterConverter()
		{
			var config = new ConfigBuilder()
				.RegisterConverters(n => n.Add<BuiltInFieldConverter>())
				.BuildConfig();

			Assert.IsNotNull(config.FieldConverters.Resolve(typeof(BuiltInFieldConverter)));
		}

		[TestMethod]
		public void CanChainCalls()
		{
			var config = new ConfigBuilder()
				.RegisterMappings(n => n.Annotated<AnnotatedContextMappingTest.Ctx>())
				.RegisterConverters(n => n.AddFromAssembly(typeof(ConfigBuilderTest).Assembly))
				.RegisterConverters(n => n.Add<BuiltInFieldConverter>())
				.BuildConfig();

			Assert.IsNotNull(config.Mappings.Resolve(typeof (AnnotatedContextMappingTest.Ctx)));
			Assert.IsNotNull(config.FieldConverters.Resolve("BUILT_IN_TEST_CONVERTER"));
			Assert.IsNotNull(config.FieldConverters.Resolve(typeof(BuiltInFieldConverter)));
		}
	}
}
