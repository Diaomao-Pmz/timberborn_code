using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Explosions
{
	// Token: 0x0200001B RID: 27
	public class UnstableCoreEffectsSpawnerSpec : ComponentSpec, IEquatable<UnstableCoreEffectsSpawnerSpec>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004328 File Offset: 0x00002528
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(UnstableCoreEffectsSpawnerSpec);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004334 File Offset: 0x00002534
		// (set) Token: 0x060000BD RID: 189 RVA: 0x0000433C File Offset: 0x0000253C
		[Serialize]
		public string ExplosionPrefabPath { get; set; }

		// Token: 0x060000BE RID: 190 RVA: 0x00004348 File Offset: 0x00002548
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnstableCoreEffectsSpawnerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004394 File Offset: 0x00002594
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ExplosionPrefabPath = ");
			builder.Append(this.ExplosionPrefabPath);
			return true;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000043C5 File Offset: 0x000025C5
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UnstableCoreEffectsSpawnerSpec left, UnstableCoreEffectsSpawnerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000043D1 File Offset: 0x000025D1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UnstableCoreEffectsSpawnerSpec left, UnstableCoreEffectsSpawnerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000043E5 File Offset: 0x000025E5
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ExplosionPrefabPath>k__BackingField);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004404 File Offset: 0x00002604
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnstableCoreEffectsSpawnerSpec);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000028CB File Offset: 0x00000ACB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004412 File Offset: 0x00002612
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(UnstableCoreEffectsSpawnerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ExplosionPrefabPath>k__BackingField, other.<ExplosionPrefabPath>k__BackingField));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004443 File Offset: 0x00002643
		[CompilerGenerated]
		protected UnstableCoreEffectsSpawnerSpec([Nullable(1)] UnstableCoreEffectsSpawnerSpec original) : base(original)
		{
			this.ExplosionPrefabPath = original.<ExplosionPrefabPath>k__BackingField;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00002949 File Offset: 0x00000B49
		public UnstableCoreEffectsSpawnerSpec()
		{
		}
	}
}
