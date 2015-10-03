using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterface.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.FileSystemInfo"/> class.
    /// </summary>	  
    [CLSCompliant(false)]
    public interface IFileSystemInfo
    {
        /// <summary>
        ///  Gets or sets the <see cref="System.IO.FileAttributes"/> of the current Gets or sets the creation time, in coordinated universal time (UTC), of the current FileSystemInfo object. . 
        /// </summary>
        FileAttributes Attributes { get; set; }
        /// <summary>
        /// Gets or sets the creation time of the current <see cref="T:System.IO.FileSystemInfo"/> object.
        /// </summary>
        IDateTime CreationTime { get; set; }
        /// <summary>
        /// Gets or sets the creation time, in coordinated universal time (UTC), of the current <see cref="T:System.IO.FileSystemInfo"/> object. 
        /// </summary>
        IDateTime CreationTimeUtc { get; set; }
        /// <summary>
        /// Gets a value indicating whether a file/directory exists. 
        /// </summary>
        bool Exists { get; }
        /// <summary>
        /// Gets the string representing the extension part of the file/directory.
        /// </summary>
        string Extension { get; }
        /// <summary>
        /// Gets the full path of the directory or file.
        /// </summary>
        string FullName { get; }
        /// <summary>
        /// Gets or sets the time the current file or directory was last accessed. 
        /// </summary>
        IDateTime LastAccessTime { get; set; }
        /// <summary>
        /// Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.
        /// </summary>
        IDateTime LastAccessTimeUtc { get; set; }
        /// <summary>
        /// Gets or sets the time when the current file or directory was last written to.
        /// </summary>
        IDateTime LastWriteTime { get; set; }
        /// <summary>
        /// Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.
        /// </summary>
        IDateTime LastWriteTimeUtc { get; set; }
        /// <summary>
        /// Gets the name of the file/directory. 
        /// </summary>
        string Name { get; }


        /// <summary>
        /// Permanently deletes a file/directory.
        /// </summary>
        void Delete();
        /// <summary>
        /// Refreshes the state of the object.
        /// </summary>
        void Refresh();
        /// <summary>
        /// Returns the path as a string.
        /// </summary>
        /// <returns>A string representing the path.</returns>
        string ToString();

    }
}
