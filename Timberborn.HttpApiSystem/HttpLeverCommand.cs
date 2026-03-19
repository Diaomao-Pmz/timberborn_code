using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000021 RID: 33
	public readonly struct HttpLeverCommand
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000048D1 File Offset: 0x00002AD1
		[UsedImplicitly]
		public string Name { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000048D9 File Offset: 0x00002AD9
		[UsedImplicitly]
		public bool? State { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000048E1 File Offset: 0x00002AE1
		[UsedImplicitly]
		public Color? Color { get; }

		// Token: 0x060000D1 RID: 209 RVA: 0x000048E9 File Offset: 0x00002AE9
		public HttpLeverCommand(string name, bool state)
		{
			this.Name = name;
			this.State = new bool?(state);
			this.Color = null;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000490A File Offset: 0x00002B0A
		public HttpLeverCommand(string name, Color color)
		{
			this.Name = name;
			this.State = null;
			this.Color = new Color?(color);
		}
	}
}
