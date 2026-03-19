using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.Common;

namespace Timberborn.TutorialSystem
{
	// Token: 0x02000015 RID: 21
	public class TutorialStage
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002D68 File Offset: 0x00000F68
		public string Id { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002D70 File Offset: 0x00000F70
		public string Intro { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002D78 File Offset: 0x00000F78
		public ImmutableArray<TutorialStep> TutorialSteps { get; }

		// Token: 0x06000060 RID: 96 RVA: 0x00002D80 File Offset: 0x00000F80
		public TutorialStage(string id, string intro, IEnumerable<TutorialStep> tutorialSteps)
		{
			this.Id = id;
			this.Intro = intro;
			this.TutorialSteps = tutorialSteps.ToImmutableArray<TutorialStep>();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002DA2 File Offset: 0x00000FA2
		public bool AllStepsAchieved
		{
			get
			{
				return this.TutorialSteps.FastAll((TutorialStep tutorialStep) => tutorialStep.Step.Achieved());
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public bool HasSteps
		{
			get
			{
				return this.TutorialSteps.Length > 0;
			}
		}
	}
}
