using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.MechanicalSystem;
using UnityEngine.UIElements;

namespace Timberborn.MechanicalSystemUI
{
	// Token: 0x0200001E RID: 30
	public class MechanicalNodeFragment : IEntityPanelFragment
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000037B4 File Offset: 0x000019B4
		public MechanicalNodeFragment(VisualElementLoader visualElementLoader, GeneratorFragmentService generatorFragmentService, ConsumerFragmentService consumerFragmentService, NetworkFragmentService networkFragmentService)
		{
			this._visualElementLoader = visualElementLoader;
			this._generatorFragmentService = generatorFragmentService;
			this._consumerFragmentService = consumerFragmentService;
			this._networkFragmentService = networkFragmentService;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000037DC File Offset: 0x000019DC
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/MechanicalNodeFragment");
			this._generatorFragmentService.Initialize(UQueryExtensions.Q<Label>(this._root, "Generator", null));
			this._consumerFragmentService.Initialize(UQueryExtensions.Q<Label>(this._root, "Consumer", null));
			this._networkFragmentService.Initialize(UQueryExtensions.Q<Label>(this._root, "Network", null));
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003865 File Offset: 0x00001A65
		public void ShowFragment(BaseComponent entity)
		{
			this._mechanicalNode = entity.GetComponent<MechanicalNode>();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003873 File Offset: 0x00001A73
		public void ClearFragment()
		{
			this._mechanicalNode = null;
			this._generatorFragmentService.Hide();
			this._consumerFragmentService.Hide();
			this._networkFragmentService.Hide();
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000038AC File Offset: 0x00001AAC
		public void UpdateFragment()
		{
			bool visible = false;
			if (this._mechanicalNode && this._mechanicalNode.Enabled)
			{
				visible = (this._generatorFragmentService.Update(this._mechanicalNode) | this._consumerFragmentService.Update(this._mechanicalNode) | this._networkFragmentService.Update(this._mechanicalNode));
			}
			this._root.ToggleDisplayStyle(visible);
		}

		// Token: 0x04000061 RID: 97
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000062 RID: 98
		public readonly GeneratorFragmentService _generatorFragmentService;

		// Token: 0x04000063 RID: 99
		public readonly ConsumerFragmentService _consumerFragmentService;

		// Token: 0x04000064 RID: 100
		public readonly NetworkFragmentService _networkFragmentService;

		// Token: 0x04000065 RID: 101
		public VisualElement _root;

		// Token: 0x04000066 RID: 102
		public MechanicalNode _mechanicalNode;
	}
}
