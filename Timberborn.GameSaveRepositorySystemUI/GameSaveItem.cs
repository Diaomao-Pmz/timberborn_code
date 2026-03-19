using System;
using Timberborn.GameSaveRepositorySystem;

namespace Timberborn.GameSaveRepositorySystemUI
{
	// Token: 0x02000004 RID: 4
	public class GameSaveItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public SaveReference SaveReference { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public string DisplayName { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public string Timestamp { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		public string GameTime { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		public bool IsAutosave { get; }

		// Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
		public GameSaveItem(SaveReference saveReference, string displayName, string timestamp, string gameTime, bool isAutosave)
		{
			this.SaveReference = saveReference;
			this.DisplayName = displayName;
			this.Timestamp = timestamp;
			this.GameTime = gameTime;
			this.IsAutosave = isAutosave;
		}
	}
}
