using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.CoreUI;
using Timberborn.RecoverableGoodSystem;
using Timberborn.SingletonSystem;
using Timberborn.TooltipSystem;
using UnityEngine.UIElements;

namespace Timberborn.RecoverableGoodSystemUI
{
	// Token: 0x0200000C RID: 12
	public class RecoverableGoodTooltip : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000026A4 File Offset: 0x000008A4
		public RecoverableGoodTooltip(RecoverableGoodElementFactory recoverableGoodElementFactory, ITooltipRegistrar tooltipRegistrar, VisualElementLoader visualElementLoader)
		{
			this._recoverableGoodElementFactory = recoverableGoodElementFactory;
			this._tooltipRegistrar = tooltipRegistrar;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026CC File Offset: 0x000008CC
		public void Load()
		{
			string elementName = "Game/RecoverableGood/RecoverableGoodTooltip";
			this._tooltip = this._visualElementLoader.LoadVisualElement(elementName);
			this._recoverableGoodElement = this._recoverableGoodElementFactory.Create();
			this._tooltip.Add(this._recoverableGoodElement.Root);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002718 File Offset: 0x00000918
		public void Enable()
		{
			this._enabled = true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002721 File Offset: 0x00000921
		public void Disable()
		{
			this._enabled = false;
			this.Hide();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002730 File Offset: 0x00000930
		public void SetRecoverableGoods(IEnumerable<BlockObject> blockObjects)
		{
			this._recoverableGoodRegistry.Clear();
			foreach (BlockObject blockObject in blockObjects)
			{
				RecoverableGoodProvider component = blockObject.GetComponent<RecoverableGoodProvider>();
				if (component != null)
				{
					component.GetRecoverableGoods(this._recoverableGoodRegistry);
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002790 File Offset: 0x00000990
		public void UpdateSingleton()
		{
			if (this._enabled)
			{
				this.Update();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027A0 File Offset: 0x000009A0
		public void Update()
		{
			if (this._recoverableGoodRegistry.TotalAmount > 0)
			{
				this._tooltipRegistrar.ShowPriority(this._tooltip);
				this._recoverableGoodElement.Update(this._recoverableGoodRegistry);
				this._recoverableGoodRegistry.Clear();
				return;
			}
			this.Hide();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027EF File Offset: 0x000009EF
		public void Hide()
		{
			this._tooltipRegistrar.HidePriority();
		}

		// Token: 0x0400001E RID: 30
		public readonly RecoverableGoodElementFactory _recoverableGoodElementFactory;

		// Token: 0x0400001F RID: 31
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x04000020 RID: 32
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000021 RID: 33
		public RecoverableGoodElement _recoverableGoodElement;

		// Token: 0x04000022 RID: 34
		public VisualElement _tooltip;

		// Token: 0x04000023 RID: 35
		public readonly RecoverableGoodRegistry _recoverableGoodRegistry = new RecoverableGoodRegistry();

		// Token: 0x04000024 RID: 36
		public bool _enabled;
	}
}
