using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CSharpLibrary.FileOperations
{
  /// <summary>
  /// Contains functionality used for file comparisons.
  /// </summary>
  public static class FileComparison
  {

    /// <summary>
    /// Used to describe the difference of a specific file between two provided locations (i.e. folder paths).
    /// </summary>
    public enum DiffInfo
    {
      /// <summary>
      /// The given file exists in the original location but is missing from the compared location.
      /// </summary>
      FileIsMissing,

      /// <summary>
      /// File exists in both locations but its contents are different.
      /// </summary>
      DifferentContents,
    }

    /// <summary>
    /// Used for grouping the found difference info regarding a specific file.
    /// </summary>
    public struct FileDifference
    {
      /// <summary>
      /// The information about the compared file.
      /// </summary>
      public FileInfo File { get; }

      /// <summary>
      /// Provide information about the found difference between the original file and the destination file
      /// </summary>
      public DiffInfo Info { get; }

      internal FileDifference(FileInfo file, DiffInfo info)
      {
        File = file;
        Info = info;
      }
    }

    /// <summary>
    /// Get the file differences between two provided locations.
    /// </summary>
    /// <param name="originFolder">
    /// The base path.
    /// </param>
    /// <param name="folderToCompareTo">
    /// The path we want to compare against.
    /// </param>
    /// <returns>
    /// A list of the found <see cref="FileDifference">file differences</see> between the two locations.
    /// <para/>
    /// <strong>Note:</strong> currently the function doesn't provide handling for a file that is 
    /// <br/>
    /// found inside the compared path ("folderToCompareTo") but is missing from the base path ("originFolder")
    /// </returns>
    public static List<FileDifference> PerfromFolderComparisonMD5(string originFolder, string folderToCompareTo)
    {
      //The returned list of found files differences
      List<FileDifference> missmatchedFiles = new List<FileDifference>();

      //Get all files from both folders including sub directories 
      FileInfo[] originFiles = (new DirectoryInfo(originFolder)).GetFiles("*.*", SearchOption.AllDirectories);
      FileInfo[] filesToCompareAgainst = (new DirectoryInfo(folderToCompareTo)).GetFiles("*.*", SearchOption.AllDirectories);

      //Create the hash algorithm once to avoid un-necessary memory overhead 
      MD5 md5 = MD5.Create();

      for (int i = 0; i < originFiles.Length; i++)
      {
        ///File exists in <see cref="originFolder"/> and is missing from <see cref="folderToCompareTo"/>
        var matchingDestFile = (filesToCompareAgainst.FirstOrDefault(destFile => destFile.Name == originFiles[i].Name));
        if (matchingDestFile == null)
        {
          missmatchedFiles.Add(new FileDifference(originFiles[i], DiffInfo.FileIsMissing));
        }
        //File exists but its contents are different
        else if (!(String.Equals(GetMD5HashFromFile(md5, originFiles[i].FullName), GetMD5HashFromFile(md5, matchingDestFile.FullName))))
        {
          missmatchedFiles.Add(new FileDifference(originFiles[i], DiffInfo.DifferentContents));
        }
      }
      return missmatchedFiles;
    }


    /// <summary>
    /// Returns the hash contents of a given file in a string format.
    /// <para/>
    /// Additional info:
    /// <br/>
    /// * <see href="https://stackoverflow.com/a/16318156/13829249">Stack Overflow - "Calculate the Hash of the Contents of a File in C#"</see>
    /// </summary>
    /// <param name="md5">
    /// Instance of hash algorithm used for acquiring the 
    /// </param>
    /// <param name="fileName">
    /// The file path to get the hash for. 
    /// </param>
    /// <returns>
    /// String representation of a hash created from the file contents.
    /// </returns>
    internal static string GetMD5HashFromFile(MD5 md5, string fileName)
    {
      byte[] retVal;
      using (FileStream file = new FileStream(fileName, FileMode.Open))
      {
        retVal = md5.ComputeHash(file);
      }
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < retVal.Length; i++)
      {
        sb.Append(retVal[i].ToString("x2"));
      }
      return sb.ToString();
    }
  }
}

