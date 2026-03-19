using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x0200000B RID: 11
	public class ManufactoryRecipeVisualizerSpec : ComponentSpec, IEquatable<ManufactoryRecipeVisualizerSpec>
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000025DB File Offset: 0x000007DB
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(ManufactoryRecipeVisualizerSpec);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000025E7 File Offset: 0x000007E7
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000025EF File Offset: 0x000007EF
		[Serialize]
		public string InitialModelName { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025F8 File Offset: 0x000007F8
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002600 File Offset: 0x00000800
		[Serialize]
		public ImmutableArray<RecipeModel> RecipeModels { get; set; }

		// Token: 0x0600002F RID: 47 RVA: 0x0000260C File Offset: 0x0000080C
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ManufactoryRecipeVisualizerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002658 File Offset: 0x00000858
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("InitialModelName = ");
			builder.Append(this.InitialModelName);
			builder.Append(", RecipeModels = ");
			builder.Append(this.RecipeModels.ToString());
			return true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026BB File Offset: 0x000008BB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ManufactoryRecipeVisualizerSpec left, ManufactoryRecipeVisualizerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026C7 File Offset: 0x000008C7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ManufactoryRecipeVisualizerSpec left, ManufactoryRecipeVisualizerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026DB File Offset: 0x000008DB
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<InitialModelName>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<RecipeModel>>.Default.GetHashCode(this.<RecipeModels>k__BackingField);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002711 File Offset: 0x00000911
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ManufactoryRecipeVisualizerSpec);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000023D3 File Offset: 0x000005D3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002720 File Offset: 0x00000920
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ManufactoryRecipeVisualizerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<InitialModelName>k__BackingField, other.<InitialModelName>k__BackingField) && EqualityComparer<ImmutableArray<RecipeModel>>.Default.Equals(this.<RecipeModels>k__BackingField, other.<RecipeModels>k__BackingField));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002774 File Offset: 0x00000974
		[CompilerGenerated]
		protected ManufactoryRecipeVisualizerSpec([Nullable(1)] ManufactoryRecipeVisualizerSpec original) : base(original)
		{
			this.InitialModelName = original.<InitialModelName>k__BackingField;
			this.RecipeModels = original.<RecipeModels>k__BackingField;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002422 File Offset: 0x00000622
		public ManufactoryRecipeVisualizerSpec()
		{
		}
	}
}
