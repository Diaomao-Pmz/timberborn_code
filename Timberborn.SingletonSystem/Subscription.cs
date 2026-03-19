using System;

namespace Timberborn.SingletonSystem
{
	// Token: 0x02000019 RID: 25
	public readonly struct Subscription
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000029B0 File Offset: 0x00000BB0
		public object Subscriber { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000029B8 File Offset: 0x00000BB8
		public Action<object> Action { get; }

		// Token: 0x06000041 RID: 65 RVA: 0x000029C0 File Offset: 0x00000BC0
		public Subscription(object subscriber, Action<object> action)
		{
			this.Subscriber = subscriber;
			this.Action = action;
		}
	}
}
