using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WalkingSystem
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	public class RunningStateUpdaterSpec : ComponentSpec, IEquatable<RunningStateUpdaterSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002C27 File Offset: 0x00000E27
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(RunningStateUpdaterSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002C33 File Offset: 0x00000E33
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002C3B File Offset: 0x00000E3B
		[Serialize]
		public float ShortWalkingDistanceThreshold { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002C44 File Offset: 0x00000E44
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002C4C File Offset: 0x00000E4C
		[Serialize]
		public float WalkingSpeedThreshold { get; set; }

		// Token: 0x0600004E RID: 78 RVA: 0x00002C58 File Offset: 0x00000E58
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RunningStateUpdaterSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CA4 File Offset: 0x00000EA4
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ShortWalkingDistanceThreshold = ");
			builder.Append(this.ShortWalkingDistanceThreshold.ToString());
			builder.Append(", WalkingSpeedThreshold = ");
			builder.Append(this.WalkingSpeedThreshold.ToString());
			return true;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D15 File Offset: 0x00000F15
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(RunningStateUpdaterSpec left, RunningStateUpdaterSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D21 File Offset: 0x00000F21
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(RunningStateUpdaterSpec left, RunningStateUpdaterSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D35 File Offset: 0x00000F35
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<ShortWalkingDistanceThreshold>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<WalkingSpeedThreshold>k__BackingField);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D6B File Offset: 0x00000F6B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RunningStateUpdaterSpec);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D79 File Offset: 0x00000F79
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D84 File Offset: 0x00000F84
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(RunningStateUpdaterSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<ShortWalkingDistanceThreshold>k__BackingField, other.<ShortWalkingDistanceThreshold>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<WalkingSpeedThreshold>k__BackingField, other.<WalkingSpeedThreshold>k__BackingField));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DD8 File Offset: 0x00000FD8
		[CompilerGenerated]
		protected RunningStateUpdaterSpec(RunningStateUpdaterSpec original) : base(original)
		{
			this.ShortWalkingDistanceThreshold = original.<ShortWalkingDistanceThreshold>k__BackingField;
			this.WalkingSpeedThreshold = original.<WalkingSpeedThreshold>k__BackingField;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DF9 File Offset: 0x00000FF9
		public RunningStateUpdaterSpec()
		{
		}
	}
}
