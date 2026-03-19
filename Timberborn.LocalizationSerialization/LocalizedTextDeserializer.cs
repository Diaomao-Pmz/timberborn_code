using System;
using Timberborn.BlueprintSystem;
using Timberborn.Localization;

namespace Timberborn.LocalizationSerialization
{
	// Token: 0x02000009 RID: 9
	public class LocalizedTextDeserializer : IDeserializer
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002268 File Offset: 0x00000468
		public LocalizedTextDeserializer(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002128 File Offset: 0x00000328
		public Type DeserializedType
		{
			get
			{
				return typeof(LocalizedText);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002278 File Offset: 0x00000478
		public object Deserialize(object source)
		{
			string text = (string)source;
			if (!string.IsNullOrWhiteSpace(text))
			{
				return new LocalizedText(this._loc.T(text));
			}
			return null;
		}

		// Token: 0x04000009 RID: 9
		public readonly ILoc _loc;
	}
}
