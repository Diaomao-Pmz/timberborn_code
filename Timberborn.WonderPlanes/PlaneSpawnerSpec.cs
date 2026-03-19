using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.WonderPlanes
{
	// Token: 0x02000011 RID: 17
	public class PlaneSpawnerSpec : ComponentSpec, IEquatable<PlaneSpawnerSpec>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000035BD File Offset: 0x000017BD
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(PlaneSpawnerSpec);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000035C9 File Offset: 0x000017C9
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000035D1 File Offset: 0x000017D1
		[Serialize]
		public string SpawnPointName { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x000035DC File Offset: 0x000017DC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PlaneSpawnerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003628 File Offset: 0x00001828
		[NullableContext(1)]
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("SpawnPointName = ");
			builder.Append(this.SpawnPointName);
			return true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003659 File Offset: 0x00001859
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PlaneSpawnerSpec left, PlaneSpawnerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003665 File Offset: 0x00001865
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PlaneSpawnerSpec left, PlaneSpawnerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003679 File Offset: 0x00001879
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SpawnPointName>k__BackingField);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003698 File Offset: 0x00001898
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlaneSpawnerSpec);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002AA6 File Offset: 0x00000CA6
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000036A6 File Offset: 0x000018A6
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PlaneSpawnerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<SpawnPointName>k__BackingField, other.<SpawnPointName>k__BackingField));
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000036D7 File Offset: 0x000018D7
		[CompilerGenerated]
		protected PlaneSpawnerSpec([Nullable(1)] PlaneSpawnerSpec original) : base(original)
		{
			this.SpawnPointName = original.<SpawnPointName>k__BackingField;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002AF5 File Offset: 0x00000CF5
		public PlaneSpawnerSpec()
		{
		}
	}
}
