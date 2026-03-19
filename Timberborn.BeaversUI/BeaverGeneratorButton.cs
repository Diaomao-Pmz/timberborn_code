using System;
using System.Collections.Generic;
using Timberborn.BottomBarSystem;
using Timberborn.ToolButtonSystem;

namespace Timberborn.BeaversUI
{
	// Token: 0x0200000F RID: 15
	public class BeaverGeneratorButton : IBottomBarElementsProvider
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002D17 File Offset: 0x00000F17
		public BeaverGeneratorButton(BeaverGeneratorTool beaverGeneratorTool, ToolButtonFactory toolButtonFactory)
		{
			this._beaverGeneratorTool = beaverGeneratorTool;
			this._toolButtonFactory = toolButtonFactory;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D2D File Offset: 0x00000F2D
		public IEnumerable<BottomBarElement> GetElements()
		{
			ToolButton toolButton = this._toolButtonFactory.CreateGrouplessRed(this._beaverGeneratorTool, BeaverGeneratorButton.ToolImageKey);
			yield return BottomBarElement.CreateSingleLevel(toolButton.Root);
			yield break;
		}

		// Token: 0x04000046 RID: 70
		public static readonly string ToolImageKey = "BeaverGeneratorTool";

		// Token: 0x04000047 RID: 71
		public readonly BeaverGeneratorTool _beaverGeneratorTool;

		// Token: 0x04000048 RID: 72
		public readonly ToolButtonFactory _toolButtonFactory;
	}
}
