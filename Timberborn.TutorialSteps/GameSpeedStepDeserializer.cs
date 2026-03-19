using System;
using Timberborn.BlueprintSystem;
using Timberborn.Localization;
using Timberborn.TimeSystem;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200002C RID: 44
	public class GameSpeedStepDeserializer : IStepDeserializer
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00004A18 File Offset: 0x00002C18
		public GameSpeedStepDeserializer(SpeedManager speedManager, ILoc loc)
		{
			this._speedManager = speedManager;
			this._loc = loc;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004A30 File Offset: 0x00002C30
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			GameSpeedStepSpec gameSpeedStepSpec = step.Specs[0] as GameSpeedStepSpec;
			if (gameSpeedStepSpec != null)
			{
				tutorialStep = this.Create(gameSpeedStepSpec.Speed, gameSpeedStepSpec.OnlyOnce);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004A6F File Offset: 0x00002C6F
		public TutorialStep Create(int speed, bool onlyOnce)
		{
			return TutorialStep.Create(new GameSpeedStep(this._speedManager, this.GetDescription(speed), GameSpeedStepDeserializer.GetActualSpeed(speed), onlyOnce), GameSpeedStepDeserializer.GetKeyBindingKey(speed), null);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004A96 File Offset: 0x00002C96
		public string GetDescription(int speed)
		{
			return this._loc.T(GameSpeedStepDeserializer.GetLocKey(speed));
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004AAC File Offset: 0x00002CAC
		public static string GetLocKey(int speed)
		{
			string result;
			switch (speed)
			{
			case 1:
				result = "Tutorial.Basics.SetSpeed1";
				break;
			case 2:
				result = "Tutorial.Basics.SetSpeed2";
				break;
			case 3:
				result = "Tutorial.Basics.SetSpeed3";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public static int GetActualSpeed(int speed)
		{
			int result;
			switch (speed)
			{
			case 1:
				result = 1;
				break;
			case 2:
				result = 3;
				break;
			case 3:
				result = 7;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004B28 File Offset: 0x00002D28
		public static string GetKeyBindingKey(int speed)
		{
			string result;
			switch (speed)
			{
			case 1:
				result = "Speed1";
				break;
			case 2:
				result = "Speed2";
				break;
			case 3:
				result = "Speed3";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}

		// Token: 0x04000094 RID: 148
		public readonly SpeedManager _speedManager;

		// Token: 0x04000095 RID: 149
		public readonly ILoc _loc;
	}
}
