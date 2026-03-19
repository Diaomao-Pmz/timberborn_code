using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EnterableSystem;
using Timberborn.TimbermeshAnimations;

namespace Timberborn.SlotSystem
{
	// Token: 0x02000014 RID: 20
	public class SlotAnimationSynchronizer : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000083 RID: 131 RVA: 0x0000311D File Offset: 0x0000131D
		public SlotAnimationSynchronizer(IRandomNumberGenerator randomNumberGenerator)
		{
			this._randomNumberGenerator = randomNumberGenerator;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000312C File Offset: 0x0000132C
		public void Awake()
		{
			this._enterable = base.GetComponent<Enterable>();
			this._slotAnimationSynchronizerSpec = base.GetComponent<SlotAnimationSynchronizerSpec>();
			SlotManager component = base.GetComponent<SlotManager>();
			component.EntererAssignedToSlot += this.OnEntererAssignedToSlot;
			component.EntererUnassignedFromSlot += this.OnEntererRemovedFromSlot;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000317A File Offset: 0x0000137A
		public void OnEntererAssignedToSlot(object sender, Enterer enterer)
		{
			if (this._enterable.NumberOfEnterersInside == 1)
			{
				this._leadingEnterer = enterer;
				return;
			}
			this.SynchronizeAnimatorTime(enterer);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003199 File Offset: 0x00001399
		public void OnEntererRemovedFromSlot(object sender, Enterer enterer)
		{
			if (enterer == this._leadingEnterer)
			{
				this._leadingEnterer = this._enterable.EnterersInside.FirstOrDefault<Enterer>();
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000031BC File Offset: 0x000013BC
		public void SynchronizeAnimatorTime(Enterer enterer)
		{
			IAnimator componentInChildren = enterer.GetComponentInChildren<IAnimator>(true);
			IAnimator componentInChildren2 = this._leadingEnterer.GetComponentInChildren<IAnimator>(true);
			float num = this._randomNumberGenerator.Range(0f, this._slotAnimationSynchronizerSpec.MaxTimeOffset);
			componentInChildren.SetTime(componentInChildren2.Time - num);
		}

		// Token: 0x04000028 RID: 40
		public readonly IRandomNumberGenerator _randomNumberGenerator;

		// Token: 0x04000029 RID: 41
		public Enterable _enterable;

		// Token: 0x0400002A RID: 42
		public SlotAnimationSynchronizerSpec _slotAnimationSynchronizerSpec;

		// Token: 0x0400002B RID: 43
		public Enterer _leadingEnterer;
	}
}
