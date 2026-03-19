using System;

namespace Timberborn.TemplateSystem
{
	// Token: 0x02000009 RID: 9
	public class TemplateMappingException : Exception
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002152 File Offset: 0x00000352
		public TemplateMappingException()
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000215A File Offset: 0x0000035A
		public TemplateMappingException(string message) : base(message)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002163 File Offset: 0x00000363
		public TemplateMappingException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
