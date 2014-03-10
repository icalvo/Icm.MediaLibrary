using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icm.MediaLibrary.Domain
{
    public interface IFileSystemObserver
    {
        void Watch();

        event EventHandler<FileChangeEventArgs> Changed;

        event EventHandler<FileCreateEventArgs> Created;

        event EventHandler<FileDeleteEventArgs> Deleted;

        event EventHandler<FileCreateEventArgs> Error;

        event EventHandler<FileRenameEventArgs> Renamed;

        void NotifyCreated(string filePath);
    }
}
