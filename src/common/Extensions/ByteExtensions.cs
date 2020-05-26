using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace TServices.Comum.Extensions
{
    /// <summary>
    ///     Métodos de extensão.
    /// </summary>
    public static class ByteExtensions
    {
        public enum ImageFormat
        {
            bmp = 1,
            jpeg = 2,
            jpg = 3,
            doc = 4,
            pdf = 5,
            gif = 6,
            tiff = 7,
            png = 8,
            docx = 9,
            unknown = 99
        }

        public static ImageFormat GetImageFormat(this byte[] data)
        {
            var bmp = Encoding.ASCII.GetBytes("BM"); // BMP
            var doc = new byte[] { 208, 207, 17, 224 }; // DOC
            var gif = Encoding.ASCII.GetBytes("GIF"); // GIF
            var pdf = new byte[] { 37, 80, 68, 70 }; // PDF
            var png = new byte[] { 137, 80, 78, 71 }; // PNG
            var tiff = new byte[] { 73, 73, 42 }; // TIFF
            var tiff2 = new byte[] { 77, 77, 42 }; // TIFF
            var jpg = new byte[] { 255, 216, 255 }; // JPG
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon
            var docx = new byte[] { 80, 75, 3, 4 }; //DOCX

            if (bmp.SequenceEqual(data.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(data.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(data.Take(png.Length)))
                return ImageFormat.png;

            if (tiff.SequenceEqual(data.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(data.Take(tiff2.Length)))
                return ImageFormat.tiff;

            if (jpeg.SequenceEqual(data.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(data.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            if (jpg.SequenceEqual(data.Take(jpg.Length)))
                return ImageFormat.jpg;

            if (doc.SequenceEqual(data.Take(doc.Length)))
                return ImageFormat.doc;

            if (pdf.SequenceEqual(data.Take(pdf.Length)))
                return ImageFormat.pdf;

            if (docx.SequenceEqual(data.Take(docx.Length)))
                return ImageFormat.docx;

            return ImageFormat.unknown;
        }

        /// <summary>
        /// Retorna o tamanho em bytes do arquivo
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long GetLenghFile(this byte[] data) => data.GetStream().Length;

        /// <summary>
        ///     Compacta usando GZip
        /// </summary>
        /// <param name="data"> </param>
        /// <returns></returns>
        public static byte[] GZipCompress(this byte[] data)
        {
            using (var ms = new MemoryStream())
            {
                using (var zip = new GZipStream(ms, CompressionLevel.Optimal))
                {
                    zip.Write(data, 0, data.Length);
                }

                return ms.ToArray();
            }
        }

        /// <summary>
        ///     Descompacta usando GZip
        /// </summary>
        /// <param name="data"> </param>
        /// <returns></returns>
        public static byte[] GZipDecompress(this byte[] data)
        {
            using (var zip = new GZipStream(GetStream(data), CompressionMode.Decompress))
            {
                using (var ms = new MemoryStream())
                {
                    zip.CopyTo(ms);

                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        ///     Retorna um stream com os bytes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Stream GetStream(this byte[] data)
        {
            return new MemoryStream(data, 0, data.Length) {Position = 0};
        }
    }
}