using System;
using Timberborn.BatchControl;
using Timberborn.CoreUI;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x0200001A RID: 26
	public class ProductivityBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem, IFinishableBatchControlRowItem
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000037DF File Offset: 0x000019DF
		public VisualElement Root { get; }

		// Token: 0x06000083 RID: 131 RVA: 0x000037E7 File Offset: 0x000019E7
		public ProductivityBatchControlRowItem(VisualElement root, Image productivity, WorkshopProductivityCounter workshopProductivityCounter)
		{
			this.Root = root;
			this._productivity = productivity;
			this._workshopProductivityCounter = workshopProductivityCounter;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003804 File Offset: 0x00001A04
		public void UpdateRowItem()
		{
			this.DisableAllProductivityMarkers();
			this.SetProductivityMarker();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003812 File Offset: 0x00001A12
		public void SetFinishedState(bool isFinished)
		{
			this.Root.ToggleDisplayStyle(isFinished);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003820 File Offset: 0x00001A20
		public void DisableAllProductivityMarkers()
		{
			this._productivity.RemoveFromClassList(ProductivityBatchControlRowItem.VeryLowClass);
			this._productivity.RemoveFromClassList(ProductivityBatchControlRowItem.LowClass);
			this._productivity.RemoveFromClassList(ProductivityBatchControlRowItem.MediumClass);
			this._productivity.RemoveFromClassList(ProductivityBatchControlRowItem.HighClass);
			this._productivity.RemoveFromClassList(ProductivityBatchControlRowItem.VeryHighClass);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003880 File Offset: 0x00001A80
		public void SetProductivityMarker()
		{
			float productivity = this._workshopProductivityCounter.CalculateProductivity();
			this._productivity.AddToClassList(ProductivityBatchControlRowItem.GetImageClass(productivity));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000038AC File Offset: 0x00001AAC
		public static string GetImageClass(float productivity)
		{
			string result;
			if (productivity > 0.4f)
			{
				if (productivity <= 0.8f)
				{
					if (productivity <= 0.6f)
					{
						result = ProductivityBatchControlRowItem.MediumClass;
					}
					else
					{
						result = ProductivityBatchControlRowItem.HighClass;
					}
				}
				else
				{
					result = ProductivityBatchControlRowItem.VeryHighClass;
				}
			}
			else if (productivity <= 0.2f)
			{
				result = ProductivityBatchControlRowItem.VeryLowClass;
			}
			else
			{
				result = ProductivityBatchControlRowItem.LowClass;
			}
			return result;
		}

		// Token: 0x04000074 RID: 116
		public static readonly string VeryLowClass = "productivity-batch-control-row-item__icon--very-low";

		// Token: 0x04000075 RID: 117
		public static readonly string LowClass = "productivity-batch-control-row-item__icon--low";

		// Token: 0x04000076 RID: 118
		public static readonly string MediumClass = "productivity-batch-control-row-item__icon--medium";

		// Token: 0x04000077 RID: 119
		public static readonly string HighClass = "productivity-batch-control-row-item__icon--high";

		// Token: 0x04000078 RID: 120
		public static readonly string VeryHighClass = "productivity-batch-control-row-item__icon--very-high";

		// Token: 0x0400007A RID: 122
		public readonly Image _productivity;

		// Token: 0x0400007B RID: 123
		public readonly WorkshopProductivityCounter _workshopProductivityCounter;
	}
}
