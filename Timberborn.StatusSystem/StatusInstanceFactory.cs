using System;
using Timberborn.SpriteOperations;
using Timberborn.TimeSystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200001B RID: 27
	public class StatusInstanceFactory
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00003942 File Offset: 0x00001B42
		public StatusInstanceFactory(IDayNightCycle dayNightCycle, SpriteResizer spriteResizer, StatusSpriteLoader statusSpriteLoader)
		{
			this._dayNightCycle = dayNightCycle;
			this._spriteResizer = spriteResizer;
			this._statusSpriteLoader = statusSpriteLoader;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000395F File Offset: 0x00001B5F
		public StatusInstance CreateStatus(StatusSubject statusSubject, StatusToggle statusToggle)
		{
			return this.CreateStatusInternal(statusSubject, statusToggle, null, null, null);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000396C File Offset: 0x00001B6C
		public StatusInstance CreateDynamicStatus(StatusSubject statusSubject, StatusToggle statusToggle, Func<float> statusValueGetter, Func<StatusWarningType> statusWarningTypeGetter, string warningSound)
		{
			return this.CreateStatusInternal(statusSubject, statusToggle, statusValueGetter, statusWarningTypeGetter, warningSound);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000397C File Offset: 0x00001B7C
		public StatusInstance CreateStatusInternal(StatusSubject statusSubject, StatusToggle statusToggle, Func<float> statusValueGetter, Func<StatusWarningType> statusWarningTypeGetter, string warningSound)
		{
			StatusSpecification statusSpecification = statusToggle.StatusSpecification;
			Sprite sprite = this._statusSpriteLoader.LoadSprite(statusSpecification.SpriteName);
			Sprite resizedSprite = this._spriteResizer.GetResizedSprite(sprite, StatusInstanceFactory.DefaultUISpriteSize);
			return new StatusInstance(statusSpecification.StatusDescription, statusSpecification.AlertDescription, statusToggle.IsPriorityStatus, statusSpecification.ShowFloatingIcon, statusSubject, sprite, resizedSprite, statusValueGetter, statusWarningTypeGetter, warningSound, this._dayNightCycle, statusSpecification.DelayInHours);
		}

		// Token: 0x0400005A RID: 90
		public static readonly int DefaultUISpriteSize = 32;

		// Token: 0x0400005B RID: 91
		public readonly IDayNightCycle _dayNightCycle;

		// Token: 0x0400005C RID: 92
		public readonly SpriteResizer _spriteResizer;

		// Token: 0x0400005D RID: 93
		public readonly StatusSpriteLoader _statusSpriteLoader;
	}
}
