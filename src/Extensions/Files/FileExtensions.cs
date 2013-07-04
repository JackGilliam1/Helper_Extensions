using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Extensions.Core.Files
{
    /*
     * Author: Jack Gilliam
     * Date Created: 4/2/2012
     */
    /// <summary>
    /// Provides functions for directories and files
    /// </summary>
    public static class FileFuncExt
    {
        /// <summary>
        /// Moves a file to a specified path
        /// </summary>
        /// <param name="file">The file to move</param>
        /// <param name="path">The path to move the file to</param>
        public static void To( this FileInfo file, string path )
        {
            file.MoveTo( path );
        }
        /// <summary>
        /// Moves multiple files to a specified path
        /// </summary>
        /// <param name="files">The files to move</param>
        /// <param name="path">The path to move the files to</param>
        public static void To( this ICollection<FileInfo> files, string path )
        {
            foreach ( var file in files )
            {
                file.To( path );
            }
        }
        /// <summary>
        /// Moves a directory to a specified path
        /// </summary>
        /// <param name="directory">Directory to move</param>
        /// <param name="path">Path to move the directory to</param>
        public static void To( this DirectoryInfo directory, string path )
        {
            directory.MoveTo( path );
        }
        /// <summary>
        /// Moves multiple directories to a specified path
        /// </summary>
        /// <param name="directories">Directories to move</param>
        /// <param name="path">Path to move the directories to</param>
        public static void To( this ICollection<DirectoryInfo> directories, string path )
        {
            foreach ( var directory in directories )
            {
                directory.MoveTo( path );
            }
        }

        /// <summary>
        /// Moves files with a specified extension within a directory to the specified path
        /// </summary>
        /// <param name="directory">Directory to locate files</param>
        /// <param name="extension">File extension to locate</param>
        /// <param name="path">Path to move files that match</param>
        public static void MoveByExt( this DirectoryInfo directory, string extension, string path )
        {
            ICollection<FileInfo> filesToMove = directory.GetByExt( extension );
            filesToMove.To( path );
        }
        /// <summary>
        /// Moves files with specified extensions within a directory to a specified path
        /// </summary>
        /// <param name="directory">Directory to locate files in</param>
        /// <param name="extensions">File extensions to locate</param>
        /// <param name="path">Path to move files that match</param>
        public static void MoveByExt( this DirectoryInfo directory, ICollection<string> extensions, string path )
        {
            ICollection<FileInfo> filesToMove = directory.GetByExt( extensions );
            filesToMove.To( path );
        }
        /// <summary>
        /// Moves files with a specified extension within multiple directories to a specified path
        /// </summary>
        /// <param name="directories">Directories to locate files</param>
        /// <param name="extension">File extension to locate</param>
        /// <param name="path">Path to move files that match</param>
        public static void MoveByExt( this ICollection<DirectoryInfo> directories, string extension, string path )
        {
            ICollection<FileInfo> filesToMove = directories.GetByExt( extension );
            filesToMove.To( path );
        }
        /// <summary>
        /// Moves files with specified extensions within specified directories to a specified path
        /// </summary>
        /// <param name="directories">Directories to locate files</param>
        /// <param name="extensions">File extensions to locate</param>
        /// <param name="path">Path to move files that match</param>
        public static void MoveByExt( this ICollection<DirectoryInfo> directories, ICollection<string> extensions, string path )
        {
            ICollection<FileInfo> filesToMove = directories.GetByExt( extensions );
            filesToMove.To( path );
        }

        /// <summary>
        /// Adds a sub directory with a specified name to a directory
        /// </summary>
        /// <param name="directory">Directory to add a sub directory to</param>
        /// <param name="folderName">Name of the subdirectory being created</param>
        /// <returns>The created subdirectory</returns>
        public static DirectoryInfo AddSubDirectory( this DirectoryInfo directory, string folderName )
        {
            DirectoryInfo newDirectory = null;
            string newFolderPath = directory.FullName + "/" + folderName;
            if ( !Directory.Exists( newFolderPath ) )
            {
                directory.CreateSubdirectory( folderName );
            }
            return newDirectory;
        }
        /// <summary>
        /// Adds multiple subdirectories with specified names to a directory
        /// </summary>
        /// <param name="directory">Directory to add sub directories to</param>
        /// <param name="folderNames">Names of the subdirectories being created</param>
        /// <returns>The created subdirectories</returns>
        public static ICollection<DirectoryInfo> AddSubDirectory( this DirectoryInfo directory, ICollection<string> folderNames )
        {
            ICollection<DirectoryInfo> newSubDirectories = new List<DirectoryInfo>( );
            foreach ( var dirName in folderNames )
            {
                directory.AddSubDirectory( dirName );
            }
            return newSubDirectories;
        }

        /// <summary>
        /// Returns files with a specified extension within a directory
        /// </summary>
        /// <param name="directory">Directory to locate files within</param>
        /// <param name="extension">File extension to locate</param>
        /// <returns>Files matching the specified extension</returns>
        public static ICollection<FileInfo> GetByExt( this DirectoryInfo directory, string extension )
        {
            ICollection<FileInfo> files = directory.GetFiles( );
            ICollection<FileInfo> foundFiles = new List<FileInfo>( );
            foundFiles = files.Where( f => f.Extension == extension ).ToList( );
            return foundFiles;
        }
        /// <summary>
        /// Returns files with specified extensions within a directory
        /// </summary>
        /// <param name="directory">Directory to locate files within</param>
        /// <param name="extensions">File extensions to locate</param>
        /// <returns>Files matching the specified extensions</returns>
        public static ICollection<FileInfo> GetByExt( this DirectoryInfo directory, ICollection<string> extensions )
        {
            IEnumerable<FileInfo> foundFiles = new List<FileInfo>( );
            foreach ( var ext in extensions )
            {
                foundFiles = foundFiles.Union( directory.GetByExt( ext ) );
            }
            return foundFiles.ToList();
        }
        /// <summary>
        /// Returns files with a specified extension within multiple directories
        /// </summary>
        /// <param name="directories">Directories to locate files within</param>
        /// <param name="extension">File extension to locate</param>
        /// <returns>Files matching the specified extension</returns>
        public static ICollection<FileInfo> GetByExt( this ICollection<DirectoryInfo> directories, string extension )
        {
            IEnumerable<FileInfo> foundFiles = new List<FileInfo>( );
            foreach ( var dirInfo in directories )
            {
                foundFiles = foundFiles.Union( dirInfo.GetByExt( extension ) );
            }
            return foundFiles.ToList();
        }
        /// <summary>
        /// Returns files with specified extensions within multiple directories
        /// </summary>
        /// <param name="directories">Directories to locate files within</param>
        /// <param name="extensions">File extensions to locate</param>
        /// <returns>Files matching the specified extensions</returns>
        public static ICollection<FileInfo> GetByExt( this ICollection<DirectoryInfo> directories, ICollection<string> extensions )
        {
            IEnumerable<FileInfo> foundFiles = new List<FileInfo>( );
            foreach ( var dirInfo in directories )
            {
                foundFiles = foundFiles.Union( dirInfo.GetByExt( extensions ) );
            }
            return foundFiles.ToList();
        }


        /// <summary>
        /// Returns directories located at specified paths
        /// </summary>
        /// <param name="directoryPaths">Directory paths</param>
        /// <returns>Directories at the specified paths</returns>
        public static ICollection<DirectoryInfo> GetDirectories( this ICollection<string> directoryPaths )
        {
            ICollection<DirectoryInfo> directories = new List<DirectoryInfo>( );
            foreach ( var dirPath in directoryPaths )
            {
                if ( Directory.Exists( dirPath ) )
                {
                    directories.Add( new DirectoryInfo( dirPath ) );
                }
            }
            return directories;
        }
        /// <summary>
        /// Returns only files (No folders will return) from a directory
        /// </summary>
        /// <param name="directory">Directory to pull files</param>
        /// <returns>All files that are not directories contained within a directory</returns>
        public static ICollection<FileInfo> GetNonDirs( this DirectoryInfo directory )
        {
            ICollection<FileInfo> files = new List<FileInfo>( );
            ICollection<DirectoryInfo> directories = directory.GetDirectories( );
            ICollection<FileInfo> filesWithin = directory.GetFiles( );
            files = filesWithin.Where( f => directories.FirstOrDefault( d => d.Name == f.Name ) == null ).ToList( );
            return files;
        }
        /// <summary>
        /// Returns only files (No folders will return) from multiple directories
        /// </summary>
        /// <param name="directories">Directories to pull files</param>
        /// <returns>All files that are not directories contained within the specified directories</returns>
        public static ICollection<FileInfo> GetNonDirs( this ICollection<DirectoryInfo> directories )
        {
            ICollection<FileInfo> files = new List<FileInfo>( );
            foreach ( var dirInfo in directories )
            {
                files = files.Union( dirInfo.GetNonDirs( ) ).ToList( );
            }
            return files;
        }
        /// <summary>
        /// Returns files located at the specified paths
        /// </summary>
        /// <param name="directoryPaths">Directory paths to locate files within</param>
        /// <returns></returns>
        public static ICollection<FileInfo> Files( this ICollection<string> directoryPaths )
        {
            ICollection<FileInfo> filesFound = new List<FileInfo>( );
            foreach ( var dirPath in directoryPaths )
            {
                if ( Directory.Exists( dirPath ) )
                {
                    ICollection<FileInfo> files = new DirectoryInfo( dirPath ).GetFiles( );
                    if ( files != null )
                    {
                        filesFound = filesFound.Union( files ).ToList( );
                    }
                }
            }
            return null;
        }
    }
}
