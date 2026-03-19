using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using Timberborn.Workshops;
using UnityEngine.UIElements;

namespace Timberborn.WorkshopsUI
{
	// Token: 0x0200001D RID: 29
	public class ProductivityFragment : IEntityPanelFragment
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00003A24 File Offset: 0x00001C24
		public ProductivityFragment(VisualElementLoader visualElementLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003A3C File Offset: 0x00001C3C
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/ProductivityFragment");
			this._text = UQueryExtensions.Q<Label>(this._root, "Text", null);
			VisualElement tooltip = UQueryExtensions.Q<VisualElement>(this._root, "Tooltip", null);
			this._root.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				tooltip.ToggleDisplayStyle(true);
			}, 0);
			this._root.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				tooltip.ToggleDisplayStyle(false);
			}, 0);
			tooltip.ToggleDisplayStyle(false);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003AE1 File Offset: 0x00001CE1
		public void ShowFragment(BaseComponent entity)
		{
			this._workshopProductivityCounter = entity.GetComponent<WorkshopProductivityCounter>();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003AEF File Offset: 0x00001CEF
		public void ClearFragment()
		{
			this._workshopProductivityCounter = null;
			this.UpdateFragment();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003B00 File Offset: 0x00001D00
		public void UpdateFragment()
		{
			if (this._workshopProductivityCounter && this._workshopProductivityCounter.Enabled)
			{
				float num = this._workshopProductivityCounter.CalculateProductivity();
				this._text.text = this._loc.T(ProductivityFragment.TitleLocKey) + " " + NumberFormatter.FormatAsPercentCeiled((double)num);
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000082 RID: 130
		public static readonly string TitleLocKey = "Work.Productivity";

		// Token: 0x04000083 RID: 131
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000084 RID: 132
		public readonly ILoc _loc;

		// Token: 0x04000085 RID: 133
		public WorkshopProductivityCounter _workshopProductivityCounter;

		// Token: 0x04000086 RID: 134
		public VisualElement _root;

		// Token: 0x04000087 RID: 135
		public Label _text;
	}
}
