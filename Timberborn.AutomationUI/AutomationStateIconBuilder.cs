using System;
using Timberborn.Automation;
using Timberborn.SelectionSystem;
using UnityEngine.UIElements;

namespace Timberborn.AutomationUI
{
	// Token: 0x0200000E RID: 14
	public class AutomationStateIconBuilder
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000027AA File Offset: 0x000009AA
		public AutomationStateIconBuilder(EntitySelectionService entitySelectionService)
		{
			this._entitySelectionService = entitySelectionService;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027B9 File Offset: 0x000009B9
		public AutomationStateIconBuilder.Builder Create(Image icon, Func<Automator> automatorGetter)
		{
			return new AutomationStateIconBuilder.Builder(this._entitySelectionService, icon, automatorGetter);
		}

		// Token: 0x04000022 RID: 34
		public static readonly string ClickableClass = "clickable";

		// Token: 0x04000023 RID: 35
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x0200000F RID: 15
		public class Builder
		{
			// Token: 0x0600002A RID: 42 RVA: 0x000027D4 File Offset: 0x000009D4
			public Builder(EntitySelectionService entitySelectionService, Image icon, Func<Automator> automatorGetter)
			{
				this._entitySelectionService = entitySelectionService;
				this._icon = icon;
				this._automatorGetter = automatorGetter;
			}

			// Token: 0x0600002B RID: 43 RVA: 0x000027F1 File Offset: 0x000009F1
			public AutomationStateIconBuilder.Builder SetClickableIcon()
			{
				this._clickable = true;
				return this;
			}

			// Token: 0x0600002C RID: 44 RVA: 0x000027FC File Offset: 0x000009FC
			public AutomationStateIcon Build()
			{
				AutomationStateIcon result = new AutomationStateIcon(this._automatorGetter, this._icon);
				if (this._clickable)
				{
					this._icon.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
					{
						this.OnStateIconClicked(this._automatorGetter);
					}, 0);
					this._icon.AddToClassList(AutomationStateIconBuilder.ClickableClass);
				}
				return result;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x0000284C File Offset: 0x00000A4C
			public void OnStateIconClicked(Func<Automator> automatorGetter)
			{
				Automator automator = automatorGetter();
				if (automator != null)
				{
					this._entitySelectionService.SelectAndFocusOn(automator);
				}
			}

			// Token: 0x04000024 RID: 36
			public readonly EntitySelectionService _entitySelectionService;

			// Token: 0x04000025 RID: 37
			public readonly Image _icon;

			// Token: 0x04000026 RID: 38
			public readonly Func<Automator> _automatorGetter;

			// Token: 0x04000027 RID: 39
			public bool _clickable;
		}
	}
}
