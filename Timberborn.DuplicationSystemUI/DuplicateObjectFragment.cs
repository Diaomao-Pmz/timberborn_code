using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.InputSystemUI;
using Timberborn.Localization;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.DuplicationSystemUI
{
	// Token: 0x02000007 RID: 7
	public class DuplicateObjectFragment : IEntityPanelFragment
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public DuplicateObjectFragment(VisualElementLoader visualElementLoader, BindableButtonFactory bindableButtonFactory, DuplicationValidator duplicationValidator, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._bindableButtonFactory = bindableButtonFactory;
			this._duplicationValidator = duplicationValidator;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212C File Offset: 0x0000032C
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/EntityPanel/DuplicateObjectFragment");
			Button button = UQueryExtensions.Q<Button>(this._root, "Button", null);
			this._button = this._bindableButtonFactory.Create(button, DuplicateObjectFragment.DuplicateObjectKey, new Action(this.Callback), true);
			this._tooltipRegistrar.RegisterWithKeyBinding(button, DuplicateObjectFragment.DuplicateObjectKey);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A8 File Offset: 0x000003A8
		public void ShowFragment(BaseComponent entity)
		{
			Action toolActivationAction;
			if (this._duplicationValidator.CanDuplicateObject(entity, out toolActivationAction))
			{
				this._toolActivationAction = toolActivationAction;
				this._root.ToggleDisplayStyle(true);
				this._button.Bind();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E3 File Offset: 0x000003E3
		public void ClearFragment()
		{
			this._toolActivationAction = null;
			this._button.Unbind();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002203 File Offset: 0x00000403
		public void UpdateFragment()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002205 File Offset: 0x00000405
		public void Callback()
		{
			Action toolActivationAction = this._toolActivationAction;
			if (toolActivationAction == null)
			{
				return;
			}
			toolActivationAction();
		}

		// Token: 0x04000008 RID: 8
		public static readonly string DuplicateObjectKey = "DuplicateObject";

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly BindableButtonFactory _bindableButtonFactory;

		// Token: 0x0400000B RID: 11
		public readonly DuplicationValidator _duplicationValidator;

		// Token: 0x0400000C RID: 12
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400000D RID: 13
		public readonly ILoc _loc;

		// Token: 0x0400000E RID: 14
		public BindableButton _button;

		// Token: 0x0400000F RID: 15
		public VisualElement _root;

		// Token: 0x04000010 RID: 16
		public Action _toolActivationAction;
	}
}
