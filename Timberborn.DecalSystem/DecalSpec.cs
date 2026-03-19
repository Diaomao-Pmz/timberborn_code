using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.DecalSystem
{
	// Token: 0x0200000C RID: 12
	public class DecalSpec : ComponentSpec, IEquatable<DecalSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002659 File Offset: 0x00000859
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(DecalSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002665 File Offset: 0x00000865
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000266D File Offset: 0x0000086D
		[Serialize]
		public string FactionId { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002676 File Offset: 0x00000876
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000267E File Offset: 0x0000087E
		[Serialize]
		public string Category { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002687 File Offset: 0x00000887
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000268F File Offset: 0x0000088F
		[Serialize]
		public AssetRef<Texture2D> Texture { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002698 File Offset: 0x00000898
		public string Id
		{
			get
			{
				return this.Texture.Asset.name;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026AC File Offset: 0x000008AC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DecalSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026F8 File Offset: 0x000008F8
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("FactionId = ");
			builder.Append(this.FactionId);
			builder.Append(", Category = ");
			builder.Append(this.Category);
			builder.Append(", Texture = ");
			builder.Append(this.Texture);
			builder.Append(", Id = ");
			builder.Append(this.Id);
			return true;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000277F File Offset: 0x0000097F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DecalSpec left, DecalSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000278B File Offset: 0x0000098B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DecalSpec left, DecalSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027A0 File Offset: 0x000009A0
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FactionId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Category>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Texture2D>>.Default.GetHashCode(this.<Texture>k__BackingField);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027F8 File Offset: 0x000009F8
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DecalSpec);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002806 File Offset: 0x00000A06
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002810 File Offset: 0x00000A10
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DecalSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<FactionId>k__BackingField, other.<FactionId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Category>k__BackingField, other.<Category>k__BackingField) && EqualityComparer<AssetRef<Texture2D>>.Default.Equals(this.<Texture>k__BackingField, other.<Texture>k__BackingField));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000287C File Offset: 0x00000A7C
		[CompilerGenerated]
		protected DecalSpec([Nullable(1)] DecalSpec original) : base(original)
		{
			this.FactionId = original.<FactionId>k__BackingField;
			this.Category = original.<Category>k__BackingField;
			this.Texture = original.<Texture>k__BackingField;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028A9 File Offset: 0x00000AA9
		public DecalSpec()
		{
		}
	}
}
