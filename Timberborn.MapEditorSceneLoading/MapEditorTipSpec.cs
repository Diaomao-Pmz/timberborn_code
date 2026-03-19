using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.MapEditorSceneLoading
{
	// Token: 0x0200000A RID: 10
	public class MapEditorTipSpec : ComponentSpec, IEquatable<MapEditorTipSpec>
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002259 File Offset: 0x00000459
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MapEditorTipSpec);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002265 File Offset: 0x00000465
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000226D File Offset: 0x0000046D
		[Serialize]
		public ImmutableArray<string> Tips { get; set; }

		// Token: 0x06000019 RID: 25 RVA: 0x00002278 File Offset: 0x00000478
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MapEditorTipSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022C4 File Offset: 0x000004C4
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Tips = ");
			builder.Append(this.Tips.ToString());
			return true;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000230E File Offset: 0x0000050E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(MapEditorTipSpec left, MapEditorTipSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000231A File Offset: 0x0000051A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(MapEditorTipSpec left, MapEditorTipSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000232E File Offset: 0x0000052E
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(this.<Tips>k__BackingField);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000234D File Offset: 0x0000054D
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MapEditorTipSpec);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000235B File Offset: 0x0000055B
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002364 File Offset: 0x00000564
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(MapEditorTipSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<ImmutableArray<string>>.Default.Equals(this.<Tips>k__BackingField, other.<Tips>k__BackingField));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002395 File Offset: 0x00000595
		[CompilerGenerated]
		protected MapEditorTipSpec([Nullable(1)] MapEditorTipSpec original) : base(original)
		{
			this.Tips = original.<Tips>k__BackingField;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023AA File Offset: 0x000005AA
		public MapEditorTipSpec()
		{
		}
	}
}
