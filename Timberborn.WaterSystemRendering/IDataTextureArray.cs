using System;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x0200000A RID: 10
	public interface IDataTextureArray
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34
		Texture2DArray OldArray { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35
		Texture2DArray NewArray { get; }
	}
}
