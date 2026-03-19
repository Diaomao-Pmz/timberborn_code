using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200004D RID: 77
	[AttributeUsage(AttributeTargets.Class)]
	internal sealed class RouteParameterConstraintAttribute : Attribute
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000026D4 File Offset: 0x000008D4
		[NotNull]
		public string ConstraintName { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000026DC File Offset: 0x000008DC
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000026E4 File Offset: 0x000008E4
		[CanBeNull]
		public Type ProposedType { get; set; }

		// Token: 0x060000A2 RID: 162 RVA: 0x000026ED File Offset: 0x000008ED
		public RouteParameterConstraintAttribute([NotNull] string constraintName)
		{
			this.ConstraintName = constraintName;
		}
	}
}
