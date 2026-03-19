using System;
using System.IO;
using System.Linq;
using System.Text;
using Timberborn.Debugging;
using Timberborn.FileSystem;
using Timberborn.PlatformUtilities;
using Timberborn.PrefabOptimization;
using UnityEngine;

namespace Timberborn.Diagnostics
{
	// Token: 0x02000008 RID: 8
	public class MeshMetricsDumper : IDevModule
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002339 File Offset: 0x00000539
		public MeshMetricsDumper(MeshMetricsRetriever meshMetricsRetriever, IPrefabOptimizationChain prefabOptimizationChain, IFileService fileService, IExplorerOpener explorerOpener)
		{
			this._meshMetricsRetriever = meshMetricsRetriever;
			this._prefabOptimizationChain = prefabOptimizationChain;
			this._fileService = fileService;
			this._explorerOpener = explorerOpener;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000235E File Offset: 0x0000055E
		public DevModuleDefinition GetDefinition()
		{
			return new DevModuleDefinition.Builder().AddMethod(DevMethod.Create("Dump mesh metrics", new Action(this.DumpMeshMetrics))).Build();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002388 File Offset: 0x00000588
		public void DumpMeshMetrics()
		{
			string text = this.DumpMeshMetricsToString();
			string text2 = MeshMetricsDumper.CreateFilePath();
			string directoryName = Path.GetDirectoryName(text2);
			this._fileService.WriteTextToFile(text2, text);
			Debug.Log("Dumped mesh metrics to " + text2);
			this._explorerOpener.OpenDirectory(directoryName);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023D4 File Offset: 0x000005D4
		public string DumpMeshMetricsToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			MeshMetricsDumper.AppendHeader(stringBuilder);
			this.AppendMetrics(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023FC File Offset: 0x000005FC
		public void AppendMetrics(StringBuilder meshMetricsDescription)
		{
			foreach (MeshMetrics meshMetrics in from prefab in this._prefabOptimizationChain.GetCached()
			select this._meshMetricsRetriever.GetMeshMetrics(prefab) into prefab
			orderby prefab.NumberOfTriangles descending
			select prefab)
			{
				meshMetricsDescription.Append(meshMetrics.Name);
				meshMetricsDescription.Append(",");
				meshMetricsDescription.Append(meshMetrics.NumberOfVertices);
				meshMetricsDescription.Append(",");
				meshMetricsDescription.Append(meshMetrics.NumberOfTriangles);
				meshMetricsDescription.Append(",");
				meshMetricsDescription.Append(meshMetrics.NumberOfTrianglesPerTile);
				meshMetricsDescription.Append(",");
				meshMetricsDescription.Append(meshMetrics.NumberOfSubmeshes);
				meshMetricsDescription.AppendLine();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002500 File Offset: 0x00000700
		public static void AppendHeader(StringBuilder meshMetricsDescription)
		{
			meshMetricsDescription.Append("Name");
			meshMetricsDescription.Append(",");
			meshMetricsDescription.Append("NumberOfVertices");
			meshMetricsDescription.Append(",");
			meshMetricsDescription.Append("NumberOfTriangles");
			meshMetricsDescription.Append(",");
			meshMetricsDescription.Append("NumberOfTrianglesPerTile");
			meshMetricsDescription.Append(",");
			meshMetricsDescription.Append("NumberOfSubmeshes");
			meshMetricsDescription.AppendLine();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002580 File Offset: 0x00000780
		public static string CreateFilePath()
		{
			string str = DateTime.Now.ToString("yyyy-MM-dd HH\\hmm\\mss\\s");
			string path = "MeshMetrics " + str + ".csv";
			return Path.Combine(UserDataFolder.Folder, "MeshMetrics", path);
		}

		// Token: 0x04000012 RID: 18
		public readonly MeshMetricsRetriever _meshMetricsRetriever;

		// Token: 0x04000013 RID: 19
		public readonly IPrefabOptimizationChain _prefabOptimizationChain;

		// Token: 0x04000014 RID: 20
		public readonly IFileService _fileService;

		// Token: 0x04000015 RID: 21
		public readonly IExplorerOpener _explorerOpener;
	}
}
