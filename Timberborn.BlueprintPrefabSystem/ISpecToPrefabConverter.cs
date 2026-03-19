using System;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.BlueprintPrefabSystem
{
	// Token: 0x02000006 RID: 6
	public interface ISpecToPrefabConverter
	{
		// Token: 0x06000007 RID: 7
		bool CanConvert(ComponentSpec spec);

		// Token: 0x06000008 RID: 8
		void Convert(GameObject owner, ComponentSpec spec);
	}
}
