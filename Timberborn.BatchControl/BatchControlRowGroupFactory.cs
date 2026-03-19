using System;
using System.Collections.Generic;
using Timberborn.CoreUI;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.BatchControl
{
	// Token: 0x02000017 RID: 23
	public class BatchControlRowGroupFactory
	{
		// Token: 0x06000070 RID: 112 RVA: 0x0000341A File Offset: 0x0000161A
		public BatchControlRowGroupFactory(VisualElementLoader visualElementLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003430 File Offset: 0x00001630
		public BatchControlRowGroup CreateUnsorted(BatchControlRow header)
		{
			return this.Create(header, null, "");
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000343F File Offset: 0x0000163F
		public BatchControlRowGroup CreateSortedWithTextHeader(string headerTextLocKey, string sortingKey)
		{
			return this.Create(this._loc.T(headerTextLocKey), sortingKey);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003454 File Offset: 0x00001654
		public BatchControlRowGroup CreateSortedWithTextHeader(string headerTextLocKey)
		{
			string text = this._loc.T(headerTextLocKey);
			return this.Create(text, text);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003478 File Offset: 0x00001678
		public BatchControlRowGroup Create(string headerText, string sortingKey)
		{
			string elementName = "Game/BatchControl/BatchControlHeaderRow";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			UQueryExtensions.Q<Label>(visualElement, "Text", null).text = headerText;
			BatchControlRowGroupChildrenCounter batchControlRowGroupChildrenCounter = this.CreateCounter();
			BatchControlRowGroup batchControlRowGroup = this.Create(new BatchControlRow(visualElement, new IBatchControlRowItem[]
			{
				batchControlRowGroupChildrenCounter
			}), new SortableNameRowComparer(), sortingKey);
			batchControlRowGroupChildrenCounter.SetRowGroup(batchControlRowGroup);
			return batchControlRowGroup;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000034D6 File Offset: 0x000016D6
		public BatchControlRowGroup Create(BatchControlRow header, IComparer<BatchControlRow> comparer = null, string sortingKey = "")
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Game/BatchControl/BatchControlRowGroup");
			visualElement.Add(header.Root);
			return new BatchControlRowGroup(visualElement, sortingKey, header, comparer);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000034FC File Offset: 0x000016FC
		public BatchControlRowGroupChildrenCounter CreateCounter()
		{
			string elementName = "Game/BatchControl/BatchControlRowGroupChildrenCounter";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			Label counter = UQueryExtensions.Q<Label>(visualElement, "Counter", null);
			return new BatchControlRowGroupChildrenCounter(visualElement, counter);
		}

		// Token: 0x0400004F RID: 79
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000050 RID: 80
		public readonly ILoc _loc;
	}
}
