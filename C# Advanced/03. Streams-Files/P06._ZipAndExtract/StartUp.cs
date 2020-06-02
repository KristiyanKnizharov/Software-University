using System;
using System.IO.Compression;

namespace P06._ZipAndExtract
{
    class StartUp
    {
        static void Main(string[] args)
        {
            ZipFile.CreateFromDirectory(@"D:\SoftUniLocalDiskD", @"C:\Users\admin\Desktop\SoftUni\C#\Advance\Streams-Files.cs\myArchive.zip");
            ZipFile.ExtractToDirectory(@"C:\Users\admin\Desktop\SoftUni\C#\Advance\Streams-Files.cs\myArchive.zip", @"C:\Users\admin\Desktop\copyMePNG");
        }
    }
}
