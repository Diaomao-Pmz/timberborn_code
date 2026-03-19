using System;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000019 RID: 25
	public class MaterialHeightCutoffSetter
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00003E46 File Offset: 0x00002046
		public void SetCutoff(Material material, float height)
		{
			material.SetFloat(MaterialHeightCutoffSetter.HeightCutoffId, height);
		}

		// Token: 0x04000049 RID: 73
		public static readonly int HeightCutoffId = Shader.PropertyToID("_HeightCutoff");
	}
}
