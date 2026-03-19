using System;
using UnityEngine;

namespace Timberborn.Rendering
{
	// Token: 0x02000010 RID: 16
	public static class Layers
	{
		// Token: 0x0400001F RID: 31
		public static readonly string UIName = "UI";

		// Token: 0x04000020 RID: 32
		public static readonly int UILayer = LayerMask.NameToLayer(Layers.UIName);

		// Token: 0x04000021 RID: 33
		public static readonly LayerMask UIMask = LayerMask.GetMask(new string[]
		{
			Layers.UIName
		});

		// Token: 0x04000022 RID: 34
		public static readonly LayerMask IgnoreRaycastMask = LayerMask.NameToLayer("Ignore Raycast");
	}
}
