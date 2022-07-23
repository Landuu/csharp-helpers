using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBarcode
{
    internal class ImageCombiner
    {
        private readonly IList<Image> images;
        private readonly int verticalOffset;
        private readonly int topOffset;
        private readonly int bottomOffset;

        public ImageCombiner(IList<Image> images, int verticalOffset = 0, int topOffset = 0, int bottomOffset = 0)
        {
            this.images = images;
            this.verticalOffset = verticalOffset;
            this.topOffset = topOffset;
            this.bottomOffset = bottomOffset;
        }

        public Bitmap CombineVert()
        {
            if (images.Count == 0) return new Bitmap(0, 0);

            int width = 0;
            int height = 0;

            foreach (var image in images)
            {
                width = image.Width > width ? image.Width : width;
                height += image.Height;
            }
            height += topOffset + verticalOffset * (images.Count - 1) + bottomOffset;

            var finalImage = new Bitmap(width, height);
            using var g = Graphics.FromImage(finalImage);
            g.Clear(Color.Black);
            int drawOffset = topOffset;
            foreach(var image in images)
            {
                var drawRegion = new Rectangle(0, drawOffset, image.Width, image.Height);
                drawOffset += image.Height + verticalOffset;
                g.DrawImage(image, drawRegion);
                image.Dispose();
            }

            return finalImage;
        }
    }
}
