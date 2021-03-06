﻿using System;
using Untech.SharePoint.Common.CodeAnnotations;
using Untech.SharePoint.Common.Collections;
using Untech.SharePoint.Common.MetaModels.Visitors;
using Untech.SharePoint.Common.Utils;

namespace Untech.SharePoint.Common.MetaModels
{
	/// <summary>
	/// Represent base meta model class that implements <see cref="IMetaModel"/>
	/// </summary>
	public abstract class BaseMetaModel : IMetaModel
	{
		/// <summary>
		/// Initializes base meta model instance.
		/// </summary>
		protected BaseMetaModel()
		{
			AdditionalProperties  = new Container<string, object>();
		}

		[NotNull]
		private Container<string, object> AdditionalProperties { get; set; }

		/// <summary>
		/// Accepts <see cref="IMetaModelVisitor"/> instance.
		/// </summary>
		/// <param name="visitor">Visitor to accept.</param>
		public abstract void Accept(IMetaModelVisitor visitor);

		/// <summary>
		/// Gets additional property value associated with the specified key.
		/// </summary>
		/// <typeparam name="T">Type of the additional property value.</typeparam>
		/// <param name="key">The key of the additional property.</param>
		/// <returns>Property value associated with the specified key.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
		[NotNull]
		public virtual T GetAdditionalProperty<T>([NotNull]string key)
		{
			Guard.CheckNotNull("key", key);

			return (T) AdditionalProperties.Resolve(key);
		}

		/// <summary>
		/// Sets additional property value associated with the specified key.
		/// </summary>
		/// <typeparam name="T">Type of the additional property value.</typeparam>
		/// <param name="key">The key of the additional property.</param>
		/// <param name="value">Property value associated with the specified key.</param>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> or <paramref name="value"/> is null.</exception>
		public virtual void SetAdditionalProperty<T>([NotNull]string key, [NotNull]T value)
		{
			Guard.CheckNotNull("key", key);
			Guard.CheckNotNull("value", value);

			AdditionalProperties.Register(key, value);
		}
	}
}