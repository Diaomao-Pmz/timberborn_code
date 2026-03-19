using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.Goods;
using Timberborn.RecoverableGoodSystem;
using UnityEngine.UIElements;

namespace Timberborn.RecoverableGoodSystemUI
{
	// Token: 0x02000007 RID: 7
	public class RecoverableGoodElementFactory
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002202 File Offset: 0x00000402
		public RecoverableGoodElementFactory(IGoodService goodService, RecoverableGoodItemFactory recoverableGoodItemFactory, VisualElementLoader visualElementLoader)
		{
			this._goodService = goodService;
			this._recoverableGoodItemFactory = recoverableGoodItemFactory;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000222C File Offset: 0x0000042C
		public RecoverableGoodElement Create()
		{
			string elementName = "Game/RecoverableGood/RecoverableGoodContent";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			Label label = UQueryExtensions.Q<Label>(visualElement, "Label", null);
			VisualElement parent = UQueryExtensions.Q<VisualElement>(visualElement, "Items", null);
			IEnumerable<RecoverableGoodItem> recoverableGoodItems = this.CreateRecoverableGoodItems(parent);
			return new RecoverableGoodElement(visualElement, label, recoverableGoodItems);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002274 File Offset: 0x00000474
		public VisualElement Create(IEnumerable<BlockObject> blockObjects)
		{
			RecoverableGoodElement recoverableGoodElement = this.Create();
			recoverableGoodElement.Root.AddToClassList(RecoverableGoodElementFactory.InBoxClass);
			RecoverableGoodRegistry recoverableGoodRegistry = this.GetRecoverableGoodRegistry(blockObjects);
			recoverableGoodElement.Update(recoverableGoodRegistry);
			return recoverableGoodElement.Root;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022AB File Offset: 0x000004AB
		public IEnumerable<RecoverableGoodItem> CreateRecoverableGoodItems(VisualElement parent)
		{
			foreach (string goodId in this._goodService.Goods)
			{
				RecoverableGoodItem recoverableGoodItem = this._recoverableGoodItemFactory.Create(goodId);
				parent.Add(recoverableGoodItem.Root);
				yield return recoverableGoodItem;
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C4 File Offset: 0x000004C4
		public RecoverableGoodRegistry GetRecoverableGoodRegistry(IEnumerable<BlockObject> blockObjects)
		{
			RecoverableGoodRegistry recoverableGoodRegistry = new RecoverableGoodRegistry();
			this.FillBlockObjectsCache(blockObjects);
			foreach (BlockObject blockObject in this._blockObjectsCache)
			{
				RecoverableGoodElementFactory.AddFromRecoverableGoodProvider(blockObject, recoverableGoodRegistry);
			}
			this._blockObjectsCache.Clear();
			return recoverableGoodRegistry;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002330 File Offset: 0x00000530
		public void FillBlockObjectsCache(IEnumerable<BlockObject> blockObjects)
		{
			foreach (BlockObject blockObject in blockObjects)
			{
				this._blockObjectsCache.Add(blockObject);
				IRecoverableObjectAdder component = blockObject.GetComponent<IRecoverableObjectAdder>();
				if (component != null)
				{
					BlockObject additionalObjectToRecover = component.GetAdditionalObjectToRecover();
					if (additionalObjectToRecover)
					{
						this._blockObjectsCache.Add(additionalObjectToRecover);
					}
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023A4 File Offset: 0x000005A4
		public static void AddFromRecoverableGoodProvider(BlockObject blockObject, RecoverableGoodRegistry recoverableGoodRegistry)
		{
			RecoverableGoodProvider component = blockObject.GetComponent<RecoverableGoodProvider>();
			if (component != null)
			{
				component.GetRecoverableGoods(recoverableGoodRegistry);
			}
		}

		// Token: 0x0400000B RID: 11
		public static readonly string InBoxClass = "recoverable-good-content--in-box";

		// Token: 0x0400000C RID: 12
		public readonly IGoodService _goodService;

		// Token: 0x0400000D RID: 13
		public readonly RecoverableGoodItemFactory _recoverableGoodItemFactory;

		// Token: 0x0400000E RID: 14
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000F RID: 15
		public readonly HashSet<BlockObject> _blockObjectsCache = new HashSet<BlockObject>();
	}
}
