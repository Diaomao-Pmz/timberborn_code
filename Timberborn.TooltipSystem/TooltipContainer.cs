using System;
using Timberborn.CoreUI;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.TooltipSystem
{
	// Token: 0x02000009 RID: 9
	public class TooltipContainer : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002622 File Offset: 0x00000822
		public TooltipContainer(EventBus eventBus, MouseTooltipPositioner mouseTooltipPositioner, RootVisualElementProvider rootVisualElementProvider)
		{
			this._eventBus = eventBus;
			this._mouseTooltipPositioner = mouseTooltipPositioner;
			this._rootVisualElementProvider = rootVisualElementProvider;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002640 File Offset: 0x00000840
		public void Load()
		{
			this._root = this._rootVisualElementProvider.Create("TooltipContainer", "Core/TooltipContainer", 3);
			this._tooltip = UQueryExtensions.Q<VisualElement>(this._root, "TooltipWrapper", null);
			this._tooltip.ToggleDisplayStyle(false);
			this._eventBus.Register(this);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002698 File Offset: 0x00000898
		public void UpdateSingleton()
		{
			this._mouseTooltipPositioner.UpdatePosition(this._tooltip);
			this._tooltip.style.visibility = 0;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026C1 File Offset: 0x000008C1
		[OnEvent]
		public void OnUIVisibilityChanged(UIVisibilityChangedEvent uiVisibilityChangedEvent)
		{
			this._root.ToggleDisplayStyle(uiVisibilityChangedEvent.UIVisible);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026D4 File Offset: 0x000008D4
		public void Show(VisualElement content)
		{
			if (!this._showingPriority)
			{
				this.ShowInternal(content);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026E5 File Offset: 0x000008E5
		public void ShowPriority(VisualElement content)
		{
			if (!this._showingPriority)
			{
				this.ClearInternal();
				this.ShowInternal(content);
				this._showingPriority = true;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002703 File Offset: 0x00000903
		public void HidePriority()
		{
			if (this._showingPriority)
			{
				this.ClearInternal();
				this._showingPriority = false;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000271A File Offset: 0x0000091A
		public void Clear()
		{
			if (!this._showingPriority)
			{
				this.ClearInternal();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000272A File Offset: 0x0000092A
		public void ShowInternal(VisualElement content)
		{
			this._tooltip.Add(content);
			this._tooltip.ToggleDisplayStyle(true);
			this._tooltip.style.visibility = 1;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000275A File Offset: 0x0000095A
		public void ClearInternal()
		{
			this._tooltip.Clear();
			this._tooltip.ToggleDisplayStyle(false);
		}

		// Token: 0x0400001B RID: 27
		public readonly EventBus _eventBus;

		// Token: 0x0400001C RID: 28
		public readonly MouseTooltipPositioner _mouseTooltipPositioner;

		// Token: 0x0400001D RID: 29
		public readonly RootVisualElementProvider _rootVisualElementProvider;

		// Token: 0x0400001E RID: 30
		public VisualElement _root;

		// Token: 0x0400001F RID: 31
		public VisualElement _tooltip;

		// Token: 0x04000020 RID: 32
		public bool _showingPriority;
	}
}
