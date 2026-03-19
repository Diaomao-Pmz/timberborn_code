using System;
using System.Collections.Generic;
using Timberborn.Common;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x02000020 RID: 32
	public class ZiplineTowerRegistry
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00004AA6 File Offset: 0x00002CA6
		public ReadOnlyList<ZiplineTower> ZiplineTowers
		{
			get
			{
				return this._ziplineTowers.AsReadOnlyList<ZiplineTower>();
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004AB3 File Offset: 0x00002CB3
		public void Add(ZiplineTower ziplineTower)
		{
			this._ziplineTowers.Add(ziplineTower);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004AC1 File Offset: 0x00002CC1
		public void Remove(ZiplineTower ziplineTower)
		{
			this._ziplineTowers.Remove(ziplineTower);
		}

		// Token: 0x04000067 RID: 103
		public readonly List<ZiplineTower> _ziplineTowers = new List<ZiplineTower>();
	}
}
