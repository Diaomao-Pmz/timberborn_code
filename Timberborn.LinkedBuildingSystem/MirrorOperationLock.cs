using System;

namespace Timberborn.LinkedBuildingSystem
{
	// Token: 0x0200000F RID: 15
	public class MirrorOperationLock
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002DA2 File Offset: 0x00000FA2
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002DAA File Offset: 0x00000FAA
		public bool IsUnlocked { get; private set; } = true;

		// Token: 0x06000054 RID: 84 RVA: 0x00002DB3 File Offset: 0x00000FB3
		public IDisposable Lock()
		{
			if (!this.IsUnlocked)
			{
				throw new InvalidOperationException("Cannot lock an already locked MirrorOperationLock");
			}
			this.IsUnlocked = false;
			return new MirrorOperationLock.MirrorLockToken(this);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DDA File Offset: 0x00000FDA
		public void Unlock()
		{
			this.IsUnlocked = true;
		}

		// Token: 0x02000010 RID: 16
		public readonly struct MirrorLockToken : IDisposable
		{
			// Token: 0x06000057 RID: 87 RVA: 0x00002DF2 File Offset: 0x00000FF2
			public MirrorLockToken(MirrorOperationLock mirrorOperationLock)
			{
				this._mirrorOperationLock = mirrorOperationLock;
			}

			// Token: 0x06000058 RID: 88 RVA: 0x00002DFB File Offset: 0x00000FFB
			public void Dispose()
			{
				this._mirrorOperationLock.Unlock();
			}

			// Token: 0x04000021 RID: 33
			public readonly MirrorOperationLock _mirrorOperationLock;
		}
	}
}
