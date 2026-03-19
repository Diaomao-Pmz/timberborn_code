using System;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x0200000A RID: 10
	public class DefinableInputBinding
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023A4 File Offset: 0x000005A4
		public KeyBinding KeyBinding { get; }

		// Token: 0x06000019 RID: 25 RVA: 0x000023AC File Offset: 0x000005AC
		public DefinableInputBinding(KeyBinding keyBinding, bool? isPrimary)
		{
			this.KeyBinding = keyBinding;
			this._isPrimary = isPrimary;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023C2 File Offset: 0x000005C2
		public bool TryGetDefinedInputBinding(out InputBinding inputBinding)
		{
			inputBinding = this.GetInputBinding();
			return inputBinding.IsDefined;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023D3 File Offset: 0x000005D3
		public InputBinding GetSingleInputBinding()
		{
			if (!this.IsPrimary())
			{
				return this.KeyBinding.SecondaryInputBinding;
			}
			return this.KeyBinding.PrimaryInputBinding;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023F4 File Offset: 0x000005F4
		public bool IsPrimary()
		{
			return this._isPrimary.Value;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002401 File Offset: 0x00000601
		public InputBinding GetInputBinding()
		{
			if (this._isPrimary != null)
			{
				return this.GetSingleInputBinding();
			}
			if (!this.KeyBinding.PrimaryInputBinding.IsDefined)
			{
				return this.KeyBinding.SecondaryInputBinding;
			}
			return this.KeyBinding.PrimaryInputBinding;
		}

		// Token: 0x04000017 RID: 23
		public readonly bool? _isPrimary;
	}
}
