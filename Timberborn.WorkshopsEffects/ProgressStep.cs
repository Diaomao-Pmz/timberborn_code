using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x0200000E RID: 14
	public class ProgressStep
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002B65 File Offset: 0x00000D65
		public float Threshold { get; }

		// Token: 0x06000055 RID: 85 RVA: 0x00002B6D File Offset: 0x00000D6D
		public ProgressStep(float threshold, GameObject[] models)
		{
			this.Threshold = threshold;
			this._models = models;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B84 File Offset: 0x00000D84
		public static ProgressStep Create(ProgressStepSpec spec, GameObject owner)
		{
			GameObject[] array = new GameObject[spec.ModelNames.Length];
			for (int i = 0; i < spec.ModelNames.Length; i++)
			{
				array[i] = owner.FindChild(spec.ModelNames[i]);
			}
			return new ProgressStep(spec.Threshold, array);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public void ShowStep()
		{
			this.SetStepVisibility(true);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002BEB File Offset: 0x00000DEB
		public void HideStep()
		{
			this.SetStepVisibility(false);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public void SetStepVisibility(bool isVisible)
		{
			for (int i = 0; i < this._models.Length; i++)
			{
				this._models[i].SetActive(isVisible);
			}
		}

		// Token: 0x04000026 RID: 38
		public readonly GameObject[] _models;
	}
}
