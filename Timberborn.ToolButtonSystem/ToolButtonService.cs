using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;

namespace Timberborn.ToolButtonSystem
{
	// Token: 0x0200000A RID: 10
	public class ToolButtonService : ILoadableSingleton, IPostLoadableSingleton
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000028E4 File Offset: 0x00000AE4
		public ToolButtonService(ToolbarButtonRetriever toolbarButtonRetriever, ToolGroupService toolGroupService, ToolUnlockingService toolUnlockingService)
		{
			this._toolbarButtonRetriever = toolbarButtonRetriever;
			this._toolGroupService = toolGroupService;
			this._toolUnlockingService = toolUnlockingService;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002938 File Offset: 0x00000B38
		public ReadOnlyList<ToolButton> ToolButtons
		{
			get
			{
				return this._toolButtons.AsReadOnlyList<ToolButton>();
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002945 File Offset: 0x00000B45
		public void Add(ToolButton toolButton)
		{
			this._toolButtons.Add(toolButton);
			this._toolToButtonMap[toolButton.Tool] = toolButton;
			this.UpdateRootTools(toolButton.Tool);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002971 File Offset: 0x00000B71
		public void Add(ToolGroupButton toolButton)
		{
			this._toolGroupButtons.Add(toolButton);
			this._rootButtons.Add(toolButton);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000298B File Offset: 0x00000B8B
		public void Load()
		{
			this._toolGroupService.ToolGroupAssigned += this.OnToolGroupAssigned;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void PostLoad()
		{
			foreach (ToolButton toolButton in this._toolButtons)
			{
				toolButton.PostLoad();
				this._toolUnlockingService.LockIfNeeded(toolButton.Tool);
			}
			foreach (ToolGroupButton toolGroupButton in this._toolGroupButtons)
			{
				toolGroupButton.PostLoad();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A48 File Offset: 0x00000C48
		public ToolButton GetToolButton<TTool>(Predicate<TTool> predicate) where TTool : ITool
		{
			return this.ToolButtons.Single(delegate(ToolButton toolButton)
			{
				ITool tool = toolButton.Tool;
				if (tool is TTool)
				{
					TTool obj = (TTool)((object)tool);
					return predicate(obj);
				}
				return false;
			});
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A7E File Offset: 0x00000C7E
		public ToolButton GetToolButton<TTool>() where TTool : ITool
		{
			return this.GetToolButton<TTool>((TTool _) => true);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public ToolGroupButton GetToolGroupButton(ToolButton toolButton)
		{
			return this._toolGroupButtons.Single((ToolGroupButton toolGroupButton) => toolGroupButton.HasToolButton(toolButton));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AD9 File Offset: 0x00000CD9
		public bool TryGetNextRootButton(out IToolbarButton nextButton)
		{
			return this._toolbarButtonRetriever.TryGetNextVisibleButton(this._rootButtons, out nextButton);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002AED File Offset: 0x00000CED
		public bool TryGetPreviousRootButton(out IToolbarButton previousButton)
		{
			return this._toolbarButtonRetriever.TryGetPreviousVisibleButton(this._rootButtons, out previousButton);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B04 File Offset: 0x00000D04
		public bool TryGetNextToolButton(out IToolbarButton toolButton)
		{
			ToolGroupButton toolGroupButton;
			if (this.TryGetActiveToolGroupButton(out toolGroupButton) && this._toolbarButtonRetriever.TryGetNextVisibleButton(toolGroupButton.ToolButtons, out toolButton))
			{
				return true;
			}
			toolButton = null;
			return false;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B3C File Offset: 0x00000D3C
		public bool TryGetPreviousToolButton(out IToolbarButton toolButton)
		{
			ToolGroupButton toolGroupButton;
			if (this.TryGetActiveToolGroupButton(out toolGroupButton) && this._toolbarButtonRetriever.TryGetPreviousVisibleButton(toolGroupButton.ToolButtons, out toolButton))
			{
				return true;
			}
			toolButton = null;
			return false;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B72 File Offset: 0x00000D72
		public bool TryGetActiveToolGroupButton(out ToolGroupButton toolGroupButton)
		{
			toolGroupButton = this._toolGroupButtons.LastOrDefault((ToolGroupButton button) => button.IsActive);
			return toolGroupButton != null;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public void OnToolGroupAssigned(object sender, ITool tool)
		{
			this.UpdateRootTools(tool);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public void UpdateRootTools(ITool tool)
		{
			ToolButton item;
			if (this._toolToButtonMap.TryGetValue(tool, out item))
			{
				bool flag = this._rootButtons.Contains(item);
				bool flag2 = this._toolGroupService.IsAssignedToAnyGroup(tool);
				if (flag && flag2)
				{
					this._rootButtons.Remove(item);
					return;
				}
				if (!flag && !flag2)
				{
					this._rootButtons.Add(item);
				}
			}
		}

		// Token: 0x0400002A RID: 42
		public readonly ToolbarButtonRetriever _toolbarButtonRetriever;

		// Token: 0x0400002B RID: 43
		public readonly ToolGroupService _toolGroupService;

		// Token: 0x0400002C RID: 44
		public readonly ToolUnlockingService _toolUnlockingService;

		// Token: 0x0400002D RID: 45
		public readonly List<ToolButton> _toolButtons = new List<ToolButton>();

		// Token: 0x0400002E RID: 46
		public readonly Dictionary<ITool, ToolButton> _toolToButtonMap = new Dictionary<ITool, ToolButton>();

		// Token: 0x0400002F RID: 47
		public readonly List<ToolGroupButton> _toolGroupButtons = new List<ToolGroupButton>();

		// Token: 0x04000030 RID: 48
		public readonly List<IToolbarButton> _rootButtons = new List<IToolbarButton>();
	}
}
