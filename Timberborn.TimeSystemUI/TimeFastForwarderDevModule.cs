using System;
using Timberborn.Debugging;
using Timberborn.InputSystem;
using Timberborn.SingletonSystem;
using Timberborn.TimeSystem;

namespace Timberborn.TimeSystemUI
{
	// Token: 0x02000008 RID: 8
	public class TimeFastForwarderDevModule : IDevModule, IInputProcessor, ILoadableSingleton
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000286A File Offset: 0x00000A6A
		public TimeFastForwarderDevModule(InputService inputService, TimeFastForwarder timeFastForwarder)
		{
			this._inputService = inputService;
			this._timeFastForwarder = timeFastForwarder;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002880 File Offset: 0x00000A80
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.CreateBindable("Jump to next daytime", TimeFastForwarderDevModule.JumpToNextDaytimeKey, new Action(this._timeFastForwarder.JumpToNextDaytime))).Build();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000028B1 File Offset: 0x00000AB1
		public void Load()
		{
			this._inputService.AddInputProcessor(this);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028BF File Offset: 0x00000ABF
		public bool ProcessInput()
		{
			if (this._inputService.IsKeyDown(TimeFastForwarderDevModule.JumpToNextDaytimeKey))
			{
				this._timeFastForwarder.JumpToNextDaytime();
				return true;
			}
			return false;
		}

		// Token: 0x04000028 RID: 40
		public static readonly string JumpToNextDaytimeKey = "JumpToNextDaytime";

		// Token: 0x04000029 RID: 41
		public readonly InputService _inputService;

		// Token: 0x0400002A RID: 42
		public readonly TimeFastForwarder _timeFastForwarder;
	}
}
