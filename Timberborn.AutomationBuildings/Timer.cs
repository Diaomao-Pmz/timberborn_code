using System;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.AutomationBuildings
{
	// Token: 0x0200004B RID: 75
	public class Timer : BaseComponent, IAwakableComponent, IInitializableEntity, IPersistentEntity, IDuplicable<Timer>, IDuplicable, ISequentialTransmitter, ITransmitter
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000316 RID: 790 RVA: 0x000088A0 File Offset: 0x00006AA0
		// (remove) Token: 0x06000317 RID: 791 RVA: 0x000088D8 File Offset: 0x00006AD8
		public event EventHandler TimerTicked;

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000890D File Offset: 0x00006B0D
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00008915 File Offset: 0x00006B15
		public TimerMode Mode { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000891E File Offset: 0x00006B1E
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00008926 File Offset: 0x00006B26
		public TimerInterval TimerIntervalA { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000892F File Offset: 0x00006B2F
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00008937 File Offset: 0x00006B37
		public TimerInterval TimerIntervalB { get; private set; }

		// Token: 0x0600031E RID: 798 RVA: 0x00008940 File Offset: 0x00006B40
		public Timer(ReferenceSerializer referenceSerializer, TimerIntervalSerializer timerIntervalSerializer, TimerIntervalFactory timerIntervalFactory)
		{
			this._referenceSerializer = referenceSerializer;
			this._timerIntervalSerializer = timerIntervalSerializer;
			this._timerIntervalFactory = timerIntervalFactory;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000895D File Offset: 0x00006B5D
		public Automator Input
		{
			get
			{
				return this._input.Transmitter;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000896A File Offset: 0x00006B6A
		public Automator ResetInput
		{
			get
			{
				return this._resetInput.Transmitter;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00008978 File Offset: 0x00006B78
		public bool UsesIntervalB
		{
			get
			{
				TimerMode mode = this.Mode;
				return mode == TimerMode.Delay || mode == TimerMode.Oscillator;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000899C File Offset: 0x00006B9C
		public bool IsProcessingNewInput
		{
			get
			{
				return this._state != this._nextState;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000089B0 File Offset: 0x00006BB0
		public void Awake()
		{
			this._automator = base.GetComponent<Automator>();
			this._input = this._automator.AddInput();
			this._resetInput = this._automator.AddInput();
			this.TimerIntervalA = this._timerIntervalFactory.CreateFromHours(1f, IntervalType.Hours);
			this.TimerIntervalB = this._timerIntervalFactory.CreateFromHours(1f, IntervalType.Hours);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00008A19 File Offset: 0x00006C19
		public void InitializeEntity()
		{
			this.UpdateOutputState();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00008A24 File Offset: 0x00006C24
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(Timer.TimerKey);
			component.Set<TimerMode>(Timer.ModeKey, this.Mode);
			component.Set<TimerInterval>(Timer.TimerIntervalAKey, this.TimerIntervalA, this._timerIntervalSerializer);
			if (this.UsesIntervalB)
			{
				component.Set<TimerInterval>(Timer.TimerIntervalBKey, this.TimerIntervalB, this._timerIntervalSerializer);
			}
			if (this.Input)
			{
				component.Set<Automator>(Timer.InputKey, this.Input, this._referenceSerializer.Of<Automator>());
			}
			if (this.ResetInput)
			{
				component.Set<Automator>(Timer.ResetInputKey, this.ResetInput, this._referenceSerializer.Of<Automator>());
			}
			if (this._state)
			{
				component.Set(Timer.StateKey, this._state);
			}
			if (this._previousInputState)
			{
				component.Set(Timer.PreviousInputStateKey, this._previousInputState);
			}
			component.Set(Timer.CounterKey, this._counter);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00008B1C File Offset: 0x00006D1C
		[BackwardCompatible(2026, 2, 5, Compatibility.Save)]
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader component = entityLoader.GetComponent(Timer.TimerKey);
			this.Mode = component.Get<TimerMode>(Timer.ModeKey);
			if (component.Has<TimerInterval>(Timer.TimerIntervalAKey))
			{
				this.TimerIntervalA = component.Get<TimerInterval>(Timer.TimerIntervalAKey, this._timerIntervalSerializer);
			}
			if (component.Has<TimerInterval>(Timer.TimerIntervalBKey))
			{
				this.TimerIntervalB = component.Get<TimerInterval>(Timer.TimerIntervalBKey, this._timerIntervalSerializer);
			}
			Automator input;
			if (component.Has<Automator>(Timer.InputKey) && component.GetObsoletable<Automator>(Timer.InputKey, this._referenceSerializer.Of<Automator>(), out input))
			{
				this.SetInput(input);
			}
			Automator resetInput;
			if (component.Has<Automator>(Timer.ResetInputKey) && component.GetObsoletable<Automator>(Timer.ResetInputKey, this._referenceSerializer.Of<Automator>(), out resetInput))
			{
				this.SetResetInput(resetInput);
			}
			this._state = (component.Has<bool>(Timer.StateKey) && component.Get(Timer.StateKey));
			this._previousInputState = (component.Has<bool>(Timer.PreviousInputStateKey) && component.Get(Timer.PreviousInputStateKey));
			if (component.Has<int>(Timer.CounterKey))
			{
				this._counter = component.Get(Timer.CounterKey);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00008C4C File Offset: 0x00006E4C
		public void DuplicateFrom(Timer source)
		{
			this.TimerIntervalA.DuplicateFrom(source.TimerIntervalA);
			this.TimerIntervalB.DuplicateFrom(source.TimerIntervalB);
			this.SetInput(source.Input);
			this.SetResetInput(source.ResetInput);
			this.SetMode(source.Mode);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00008C9F File Offset: 0x00006E9F
		public float GetProgress(out bool isCountingTimeB)
		{
			isCountingTimeB = this.IsCountingTimeB();
			return (float)this._counter / (float)(isCountingTimeB ? this.TimeB : this.TimeA);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008CC4 File Offset: 0x00006EC4
		public int GetTicksLeft()
		{
			int num = this.IsCountingTimeB() ? this.TimeB : this.TimeA;
			return Math.Max(0, num - this._counter);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00008CF6 File Offset: 0x00006EF6
		public bool IsUsingTicks()
		{
			if (!this.IsCountingTimeB())
			{
				return this.TimerIntervalA.Type == IntervalType.Ticks;
			}
			return this.TimerIntervalB.Type == IntervalType.Ticks;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00008D1D File Offset: 0x00006F1D
		public void SetMode(TimerMode timerMode)
		{
			if (timerMode != this.Mode)
			{
				this.Mode = timerMode;
				this.Reset();
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00008D35 File Offset: 0x00006F35
		public void SetInput(Automator automator)
		{
			this._input.Connect(automator);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00008D43 File Offset: 0x00006F43
		public void SetResetInput(Automator automator)
		{
			this._resetInput.Connect(automator);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00008D51 File Offset: 0x00006F51
		public void EvaluateNext()
		{
			this.EvaluateTimer();
			this._nextPreviousInputState = this.InputState;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008D68 File Offset: 0x00006F68
		public void CommitTick()
		{
			this._state = this._nextState;
			this._counter = this._nextCounter;
			this._previousInputState = this._nextPreviousInputState;
			this.UpdateOutputState();
			EventHandler timerTicked = this.TimerTicked;
			if (timerTicked == null)
			{
				return;
			}
			timerTicked(this, EventArgs.Empty);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008DB5 File Offset: 0x00006FB5
		public void Reset()
		{
			this._state = false;
			this._counter = 0;
			this.UpdateOutputState();
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00008DCB File Offset: 0x00006FCB
		public int TimeA
		{
			get
			{
				return this.TimerIntervalA.Ticks;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public int TimeB
		{
			get
			{
				return this.TimerIntervalB.Ticks;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00008DE5 File Offset: 0x00006FE5
		public bool InputState
		{
			get
			{
				return this._input.BooleanState;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00008DF2 File Offset: 0x00006FF2
		public bool ResetInputState
		{
			get
			{
				return this._resetInput.BooleanState;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00008DFF File Offset: 0x00006FFF
		public void UpdateOutputState()
		{
			this._automator.SetState(this._state);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00008E14 File Offset: 0x00007014
		public void EvaluateTimer()
		{
			if (this.ResetInputState)
			{
				this._nextState = false;
				this._nextCounter = 0;
				return;
			}
			switch (this.Mode)
			{
			case TimerMode.Delay:
				this.EvaluateDelay();
				return;
			case TimerMode.Pulse:
				this.EvaluatePulse();
				return;
			case TimerMode.Oscillator:
				this.EvaluateOscillator();
				return;
			case TimerMode.Accumulator:
				this.EvaluateAccumulator();
				return;
			default:
				throw new ArgumentOutOfRangeException("Mode", this.Mode, null);
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00008E8C File Offset: 0x0000708C
		public void EvaluateDelay()
		{
			if (this.InputState)
			{
				this._nextCounter = (this._state ? this.TimeA : (this._counter + 1));
				this._nextState = (this._state || this._nextCounter >= this.TimeA);
				return;
			}
			if (this._state)
			{
				this._nextState = (this._previousInputState ? (this.TimeB > 1) : (this._counter + 1 < this.TimeB));
				this._nextCounter = (this._nextState ? (this._previousInputState ? 1 : (this._counter + 1)) : 0);
				return;
			}
			this._nextState = false;
			this._nextCounter = 0;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00008F48 File Offset: 0x00007148
		public void EvaluatePulse()
		{
			if (this.InputState && !this._previousInputState)
			{
				this._nextCounter = 1;
				this._nextState = true;
				return;
			}
			if (this._state)
			{
				this._nextState = (this._counter < this.TimeA);
				this._nextCounter = (this._nextState ? (this._counter + 1) : 0);
				return;
			}
			this._nextCounter = 0;
			this._nextState = false;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00008FB8 File Offset: 0x000071B8
		public void EvaluateOscillator()
		{
			if (this.InputState)
			{
				this._nextState = ((this._state || !this._previousInputState) ? (this._counter < this.TimeA) : (this._counter >= this.TimeB));
				this._nextCounter = ((this._nextState != this._state) ? 1 : (this._counter + 1));
				return;
			}
			this._nextCounter = 0;
			this._nextState = false;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00009034 File Offset: 0x00007234
		public void EvaluateAccumulator()
		{
			if (this._state)
			{
				this._nextCounter = this.TimeA;
				this._nextState = true;
				return;
			}
			this._nextCounter = (this.InputState ? (this._counter + 1) : this._counter);
			this._nextState = (this._nextCounter >= this.TimeA);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00009094 File Offset: 0x00007294
		public bool IsCountingTimeB()
		{
			if (this.UsesIntervalB)
			{
				TimerMode mode = this.Mode;
				bool result;
				if (mode != TimerMode.Delay)
				{
					if (mode != TimerMode.Oscillator)
					{
						throw new ArgumentOutOfRangeException("Mode", this.Mode, null);
					}
					result = (!this._state && this.InputState && this._previousInputState);
				}
				else
				{
					result = (this._state && !this.InputState && !this._previousInputState);
				}
				return result;
			}
			return false;
		}

		// Token: 0x04000179 RID: 377
		public static readonly ComponentKey TimerKey = new ComponentKey("Timer");

		// Token: 0x0400017A RID: 378
		public static readonly PropertyKey<TimerMode> ModeKey = new PropertyKey<TimerMode>("Mode");

		// Token: 0x0400017B RID: 379
		public static readonly PropertyKey<Automator> InputKey = new PropertyKey<Automator>("Input");

		// Token: 0x0400017C RID: 380
		public static readonly PropertyKey<Automator> ResetInputKey = new PropertyKey<Automator>("ResetInput");

		// Token: 0x0400017D RID: 381
		public static readonly PropertyKey<TimerInterval> TimerIntervalAKey = new PropertyKey<TimerInterval>("TimerIntervalA");

		// Token: 0x0400017E RID: 382
		public static readonly PropertyKey<TimerInterval> TimerIntervalBKey = new PropertyKey<TimerInterval>("TimerIntervalB");

		// Token: 0x0400017F RID: 383
		public static readonly PropertyKey<bool> StateKey = new PropertyKey<bool>("State");

		// Token: 0x04000180 RID: 384
		public static readonly PropertyKey<bool> PreviousInputStateKey = new PropertyKey<bool>("PreviousInputState");

		// Token: 0x04000181 RID: 385
		public static readonly PropertyKey<int> CounterKey = new PropertyKey<int>("Counter");

		// Token: 0x04000186 RID: 390
		public readonly ReferenceSerializer _referenceSerializer;

		// Token: 0x04000187 RID: 391
		public readonly TimerIntervalSerializer _timerIntervalSerializer;

		// Token: 0x04000188 RID: 392
		public readonly TimerIntervalFactory _timerIntervalFactory;

		// Token: 0x04000189 RID: 393
		public AutomatorConnection _input;

		// Token: 0x0400018A RID: 394
		public AutomatorConnection _resetInput;

		// Token: 0x0400018B RID: 395
		public Automator _automator;

		// Token: 0x0400018C RID: 396
		public bool _state;

		// Token: 0x0400018D RID: 397
		public bool _nextState;

		// Token: 0x0400018E RID: 398
		public bool _previousInputState;

		// Token: 0x0400018F RID: 399
		public bool _nextPreviousInputState;

		// Token: 0x04000190 RID: 400
		public int _counter;

		// Token: 0x04000191 RID: 401
		public int _nextCounter;
	}
}
