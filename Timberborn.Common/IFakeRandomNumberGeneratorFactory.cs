using System;

namespace Timberborn.Common
{
	// Token: 0x02000021 RID: 33
	public interface IFakeRandomNumberGeneratorFactory
	{
		// Token: 0x06000076 RID: 118
		IFakeRandomNumberGenerator Create(Guid guid, int salt);
	}
}
