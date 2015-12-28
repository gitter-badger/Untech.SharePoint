﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint;
using Untech.SharePoint.Common.Data;
using Untech.SharePoint.Common.Data.Mapper;
using Untech.SharePoint.Common.MetaModels;
using Untech.SharePoint.Server.Utils;

namespace Untech.SharePoint.Server.Data
{
	internal class SpListItemsProvider : BaseSpListItemsProvider<SPListItem>
	{
		private readonly SPList _spList;

		public SpListItemsProvider(SPWeb web, MetaList list)
			: base(list)
		{
			_spList = web.Lists[list.Title];
		}

		protected override IList<SPListItem> FetchInternal(string caml)
		{
			return _spList.GetItems(CamlUtility.CamlStringToSPQuery(caml))
				.Cast<SPListItem>()
				.ToList();
		}

		protected override SPListItem GetInternal(int id, MetaContentType contentType)
		{
			var spItem = _spList.GetItemById(id);

			if (spItem.ContentTypeId.ToString() != contentType.Id)
			{
				throw new InvalidOperationException("ContentType mismatch");
			}

			return spItem;
		}

		protected override int AddInternal(object item, TypeMapper<SPListItem> mapper)
		{
			var spItem = _spList.AddItem();

			mapper.Map(item, spItem);

			spItem.Update();

			return spItem.ID;
		}

		protected override void UpdateInternal(int id, object item, TypeMapper<SPListItem> mapper)
		{
			var spItem = _spList.GetItemById(id);

			mapper.Map(item, spItem);

			spItem.Update();
		}

		protected override void DeleteInternal(int id)
		{
			var spItem = _spList.GetItemById(id);

			spItem.Delete();
		}
	}
}