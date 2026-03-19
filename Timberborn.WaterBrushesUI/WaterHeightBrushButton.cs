using System;
using System.Collections.Generic;
using Timberborn.BottomBarSystem;
using Timberborn.ToolButtonSystem;

namespace Timberborn.WaterBrushesUI
{
	// Token: 0x02000009 RID: 9
	public class WaterHeightBrushButton : IBottomBarElementsProvider
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000215A File Offset: 0x0000035A
		public WaterHeightBrushButton(WaterHeightBrushTool waterHeightBrushTool, ToolButtonFactory toolButtonFactory)
		{
			this._waterHeightBrushTool = waterHeightBrushTool;
			this._toolButtonFactory = toolButtonFactory;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002170 File Offset: 0x00000370
		public IEnumerable<BottomBarElement> GetElements()
		{
			ToolButton toolButton = this._toolButtonFactory.CreateGrouplessRed(this._waterHeightBrushTool, WaterHeightBrushButton.ToolImageKey);
			yield return BottomBarElement.CreateSingleLevel(toolButton.Root);
			yield break;
		}

		// Token: 0x04000009 RID: 9
		public static readonly string ToolImageKey = "WaterHeightBrushTool";

		// Token: 0x0400000A RID: 10
		public readonly WaterHeightBrushTool _waterHeightBrushTool;

		// Token: 0x0400000B RID: 11
		public readonly ToolButtonFactory _toolButtonFactory;
	}
}
