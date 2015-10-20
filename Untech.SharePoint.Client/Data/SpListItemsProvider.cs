﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint.Client;
using Untech.SharePoint.Client.MetaModels;
using Untech.SharePoint.Client.Utils;
using Untech.SharePoint.Common.Data;
using Untech.SharePoint.Common.MetaModels;

namespace Untech.SharePoint.Client.Data
{
	internal class SpListItemsProvider : ISpListItemsProvider
	{
		public SpListItemsProvider(ClientContext clientContext, SpCommonService commonService, MetaList list)
		{
			ClientContext = clientContext;
			List = list;
			CommonService = commonService;

			//SpList = web.Lists[list.Title];
		}

		public ClientContext ClientContext { get; private set; }

		public SpCommonService CommonService { get; private set; }

		public MetaList List { get; private set; }

		public List SpList { get; private set; }

		public IEnumerable<T> Fetch<T>(string caml)
		{
			return FetchInternal(caml)
				.Select(Materialize<T>);
		}

		public bool Any(string caml)
		{
			return FetchInternal(caml).Any();
		}

		public int Count(string caml)
		{
			return FetchInternal(caml).Count;
		}

		public T SingleOrDefault<T>(string caml)
		{
			var foundItems = FetchInternal(caml, 2);

			if (foundItems.Count > 1)
			{
				throw new InvalidOperationException("Multiple items match");
			}
			return foundItems.Count == 1 ? Materialize<T>(foundItems[0]) : default(T);
		}

		public T FirstOrDefault<T>(string caml)
		{
			var foundItems = FetchInternal(caml, 1);

			return foundItems.Count == 1 ? Materialize<T>(foundItems[0]) : default(T);
		}

		public T ElementAtOrDefault<T>(string caml, int index)
		{
			var foundItem = FetchInternal(caml, (uint) (index + 1)).ElementAtOrDefault(index);

			return foundItem != null ? Materialize<T>(foundItem) : default(T);
		}

		public void Add<T>(T item)
		{
			if (List.IsExternal)
			{
				throw new InvalidOperationException();
			}

			var contentType = List.ContentTypes[typeof(T)];
			var mapper = contentType.GetMapper();
			var idField = contentType.Fields.SingleOrDefault<MetaField>(n => n.InternalName == "ID");

			if (idField == null)
			{
				throw new InvalidOperationException();
			}

			var spItem = SpList.AddItem();

			mapper.Map(item, spItem);

			spItem.Update();
		}

		public void Update<T>(T item)
		{
			if (List.IsExternal)
			{
				throw new InvalidOperationException();
			}

			var contentType = List.ContentTypes[typeof(T)];
			var mapper = contentType.GetMapper();
			var idField = contentType.Fields.SingleOrDefault<MetaField>(n => n.InternalName == "ID");

			if (idField == null)
			{
				throw new InvalidOperationException();
			}

			var idValue = (int) idField.GetMapper().MemberGetter(item);

			var spItem = SpList.GetItemById(idValue);

			mapper.Map(item, spItem);

			spItem.Update();
		}

		private IList<ListItem> FetchInternal(string caml)
		{
			return SpList.GetItems(CamlUtility.CamlStringToSPQuery(caml))
				.Cast<ListItem>()
				.ToList();
		}

		private IList<ListItem> FetchInternal(string caml, uint overrideRowLimit)
		{
			return SpList.GetItems(CamlUtility.CamlStringToSPQuery(caml, overrideRowLimit))
				.Cast<ListItem>()
				.ToList();
		}

		private T Materialize<T>(ListItem item)
		{
			var contentType = List.ContentTypes[typeof (T)];
			var mapper = contentType.GetMapper();

			var materializedItem = (T) mapper.TypeCreator();

			mapper.Map(item, materializedItem);

			return materializedItem;
		}


	}
}