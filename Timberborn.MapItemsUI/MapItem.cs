using System;
using Timberborn.MapRepositorySystem;
using UnityEngine;

namespace Timberborn.MapItemsUI
{
	// Token: 0x02000006 RID: 6
	public class MapItem
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020E6 File Offset: 0x000002E6
		public MapFileReference MapFileReference { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020EE File Offset: 0x000002EE
		public string DisplayName { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020F6 File Offset: 0x000002F6
		public string DisplayDescription { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020FE File Offset: 0x000002FE
		public Vector2Int? Size { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002106 File Offset: 0x00000306
		public bool IsRecommended { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000210E File Offset: 0x0000030E
		public bool IsUnconventional { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002116 File Offset: 0x00000316
		public bool IsDeletable { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000211E File Offset: 0x0000031E
		public bool IsDev { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002126 File Offset: 0x00000326
		public MapIcon MapIcon { get; }

		// Token: 0x06000010 RID: 16 RVA: 0x00002130 File Offset: 0x00000330
		public MapItem(MapFileReference mapFileReference, string displayName, string displayDescription, Vector2Int? size, bool isRecommended, bool isUnconventional, bool isDeletable, bool isDev, MapIcon mapIcon)
		{
			this.MapFileReference = mapFileReference;
			this.DisplayName = displayName;
			this.DisplayDescription = displayDescription;
			this.Size = size;
			this.IsRecommended = isRecommended;
			this.IsUnconventional = isUnconventional;
			this.IsDeletable = isDeletable;
			this.IsDev = isDev;
			this.MapIcon = mapIcon;
		}
	}
}
