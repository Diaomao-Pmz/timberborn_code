using System;
using Timberborn.Common;
using Timberborn.MultithreadingAnalysis;
using UnityEngine.UIElements;

namespace Timberborn.MultithreadingAnalysisUI
{
	// Token: 0x02000004 RID: 4
	public class MarkerView
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public MarkerView(VisualElement root, Marker marker)
		{
			this.Root = root;
			this._marker = marker;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DE File Offset: 0x000002DE
		public string GetTooltipText()
		{
			return "<b>" + this._marker.Id + "</b>\nThread: " + this._marker.Thread.DisplayName();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		public void SetScale(float scale, long referenceTimestamp)
		{
			this.Root.style.left = new StyleLength(new Length(scale * (float)(this._marker.Timestamp - referenceTimestamp) - 0.5f * MarkerView.PixelWidth, 0));
			this.Root.style.width = new StyleLength(new Length(MarkerView.PixelWidth, 0));
		}

		// Token: 0x04000006 RID: 6
		public static readonly float PixelWidth = 3f;

		// Token: 0x04000008 RID: 8
		public readonly Marker _marker;
	}
}
