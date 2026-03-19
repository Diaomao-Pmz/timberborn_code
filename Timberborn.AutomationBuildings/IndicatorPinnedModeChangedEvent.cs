using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200001E RID: 30
	[NullableContext(1)]
	[Nullable(0)]
	public class IndicatorPinnedModeChangedEvent : IEquatable<IndicatorPinnedModeChangedEvent>
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00004AC0 File Offset: 0x00002CC0
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(IndicatorPinnedModeChangedEvent);
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004ACC File Offset: 0x00002CCC
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("IndicatorPinnedModeChangedEvent");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004B18 File Offset: 0x00002D18
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			return false;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004B1B File Offset: 0x00002D1B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(IndicatorPinnedModeChangedEvent left, IndicatorPinnedModeChangedEvent right)
		{
			return !(left == right);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004B27 File Offset: 0x00002D27
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(IndicatorPinnedModeChangedEvent left, IndicatorPinnedModeChangedEvent right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004B3B File Offset: 0x00002D3B
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004B4D File Offset: 0x00002D4D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IndicatorPinnedModeChangedEvent);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004B5B File Offset: 0x00002D5B
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(IndicatorPinnedModeChangedEvent other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000020F8 File Offset: 0x000002F8
		[CompilerGenerated]
		protected IndicatorPinnedModeChangedEvent(IndicatorPinnedModeChangedEvent original)
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000020F8 File Offset: 0x000002F8
		public IndicatorPinnedModeChangedEvent()
		{
		}
	}
}
