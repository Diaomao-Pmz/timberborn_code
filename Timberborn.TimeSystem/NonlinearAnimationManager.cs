using System;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.TimeSystem
{
	// Token: 0x02000013 RID: 19
	public class NonlinearAnimationManager : IUpdatableSingleton
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002B30 File Offset: 0x00000D30
		public float SpeedMultiplier
		{
			get
			{
				if (Time.timeScale != 0f)
				{
					return this.NonlinearSpeed / Time.timeScale;
				}
				return 0f;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002B50 File Offset: 0x00000D50
		public void UpdateSingleton()
		{
			this._nonlinearTime += Time.deltaTime * this.SpeedMultiplier;
			Shader.SetGlobalFloat(NonlinearAnimationManager.NonlinearTimeProperty, this._nonlinearTime);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002B7B File Offset: 0x00000D7B
		public float NonlinearSpeed
		{
			get
			{
				return Mathf.Pow(Time.timeScale, NonlinearAnimationManager.Exponent);
			}
		}

		// Token: 0x04000026 RID: 38
		public static readonly int NonlinearTimeProperty = Shader.PropertyToID("_NonlinearTime");

		// Token: 0x04000027 RID: 39
		public static readonly float Exponent = 0.5f;

		// Token: 0x04000028 RID: 40
		public float _nonlinearTime;
	}
}
