using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;

namespace Timberborn.TimbermeshAnimations
{
	// Token: 0x02000017 RID: 23
	public class TimbermeshAnimatorController : BaseComponent, IAwakableComponent, IStartableComponent, IAnimatorController
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000347C File Offset: 0x0000167C
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003484 File Offset: 0x00001684
		public AnimatorState CurrentState { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000348D File Offset: 0x0000168D
		public IEnumerable<string> AnimationNames
		{
			get
			{
				return this._spec.AnimationNames;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000349A File Offset: 0x0000169A
		public IReadOnlyDictionary<string, bool> BoolValues
		{
			get
			{
				return this._boolValues;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000034A2 File Offset: 0x000016A2
		public IReadOnlyDictionary<string, float> FloatValues
		{
			get
			{
				return this._floatValues;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000034AA File Offset: 0x000016AA
		public void Awake()
		{
			this._animator = base.GameObject.GetComponentsInChildren<TimbermeshAnimator>().Single<TimbermeshAnimator>();
			this._spec = base.GetComponent<TimbermeshAnimatorControllerSpec>();
			this.ValidateAnimations();
			this.InitializeAllParameters();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000034DA File Offset: 0x000016DA
		public void Start()
		{
			this._animator.Enabled = true;
			this.UpdateState();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000034F0 File Offset: 0x000016F0
		public bool HasParameter(string parameterName)
		{
			return this._spec.BoolParameters.Contains(parameterName) || this._spec.FloatParameters.Contains(parameterName);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000352C File Offset: 0x0000172C
		public void SetFloat(string parameterName, float value)
		{
			if (this._floatValues[parameterName] != value)
			{
				this._floatValues[parameterName] = value;
				if (this.CurrentState != null && parameterName == this.CurrentState.SpeedModifier)
				{
					this.UpdateAnimationSpeed();
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000357C File Offset: 0x0000177C
		public void SetBool(string parameterName, bool value)
		{
			if (this._boolValues[parameterName] != value)
			{
				this._boolValues[parameterName] = value;
				this.UpdateState();
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000035A0 File Offset: 0x000017A0
		public void Enable()
		{
			if (!this._enabled)
			{
				this._enabled = true;
				this.UpdateState();
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000035B7 File Offset: 0x000017B7
		public void Disable()
		{
			this._enabled = false;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000035C0 File Offset: 0x000017C0
		public void InitializeAllParameters()
		{
			foreach (string key in this._spec.BoolParameters)
			{
				this._boolValues.Add(key, false);
			}
			foreach (string key2 in this._spec.FloatParameters)
			{
				this._floatValues.Add(key2, 1f);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000363C File Offset: 0x0000183C
		public void ValidateAnimations()
		{
			for (int i = 0; i < this._spec.AnimatorStates.Length; i++)
			{
				string animationName = this._spec.AnimatorStates[i].AnimationName;
				if (!this._animator.HasAnimation(animationName))
				{
					throw new Exception(string.Concat(new string[]
					{
						"Missing animation: ",
						animationName,
						" in ",
						base.Name,
						" animator."
					}));
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000036C8 File Offset: 0x000018C8
		public void UpdateState()
		{
			if (this._enabled)
			{
				AnimatorState animatorState = this.FindBestMatchState();
				if (this.CurrentState != animatorState)
				{
					this.SetAnimatorState(animatorState);
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000036FC File Offset: 0x000018FC
		public AnimatorState FindBestMatchState()
		{
			AnimatorState result = this._spec.AnimatorStates[0];
			int num = int.MinValue;
			for (int i = this._spec.AnimatorStates.Length - 1; i >= 0; i--)
			{
				AnimatorState animatorState = this._spec.AnimatorStates[i];
				int num2;
				if (this.IsExactMatch(animatorState, out num2))
				{
					result = animatorState;
					break;
				}
				if (num2 > num)
				{
					result = animatorState;
					num = num2;
				}
			}
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003778 File Offset: 0x00001978
		public bool IsExactMatch(AnimatorState animatorState, out int matchingStatesCount)
		{
			matchingStatesCount = 0;
			for (int i = 0; i < animatorState.Conditions.Length; i++)
			{
				AnimatorStateCondition animatorStateCondition = animatorState.Conditions[i];
				bool flag = this._boolValues[animatorStateCondition.ParameterName];
				bool flag2 = flag && animatorStateCondition.MustBeTrue;
				bool flag3 = !flag && !animatorStateCondition.MustBeTrue;
				if (flag2 || flag3)
				{
					matchingStatesCount++;
				}
			}
			return matchingStatesCount == animatorState.Conditions.Length;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000037FE File Offset: 0x000019FE
		public void SetAnimatorState(AnimatorState animatorState)
		{
			this.CurrentState = animatorState;
			this._animator.Play(this.CurrentState.AnimationName, this.CurrentState.Looped);
			this.UpdateAnimationSpeed();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003830 File Offset: 0x00001A30
		public void UpdateAnimationSpeed()
		{
			float num = (!string.IsNullOrWhiteSpace(this.CurrentState.SpeedModifier)) ? this._floatValues[this.CurrentState.SpeedModifier] : 1f;
			this._animator.Speed = this.CurrentState.Speed * num;
		}

		// Token: 0x04000043 RID: 67
		public TimbermeshAnimator _animator;

		// Token: 0x04000044 RID: 68
		public TimbermeshAnimatorControllerSpec _spec;

		// Token: 0x04000045 RID: 69
		public readonly Dictionary<string, bool> _boolValues = new Dictionary<string, bool>();

		// Token: 0x04000046 RID: 70
		public readonly Dictionary<string, float> _floatValues = new Dictionary<string, float>();

		// Token: 0x04000047 RID: 71
		public bool _enabled = true;
	}
}
