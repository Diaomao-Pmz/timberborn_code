using System;
using System.Collections.Generic;
using Timberborn.BottomBarSystem;
using Timberborn.ToolButtonSystem;

namespace Timberborn.BotsUI
{
	// Token: 0x02000009 RID: 9
	public class BotGeneratorButton : IBottomBarElementsProvider
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002238 File Offset: 0x00000438
		public BotGeneratorButton(BotGeneratorTool botGeneratorTool, ToolButtonFactory toolButtonFactory)
		{
			this._botGeneratorTool = botGeneratorTool;
			this._toolButtonFactory = toolButtonFactory;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000224E File Offset: 0x0000044E
		public IEnumerable<BottomBarElement> GetElements()
		{
			ToolButton toolButton = this._toolButtonFactory.CreateGrouplessRed(this._botGeneratorTool, BotGeneratorButton.ToolImageKey);
			yield return BottomBarElement.CreateSingleLevel(toolButton.Root);
			yield break;
		}

		// Token: 0x04000011 RID: 17
		public static readonly string ToolImageKey = "BotGeneratorTool";

		// Token: 0x04000012 RID: 18
		public readonly BotGeneratorTool _botGeneratorTool;

		// Token: 0x04000013 RID: 19
		public readonly ToolButtonFactory _toolButtonFactory;
	}
}
