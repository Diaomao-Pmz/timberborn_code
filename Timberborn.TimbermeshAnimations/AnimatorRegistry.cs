using System;
using System.Collections.Generic;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x02000009 RID: 9
	public class AnimatorRegistry : IUpdatableSingleton
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002288 File Offset: 0x00000488
		public void UpdateSingleton()
		{
			float deltaTime = Time.deltaTime;
			for (int i = 0; i < this._animators.Count; i++)
			{
				TimbermeshAnimator timbermeshAnimator = this._animators[i];
				if (timbermeshAnimator && timbermeshAnimator.isActiveAndEnabled)
				{
					timbermeshAnimator.UpdateAnimation(deltaTime);
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022D5 File Offset: 0x000004D5
		public void Add(TimbermeshAnimator timbermeshAnimator)
		{
			this._animators.Add(timbermeshAnimator);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022E3 File Offset: 0x000004E3
		public void Remove(TimbermeshAnimator timbermeshAnimator)
		{
			this._animators.Remove(timbermeshAnimator);
		}

		// Token: 0x0400000E RID: 14
		public readonly List<TimbermeshAnimator> _animators = new List<TimbermeshAnimator>();
	}
}
