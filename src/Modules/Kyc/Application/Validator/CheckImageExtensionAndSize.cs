using Microsoft.AspNetCore.Http;

namespace CXChange.Modules.Kyc.Application.Validator
{
    internal class CheckImageExtensionAndSize
    {


        public static bool CheckExtension(IFormFile image)
        {
            if (image == null) return false;
            string fileName = image.FileName;
            string fileEXtension = Path.GetExtension(fileName);
            if (!string.IsNullOrEmpty(fileEXtension))
            {
                fileEXtension = fileEXtension.Substring(1).ToLower();
                if (fileEXtension == "jpg" || fileEXtension == "jpeg")
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public static bool CheckKycImageSignature(IFormFile file)
        {
            if (file == null) return false;
            Dictionary<string, List<byte[]>> _fileSignature =
new Dictionary<string, List<byte[]>>
{
    { ".jpeg", new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
        }
    },
     { ".jpg", new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0},
            new byte[] { 0xFF, 0xD8 ,0xFF, 0xE1 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE8},
        }
    },
      { ".JPEG", new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
        }
    },
     { ".JPG", new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0},
            new byte[] { 0xFF, 0xD8 ,0xFF, 0xE1 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE8},
        }
     },



};
            var ext = Path.GetExtension(file.FileName);
            using (var memorystream = new MemoryStream())
            {
                file.CopyTo(memorystream);
                using (var reader = new BinaryReader(memorystream))
                {

                    var signatures = _fileSignature[ext];
                    var length = signatures.Max(m => m.Length);
                    var filetoarray = memorystream.ToArray();

                    var issignaturetrue = signatures.Any(signature =>
                        filetoarray.Take(signature.Length).SequenceEqual(signature));
                    if (issignaturetrue)
                    {
                        return true;
                    }
                    return false;
                }






            }
        }
        public static bool CheckKycImageSize(IFormFile file, long allowedSize)
        {
            if (file == null) return false;
            return file.Length <= allowedSize;
        }

    }
}
