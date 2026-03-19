using System;
using System.Collections.Generic;
using Timberborn.BottomBarSystem;
using Timberborn.Localization;
using Timberborn.ToolButtonSystem;

namespace Timberborn.CursorToolSystem
{
	// Token: 0x02000004 RID: 4
	public class CursorButton : IBottomBarElementsProvider
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public CursorButton(ILoc loc, CursorTool cursorTool, ToolButtonFactory toolButtonFactory)
		{
			this._loc = loc;
			this._cursorTool = cursorTool;
			this._toolButtonFactory = toolButtonFactory;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DD File Offset: 0x000002DD
		public IEnumerable<BottomBarElement> GetElements()
		{
			ToolButton toolButton = this._toolButtonFactory.CreateGrouplessRed(this._cursorTool, CursorButton.ToolImageKey);
			toolButton.InitializeTooltip(this._loc.T(CursorButton.CursorTooltipLocKey));
			yield return BottomBarElement.CreateSingleLevel(toolButton.Root);
			yield break;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string ToolImageKey = "Cursor";

		// Token: 0x04000007 RID: 7
		public static readonly string CursorTooltipLocKey = "Tool.Cursor.Tooltip";

		// Token: 0x04000008 RID: 8
		public readonly ILoc _loc;

		// Token: 0x04000009 RID: 9
		public readonly CursorTool _cursorTool;

		// Token: 0x0400000A RID: 10
		public readonly ToolButtonFactory _toolButtonFactory;
	}
}
