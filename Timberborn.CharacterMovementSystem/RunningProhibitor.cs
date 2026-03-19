using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x02000014 RID: 20
	public class RunningProhibitor : BaseComponent, IAwakableComponent
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003798 File Offset: 0x00001998
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000037A0 File Offset: 0x000019A0
		public bool RunningProhibited
		{
			get
			{
				return this._runningProhibited;
			}
			set
			{
				if (this._runningProhibited != value)
				{
					this._runningProhibited = value;
					this._characterAnimator.SetBool(RunningProhibitor.RunningProhibitedParameterName, value);
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000037C3 File Offset: 0x000019C3
		public void Awake()
		{
			this._characterAnimator = base.GetComponent<CharacterAnimator>();
		}

		// Token: 0x04000049 RID: 73
		public static readonly string RunningProhibitedParameterName = "RunningProhibited";

		// Token: 0x0400004A RID: 74
		public CharacterAnimator _characterAnimator;

		// Token: 0x0400004B RID: 75
		public bool _runningProhibited;
	}
}
