using Avalonia.Media;
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

    public static void WritePixels(this ILockedFramebuffer framebuffer, int targetX, int targetY, int targetWidth, int targetHeight, byte[] pixelBytes)
    {
        //TODO: Idk if this is correct
        Span<byte> pixels = framebuffer.GetPixels();
        int rowBytes = framebuffer.RowBytes;
        int width = framebuffer.Size.Width;

        int startX = Math.Max(0, targetX);
        int endX = Math.Min(width, targetX + targetWidth);

        int startY = Math.Max(0, targetY);
        int endY = Math.Min(framebuffer.Size.Height, targetY + targetHeight);

        int bytePerPixel = framebuffer.Format.BitsPerPixel / 8;

        for (int y = startY; y < endY; y++)
        {
            int rowIndex = y * rowBytes;
            int startOffset = rowIndex + startX * bytePerPixel;
            int endOffset = rowIndex + endX * bytePerPixel;

            int srcRowStartIndex = (y - targetY) * targetWidth * bytePerPixel;

            pixelBytes.AsSpan(srcRowStartIndex, endOffset - startOffset).CopyTo(pixels.Slice(startOffset));
        }
    }
}