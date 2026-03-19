using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Explosions
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	public class UnstableCoreSpec : ComponentSpec, IEquatable<UnstableCoreSpec>
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000048F1 File Offset: 0x00002AF1
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(UnstableCoreSpec);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000048FD File Offset: 0x00002AFD
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00004905 File Offset: 0x00002B05
		[Serialize]
		public int MinExplosionRadius { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000490E File Offset: 0x00002B0E
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00004916 File Offset: 0x00002B16
		[Serialize]
		public int MaxExplosionRadius { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000491F File Offset: 0x00002B1F
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004927 File Offset: 0x00002B27
		[Serialize]
		public int DefaultExplosionRadius { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004930 File Offset: 0x00002B30
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00004938 File Offset: 0x00002B38
		[Serialize]
		public float InnerRadius { get; set; }

		// Token: 0x060000F1 RID: 241 RVA: 0x00004944 File Offset: 0x00002B44
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnstableCoreSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004990 File Offset: 0x00002B90
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinExplosionRadius = ");
			builder.Append(this.MinExplosionRadius.ToString());
			builder.Append(", MaxExplosionRadius = ");
			builder.Append(this.MaxExplosionRadius.ToString());
			builder.Append(", DefaultExplosionRadius = ");
			builder.Append(this.DefaultExplosionRadius.ToString());
			builder.Append(", InnerRadius = ");
			builder.Append(this.InnerRadius.ToString());
			return true;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004A4F File Offset: 0x00002C4F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UnstableCoreSpec left, UnstableCoreSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004A5B File Offset: 0x00002C5B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UnstableCoreSpec left, UnstableCoreSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004A70 File Offset: 0x00002C70
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((base.GetHashCode() * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MinExplosionRadius>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxExplosionRadius>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<DefaultExplosionRadius>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<InnerRadius>k__BackingField);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004ADF File Offset: 0x00002CDF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnstableCoreSpec);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000028CB File Offset: 0x00000ACB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004AF0 File Offset: 0x00002CF0
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UnstableCoreSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<int>.Default.Equals(this.<MinExplosionRadius>k__BackingField, other.<MinExplosionRadius>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxExplosionRadius>k__BackingField, other.<MaxExplosionRadius>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<DefaultExplosionRadius>k__BackingField, other.<DefaultExplosionRadius>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<InnerRadius>k__BackingField, other.<InnerRadius>k__BackingField));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004B74 File Offset: 0x00002D74
		[CompilerGenerated]
		protected UnstableCoreSpec(UnstableCoreSpec original) : base(original)
		{
			this.MinExplosionRadius = original.<MinExplosionRadius>k__BackingField;
			this.MaxExplosionRadius = original.<MaxExplosionRadius>k__BackingField;
			this.DefaultExplosionRadius = original.<DefaultExplosionRadius>k__BackingField;
			this.InnerRadius = original.<InnerRadius>k__BackingField;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002949 File Offset: 0x00000B49
		public UnstableCoreSpec()
		{
		}
	}
}
