using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.WaterSourceSystem
{
	// Token: 0x02000018 RID: 24
	public class WaterSourceContamination : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000034BF File Offset: 0x000016BF
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000034C7 File Offset: 0x000016C7
		public float Contamination { get; private set; }

		// Token: 0x060000C0 RID: 192 RVA: 0x000034D0 File Offset: 0x000016D0
		public void Awake()
		{
			this._waterSourceContaminationSpec = base.GetComponent<WaterSourceContaminationSpec>();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000034DE File Offset: 0x000016DE
		public void InitializeEntity()
		{
			this.ResetContamination();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000034E6 File Offset: 0x000016E6
		public void ResetContamination()
		{
			this.SetContamination(this._waterSourceContaminationSpec.DefaultContamination);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000034F9 File Offset: 0x000016F9
		public void SetContamination(float strength)
		{
			this.Contamination = Mathf.Min(strength, 1f);
		}

		// Token: 0x04000046 RID: 70
		public WaterSourceContaminationSpec _waterSourceContaminationSpec;
	}
}
