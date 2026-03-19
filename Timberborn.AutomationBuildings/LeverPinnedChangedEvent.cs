using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000024 RID: 36
	[NullableContext(1)]
	[Nullable(0)]
	public class LeverPinnedChangedEvent : IEquatable<LeverPinnedChangedEvent>
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000051DD File Offset: 0x000033DD
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(LeverPinnedChangedEvent);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000051EC File Offset: 0x000033EC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LeverPinnedChangedEvent");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00004B18 File Offset: 0x00002D18
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			return false;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005238 File Offset: 0x00003438
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(LeverPinnedChangedEvent left, LeverPinnedChangedEvent right)
		{
			return !(left == right);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005244 File Offset: 0x00003444
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(LeverPinnedChangedEvent left, LeverPinnedChangedEvent right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005258 File Offset: 0x00003458
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000526A File Offset: 0x0000346A
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as LeverPinnedChangedEvent);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005278 File Offset: 0x00003478
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(LeverPinnedChangedEvent other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000020F8 File Offset: 0x000002F8
		[CompilerGenerated]
		protected LeverPinnedChangedEvent(LeverPinnedChangedEvent original)
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000020F8 File Offset: 0x000002F8
		public LeverPinnedChangedEvent()
		{
		}
	}
}
