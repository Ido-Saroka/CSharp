using AutoFixture;
using CSharpLibrary.FileOperations;
using System;
using System.IO;
using System.Security.Cryptography;
using Xunit;

namespace CSharpLibrary.Tests.FileOperations.Tests
{
  public class FileComparisonTests
  {

    /// <summary>
    /// 
    /// </summary>
    private readonly Fixture _fixture;

    /// <summary>
    /// Test class constructor will initialize variables needed for the various test methods
    /// </summary>
    public FileComparisonTests()
    {
      _fixture = new Fixture();
      _fixture.Register(() => new DirectoryInfo(_fixture.Create<string>()));
      _fixture.Register(() => new FileInfo(_fixture.Create<string>()));
    }


    [Theory]
    [InlineData("", "d41d8cd98f00b204e9800998ecf8427e")]
    [InlineData("content", "9a0364b9e99bb480dd25e1f0284c8555")]
    public void GetMD5HashFromFile_CheckReturnedHash(string fileContent, string expectedHash)
    {
      // Arrange
      var fileName = _fixture.Create<FileInfo>().FullName;
      // create file
      File.WriteAllText(fileName, fileContent);

      // Act
      var hash = FileComparison.GetMD5HashFromFile(MD5.Create(), fileName);

      // Assert
      Assert.Equal(expectedHash, hash);
    }


    private static HashAlgorithm CreateFileMocking(HashAlgorithm algorithmToUseForMocking)
    {
      throw new NotImplementedException();
    }


    public void PerfromFolderComparisonMD5_MockTests()
    {
      //Arrange


      //Act


      //Assert
    }




  }
}
