using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.MechanicalSystem;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.MechanicalConnectorSystem
{
	// Token: 0x0200000A RID: 10
	public class MechanicalConnectors : BaseComponent, IAwakableComponent
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002589 File Offset: 0x00000789
		public MechanicalConnectors(MaterialColorer materialColorer)
		{
			this._materialColorer = materialColorer;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025A3 File Offset: 0x000007A3
		public void Awake()
		{
			this._entityMaterials = base.GetComponent<EntityMaterials>();
			this._highlightableObject = base.GetComponent<HighlightableObject>();
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025C9 File Offset: 0x000007C9
		public void Add(Transput transput, GameObject connector)
		{
			this._connectors.Add(transput, connector);
			MechanicalConnectors.Hide(connector);
			if (this._entityMaterials)
			{
				this._entityMaterials.AddMaterials(connector);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025F8 File Offset: 0x000007F8
		public void Show(Transput transput)
		{
			GameObject gameObject = this._connectors[transput];
			gameObject.SetActive(true);
			this._highlightableObject.RefreshHighlight();
			if (this._blockObject.IsUnfinished)
			{
				this._materialColorer.EnableGrayscale(gameObject);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002640 File Offset: 0x00000840
		public void Hide(Transput transput)
		{
			GameObject connector;
			if (this._connectors.TryGetValue(transput, out connector))
			{
				MechanicalConnectors.Hide(connector);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002663 File Offset: 0x00000863
		public static void Hide(GameObject connector)
		{
			connector.SetActive(false);
		}

		// Token: 0x04000014 RID: 20
		public readonly Dictionary<Transput, GameObject> _connectors = new Dictionary<Transput, GameObject>();

		// Token: 0x04000015 RID: 21
		public readonly MaterialColorer _materialColorer;

		// Token: 0x04000016 RID: 22
		public EntityMaterials _entityMaterials;

		// Token: 0x04000017 RID: 23
		public HighlightableObject _highlightableObject;

		// Token: 0x04000018 RID: 24
		public BlockObject _blockObject;
	}
}
