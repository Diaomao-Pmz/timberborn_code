using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.NeedSpecs;

namespace Timberborn.NeedBehaviorSystem
{
	// Token: 0x0200001B RID: 27
	public class NeedBehaviorKeyGenerator
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003084 File Offset: 0x00001284
		public string GenerateKey(IReadOnlyList<InstantEffectSpec> effects)
		{
			for (int i = 0; i < effects.Count; i++)
			{
				this._keyParts.Add(effects[i].NeedId);
			}
			return this.GenerateKey("Instant");
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000030C4 File Offset: 0x000012C4
		public string GenerateKey(IReadOnlyList<ContinuousEffectSpec> effects)
		{
			for (int i = 0; i < effects.Count; i++)
			{
				this._keyParts.Add(effects[i].NeedId);
			}
			return this.GenerateKey("Continuous");
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003104 File Offset: 0x00001304
		public string GenerateKey(string suffix)
		{
			this._keyParts.Sort();
			for (int i = 0; i < this._keyParts.Count; i++)
			{
				this._keyBuilder.Append(this._keyParts[i]);
			}
			this._keyParts.Clear();
			this._keyBuilder.Append(suffix);
			string result = this._keyBuilder.ToString();
			this._keyBuilder.Clear();
			return result;
		}

		// Token: 0x04000043 RID: 67
		public readonly List<string> _keyParts = new List<string>();

		// Token: 0x04000044 RID: 68
		public readonly StringBuilder _keyBuilder = new StringBuilder();
	}
}
