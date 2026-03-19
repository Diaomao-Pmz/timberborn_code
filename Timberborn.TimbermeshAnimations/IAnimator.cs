using System;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x0200000D RID: 13
	public interface IAnimator
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003C RID: 60
		// (remove) Token: 0x0600003D RID: 61
		event EventHandler AnimationChanged;

		// Token: 0x1700000D RID: 13
		// (set) Token: 0x0600003E RID: 62
		bool PlayBackwards { set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003F RID: 63
		// (set) Token: 0x06000040 RID: 64
		bool Enabled { get; set; }

		// Token: 0x1700000F RID: 15
		// (set) Token: 0x06000041 RID: 65
		float Speed { set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66
		float Time { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000043 RID: 67
		float RepeatedTime { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000044 RID: 68
		string AnimationName { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000045 RID: 69
		float AnimationLength { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000046 RID: 70
		bool PlayingFinished { get; }

		// Token: 0x06000047 RID: 71
		void Play(string animationName, bool looped = true);

		// Token: 0x06000048 RID: 72
		void Stop();

		// Token: 0x06000049 RID: 73
		void SetTime(float time);
	}
}
