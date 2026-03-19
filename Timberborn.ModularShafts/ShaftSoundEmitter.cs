using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000018 RID: 24
	public class ShaftSoundEmitter : BaseComponent, IAwakableComponent, IUpdatableComponent, IFinishedStateListener
	{
		// Token: 0x060000EE RID: 238 RVA: 0x000049CE File Offset: 0x00002BCE
		public ShaftSoundEmitter(ShaftSoundController shaftSoundController)
		{
			this._shaftSoundController = shaftSoundController;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000049DD File Offset: 0x00002BDD
		public void Awake()
		{
			this._modularShaftAnimator = base.GetComponent<ModularShaftAnimator>();
			base.DisableComponent();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000049F1 File Offset: 0x00002BF1
		public void Update()
		{
			this.UpdateSound();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000049F9 File Offset: 0x00002BF9
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.UpdateSound();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004A07 File Offset: 0x00002C07
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			if (this._oldIsOn)
			{
				this._shaftSoundController.RemoveEmitter(this);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004A23 File Offset: 0x00002C23
		public bool CanEmitSound
		{
			get
			{
				return this._modularShaftAnimator.IsAnimated;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004A30 File Offset: 0x00002C30
		public void UpdateSound()
		{
			if (this._oldIsOn != this.CanEmitSound)
			{
				this.ToggleSound();
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004A46 File Offset: 0x00002C46
		public void ToggleSound()
		{
			this._oldIsOn = this.CanEmitSound;
			if (this._oldIsOn)
			{
				this._shaftSoundController.AddEmitter(this);
				return;
			}
			this._shaftSoundController.RemoveEmitter(this);
		}

		// Token: 0x04000073 RID: 115
		public readonly ShaftSoundController _shaftSoundController;

		// Token: 0x04000074 RID: 116
		public ModularShaftAnimator _modularShaftAnimator;

		// Token: 0x04000075 RID: 117
		public bool _oldIsOn;
	}
}
