using System;

namespace Timberborn.MapStateSystem
{
	// Token: 0x02000007 RID: 7
	public class MapEditorMode
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public bool IsMapEditor { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public MapEditorMode(bool isMapEditor)
		{
			this.IsMapEditor = isMapEditor;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002115 File Offset: 0x00000315
		public static MapEditorMode MapEditorInstance()
		{
			return new MapEditorMode(true);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000211D File Offset: 0x0000031D
		public static MapEditorMode NonMapEditorInstance()
		{
			return new MapEditorMode(false);
		}
	}
}
