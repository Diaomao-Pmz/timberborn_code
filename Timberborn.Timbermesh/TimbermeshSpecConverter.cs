using System;
using Timberborn.BlueprintPrefabSystem;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.Timbermesh
{
	// Token: 0x02000012 RID: 18
	public class TimbermeshSpecConverter : ISpecToPrefabConverter
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002AA9 File Offset: 0x00000CA9
		public bool CanConvert(ComponentSpec spec)
		{
			return spec is TimbermeshSpec;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public void Convert(GameObject owner, ComponentSpec spec)
		{
			owner.AddComponent<TimbermeshDescription>().SetModelName(((TimbermeshSpec)spec).Model.Path);
		}
	}
}
