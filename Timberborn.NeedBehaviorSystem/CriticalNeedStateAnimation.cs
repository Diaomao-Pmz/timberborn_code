using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.NeedSystem;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x0200000F RID: 15
	public class CriticalNeedStateAnimation : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002874 File Offset: 0x00000A74
		public void Awake()
		{
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
			this._criticalNeedStateAnimationSpec = base.GetComponent<CriticalNeedStateAnimationSpec>();
			base.GetComponent<NeedManager>().NeedChangedCriticalState += this.OnNeedChangedCriticalState;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028A5 File Offset: 0x00000AA5
		public void OnNeedChangedCriticalState(object sender, NeedChangedCriticalStateEventArgs e)
		{
			if (e.NeedSpec.Id == this._criticalNeedStateAnimationSpec.NeedId)
			{
				this._characterAnimator.SetBool(this._criticalNeedStateAnimationSpec.Animation, e.IsInCriticalState);
			}
		}

		// Token: 0x0400002B RID: 43
		public CharacterAnimator _characterAnimator;

		// Token: 0x0400002C RID: 44
		public CriticalNeedStateAnimationSpec _criticalNeedStateAnimationSpec;
	}
}
