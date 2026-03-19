using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.DeconstructionSystem
{
	// Token: 0x0200000C RID: 12
	public class DeconstructionParticleFactorySpec : ComponentSpec, IEquatable<DeconstructionParticleFactorySpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002660 File Offset: 0x00000860
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DeconstructionParticleFactorySpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000266C File Offset: 0x0000086C
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002674 File Offset: 0x00000874
		[Serialize]
		public float MinParticleSpawnThreshold { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000267D File Offset: 0x0000087D
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002685 File Offset: 0x00000885
		[Serialize]
		public float MaxParticleSpawnThreshold { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000268E File Offset: 0x0000088E
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002696 File Offset: 0x00000896
		[Serialize]
		public int MinParticlesForThreshold { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000269F File Offset: 0x0000089F
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000026A7 File Offset: 0x000008A7
		[Serialize]
		public int MaxParticlesForThreshold { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026B0 File Offset: 0x000008B0
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000026B8 File Offset: 0x000008B8
		[Serialize]
		public string ParticlePrefabPath { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000026C1 File Offset: 0x000008C1
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000026C9 File Offset: 0x000008C9
		[Serialize]
		public float MaxNeighboursCount { get; set; }

		// Token: 0x0600002F RID: 47 RVA: 0x000026D4 File Offset: 0x000008D4
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DeconstructionParticleFactorySpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002720 File Offset: 0x00000920
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MinParticleSpawnThreshold = ");
			builder.Append(this.MinParticleSpawnThreshold.ToString());
			builder.Append(", MaxParticleSpawnThreshold = ");
			builder.Append(this.MaxParticleSpawnThreshold.ToString());
			builder.Append(", MinParticlesForThreshold = ");
			builder.Append(this.MinParticlesForThreshold.ToString());
			builder.Append(", MaxParticlesForThreshold = ");
			builder.Append(this.MaxParticlesForThreshold.ToString());
			builder.Append(", ParticlePrefabPath = ");
			builder.Append(this.ParticlePrefabPath);
			builder.Append(", MaxNeighboursCount = ");
			builder.Append(this.MaxNeighboursCount.ToString());
			return true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000281F File Offset: 0x00000A1F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DeconstructionParticleFactorySpec left, DeconstructionParticleFactorySpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000282B File Offset: 0x00000A2B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DeconstructionParticleFactorySpec left, DeconstructionParticleFactorySpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002840 File Offset: 0x00000A40
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MinParticleSpawnThreshold>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxParticleSpawnThreshold>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MinParticlesForThreshold>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MaxParticlesForThreshold>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ParticlePrefabPath>k__BackingField)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.<MaxNeighboursCount>k__BackingField);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028DD File Offset: 0x00000ADD
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DeconstructionParticleFactorySpec);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028EB File Offset: 0x00000AEB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028F4 File Offset: 0x00000AF4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DeconstructionParticleFactorySpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<float>.Default.Equals(this.<MinParticleSpawnThreshold>k__BackingField, other.<MinParticleSpawnThreshold>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxParticleSpawnThreshold>k__BackingField, other.<MaxParticleSpawnThreshold>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MinParticlesForThreshold>k__BackingField, other.<MinParticlesForThreshold>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MaxParticlesForThreshold>k__BackingField, other.<MaxParticlesForThreshold>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ParticlePrefabPath>k__BackingField, other.<ParticlePrefabPath>k__BackingField) && EqualityComparer<float>.Default.Equals(this.<MaxNeighboursCount>k__BackingField, other.<MaxNeighboursCount>k__BackingField));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029B0 File Offset: 0x00000BB0
		[CompilerGenerated]
		protected DeconstructionParticleFactorySpec([Nullable(1)] DeconstructionParticleFactorySpec original) : base(original)
		{
			this.MinParticleSpawnThreshold = original.<MinParticleSpawnThreshold>k__BackingField;
			this.MaxParticleSpawnThreshold = original.<MaxParticleSpawnThreshold>k__BackingField;
			this.MinParticlesForThreshold = original.<MinParticlesForThreshold>k__BackingField;
			this.MaxParticlesForThreshold = original.<MaxParticlesForThreshold>k__BackingField;
			this.ParticlePrefabPath = original.<ParticlePrefabPath>k__BackingField;
			this.MaxNeighboursCount = original.<MaxNeighboursCount>k__BackingField;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A0C File Offset: 0x00000C0C
		public DeconstructionParticleFactorySpec()
		{
		}
	}
}
