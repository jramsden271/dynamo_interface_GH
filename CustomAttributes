using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using System;
using System.Drawing;

namespace SCA.test_interface
{
    public class CustomAttributes : GH_ComponentAttributes
    {
        int paramheight;
        int paramwidthinput;
        int paramwidthoutput;

        public CustomAttributes(IGH_Component owner)
            : base(owner)
        {
        }
        
        protected override void Layout()
        {
            base.Layout();
            
            this.Pivot = GH_Convert.ToPoint(this.Pivot);
            this.Bounds = new RectangleF(this.Pivot, new SizeF((float)(paramwidthoutput + paramwidthinput + 50), (float)(Math.Max(Owner.Params.Input.Count * paramheight, Owner.Params.Output.Count * paramheight)) + 40f));
            RectangleF componentBox = new RectangleF(this.Bounds.Location, this.Bounds.Size);
            componentBox.X += 8f;
            componentBox.Y += 30f;
            componentBox.Width -= 18f;
            componentBox.Height -= 40f;
            GH_ComponentAttributes.LayoutInputParams(base.Owner, componentBox);
            GH_ComponentAttributes.LayoutOutputParams(base.Owner, componentBox);
            
            }
         

        public override void ExpireLayout()
        {
            base.ExpireLayout();
        }




        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            if (channel == GH_CanvasChannel.Wires)
            {
                base.Render(canvas, graphics, channel);
                this.Layout();
            }
            if (channel == GH_CanvasChannel.Objects)
            {


                paramheight = 0;
                //calc width and height
                Font paramfont = new Font("ubuntu", 12f);
                int inputwidth = 0;
                foreach (var inp in this.Owner.Params.Input)
                {
                    inputwidth = Math.Max((int)(graphics.MeasureString(inp.Name, paramfont).Width), inputwidth);
                    paramheight = Math.Max(paramheight, (int)(graphics.MeasureString(inp.Name, paramfont).Height));
                }
                int outputwidth = 0;
                foreach (var inp in this.Owner.Params.Output)
                {
                    outputwidth = Math.Max((int)(graphics.MeasureString(inp.Name, paramfont).Width), outputwidth);
                    paramheight = Math.Max(paramheight, (int)(graphics.MeasureString(inp.Name, paramfont).Height));
                }
                paramheight += 2;
                paramwidthinput = inputwidth;
                paramwidthoutput = outputwidth;



                this.Layout();
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 203, 198, 190)), Rectangle.Round(this.Bounds));

                Pen pen;
                SolidBrush parambrush;
                SolidBrush brush;

                pen = new Pen(Color.FromArgb(160, 160, 160, 160), 1f);
                brush = new SolidBrush(Color.FromArgb(255, 0, 0, 160));
                parambrush = new SolidBrush(System.Drawing.Color.Red);

                //title

                var titlerectangle = new Rectangle((int)(this.Pivot.X), (int)(Pivot.Y), (int)(Bounds.Width), 26);
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 94, 92, 89)), titlerectangle);
                
                var titlesize = graphics.MeasureString(this.Owner.Name, new Font("ubuntu", 12f));
                var titleloc = new PointF((float)(titlerectangle.X + titlerectangle.Width * 0.5 - titlesize.Width * 0.5), (float)(titlerectangle.Y + 3));
                graphics.DrawString(this.Owner.Name, new Font("ubuntu", 12f), Brushes.White, titleloc);


                //params
                var parambgcol = new SolidBrush(Color.FromArgb(255, 228, 225, 221));
                foreach (var inp in this.Owner.Params.Input)
                {
                    graphics.FillEllipse(new SolidBrush(Color.Black), (float)(inp.Attributes.Pivot.X - 4), (float)(inp.Attributes.Pivot.Y - 4), 8f, 8f);

                    var textloc = new PointF((float)(inp.Attributes.Pivot.X + 1), (float)(inp.Attributes.Pivot.Y - 10)); 

                    var size = graphics.MeasureString(inp.Name, paramfont);
                    size.Width = inputwidth;
                    RectangleF rect = new RectangleF(textloc, size);
                    graphics.FillRectangle(parambgcol, rect);
                    graphics.DrawString(inp.Name, paramfont, new SolidBrush(Color.FromArgb(255, 124, 124, 124)), textloc);
                }
                foreach (var inp in this.Owner.Params.Output)
                {
                    graphics.FillEllipse(new SolidBrush(Color.Black), (float)(inp.Attributes.Pivot.X - 4), (float)(inp.Attributes.Pivot.Y - 4), 8f, 8f);

                    var textloc = new PointF((float)(inp.Attributes.Pivot.X -1), (float)(inp.Attributes.Pivot.Y - 10));

                    var size = graphics.MeasureString(inp.Name, paramfont);
                    size.Width = outputwidth;
                    var textlocmod = new PointF(textloc.X - size.Width, textloc.Y);
                    RectangleF rect = new RectangleF(textlocmod, size);
                    graphics.FillRectangle(parambgcol, rect);
                    graphics.DrawString(inp.Name, paramfont, new SolidBrush(Color.FromArgb(255, 124, 124, 124)), textlocmod);
                }

                //border
                graphics.DrawRectangle(new Pen(Color.FromArgb(255, 94, 92, 89), 1f), new Rectangle((int)Bounds.X, (int)Bounds.Y, (int)Bounds.Width, (int)Bounds.Height));
            }
        }
    }
}
