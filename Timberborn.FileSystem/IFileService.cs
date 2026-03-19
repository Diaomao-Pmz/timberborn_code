using System;
using System.IO;

namespace Timberborn.FileSystem
{
	// Token: 0x02000009 RID: 9
	public interface IFileService
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26
		bool HasDocumentsPermissions { get; }

		// Token: 0x0600001B RID: 27
		bool FileExists(string fileName);

		// Token: 0x0600001C RID: 28
		Stream CreateFile(string fileName);

		// Token: 0x0600001D RID: 29
		Stream OpenFile(string fileName);

		// Token: 0x0600001E RID: 30
		void DeleteFile(string fileName);

		// Token: 0x0600001F RID: 31
		FileInfo GetFileInfo(string fileName);

		// Token: 0x06000020 RID: 32
		bool DirectoryExistsAndNotEmpty(string directoryName, string fileExtension);

		// Token: 0x06000021 RID: 33
		void CreateDirectory(string directoryName);

		// Token: 0x06000022 RID: 34
		void DeleteDirectory(string directoryName);

		// Token: 0x06000023 RID: 35
		string CombineIntoPath(string path1, string path2, string extension = "");

		// Token: 0x06000024 RID: 36
		void WriteTextToFile(string path, string text);

		// Token: 0x06000025 RID: 37
		void CopyFile(string sourceFileName, string destinationFileName);

		// Token: 0x06000026 RID: 38
		DirectoryCreationResult CreateDirectoryIfValid(string directoryPath);
	}
}
