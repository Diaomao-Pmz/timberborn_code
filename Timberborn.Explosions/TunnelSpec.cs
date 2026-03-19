using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;

namespace Timberborn.Explosions
{
	// Token: 0x02000018 RID: 24
	public class TunnelSpec : ComponentSpec, IEquatable<TunnelSpec>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003CE0 File Offset: 0x00001EE0
		[Nullable(1)]
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(TunnelSpec);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003CEC File Offset: 0x00001EEC
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003CF4 File Offset: 0x00001EF4
		[Serialize]
		public string ExplosionPrefabPath { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003CFD File Offset: 0x00001EFD
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003D05 File Offset: 0x00001F05
		[Serialize]
		public string TunnelSupportTemplateName { get; set; }

		// Token: 0x06000093 RID: 147 RVA: 0x00003D10 File Offset: 0x00001F10
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TunnelSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003D5C File Offset: 0x00001F5C
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
			builder.Append(", TunnelSupportTemplateName = ");
			builder.Append(this.TunnelSupportTemplateName);
			return true;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003DB1 File Offset: 0x00001FB1
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(TunnelSpec left, TunnelSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003DBD File Offset: 0x00001FBD
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(TunnelSpec left, TunnelSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003DD1 File Offset: 0x00001FD1
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ExplosionPrefabPath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TunnelSupportTemplateName>k__BackingField);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003E07 File Offset: 0x00002007
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TunnelSpec);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000028CB File Offset: 0x00000ACB
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003E18 File Offset: 0x00002018
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(TunnelSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<string>.Default.Equals(this.<ExplosionPrefabPath>k__BackingField, other.<ExplosionPrefabPath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TunnelSupportTemplateName>k__BackingField, other.<TunnelSupportTemplateName>k__BackingField));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003E6C File Offset: 0x0000206C
		[CompilerGenerated]
		protected TunnelSpec([Nullable(1)] TunnelSpec original) : base(original)
		{
			this.ExplosionPrefabPath = original.<ExplosionPrefabPath>k__BackingField;
			this.TunnelSupportTemplateName = original.<TunnelSupportTemplateName>k__BackingField;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002949 File Offset: 0x00000B49
		public TunnelSpec()
		{
		}
	}
}
