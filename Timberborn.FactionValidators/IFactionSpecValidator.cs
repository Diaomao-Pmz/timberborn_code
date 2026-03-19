using System;
using Timberborn.FactionSystem;

namespace Timberborn.FactionValidators
{
	// Token: 0x0200000E RID: 14
	public interface IFactionSpecValidator
	{
		// Token: 0x0600001C RID: 28
		bool IsValid(FactionSpec faction, out string errorMessage);
	}
}
