using Avalonia.Platform;

namespace ColorPicker.AvaloniaUI;

internal static class LockedFramebufferExtensions
{
    public static Span<byte> GetPixels(this ILockedFramebuffer framebuffer)
    {
        unsafe
        {
            return new Span<byte>((byte*)framebuffer.Address, framebuffer.RowBytes * framebuffer.Size.Height);
        }
    }

    public static void WritePixels(this ILockedFramebuffer framebuffer, int targetX, int targetY, int targetWidth,
        int targetHeight, byte[] pixelBytes)
    {
        var pixels = framebuffer.GetPixels();
        var rowBytes = framebuffer.RowBytes;
        var width = framebuffer.Size.Width;

        var startX = Math.Max(0, targetX);
        var endX = Math.Min(width, targetX + targetWidth);

        var startY = Math.Max(0, targetY);
        var endY = Math.Min(framebuffer.Size.Height, targetY + targetHeight);

        var bytePerPixel = framebuffer.Format.BitsPerPixel / 8;

        for (var y = startY; y < endY; y++)
        {
            var rowIndex = y * rowBytes;
            var startOffset = rowIndex + startX * bytePerPixel;
            var endOffset = rowIndex + endX * bytePerPixel;

            var srcRowStartIndex = (y - targetY) * targetWidth * bytePerPixel;

            pixelBytes.AsSpan(srcRowStartIndex, endOffset - startOffset).CopyTo(pixels.Slice(startOffset));
        }
    }
}