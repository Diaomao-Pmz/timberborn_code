using System;
using UnityEngine.InputSystem.LowLevel;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x02000012 RID: 18
	public static class InputEventPtrExtensions
	{
		// Token: 0x0600006F RID: 111 RVA: 0x0000313A File Offset: 0x0000133A
		public static bool IsAnyStateEvent(this InputEventPtr inputEvent)
		{
			return inputEvent.IsA<StateEvent>() || inputEvent.IsA<DeltaStateEvent>();
		}
	}
}
