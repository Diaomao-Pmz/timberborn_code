using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;

namespace Timberborn.ToolButtonSystem
{
	// Token: 0x02000005 RID: 5
	public class ToolbarButtonRetriever
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020C0 File Offset: 0x000002C0
		public bool TryGetNextVisibleButton(IReadOnlyList<IToolbarButton> buttons, out IToolbarButton nextButton)
		{
			int num = ToolbarButtonRetriever.GetActiveButtonIndex(buttons) + 1;
			for (int i = num; i < buttons.Count; i++)
			{
				if (buttons[i].IsVisible)
				{
					nextButton = buttons[i];
					return true;
				}
			}
			for (int j = 0; j < num; j++)
			{
				if (buttons[j].IsVisible)
				{
					nextButton = buttons[j];
					return true;
				}
			}
			nextButton = null;
			return false;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000212C File Offset: 0x0000032C
		public bool TryGetPreviousVisibleButton(IReadOnlyList<IToolbarButton> buttons, out IToolbarButton previousButton)
		{
			int num = ToolbarButtonRetriever.GetActiveButtonIndex(buttons) - 1;
			for (int i = num; i >= 0; i--)
			{
				if (buttons[i].IsVisible)
				{
					previousButton = buttons[i];
					return true;
				}
			}
			for (int j = buttons.Count - 1; j > num; j--)
			{
				if (buttons[j].IsVisible)
				{
					previousButton = buttons[j];
					return true;
				}
			}
			previousButton = null;
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002198 File Offset: 0x00000398
		public static int GetActiveButtonIndex(IReadOnlyList<IToolbarButton> buttons)
		{
			IToolbarButton obj = buttons.LastOrDefault((IToolbarButton button) => button.IsActive);
			return buttons.IndexOf(obj);
		}
	}
}
