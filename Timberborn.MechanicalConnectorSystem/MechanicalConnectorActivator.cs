using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Coordinates;
using Timberborn.MechanicalSystem;

namespace Timberborn.MechanicalConnectorSystem
{
	// Token: 0x02000007 RID: 7
	public class MechanicalConnectorActivator : BaseComponent, IAwakableComponent, IUnfinishedStateListener, IFinishedStateListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		public MechanicalConnectorActivator(MechanicalConnectorFactory mechanicalConnectorFactory, TransputMap transputMap)
		{
			this._mechanicalConnectorFactory = mechanicalConnectorFactory;
			this._transputMap = transputMap;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211F File Offset: 0x0000031F
		public void Awake()
		{
			this._mechanicalNode = base.GetComponent<MechanicalNode>();
			this._buildingModel = base.GetComponent<BuildingModel>();
			this._mechanicalConnectors = base.GetComponent<MechanicalConnectors>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002145 File Offset: 0x00000345
		public void OnEnterUnfinishedState()
		{
			this.InitializeTransputs();
			this._transputMap.TransputAdded += this.OnTransputAdded;
			this._transputMap.TransputRemoved += this.OnTransputRemoved;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000217B File Offset: 0x0000037B
		public void OnExitUnfinishedState()
		{
			this._transputMap.TransputAdded -= this.OnTransputAdded;
			this._transputMap.TransputRemoved -= this.OnTransputRemoved;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002145 File Offset: 0x00000345
		public void OnEnterFinishedState()
		{
			this.InitializeTransputs();
			this._transputMap.TransputAdded += this.OnTransputAdded;
			this._transputMap.TransputRemoved += this.OnTransputRemoved;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000217B File Offset: 0x0000037B
		public void OnExitFinishedState()
		{
			this._transputMap.TransputAdded -= this.OnTransputAdded;
			this._transputMap.TransputRemoved -= this.OnTransputRemoved;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021AC File Offset: 0x000003AC
		public void InitializeTransputs()
		{
			if (!this._isInitialized)
			{
				this._connectableTransputs.AddRange(this._mechanicalNode.Transputs.Where(new Func<Transput, bool>(MechanicalConnectorActivator.IsConnectable)));
				foreach (Transput transput in this._connectableTransputs)
				{
					this.InitializeTransput(transput);
				}
				this._isInitialized = true;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002238 File Offset: 0x00000438
		public void OnTransputAdded(object sender, Transput other)
		{
			foreach (Transput transput in this._connectableTransputs)
			{
				if (transput.Faces(other))
				{
					this.ShowConnector(transput, other);
					break;
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002298 File Offset: 0x00000498
		public void OnTransputRemoved(object sender, Transput other)
		{
			foreach (Transput transput in this._connectableTransputs)
			{
				if (transput.Faces(other))
				{
					this._mechanicalConnectors.Hide(transput);
					break;
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022FC File Offset: 0x000004FC
		public void InitializeTransput(Transput transput)
		{
			this._mechanicalConnectorFactory.Create(transput, this._buildingModel.FinishedModel.transform, this._mechanicalConnectors);
			Transput facingTransput = this._transputMap.GetFacingTransput(transput);
			if (facingTransput != null)
			{
				this.ShowConnector(transput, facingTransput);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002344 File Offset: 0x00000544
		public void ShowConnector(Transput transput, Transput target)
		{
			MechanicalNode parentNode = target.ParentNode;
			if (parentNode.IsShaft || parentNode.IsGenerator || parentNode.IsIntermediary)
			{
				this._mechanicalConnectors.Show(transput);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000237C File Offset: 0x0000057C
		public static bool IsConnectable(Transput transput)
		{
			return transput.Direction.IsHorizontal() || transput.Direction == Direction3D.Top;
		}

		// Token: 0x04000008 RID: 8
		public readonly MechanicalConnectorFactory _mechanicalConnectorFactory;

		// Token: 0x04000009 RID: 9
		public readonly TransputMap _transputMap;

		// Token: 0x0400000A RID: 10
		public MechanicalNode _mechanicalNode;

		// Token: 0x0400000B RID: 11
		public BuildingModel _buildingModel;

		// Token: 0x0400000C RID: 12
		public MechanicalConnectors _mechanicalConnectors;

		// Token: 0x0400000D RID: 13
		public readonly List<Transput> _connectableTransputs = new List<Transput>();

		// Token: 0x0400000E RID: 14
		public bool _isInitialized;
	}
}
