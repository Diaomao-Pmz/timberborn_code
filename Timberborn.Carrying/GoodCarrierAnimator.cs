using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.EntitySystem;
using Timberborn.Goods;
using Timberborn.WalkingSystem;

namespace Timberborn.Carrying
{
	// Token: 0x02000010 RID: 16
	public class GoodCarrierAnimator : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002E3F File Offset: 0x0000103F
		public GoodCarrierAnimator(IGoodService goodService)
		{
			this._goodService = goodService;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E4E File Offset: 0x0000104E
		public void Awake()
		{
			this._backpackCarrier = base.GetComponent<BackpackCarrier>();
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			this._goodCarrier = base.GetComponent<GoodCarrier>();
			this._walkingEnforcerToggle = base.GetComponent<WalkingEnforcer>().GetWalkingEnforcerToggle();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E85 File Offset: 0x00001085
		public void InitializeEntity()
		{
			this.UpdateAnimations();
			this._backpackCarrier.BackpackChanged += this.OnBackpackChanged;
			this._goodCarrier.CarriedGoodsChanged += this.OnCarriedGoodsChanged;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EBB File Offset: 0x000010BB
		public void OnBackpackChanged(object sender, EventArgs e)
		{
			this.UpdateAnimations();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002EC3 File Offset: 0x000010C3
		public void OnCarriedGoodsChanged(object sender, CarriedGoodsChangedEventArgs e)
		{
			if (!this._backpackCarrier.IsBackpackEnabled)
			{
				this.UpdateHandCarryingAnimations();
			}
			this.UpdateWalkingAnimation();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002EDE File Offset: 0x000010DE
		public void UpdateAnimations()
		{
			this.UpdateWalkingAnimation();
			if (this._backpackCarrier.IsBackpackEnabled)
			{
				this.SetCarryAnimation(string.Empty);
				return;
			}
			this.UpdateHandCarryingAnimations();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F05 File Offset: 0x00001105
		public void UpdateWalkingAnimation()
		{
			if (this._backpackCarrier.IsBackpackEnabled && this._goodCarrier.IsCarrying)
			{
				this._walkingEnforcerToggle.EnableForcedWalking();
				return;
			}
			this._walkingEnforcerToggle.DisableForcedWalking();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F38 File Offset: 0x00001138
		public void SetCarryAnimation(string carryAnimation)
		{
			this.SetAnimation(GoodCarrierAnimator.CarryOnArmAnimationName, carryAnimation);
			this.SetAnimation(GoodCarrierAnimator.CarryInHandsAnimationName, carryAnimation);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F54 File Offset: 0x00001154
		public void UpdateHandCarryingAnimations()
		{
			string carryAnimation = this._goodCarrier.IsCarrying ? this._goodService.GetGood(this._goodCarrier.CarriedGoods.GoodId).CarryingAnimation : string.Empty;
			this.SetCarryAnimation(carryAnimation);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FA0 File Offset: 0x000011A0
		public void SetAnimation(string animationName, string activeAnimationName)
		{
			this._characterAnimator.SetBool(animationName, animationName == activeAnimationName);
		}

		// Token: 0x0400002A RID: 42
		public static readonly string CarryOnArmAnimationName = "CarryOnArm";

		// Token: 0x0400002B RID: 43
		public static readonly string CarryInHandsAnimationName = "CarryInHands";

		// Token: 0x0400002C RID: 44
		public readonly IGoodService _goodService;

		// Token: 0x0400002D RID: 45
		public BackpackCarrier _backpackCarrier;

		// Token: 0x0400002E RID: 46
		public CharacterAnimator _characterAnimator;

		// Token: 0x0400002F RID: 47
		public GoodCarrier _goodCarrier;

		// Token: 0x04000030 RID: 48
		public WalkingEnforcerToggle _walkingEnforcerToggle;
	}
}
