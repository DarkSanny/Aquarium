using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Aquarium.UI
{
    public static class BitmapExtensions
    {
        public enum Direction
        {
            Horizontal = 0,
            Vertical = 1,
            Both = 2
        }

        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable", "source");
            }

            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static void GetFlippedAndSave(this Bitmap original, Direction direction, string savedName)
        {
            var clone = original.Clone<Bitmap>();
            switch (direction)
            {
                case Direction.Horizontal:
                    clone.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case Direction.Vertical:
                    clone.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;
                case Direction.Both:
                    clone.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    break;
            }
            var filePath = AppDomain.CurrentDomain.BaseDirectory + savedName;
            clone.Save(filePath, ImageFormat.Png);
        }
    }
}