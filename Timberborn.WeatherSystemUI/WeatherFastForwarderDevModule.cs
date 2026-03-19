using System;
using Timberborn.Debugging;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using Timberborn.WeatherSystem;

namespace Timberborn.WeatherSystemUI
{
	// Token: 0x02000009 RID: 9
	public class WeatherFastForwarderDevModule : IDevModule, IInputProcessor, ILoadableSingleton
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002362 File Offset: 0x00000562
		public WeatherFastForwarderDevModule(InputService inputService, WeatherFastForwarder weatherFastForwarder)
		{
			this._inputService = inputService;
			this._weatherFastForwarder = weatherFastForwarder;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002378 File Offset: 0x00000578
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.CreateBindable("Jump to next season", WeatherFastForwarderDevModule.JumpToNextSeasonKey, new Action(this._weatherFastForwarder.JumpToNextSeason))).Build();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023A9 File Offset: 0x000005A9
		public void Load()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023B7 File Offset: 0x000005B7
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(WeatherFastForwarderDevModule.JumpToNextSeasonKey))
			{
				this._weatherFastForwarder.JumpToNextSeason();
				return true;
			}
			return false;
		}

		// Token: 0x04000019 RID: 25
		public static readonly string JumpToNextSeasonKey = "JumpToNextSeason";

		// Token: 0x0400001A RID: 26
		public readonly InputService _inputService;

		// Token: 0x0400001B RID: 27
		public readonly WeatherFastForwarder _weatherFastForwarder;
	}
}
