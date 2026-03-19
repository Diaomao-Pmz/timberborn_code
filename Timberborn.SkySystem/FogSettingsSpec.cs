using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.SkySystem
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public class FogSettingsSpec : IEquatable<FogSettingsSpec>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A04 File Offset: 0x00000C04
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(FogSettingsSpec);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002A10 File Offset: 0x00000C10
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002A18 File Offset: 0x00000C18
		[Serialize]
		public Color FogColor { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002A21 File Offset: 0x00000C21
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002A29 File Offset: 0x00000C29
		[Serialize]
		public float FogDensity { get; set; }

		// Token: 0x06000046 RID: 70 RVA: 0x00002A34 File Offset: 0x00000C34
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("FogSettingsSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A80 File Offset: 0x00000C80
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("FogColor = ");
			builder.Append(this.FogColor.ToString());
			builder.Append(", FogDensity = ");
			builder.Append(this.FogDensity.ToString());
			return true;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002AE1 File Offset: 0x00000CE1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(FogSettingsSpec left, FogSettingsSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002AED File Offset: 0x00000CED
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(FogSettingsSpec left, FogSettingsSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B01 File Offset: 0x00000D01
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<FogColor>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<FogDensity>k__BackingField);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B41 File Offset: 0x00000D41
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FogSettingsSpec);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B50 File Offset: 0x00000D50
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(FogSettingsSpec other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Color>.Default.Equals(this.<FogColor>k__BackingField, other.<FogColor>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<FogDensity>k__BackingField, other.<FogDensity>k__BackingField));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002BB1 File Offset: 0x00000DB1
		[CompilerGenerated]
		protected FogSettingsSpec(FogSettingsSpec original)
		{
			this.FogColor = original.<FogColor>k__BackingField;
			this.FogDensity = original.<FogDensity>k__BackingField;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000020F6 File Offset: 0x000002F6
		public FogSettingsSpec()
		{
		}
	}
}
