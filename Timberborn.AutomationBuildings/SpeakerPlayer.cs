using System;
using System.Collections.Generic;
using Timberborn.Automation;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x02000044 RID: 68
	public class SpeakerPlayer : ISamplingSingleton
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x000083CF File Offset: 0x000065CF
		public void AddSpeaker(Speaker speaker)
		{
			this._speakers.Add(speaker);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000083DE File Offset: 0x000065DE
		public void RemoveSpeaker(Speaker speaker)
		{
			this._speakers.Remove(speaker);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000083F0 File Offset: 0x000065F0
		public void Sample()
		{
			foreach (Speaker speaker in this._speakers)
			{
				speaker.PlayIfRequested();
			}
		}

		// Token: 0x0400016C RID: 364
		public readonly HashSet<Speaker> _speakers = new HashSet<Speaker>();
	}
}
