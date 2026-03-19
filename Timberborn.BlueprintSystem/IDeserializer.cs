using System;

namespace Timberborn.BlueprintSystem
{
	// Token: 0x0200001E RID: 30
	public interface IDeserializer
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000BF RID: 191
		Type DeserializedType { get; }

		// Token: 0x060000C0 RID: 192
		object Deserialize(object source);
	}
}
