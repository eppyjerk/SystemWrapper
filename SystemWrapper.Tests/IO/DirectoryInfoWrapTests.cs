using SystemWrapper.IO;
using NUnit.Framework;
using SystemInterface.IO;
using System.Linq;

namespace SystemWrapper.Tests.IO
{
    [TestFixture]
    [Author("Vadim Kreynin", "Vadim@kreynin.com")]
    public class DirectoryInfoWrapTests
    {
        [Test]
        public void Create_two_directories_and_then_delete_them()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            IDirectoryInfo[] directoriesBefore = directoryInfoWrap.GetDirectories();

            directoryInfoWrap.CreateSubdirectory("Dir1");
            directoryInfoWrap.CreateSubdirectory("Dir2");
            IDirectoryInfo[] directoriesAfterCreate = directoryInfoWrap.GetDirectories();

            Assert.AreEqual("Dir1", directoriesAfterCreate[0].Name);
            Assert.AreEqual("Dir2", directoriesAfterCreate[1].Name);
            directoriesAfterCreate[0].Delete();
            directoriesAfterCreate[1].Delete();

            var directoriesAfterDelete = directoryInfoWrap.GetDirectories();
            Assert.AreEqual(directoriesBefore.Length, directoriesAfterDelete.Length);
        }

        [Test]
        public void GetFiles_must_have_files_in_Debug_folder()
        {
            IDirectoryInfo directoryWrap = new DirectoryInfoWrap(new DirectoryWrap().GetCurrentDirectory());
            IFileInfo[] fileInfoWraps = directoryWrap.GetFiles();
            Assert.IsTrue(fileInfoWraps.Length > 0);
        }

        [Test]
        public void EnumerateDirectories_test()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var subdirectories = directoryInfoWrap.Parent.EnumerateDirectories();

            Assert.AreEqual(1, subdirectories.Count());
            Assert.AreEqual(directoryInfoWrap.Name, subdirectories.First().Name);
        }

        [Test]
        public void EnumerateDirectories_searchPattern_match()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var subdirectories = directoryInfoWrap.Parent.EnumerateDirectories(directoryInfoWrap.Name);
            Assert.AreEqual(1, subdirectories.Count());
            Assert.AreEqual(directoryInfoWrap.Name, subdirectories.First().Name);
        }

        [Test]
        public void EnumerateDirectories_searchPattern_nomatch()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var subdirectories = directoryInfoWrap.EnumerateDirectories("XXX");
            Assert.AreEqual(0, subdirectories.Count());
        }

        [Test]
        public void EnumerateDirectories_searchPatternOption_all()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var subdirectories = directoryInfoWrap.Parent.Parent.EnumerateDirectories(directoryInfoWrap.Name, System.IO.SearchOption.AllDirectories);
            Assert.AreEqual(2, subdirectories.Count()); // 2: bin\Debug and obj\Debug
            Assert.IsTrue(subdirectories.All(dir => dir.Name.Equals(directoryInfoWrap.Name)));
        }

        [Test]
        public void EnumerateDirectories_searchPatternOption_top()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var subdirectories = directoryInfoWrap.Parent.EnumerateDirectories(directoryInfoWrap.Name, System.IO.SearchOption.TopDirectoryOnly);
            Assert.AreEqual(1, subdirectories.Count());
            Assert.AreEqual(directoryInfoWrap.Name, subdirectories.First().Name);
        }

        [Test]
        public void EnumerateFiles_test()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var files = directoryInfoWrap.Parent.Parent.EnumerateFiles();
            Assert.Greater(files.Count(), 0);
        }

        [Test]
        public void EnumerateFiles_searchPattern_match()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var files = directoryInfoWrap.EnumerateFiles(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name);
            Assert.AreEqual(1, files.Count());
        }

        [Test]
        public void EnumerateFiles_searchPattern_nomatch()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var files = directoryInfoWrap.Parent.Parent.EnumerateFiles("XXXX");
            Assert.AreEqual(0, files.Count());
        }

        [Test]
        public void EnumerateFiles_searchPatternOption_all()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var files = directoryInfoWrap.Parent.Parent.Parent.Parent.EnumerateFiles(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name, System.IO.SearchOption.AllDirectories);
            Assert.Greater(files.Count(), 0);
        }

        [Test]
        public void EnumerateFiles_searchPatternOption_top()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var files = directoryInfoWrap.EnumerateFiles(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name, System.IO.SearchOption.TopDirectoryOnly);
            Assert.AreEqual(1, files.Count());
        }

        [Test]
        public void EnumerateFileSystemInfos_test()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var infos = directoryInfoWrap.EnumerateFileSystemInfos();

            Assert.IsTrue(infos.Any(i => i is IFileInfo));
            Assert.IsTrue(infos.Any(i => i is IDirectoryInfo));
        }

        [Test]
        public void EnumerateFileSystemInfos_searchPattern_match()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var infos = directoryInfoWrap.EnumerateFileSystemInfos("*a*");

            Assert.IsTrue(infos.Any(i => i is IFileInfo));
            Assert.IsTrue(infos.Any(i => i is IDirectoryInfo));
        }

        [Test]
        public void EnumerateFileSystemInfos_searchPattern_nomatch()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var infos = directoryInfoWrap.EnumerateFileSystemInfos("*NOT_FOUND*");

            Assert.AreEqual(0, infos.Count());
        }

        [Test]
        public void EnumerateFileSystemInfos_searchPatternOption_top()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var infos = directoryInfoWrap.EnumerateFileSystemInfos("*a*", System.IO.SearchOption.TopDirectoryOnly);

            Assert.IsTrue(infos.Any(i => i is IFileInfo));
            Assert.IsTrue(infos.Any(i => i is IDirectoryInfo));
        }

        [Test]
        public void EnumerateFileSystemInfos_searchPatternOption_all()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            var infos = directoryInfoWrap.EnumerateFileSystemInfos("*BinaryReader*", System.IO.SearchOption.AllDirectories);

            Assert.IsTrue(infos.Any(i => i is IFileInfo));
            Assert.IsFalse(infos.Any(i => i is IDirectoryInfo));
        }
    }
}