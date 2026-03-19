using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.InputSystem
{
	// Token: 0x02000009 RID: 9
	public class CustomCursorSpec : ComponentSpec, IEquatable<CustomCursorSpec>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002263 File Offset: 0x00000463
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(CustomCursorSpec);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000226F File Offset: 0x0000046F
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002277 File Offset: 0x00000477
		[Serialize]
		public string Id { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002280 File Offset: 0x00000480
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002288 File Offset: 0x00000488
		[Serialize]
		public AssetRef<Texture2D> WindowsCursor { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002291 File Offset: 0x00000491
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002299 File Offset: 0x00000499
		[Serialize]
		public AssetRef<Texture2D> MacOsCursor { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022A2 File Offset: 0x000004A2
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000022AA File Offset: 0x000004AA
		[Serialize]
		public Vector2 Hotspot { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022B3 File Offset: 0x000004B3
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000022BB File Offset: 0x000004BB
		[Serialize]
		public Vector2 WindowsCursorOffset { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000022C4 File Offset: 0x000004C4
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000022CC File Offset: 0x000004CC
		[Serialize]
		public Vector2 MacOsCursorOffset { get; set; }

		// Token: 0x06000022 RID: 34 RVA: 0x000022D8 File Offset: 0x000004D8
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CustomCursorSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002324 File Offset: 0x00000524
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", WindowsCursor = ");
			builder.Append(this.WindowsCursor);
			builder.Append(", MacOsCursor = ");
			builder.Append(this.MacOsCursor);
			builder.Append(", Hotspot = ");
			builder.Append(this.Hotspot.ToString());
			builder.Append(", WindowsCursorOffset = ");
			builder.Append(this.WindowsCursorOffset.ToString());
			builder.Append(", MacOsCursorOffset = ");
			builder.Append(this.MacOsCursorOffset.ToString());
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002407 File Offset: 0x00000607
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(CustomCursorSpec left, CustomCursorSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002413 File Offset: 0x00000613
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(CustomCursorSpec left, CustomCursorSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002428 File Offset: 0x00000628
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Texture2D>>.Default.GetHashCode(this.<WindowsCursor>k__BackingField)) * -1521134295 + EqualityComparer<AssetRef<Texture2D>>.Default.GetHashCode(this.<MacOsCursor>k__BackingField)) * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.<Hotspot>k__BackingField)) * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.<WindowsCursorOffset>k__BackingField)) * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.<MacOsCursorOffset>k__BackingField);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024C5 File Offset: 0x000006C5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CustomCursorSpec);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024D3 File Offset: 0x000006D3
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024DC File Offset: 0x000006DC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(CustomCursorSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<AssetRef<Texture2D>>.Default.Equals(this.<WindowsCursor>k__BackingField, other.<WindowsCursor>k__BackingField) && EqualityComparer<AssetRef<Texture2D>>.Default.Equals(this.<MacOsCursor>k__BackingField, other.<MacOsCursor>k__BackingField) && EqualityComparer<Vector2>.Default.Equals(this.<Hotspot>k__BackingField, other.<Hotspot>k__BackingField) && EqualityComparer<Vector2>.Default.Equals(this.<WindowsCursorOffset>k__BackingField, other.<WindowsCursorOffset>k__BackingField) && EqualityComparer<Vector2>.Default.Equals(this.<MacOsCursorOffset>k__BackingField, other.<MacOsCursorOffset>k__BackingField));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002598 File Offset: 0x00000798
		[CompilerGenerated]
		protected CustomCursorSpec([Nullable(1)] CustomCursorSpec original) : base(original)
		{
			this.Id = original.<Id>k__BackingField;
			this.WindowsCursor = original.<WindowsCursor>k__BackingField;
			this.MacOsCursor = original.<MacOsCursor>k__BackingField;
			this.Hotspot = original.<Hotspot>k__BackingField;
			this.WindowsCursorOffset = original.<WindowsCursorOffset>k__BackingField;
			this.MacOsCursorOffset = original.<MacOsCursorOffset>k__BackingField;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025F4 File Offset: 0x000007F4
		public CustomCursorSpec()
		{
		}
	}
}
