using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Timberborn.BlueprintSystem;
using UnityEngine;

namespace Timberborn.GameDistrictsUI
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	public class DistrictConnectionDrawingServiceSpec : ComponentSpec, IEquatable<DistrictConnectionDrawingServiceSpec>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002A90 File Offset: 0x00000C90
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DistrictConnectionDrawingServiceSpec);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002A9C File Offset: 0x00000C9C
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002AA4 File Offset: 0x00000CA4
		[Serialize]
		public Color ConnectionHighlight { get; set; }

		// Token: 0x06000050 RID: 80 RVA: 0x00002AB0 File Offset: 0x00000CB0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DistrictConnectionDrawingServiceSpec");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002AFC File Offset: 0x00000CFC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("ConnectionHighlight = ");
			builder.Append(this.ConnectionHighlight.ToString());
			return true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B46 File Offset: 0x00000D46
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DistrictConnectionDrawingServiceSpec left, DistrictConnectionDrawingServiceSpec right)
		{
			return !(left == right);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B52 File Offset: 0x00000D52
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DistrictConnectionDrawingServiceSpec left, DistrictConnectionDrawingServiceSpec right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B66 File Offset: 0x00000D66
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<ConnectionHighlight>k__BackingField);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B85 File Offset: 0x00000D85
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DistrictConnectionDrawingServiceSpec);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002437 File Offset: 0x00000637
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ComponentSpec other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B93 File Offset: 0x00000D93
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DistrictConnectionDrawingServiceSpec other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<ConnectionHighlight>k__BackingField, other.<ConnectionHighlight>k__BackingField));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002BC4 File Offset: 0x00000DC4
		[CompilerGenerated]
		protected DistrictConnectionDrawingServiceSpec(DistrictConnectionDrawingServiceSpec original) : base(original)
		{
			this.ConnectionHighlight = original.<ConnectionHighlight>k__BackingField;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002486 File Offset: 0x00000686
		public DistrictConnectionDrawingServiceSpec()
		{
		}
	}
}
