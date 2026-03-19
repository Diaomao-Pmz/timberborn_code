using System;

namespace Timberborn.TemplateInstantiation
{
	// Token: 0x02000005 RID: 5
	public class CachedTemplateInitializer
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public Action<object, object> Method { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public int SubjectIndex { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000210E File Offset: 0x0000030E
		public int DecoratorIndex { get; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002116 File Offset: 0x00000316
		public CachedTemplateInitializer(Action<object, object> method, int subjectIndex, int decoratorIndex)
		{
			this.Method = method;
			this.SubjectIndex = subjectIndex;
			this.DecoratorIndex = decoratorIndex;
		}
	}
}
