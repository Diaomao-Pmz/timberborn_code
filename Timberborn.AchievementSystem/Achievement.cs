using System;

namespace Timberborn.AchievementSystem
{
	// Token: 0x02000004 RID: 4
	public abstract class Achievement
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		private event EventHandler UnlockCallback;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5
		public abstract string Id { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000212D File Offset: 0x0000032D
		public bool IsEnabled
		{
			get
			{
				return this.UnlockCallback != null;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002138 File Offset: 0x00000338
		public void Enable(EventHandler unlockCallback)
		{
			this.UnlockCallback = unlockCallback;
			this.EnableInternal();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002147 File Offset: 0x00000347
		public void Unlock()
		{
			EventHandler unlockCallback = this.UnlockCallback;
			if (unlockCallback != null)
			{
				unlockCallback(this, EventArgs.Empty);
			}
			this.Disable();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002166 File Offset: 0x00000366
		public void Disable()
		{
			if (this.IsEnabled)
			{
				this.UnlockCallback = null;
				this.DisableInternal();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000217D File Offset: 0x0000037D
		public virtual void EnableInternal()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000217D File Offset: 0x0000037D
		public virtual void DisableInternal()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020B6 File Offset: 0x000002B6
		public Achievement()
		{
		}
	}
}
