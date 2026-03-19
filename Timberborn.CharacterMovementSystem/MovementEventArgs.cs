using System;
using Timberborn.Navigation;

namespace Timberborn.CharacterMovementSystem
{
	// Token: 0x02000010 RID: 16
	public readonly struct MovementEventArgs
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002F96 File Offset: 0x00001196
		public PathCorner From { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002F9E File Offset: 0x0000119E
		public PathCorner To { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002FA6 File Offset: 0x000011A6
		public PathCorner? Next { get; }

		// Token: 0x06000067 RID: 103 RVA: 0x00002FAE File Offset: 0x000011AE
		public MovementEventArgs(PathCorner from, PathCorner to, PathCorner? next)
		{
			this.From = from;
			this.To = to;
			this.Next = next;
		}
	}
}
