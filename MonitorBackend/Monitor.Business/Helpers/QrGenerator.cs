using NLog;
using ZXing;
using System;
using System.IO;
using ZXing.QrCode;
using System.Drawing;
using ZXing.QrCode.Internal;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Monitor.Business.Common;

namespace Monitor.Business.Helpers
{
    public class QrGenerator
    {
        public const int DefaultQrCodeSize = 430;
        private readonly Logger _logger;
        private readonly ColorMap[] ColorMap = new ColorMap[]
        {
            new ColorMap
            {
                OldColor = Color.White,
                NewColor = Color.Transparent,
            },
        };

        public QrGenerator()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public byte[] Generate(QrLabelModel model)
        {
            if (!model.IsValid)
                throw new ArgumentException("Body of qr code cannot be null or empty.", nameof(model.Body));

            using Bitmap tmp = GenerateBitmap(model.Body);

            if (model.Size <= 0)
            {
                model.Size = DefaultQrCodeSize;
            }

            var result = ScaleBitmap(tmp, model.Size, model.Size);

            return ConvertBitmapToByteArray(result);
        }

        private Bitmap GenerateBitmap(string body)
        {
            var barcodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = DefaultQrCodeSize,
                    Width = DefaultQrCodeSize,
                    Margin = 0,
                    ErrorCorrection = ErrorCorrectionLevel.H,
                }
            };

            var pixelData = barcodeWriter.Write(body);
            var result = new Bitmap(DefaultQrCodeSize, DefaultQrCodeSize);

            using (var qrCodeBitmap = new Bitmap(DefaultQrCodeSize, DefaultQrCodeSize, PixelFormat.Format32bppRgb))
            {
                var bitmapData = qrCodeBitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

                try
                {
                    Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                catch (Exception e)
                {
                    _logger.Error(e, "An error occurred while creating a qr code bitmap.");
                }
                finally
                {
                    qrCodeBitmap.UnlockBits(bitmapData);
                }

                using var graphics = Graphics.FromImage(result);
                using var imageAttributes = new ImageAttributes();

                imageAttributes.SetRemapTable(ColorMap);

                var rect = new Rectangle(0, 0, DefaultQrCodeSize, DefaultQrCodeSize);

                graphics.DrawImage(qrCodeBitmap, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, imageAttributes);
            }

            return result;
        }

        private Bitmap ScaleBitmap(Bitmap rawBitmap, int width, int height)
        {
            Bitmap result = null;

            using (rawBitmap)
            {
                result = new Bitmap(rawBitmap, new Size(width, height));
            }

            return result;
        }

        private byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            using (bitmap)
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);

                return stream.ToArray();
            }
        }
    }
}
