using System;
using Timberborn.BaseComponentSystem;
using Timberborn.MortalComponents;
using Timberborn.TimbermeshAnimations;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x02000008 RID: 8
	public class CharacterAnimator : BaseComponent, IAwakableComponent, IDeadNeededComponent
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000227D File Offset: 0x0000047D
		public void Awake()
		{
			this._animatorController = base.GetComponent<IAnimatorController>();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000228B File Offset: 0x0000048B
		public bool HasParameter(string parameterName)
		{
			return this._animatorController.HasParameter(parameterName);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002299 File Offset: 0x00000499
		public void SetBool(string parameterName, bool value)
		{
			this._animatorController.SetBool(parameterName, value);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022A8 File Offset: 0x000004A8
		public void SetFloat(string parameterName, float value)
		{
			this._animatorController.SetFloat(parameterName, value);
		}

		// Token: 0x04000011 RID: 17
		public IAnimatorController _animatorController;
	}
}
