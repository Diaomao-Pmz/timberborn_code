using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.PathSystem
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	public class PathModelTypeEnforcerSpec : ComponentSpec, IEquatable<PathModelTypeEnforcerSpec>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000353F File Offset: 0x0000173F
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PathModelTypeEnforcerSpec);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000354B File Offset: 0x0000174B
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003553 File Offset: 0x00001753
		[Serialize]
		public PathModelType PathModelType { get; set; }

		// Token: 0x0600007E RID: 126 RVA: 0x0000355C File Offset: 0x0000175C
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PathModelTypeEnforcerSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000035A8 File Offset: 0x000017A8
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("PathModelType = ");
			builder.Append(this.PathModelType.ToString());
			return true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000035F2 File Offset: 0x000017F2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PathModelTypeEnforcerSpec left, PathModelTypeEnforcerSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000035FE File Offset: 0x000017FE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PathModelTypeEnforcerSpec left, PathModelTypeEnforcerSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003612 File Offset: 0x00001812
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<PathModelType>.Default.GetHashCode(this.<PathModelType>k__BackingField);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003631 File Offset: 0x00001831
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PathModelTypeEnforcerSpec);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002983 File Offset: 0x00000B83
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000363F File Offset: 0x0000183F
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(PathModelTypeEnforcerSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<PathModelType>.Default.Equals(this.<PathModelType>k__BackingField, other.<PathModelType>k__BackingField));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003670 File Offset: 0x00001870
		[CompilerGenerated]
		protected PathModelTypeEnforcerSpec(PathModelTypeEnforcerSpec original) : base(original)
		{
			this.PathModelType = original.<PathModelType>k__BackingField;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public PathModelTypeEnforcerSpec()
		{
		}
	}
}
