using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.PathSystem
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	public class PathSpec : ComponentSpec, IEquatable<PathSpec>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003702 File Offset: 0x00001902
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PathSpec);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000370E File Offset: 0x0000190E
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003716 File Offset: 0x00001916
		[Serialize]
		public Vector3Int MainPathCoordinates { get; set; }

		// Token: 0x06000090 RID: 144 RVA: 0x00003720 File Offset: 0x00001920
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PathSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000376C File Offset: 0x0000196C
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("MainPathCoordinates = ");
			builder.Append(this.MainPathCoordinates.ToString());
			return true;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000037B6 File Offset: 0x000019B6
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PathSpec left, PathSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000037C2 File Offset: 0x000019C2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PathSpec left, PathSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000037D6 File Offset: 0x000019D6
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Vector3Int>.Default.GetHashCode(this.<MainPathCoordinates>k__BackingField);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000037F5 File Offset: 0x000019F5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PathSpec);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002983 File Offset: 0x00000B83
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003803 File Offset: 0x00001A03
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PathSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Vector3Int>.Default.Equals(this.<MainPathCoordinates>k__BackingField, other.<MainPathCoordinates>k__BackingField));
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003834 File Offset: 0x00001A34
		[CompilerGenerated]
		protected PathSpec(PathSpec original) : base(original)
		{
			this.MainPathCoordinates = original.<MainPathCoordinates>k__BackingField;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public PathSpec()
		{
		}
	}
}
