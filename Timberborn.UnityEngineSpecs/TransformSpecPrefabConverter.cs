using System;
using Timberborn.BlueprintPrefabSystem;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.UnityEngineSpecs
{
	// Token: 0x02000010 RID: 16
	public class TransformSpecPrefabConverter : ISpecToPrefabConverter
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00003574 File Offset: 0x00001774
		public bool CanConvert(ComponentSpec spec)
		{
			return spec is TransformSpec;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003580 File Offset: 0x00001780
		public void Convert(GameObject owner, ComponentSpec spec)
		{
			TransformSpec transformSpec = (TransformSpec)spec;
			owner.transform.SetLocalPositionAndRotation(transformSpec.Position, Quaternion.Euler(transformSpec.Rotation));
			owner.transform.localScale = transformSpec.Scale;
		}
	}
}
