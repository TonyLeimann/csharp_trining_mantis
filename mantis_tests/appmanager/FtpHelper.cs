using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.FtpClient;

namespace mantis_tests
{
    public class FtpHelper:HelperBase
    {
        private FtpClient _ftpClient;
        public FtpHelper(ApplicationManager manager) : base(manager) 
        {
            _ftpClient = new FtpClient();
            _ftpClient.Host = "localhost";
            _ftpClient.Credentials = new System.Net.NetworkCredential("mantis", "mantis");
            _ftpClient.Connect();
        }    

        public void BackupFile(String path) 
        { 
            String backupPath = path + ".bak";
            if (_ftpClient.FileExists(backupPath))
            {
                return;
            }
            _ftpClient.Rename(path, backupPath);
        }

        public void RestoreBackupFile(String path) 
        {
            String backupPath = path + ".bak";
            if (!_ftpClient.FileExists(backupPath))
            {
                return;
            }
            if (_ftpClient.FileExists(path))
            {
                _ftpClient.DeleteFile(path);
            }
            _ftpClient.Rename(backupPath, path);
        }

        public void Upload(String path, Stream localfile)
        {
            if (_ftpClient.FileExists(path))
            {
                _ftpClient.DeleteFile(path);
            }

            using (Stream ftpStream = _ftpClient.OpenWrite(path))
            {
                byte[] buffer = new byte[8 * 1024];
                int count = localfile.Read(buffer, 0, buffer.Length);
                while (count > 0) 
                {
                    ftpStream.Write(buffer, 0, count);
                    count = localfile.Read(buffer, 0, buffer.Length);
                }

            }



        }
    }
}
