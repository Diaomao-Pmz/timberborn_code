using System;
using UnityEngine;

namespace Timberborn.Common
{
	// Token: 0x02000005 RID: 5
	public class ApplicationFocusLogger : MonoBehaviour
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
		public void OnApplicationFocus(bool hasFocus)
		{
			if (!Application.isEditor)
			{
				Debug.Log(hasFocus ? string.Format("Application focus gained at {0:u}", DateTime.Now) : string.Format("Application focus lost at {0:u}", DateTime.Now));
			}
		}
	}
}
