using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Forestry;
using Timberborn.WalkingSystem;

namespace Timberborn.ForestryEffects
{
	// Token: 0x0200000B RID: 11
	public class TreeCutterSwimmingBlocker : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002460 File Offset: 0x00000660
		public void Awake()
		{
			this._swimmingAnimator = base.GetComponent<SwimmingAnimator>();
			TreeCutter component = base.GetComponent<TreeCutter>();
			component.CuttingStarted += this.OnCuttingStarted;
			component.CuttingStopped += this.OnCuttingStopped;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002497 File Offset: 0x00000697
		public void OnCuttingStarted(object sender, EventArgs e)
		{
			this._swimmingAnimator.BlockSwimmingMovementAndResetPosition();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024A4 File Offset: 0x000006A4
		public void OnCuttingStopped(object sender, EventArgs e)
		{
			this._swimmingAnimator.UnblockSwimmingMovement();
		}

		// Token: 0x04000010 RID: 16
		public SwimmingAnimator _swimmingAnimator;
	}
}
