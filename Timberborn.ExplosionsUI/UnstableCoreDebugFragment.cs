using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.Explosions;
using Timberborn.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.ExplosionsUI
{
	// Token: 0x02000009 RID: 9
	public class UnstableCoreDebugFragment : IEntityPanelFragment, IInputProcessor
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000254C File Offset: 0x0000074C
		public UnstableCoreDebugFragment(DebugFragmentFactory debugFragmentFactory, InputService inputService, EntityService entityService)
		{
			this._debugFragmentFactory = debugFragmentFactory;
			this._inputService = inputService;
			this._entityService = entityService;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000256C File Offset: 0x0000076C
		public VisualElement InitializeFragment()
		{
			this._root = this._debugFragmentFactory.Create(new DebugFragmentButton[]
			{
				new DebugFragmentButton(new Action(this.OnExplodeClicked), "Explode"),
				new DebugFragmentButton(new Action(this.OnRemoveButtonClicked), "Delete without exploding")
			});
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025DC File Offset: 0x000007DC
		public void ShowFragment(BaseComponent entity)
		{
			this._unstableCore = entity.GetComponent<UnstableCore>();
			if (this._unstableCore)
			{
				this._unstableCoreExplosionBlocker = this._unstableCore.GetComponent<UnstableCoreExplosionBlocker>();
				this._inputService.AddInputProcessor(this);
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002126 File Offset: 0x00000326
		public void UpdateFragment()
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000262B File Offset: 0x0000082B
		public void ClearFragment()
		{
			if (this._unstableCore)
			{
				this._inputService.RemoveInputProcessor(this);
				this._unstableCore = null;
				this._unstableCoreExplosionBlocker = null;
				this._root.ToggleDisplayStyle(false);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002660 File Offset: 0x00000860
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(UnstableCoreDebugFragment.DetonateKey))
			{
				this.DetonateSelected();
				return true;
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000267D File Offset: 0x0000087D
		public void OnExplodeClicked()
		{
			this.DetonateSelected();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002685 File Offset: 0x00000885
		public void OnRemoveButtonClicked()
		{
			this._unstableCoreExplosionBlocker.BlockExplosion();
			this._entityService.Delete(this._unstableCore);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026A4 File Offset: 0x000008A4
		public void DetonateSelected()
		{
			this._unstableCoreExplosionBlocker.Disable();
			if (this._inputService.IsKeyHeld(UnstableCoreDebugFragment.DetonationDelayKey))
			{
				this._unstableCore.ActivateDelayed(10f);
				return;
			}
			if (this._inputService.IsKeyHeld(UnstableCoreDebugFragment.LongDetonationDelayKey))
			{
				this._unstableCore.ActivateDelayed(20f);
				return;
			}
			this._unstableCore.Activate();
		}

		// Token: 0x0400001D RID: 29
		public static readonly string DetonationDelayKey = "DetonationDelay";

		// Token: 0x0400001E RID: 30
		public static readonly string LongDetonationDelayKey = "LongDetonationDelay";

		// Token: 0x0400001F RID: 31
		public static readonly string DetonateKey = "DetonateUnstableCore";

		// Token: 0x04000020 RID: 32
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x04000021 RID: 33
		public readonly InputService _inputService;

		// Token: 0x04000022 RID: 34
		public readonly EntityService _entityService;

		// Token: 0x04000023 RID: 35
		public VisualElement _root;

		// Token: 0x04000024 RID: 36
		public UnstableCore _unstableCore;

		// Token: 0x04000025 RID: 37
		public UnstableCoreExplosionBlocker _unstableCoreExplosionBlocker;
	}
}
