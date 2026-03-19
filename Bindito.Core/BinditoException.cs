using System;
using System.Runtime.Serialization;

namespace Bindito.Core
{
	// Token: 0x0200006D RID: 109
	public class BinditoException : Exception
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00002938 File Offset: 0x00000B38
		public BinditoException()
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002940 File Offset: 0x00000B40
		protected BinditoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000294A File Offset: 0x00000B4A
		public BinditoException(string message) : base(message)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00002953 File Offset: 0x00000B53
		public BinditoException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
